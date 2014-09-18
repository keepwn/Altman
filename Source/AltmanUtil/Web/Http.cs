using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using Altman.Common.AltEventArgs;

namespace Altman.Web
{
    internal class Http
    {
        public class ProgressBar
        {
            internal long BytesSent;
            internal long TotalBytesToSend = -1L;
            internal long BytesReceived;
            internal long TotalBytesToReceive = -1L;
            internal bool HasUploadPhase;
            internal void Reset()
            {
                this.BytesSent = 0L;
                this.TotalBytesToSend = -1L;
                this.BytesReceived = 0L;
                this.TotalBytesToReceive = -1L;
                this.HasUploadPhase = false;
            }
        }

        private string _method = null;
        private WebHeaderCollection _headers = null;
        private long _requestContentLength;
        private long _responseContentLength;

        private bool _allowAutoRedirect = true;
        private ICredentials _credentials;
        private bool _proxySet;
        private IWebProxy _proxy;
        private CookieContainer _cookieContainer = null;

        private string _characterSet = null; //响应的字符集
        private HttpStatusCode _statusCode;

        private HttpWebRequest _httpWebRequest;
        private HttpWebResponse _httpWebResponse;

        public ICredentials Credentials
        {
            get { return _credentials; }
            set { _credentials = value; }
        }
        public WebHeaderCollection Headers
        {
            get
            {
                if (_headers == null)
                {
                    _headers = new WebHeaderCollection();
                }
                return _headers;
            }
            set { _headers = value; }
        }
        public CookieContainer Cookies
        {
            get
            {
                if (_cookieContainer == null)
                {
                    _cookieContainer = new CookieContainer();
                }
                return _cookieContainer;
            }
            set { _cookieContainer = value; }
        }


        public Http()
        {
            //Http协议的并发连接数
            ServicePointManager.DefaultConnectionLimit = 512;
            //不使用100-continue
            ServicePointManager.Expect100Continue = false;

            _headers = new WebHeaderCollection();
        }

