using System.Collections.Generic;
using System.Net;

namespace Altman.Util.Setting
{
    public static class GlobalSetting
    {
        public static string HttpUserAgent
        {
            get { return HttpHeader[HttpRequestHeader.UserAgent]; }
            set { HttpHeader[HttpRequestHeader.UserAgent] = value; }
        }
        public static string HttpReferer
        {
            get { return HttpHeader[HttpRequestHeader.Referer]; }
            set { HttpHeader[HttpRequestHeader.Referer] = value; }
        }

        public static List<string> UserAgent=new List<string>();
        public static WebHeaderCollection HttpHeader = null;
        public static CookieContainer HttpCookie = null;
        public static bool IsParamRandom = false;
        public static bool IsShowDisclaimer = true;
        public static IWebProxy Proxy = null;
        public static object Setting = null;

        public static void Clear()
        {
            UserAgent = new List<string>();
            HttpHeader = null;
            HttpCookie = null;
            Proxy = null;
            Setting = null;
        }
    }
}
