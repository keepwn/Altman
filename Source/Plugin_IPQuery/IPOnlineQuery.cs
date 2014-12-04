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
		public async Task<string> GetHtml(string url, string post)
		{
			var client = new WebClient();
			//client.Headers.Add("Accept-Language", "zh-CN");
			client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko");
			client.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
			client.Headers.Add("Referer", "http://tool.17mon.cn/ip.php");
			var postString = post;
			var postData = Encoding.Default.GetBytes(postString);
			var responseData = client.UploadDataTaskAsync(new Uri(url), "POST", postData);
			var srcString = Encoding.UTF8.GetString(await responseData);
			return srcString;
		}

		public async Task<JObject> GetJson(string url, string post)
		{
			var client = new WebClient();
			client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko");
			client.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
			client.Headers.Add("Referer", "http://tool.17mon.cn/ip.php");
			client.Headers.Add("X-Requested-With", "XMLHttpRequest");
			var postString = post;
			var postData = Encoding.Default.GetBytes(postString);
			var responseData = client.UploadDataTaskAsync(new Uri(url), "POST", postData);
			var srcString = Encoding.Default.GetString(await responseData);
			return JObject.Parse(srcString);		
		}

		public async Task<string> GetIpInfo(string ip, string type)
		{
			var url = "http://tool.17mon.cn/ip.php?a=ajax";
			var post = string.Format("type={0}&ip={1}", type, ip);
			switch (type)
			{
				case "taobao":
					return NormalizeTaobao(await GetJson(url, post));
				case "sina":
					return NormalizeSina(await GetJson(url, post));
				case "tencent":
					return NormalizeTencent(await GetJson(url, post));
				case "17mon":
					url = "http://tool.17mon.cn/ip.php";
					post = string.Format("ip={0}", ip);
					var html = await GetHtml(url, post);
					return Normalize17Mon(html);
				default:
					return "";
			}
		}

		public string Normalize17Mon(string html)
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

		public string NormalizeTaobao(JObject json)
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

		public string NormalizeSina(JObject json)
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

		public string NormalizeTencent(JObject json)
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
