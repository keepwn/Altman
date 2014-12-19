using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Plugin_IPQuery
{
	public class IPOnlineQuery
	{
		public class QueryCompletedEventArgs : EventArgs
		{
			public string Result;
			public string Token;
			public QueryCompletedEventArgs(string result,string token)
			{
				Result = result;
				Token = token;
			}
		}

		private void GetHtml(string url, string post, object token)
		{
			var client = new WebClient();
			client.UploadDataCompleted += client_UploadDataCompleted_html;
			//client.Headers.Add("Accept-Language", "zh-CN");
			client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko");
			client.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
			client.Headers.Add("Referer", "http://tool.17mon.cn/ip.php");
			var postString = post;
			var postData = Encoding.Default.GetBytes(postString);
			client.UploadDataAsync(new Uri(url), "POST", postData, token);
			//var srcString = Encoding.UTF8.GetString(responseData);
			//return srcString;
		}

		private void GetJson(string url, string post, object token)
		{
			var client = new WebClient();
			client.UploadDataCompleted += client_UploadDataCompleted_json;
			client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko");
			client.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
			client.Headers.Add("Referer", "http://tool.17mon.cn/ip.php");
			client.Headers.Add("X-Requested-With", "XMLHttpRequest");
			var postString = post;
			var postData = Encoding.Default.GetBytes(postString);
			client.UploadDataAsync(new Uri(url), "POST", postData, token);
			//var srcString = Encoding.Default.GetString(await responseData);
			//return JObject.Parse(srcString);
		}

		public EventHandler<QueryCompletedEventArgs> OnQueryCompleted;
		private void client_UploadDataCompleted_html(object sender, UploadDataCompletedEventArgs e)
		{
			if (OnQueryCompleted != null)
			{
				var token = e.UserState is string ? (string) e.UserState : "";
				var data = "";
				if (e.Error == null)
				{
					var srcString = Encoding.UTF8.GetString(e.Result);
					switch (token)
					{
						case "17mon":
							data = Normalize17Mon(srcString);
							break;
					}
				}
				else
				{
					data = e.Error.Message;
				}
				OnQueryCompleted(sender, new QueryCompletedEventArgs(data, token));
			}
		}

		private void client_UploadDataCompleted_json(object sender, UploadDataCompletedEventArgs e)
		{
			if (OnQueryCompleted != null)
			{
				var token = e.UserState is string ? (string) e.UserState : "";
				var data = "";
				if (e.Error == null)
				{
					var srcString = Encoding.UTF8.GetString(e.Result);
					var json = JObject.Parse(srcString);
					switch (token)
					{
						case "taobao":
							data = NormalizeTaobao(json);
							break;
						case "sina":
							data = NormalizeSina(json);
							break;
						case "tencent":
							data = NormalizeTencent(json);
							break;
					}
				}
				else
				{
					data = e.Error.Message;
				}
				OnQueryCompleted(sender, new QueryCompletedEventArgs(data, token));
			}
		}

		public void GetIpInfo(string ip, string type)
		{
			var url = "http://tool.17mon.cn/ip.php?a=ajax";
			var post = string.Format("type={0}&ip={1}", type, ip);
			switch (type)
			{
				case "taobao":
				case "sina":
				case "tencent":
					GetJson(url, post, type);
					break;
				case "17mon":
					url = "http://tool.17mon.cn/ip.php";
					post = string.Format("ip={0}", ip);
					GetHtml(url, post, type);
					break;
			}
		}

		private string Normalize17Mon(string html)
		{
			var pattern = "<span id=\"myself\">(?<ip>.*?)</span>";
			var regex = new Regex(pattern, RegexOptions.Singleline);
			var m = regex.Match(html);
			if (m.Success)
			{
				return m.Groups["ip"].Value.Trim();
			}
			return "error";
		}

		private string NormalizeTaobao(JObject json)
		{
			try
			{
				var result = json;
				var country = result["data"]["data"]["country"];
				var area = result["data"]["data"]["area"];
				var region = result["data"]["data"]["region"];
				var city = result["data"]["data"]["city"];
				var isp = result["data"]["data"]["isp"];

				var sb = new StringBuilder();
				sb.AppendLine("country: " + country);
				sb.AppendLine("area: " + area);
				sb.AppendLine("region: " + region);
				sb.AppendLine("city: " + city);
				sb.AppendLine("isp: " + isp);
				return sb.ToString();
			}
			catch
			{
				return "error";
			}
		}

		private string NormalizeSina(JObject json)
		{
			try
			{
				var result = json;
				var country = result["data"]["data"]["country"];
				var province = result["data"]["data"]["province"];
				var city = result["data"]["data"]["city"];
				var isp = result["data"]["data"]["isp"];

				var sb = new StringBuilder();
				sb.AppendLine("country: " + country);
				sb.AppendLine("region: " + province);
				sb.AppendLine("city: " + city);
				sb.AppendLine("isp: " + isp);
				return sb.ToString();
			}
			catch
			{
				return "error";
			}
		}

		private string NormalizeTencent(JObject json)
		{
			try
			{
				var result = json;
				var area = result["data"]["area"];
				var isp = result["data"]["isp"];

				var sb = new StringBuilder();
				sb.AppendLine("area: " + area);
				sb.AppendLine("isp: " + isp);
				return sb.ToString();
			}
			catch
			{
				return "error";
			}
		}
	}
}
