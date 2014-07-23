using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Altman.Common.AltEventArgs;

namespace Altman.Web
{
    internal class ProxyServer
    {
        private int _listeningPort = 8888;
        private string _listeningIPInterface = "127.0.0.1";
        private string _certFilePath = "https.pfx";

        private static readonly ProxyServer _server = new ProxyServer();

        private static readonly int BUFFER_SIZE = 8192;
        private static readonly char[] semicolonSplit = new char[] { ';' };
        private static readonly char[] equalSplit = new char[] { '=' };
        private static readonly String[] colonSpaceSplit = new string[] { ": " };
        private static readonly char[] spaceSplit = new char[] { ' ' };
        private static readonly char[] commaSplit = new char[] { ',' };
        private static readonly Regex cookieSplitRegEx = new Regex(@",(?! )");
        private static X509Certificate2 _certificate;
        private static object _outputLockObj = new object();

        private TcpListener _listener;
        private Thread _listenerThread;

        public IPAddress ListeningIp
        {
            get
            {
                IPAddress addr;
                IPAddress.TryParse(_listeningIPInterface, out addr);
                return addr;
            }
        }
        public int ListeningPort
        {
            get
            {
                return _listeningPort;
            }
        }

        private ProxyServer()
        {
            _listener = new TcpListener(ListeningIp, ListeningPort);
            //支持https
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        public bool DumpHeaders { get; set; }
        public bool DumpPostData { get; set; }
        public bool DumpResponseData { get; set; }
        public static ProxyServer Server
        {
            get { return _server; }
        }

        /// <summary>
        /// 开始
        /// </summary>
        public bool Start()
        {
            try
            {
                String certFilePath = String.Empty;
                certFilePath = _certFilePath;
                try
                {
                    //生成证书实例
                    _certificate = new X509Certificate2(certFilePath, "");
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Could not create the certificate from file from {0}", certFilePath), ex);
                }
                _listener.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            _listenerThread = new Thread(new ParameterizedThreadStart(Listen));
            _listenerThread.Start(_listener);

            return true;
        }
        public void Stop()
        {
            _listener.Stop();

            //等待完成当前连接
            _listenerThread.Abort();
            _listenerThread.Join();
            _listenerThread.Join();
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        private static void Listen(Object obj)
        {
            TcpListener listener = (TcpListener)obj;
            try
            {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    while (!ThreadPool.QueueUserWorkItem(new WaitCallback(ProxyServer.ProcessClient), client))
                    { }
                }
            }
            catch (ThreadAbortException) { }
            catch (SocketException) { }
        }

        private static void ProcessClient(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            try
            {
                DoHttpProcessing(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }

        /// <summary>
        /// 处理http数据
        /// </summary>
        /// <param name="client"></param>
        private static void DoHttpProcessing(TcpClient client)
        {
            Stream clientStream = client.GetStream();
            Stream outStream = clientStream; //use this stream for writing out - may change if we use ssl
            SslStream sslStream = null;
            StreamReader clientStreamReader = new StreamReader(clientStream);

            if (Server.DumpHeaders || Server.DumpPostData || Server.DumpResponseData)
            {
                //确保按顺序打印数据
                Monitor.TryEnter(_outputLockObj, TimeSpan.FromMilliseconds(-1.0));
            }

            try
            {
                //读取第一行（http cmd）
                String httpCmd = clientStreamReader.ReadLine();
                if (String.IsNullOrEmpty(httpCmd))
                {
                    clientStreamReader.Close();
                    return;
                }
                //分割为3段
                String[] splitBuffer = httpCmd.Split(spaceSplit, 3);

                String method = splitBuffer[0];
                String remoteUri = splitBuffer[1];
                Version version = new Version(1, 0);

                HttpWebRequest webRequest;
                HttpWebResponse webResponse = null;

                /**
                 * http包发往代理服务器时，方法包括（get,post,connect）
                 * 当方法为connect时，表示ssl连接
                 */
                if (splitBuffer[0].ToUpper() == "CONNECT")
                {
                    //
                    remoteUri = "https://" + splitBuffer[1];
                    while (!String.IsNullOrEmpty(clientStreamReader.ReadLine())) { }
                    StreamWriter connectStreamWriter = new StreamWriter(clientStream);
                    connectStreamWriter.WriteLine("HTTP/1.0 200 Connection established");
                    connectStreamWriter.WriteLine("Timestamp: {0}", DateTime.Now.ToString());
                    connectStreamWriter.WriteLine("Proxy-agent: altman-agent");
                    connectStreamWriter.WriteLine();
                    connectStreamWriter.Flush();

                    sslStream = new SslStream(clientStream, false);
                    try
                    {
                        //进行身份验证
                        sslStream.AuthenticateAsServer(_certificate, false, SslProtocols.Tls | SslProtocols.Ssl3 | SslProtocols.Ssl2, true);
                    }
                    catch (Exception)
                    {
                        sslStream.Close();
                        clientStreamReader.Close();
                        connectStreamWriter.Close();
                        clientStream.Close();
                        return;
                    }

                    //创建https，加密client的通信
                    clientStream = sslStream;
                    clientStreamReader = new StreamReader(sslStream);
                    outStream = sslStream;
                    //读取新的http cmd
                    httpCmd = clientStreamReader.ReadLine();
                    if (String.IsNullOrEmpty(httpCmd))
                    {
                        clientStreamReader.Close();
                        clientStream.Close();
                        sslStream.Close();
                        return;
                    }
                    splitBuffer = httpCmd.Split(spaceSplit, 3);
                    method = splitBuffer[0];
                    remoteUri = remoteUri + splitBuffer[1];
                }

                //重新组合客户端请求的第一行
                webRequest = (HttpWebRequest)HttpWebRequest.Create(remoteUri);
                webRequest.Method = method;
                webRequest.ProtocolVersion = version;

                //读取客户端请求的headers，并复制
                int contentLen = ReadRequestHeaders(clientStreamReader, webRequest);

                webRequest.Proxy = null;
                webRequest.KeepAlive = false;
                webRequest.AllowAutoRedirect = false;
                webRequest.AutomaticDecompression = DecompressionMethods.None;

                //打印headers
                if (Server.DumpHeaders)
                {
                    //Console.WriteLine("{0} {1} HTTP/{2}", webRequest.Method, webRequest.RequestUri.AbsoluteUri, webRequest.ProtocolVersion);
                    DumpHeaderCollectionToConsole(webRequest.Headers);
                }

                if (method.ToUpper() == "GET")
                {
                    //
                }
                else if (method.ToUpper() == "POST")
                {
                    char[] postBuffer = new char[contentLen];
                    int bytesRead;
                    int totalBytesRead = 0;
                    StreamWriter sw = new StreamWriter(webRequest.GetRequestStream());
                    while (totalBytesRead < contentLen &&
                           (bytesRead = clientStreamReader.ReadBlock(postBuffer, 0, contentLen)) > 0)
                    {
                        totalBytesRead += bytesRead;
                        sw.Write(postBuffer, 0, bytesRead);
                        if (Server.DumpPostData)
                        {
                            if (ProxyReceiveToDo != null)
                                ProxyReceiveToDo(null, new ProxyReceiveEventArgs(new string(postBuffer)));
                        }
                    }

                    sw.Close();
                }

                webRequest.Timeout = 15000;

                try
                {
                    webResponse = (HttpWebResponse)webRequest.GetResponse();
                }
                catch (WebException webEx)
                {
                    webResponse = webEx.Response as HttpWebResponse;
                }
                if (webResponse != null)
                {
                    List<Tuple<String, String>> responseHeaders = ProcessResponse(webResponse);
                    StreamWriter myResponseWriter = new StreamWriter(outStream);
                    Stream responseStream = webResponse.GetResponseStream();
                    try
                    {
                        //发送response状态码和headers
                        WriteResponseStatus(webResponse.StatusCode, webResponse.StatusDescription, myResponseWriter);
                        WriteResponseHeaders(myResponseWriter, responseHeaders);

                        Byte[] buffer;
                        if (webResponse.ContentLength > 0)
                            buffer = new Byte[webResponse.ContentLength];
                        else
                            buffer = new Byte[BUFFER_SIZE];

                        int bytesRead;

                        while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            outStream.Write(buffer, 0, bytesRead);
                            if (Server.DumpResponseData)
                            {
                                if (ProxyReceiveToDo != null)
                                    ProxyReceiveToDo(null, new ProxyReceiveEventArgs(Encoding.Default.GetString(buffer, 0, bytesRead)));
                            }
                        }

                        responseStream.Close();
                        outStream.Flush();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        responseStream.Close();
                        webResponse.Close();
                        myResponseWriter.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Server.DumpHeaders || Server.DumpPostData || Server.DumpResponseData)
                {
                    //release the lock
                    Monitor.Exit(_outputLockObj);
                }

                clientStreamReader.Close();
                clientStream.Close();
                if (sslStream != null)
                    sslStream.Close();
                outStream.Close();
            }

        }

        private static List<Tuple<String, String>> ProcessResponse(HttpWebResponse response)
        {
            String value = null;
            String header = null;
            List<Tuple<String, String>> returnHeaders = new List<Tuple<String, String>>();
            foreach (String s in response.Headers.Keys)
            {
                if (s.ToLower() == "set-cookie")
                {
                    header = s;
                    value = response.Headers[s];
                }
                else
                    returnHeaders.Add(new Tuple<String, String>(s, response.Headers[s]));
            }

            if (!String.IsNullOrWhiteSpace(value))
            {
                response.Headers.Remove(header);
                String[] cookies = cookieSplitRegEx.Split(value);
                foreach (String cookie in cookies)
                    returnHeaders.Add(new Tuple<String, String>("Set-Cookie", cookie));

            }
            returnHeaders.Add(new Tuple<String, String>("X-Proxied-By", "altman-agent"));
            return returnHeaders;
        }

        private static void WriteResponseStatus(HttpStatusCode code, String description, StreamWriter myResponseWriter)
        {
            String s = String.Format("HTTP/1.0 {0} {1}", (Int32)code, description);
            myResponseWriter.WriteLine(s);
            if (ProxyServer.Server.DumpHeaders)
            {
                if (ProxyReceiveToDo != null)
                    ProxyReceiveToDo(null, new ProxyReceiveEventArgs(s));
            }
        }
        private static void WriteResponseHeaders(StreamWriter myResponseWriter, List<Tuple<String, String>> headers)
        {
            if (headers != null)
            {
                foreach (Tuple<String, String> header in headers)
                    myResponseWriter.WriteLine(String.Format("{0}: {1}", header.Item1, header.Item2));
            }
            myResponseWriter.WriteLine();
            myResponseWriter.Flush();

            if (Server.DumpHeaders)
                DumpHeaderCollectionToConsole(headers);
        }

        /// <summary>
        /// 打印header
        /// </summary>
        private static void DumpHeaderCollectionToConsole(WebHeaderCollection headers)
        {
            foreach (String s in headers.AllKeys)
            {
                //Console.WriteLine("{0}: {1}", s, headers[s]);
                if (ProxyReceiveToDo != null)
                    ProxyReceiveToDo(null, new ProxyReceiveEventArgs(string.Format("{0}: {1}", s, headers[s])));
            }
        }
        /// <summary>
        /// 打印header
        /// </summary>
        private static void DumpHeaderCollectionToConsole(List<Tuple<String, String>> headers)
        {
            foreach (Tuple<String, String> header in headers)
            {
                if (ProxyReceiveToDo != null)
                    ProxyReceiveToDo(null, new ProxyReceiveEventArgs(string.Format("{0}: {1}", header.Item1, header.Item2)));
            }
        }

        /// <summary>
        /// 读取请求的headers
        /// </summary>
        private static int ReadRequestHeaders(StreamReader sr, HttpWebRequest webRequest)
        {
            String httpCmd;
            int contentLength = 0;
            do
            {
                httpCmd = sr.ReadLine();
                if (String.IsNullOrEmpty(httpCmd))
                    return contentLength;
                String[] header = httpCmd.Split(colonSpaceSplit, 2, StringSplitOptions.None);
                switch (header[0].ToLower())
                {
                    case "host":
                        webRequest.Host = header[1];
                        break;
                    case "user-agent":
                        webRequest.UserAgent = header[1];
                        break;
                    case "accept":
                        webRequest.Accept = header[1];
                        break;
                    case "referer":
                        webRequest.Referer = header[1];
                        break;
                    case "cookie":
                        webRequest.Headers["Cookie"] = header[1];
                        break;
                    case "proxy-connection":
                    case "connection":
                    case "keep-alive":
                        //ignore these
                        break;
                    case "content-length":
                        int.TryParse(header[1], out contentLength);
                        break;
                    case "content-type":
                        webRequest.ContentType = header[1];
                        break;
                    //case "expect":
                    //    webRequest.Expect = header[1];
                    //    break;
                    case "if-modified-since":
                        String[] sb = header[1].Trim().Split(semicolonSplit);
                        DateTime d;
                        if (DateTime.TryParse(sb[0], out d))
                            webRequest.IfModifiedSince = d;
                        break;
                    default:
                        try
                        {
                            webRequest.Headers.Add(header[0], header[1]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Could not add header {0}.  Exception message:{1}", header[0], ex.Message);
                        }
                        break;
                }
            } while (!String.IsNullOrWhiteSpace(httpCmd));
            return contentLength;
        }

        public static event EventHandler<ProxyReceiveEventArgs> ProxyReceiveToDo;
    }
}