        private HttpWebRequest GetHttpWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            //拷贝headers到request
            CopyHeadersTo(request);
            //判断uri是否为https
            CheckIsHttps(request);
            if (_credentials != null)
            {
                request.Credentials = _credentials;
            }
            if (_proxySet)
            {
                request.Proxy = _proxy;
            }
            if (_method != null)
            {
                request.Method = _method;
            }
            if (_requestContentLength != -1L)
            {
                request.ContentLength = _requestContentLength;
            }
            if (_cookieContainer != null)
            {
                request.CookieContainer = _cookieContainer;
            }
            request.AllowAutoRedirect = _allowAutoRedirect;
            return request;
        }
        private HttpWebResponse GetHttpWebResponse(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
        private void CopyHeadersTo(HttpWebRequest request)
        {
            if (_headers != null)
            {
                string text = _headers["Accept"];
                string text2 = _headers["Connection"];
                string text3 = _headers["Content-Type"];
                string text4 = _headers["Expect"];
                string text5 = _headers["Referer"];
                string text6 = _headers["User-Agent"];
                string text7 = _headers["Host"];
                _headers.Remove("Accept");
                _headers.Remove("Connection");
                _headers.Remove("Content-Type");
                _headers.Remove("Expect");
                _headers.Remove("Referer");
                _headers.Remove("User-Agent");
                _headers.Remove("Host");
                request.Headers = _headers;
                if (text != null && text.Length > 0)
                {
                    request.Accept = text;
                }
                if (text2 != null && text2.Length > 0)
                {
                    request.Connection = text2;
                }
                if (text3 != null && text3.Length > 0)
                {
                    request.ContentType = text3;
                }
                if (text4 != null && text4.Length > 0)
                {
                    request.Expect = text4;
                }
                if (text5 != null && text5.Length > 0)
                {
                    request.Referer = text5;
                }
                if (text6 != null && text6.Length > 0)
                {
                    request.UserAgent = text6;
                }
                if (!string.IsNullOrEmpty(text7))
                {
                    request.Host = text7;
                }
            }
        }

        public byte[] Get(string requestUriString)
        {
            Uri uri = new Uri(requestUriString);
            return UploadData(uri, "GET", null);
        }
        public byte[] Post(string requestUriString, byte[] requestData)
        {
            Uri uri = new Uri(requestUriString);
            _headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            return UploadData(uri, "POST", requestData);
        }
        private byte[] UploadData(Uri requestUriString, string requestMethod, byte[] requestData)
        {
            if (requestUriString == null)
            {
                throw new ArgumentNullException("uri");
            }
            if (requestMethod == null)
            {
                throw new ArgumentNullException("method");
            }
            HttpWebRequest httpWebRequest = null;
            byte[] result = null;
            try
            {
                byte[] array = UploadDataInternal(requestUriString, requestMethod, requestData, out httpWebRequest);
                result = array;
            }
            finally
            {
                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
            }
            return result;
        }
        private byte[] UploadDataInternal(Uri uri, string method, byte[] data, out HttpWebRequest request)
        {
            request = null;
            byte[] result = null;
            try
            {
                _method = method;
                _requestContentLength = (data == null) ? -1L : data.Length;
                request = (_httpWebRequest = GetHttpWebRequest(uri));
                request.KeepAlive = true;
                UploadBits(request, data);
                byte[] array = DownloadBits(request);
                result = array;
            }
            catch (Exception ex)
            {
                if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
                {
                }
                if (!(ex is WebException) && !(ex is SecurityException))
                {
                }
                throw;
            }
            return result;
        }
        private void UploadBits(HttpWebRequest request, byte[] data)
        {
            if (data == null) return;
            using (Stream stream = request.GetRequestStream())
            {
                //这是本来的实现方式，考虑到需要进度提示，故改用WriteBytesToStream
                //stream.Write(data, 0, data.Length);
                WriteBytesToStream(stream, data);
            }
        }
        private byte[] DownloadBits(HttpWebRequest request)
        {
            HttpWebResponse response = _httpWebResponse = GetHttpWebResponse(request);
            _responseContentLength = response.ContentLength;
            byte[] arraryByte = null;
            using (Stream stream = response.GetResponseStream())
            {
                arraryByte = ReadBytesFromStream(stream);
            }
            return arraryByte;
        }
        private void WriteBytesToStream(Stream stream, byte[] data)
        {
            const int chunkSize = 1024;
            int position = 0;
            long dataLength = data.LongLength;

            ProgressBar progress = new ProgressBar();
            progress.TotalBytesToSend = dataLength;
            progress.HasUploadPhase = true;
            while (position < dataLength)
            {
                long num = position + chunkSize > dataLength ? dataLength - position : chunkSize;
                stream.Write(data, position, (int)num);
                position += chunkSize;
                //报告上传进度
                progress.BytesSent += num;
                ProgressChanged(progress);
            }
        }
        private byte[] ReadBytesFromStream(Stream stream)
        {
            const int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];

            ProgressBar progress = new ProgressBar();
            progress.TotalBytesToSend = _responseContentLength;
            progress.HasUploadPhase = false;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int num;
                while ((num = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, num);
                    //报告上传进度
                    progress.TotalBytesToReceive += num;
                    ProgressChanged(progress);
                }
                return memoryStream.ToArray();
            }
        }
        private void ProgressChanged(ProgressBar progress)
        {
            if (progress.HasUploadPhase)
            {
                if (UploadProgressChanged != null)
                {
                    UploadProgressChanged(null, new AltUploadProgressChangedEventArgs(progress.BytesSent, progress.TotalBytesToSend));
                }
            }
            else
            {
                if (DownloadProgressChanged != null)
                {
                    DownloadProgressChanged(null, new AltDownloadProgressChangedEventArgs(progress.BytesReceived, progress.TotalBytesToReceive));
                }
            }
        }

        public event EventHandler<AltUploadProgressChangedEventArgs> UploadProgressChanged;
        public event EventHandler<AltDownloadProgressChangedEventArgs> DownloadProgressChanged;

        #region https支持
        private void CheckIsHttps(HttpWebRequest request)
        {
            {
                Regex regexUri = new Regex("^https://", RegexOptions.IgnoreCase);
                if (regexUri.IsMatch(request.RequestUri.ToString()))
                    ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            }
        }
        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors errors)
        {
            return true;
        }
        #endregion

        private void ClearWebState()
        {
            this._method = null;
            this._headers = null;
            this._requestContentLength = -1L;
            this._httpWebRequest = null;
            this._httpWebResponse = null;
        }

    }
}
