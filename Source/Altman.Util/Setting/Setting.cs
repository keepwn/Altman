using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Altman.Util.Setting
{
    public class Setting
    {
        #region struct

	    public struct BasicStruct
	    {
		    public string Language;
			public bool IsShowDisclaimer;
		    public bool IsOpenIPythonSupport;
	    }
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

	    private BasicStruct _basic;
        private UserAgentStruct _userAgent;
        private HttpHeaderStruct _httpHeader;
        private PolicyStruct _policy;
        private ProxyStruct _proxy;

		public BasicStruct BasicSetting
		{
			get { return _basic; }
			set { _basic = value; }
		}
        public UserAgentStruct UserAgentSetting
        {
            get { return _userAgent; }
			set { _userAgent = value; }
        }
        public HttpHeaderStruct HttpHeaderSetting
        {
            get { return _httpHeader; }
			set { _httpHeader = value; }
        }
        public PolicyStruct PolicySetting
        {
            get { return _policy; }
			set { _policy = value; }
        }
        public ProxyStruct ProxySetting
        {
            get { return _proxy; }
			set { _proxy = value; }
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

		public IWebProxy Proxy
		{
			get
			{
				IWebProxy proxy = null;
				int type = _proxy.IsNoOrIeOrCustomProxy;
				switch (type)
				{
					case 1:
						proxy = WebRequest.GetSystemWebProxy();
						break;
					case 2:
						var uri = new Uri("http://" + _proxy.ProxyAddr + ":" + _proxy.ProxyPort);
						var currentWebProxy = new WebProxy(uri, false);
						if (string.IsNullOrEmpty(_proxy.ProxyUser) && string.IsNullOrEmpty(_proxy.ProxyPassword))
						{
							currentWebProxy.Credentials = new NetworkCredential(_proxy.ProxyUser,
								_proxy.ProxyPassword,
								_proxy.ProxyDomain);
						}
						else
						{
							currentWebProxy.Credentials = CredentialCache.DefaultCredentials;
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

	    public string Language
	    {
			get { return _basic.Language; }
			set { _basic.Language = value; }
	    }
        public bool IsShowDisclaimer
        {
			get { return _basic.IsShowDisclaimer; }
			set { _basic.IsShowDisclaimer = value; }
        }

	    public bool IsOpenIPythonSupport
	    {
			get { return _basic.IsOpenIPythonSupport; }
			set { _basic.IsOpenIPythonSupport = value; }
	    }

	    public Setting()
	    {
		    
	    }
        public Setting(BasicStruct basic, UserAgentStruct userAgent,HttpHeaderStruct httpHeader,PolicyStruct policy,ProxyStruct proxy)
        {
	        _basic = basic;
            _userAgent = userAgent;
            _httpHeader = httpHeader;
            _policy = policy;
            _proxy = proxy;
        }
    }
}
