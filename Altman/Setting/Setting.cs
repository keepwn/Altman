using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Altman.Setting
{
    public class Setting
    {
        #region struct
        public struct UserAgentStruct
        {
            public bool IsRandom;
            public KeyValuePair<string, string> Selected;
            public Dictionary<string, string> UserAgentList;
        }
        public struct HttpHeaderStruct
        {
            public Dictionary<string, string> HttpHeaderList;
        }
        public struct PolicyStruct
        {
            public bool IsParamRandom;
            public bool IsShowDisclaimer;
        }
        public struct ProxyStruct
        {
            public int IsNoOrIeOrCustomProxy;//0,1,2
            public string ProxyAddr;
            public string ProxyPort;
            public string ProxyUser;
            public string ProxyPassword;
            public string ProxyDomain;
        }
        #endregion

        private UserAgentStruct _userAgent;
        private HttpHeaderStruct _httpHeader;
        private PolicyStruct _policy;
        private ProxyStruct _proxy;

        public UserAgentStruct GetUserAgentStruct
        {
            get { return _userAgent; }
        }
        public HttpHeaderStruct GetHttpHeaderStruct
        {
            get { return _httpHeader; }
        }
        public PolicyStruct GetPolicyStruct
        {
            get { return _policy; }
        }
        public ProxyStruct GetProxyStruct
        {
            get { return _proxy; }
        }

        public List<string> UserAgent
        {
            get
            {
                //程序通过判断list的个数，来选择是否开启随机功能
                List<string> list = new List<string>();
                if (_userAgent.IsRandom)
                {
                    list.AddRange(_userAgent.UserAgentList.Select(i => i.Value));
                }
                else
                {
                    list.Add(_userAgent.Selected.Value);
                }
                return list;
            }
        }
        public WebHeaderCollection HttpHeader
        {
            get
            {
                WebHeaderCollection header = new WebHeaderCollection();
                foreach (KeyValuePair<string, string> i in _httpHeader.HttpHeaderList)
                {
                    header.Add(i.Key,i.Value);
                }
                return header;
            }
        }

        public bool IsParamRandom
        {
            get { return _policy.IsParamRandom; }
            set { _policy.IsParamRandom = value; }
        }
        public bool IsShowDisclaimer
        {
            get { return _policy.IsShowDisclaimer; }
            set { _policy.IsShowDisclaimer = value; }
        }

        public IWebProxy Proxy
        {
            get
            {
                IWebProxy proxy=null;
                int type = _proxy.IsNoOrIeOrCustomProxy;
                switch (type)
                {
                    case 1:
                        proxy = WebRequest.GetSystemWebProxy();
                        break;
                    case 2:
                        Uri uri = new Uri("http://" + _proxy.ProxyAddr + ":" + _proxy.ProxyPort);
                        WebProxy currentWebProxy = new WebProxy(uri, false);
                        if (string.IsNullOrEmpty(_proxy.ProxyUser) && string.IsNullOrEmpty(_proxy.ProxyPassword))
                        {
                            currentWebProxy.Credentials = new System.Net.NetworkCredential(_proxy.ProxyUser,
                                _proxy.ProxyPassword,
                                _proxy.ProxyDomain);
                        }
                        else
                        {
                            currentWebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;                        
                        }
                        proxy = currentWebProxy;
                        break;
                    default:
                        proxy = null;
                        break;
                }
                return proxy;
            }
        }

        public Setting(UserAgentStruct userAgent,HttpHeaderStruct httpHeader,PolicyStruct policy,ProxyStruct proxy)
        {
            _userAgent = userAgent;
            _httpHeader = httpHeader;
            _policy = policy;
            _proxy = proxy;
        }
    }
}
