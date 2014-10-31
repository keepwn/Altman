using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Altman.Plugin;
using Altman.Plugin.Interface;

namespace Plugin_Encoder
{
	public class EncoderService
	{
		public static string Base64_Encode(PluginParameter args)
		{
			var str = args[0];
			return Convert.ToBase64String(Encoding.Default.GetBytes(str));
		}

		public static string Base64_Decode(PluginParameter args)
		{
			var str = args[0];
			try
			{
				var bytes = Convert.FromBase64String(str);
				return Encoding.Default.GetString(bytes);
			}
			catch
			{
				return "FromBase64 Fail";
			}
		}

		public static string ToMd5_16(PluginParameter args)
		{
			var str = args[0];
			var mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			var tmp = BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(str)), 4, 8);
			tmp = tmp.Replace("-", "");
			return tmp;
		}

		public static string ToMd5_32(PluginParameter args)
		{
			var str = args[0];

			var md5 = MD5.Create();
			var bytes = Encoding.UTF8.GetBytes(str);
			var array = md5.ComputeHash(bytes);
			md5.Clear();

			var tmp = "";
			foreach (var b in array)
			{
				tmp += b.ToString("X2");
			}
			return tmp;
		}

		public static string ToSha1(PluginParameter args)
		{
			var str = args[0];
			SHA1 sha1 = new SHA1CryptoServiceProvider();
			var bytesSha1Out = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
			var strSha1Out = BitConverter.ToString(bytesSha1Out);
			return strSha1Out.Replace("-", "");
		}

		public static string ToUrlEncode(PluginParameter args)
		{
			var str = args[0];
			return Uri.EscapeDataString(str);
		}

		public static string ToUrlEncodeAll(PluginParameter args)
		{
			var str = args[0];
			var sb = new StringBuilder();
			var bytes = Encoding.UTF8.GetBytes(str);
			foreach (var b in bytes)
			{
				sb.Append("%" + Convert.ToString(b, 16));
			}
			return sb.ToString().ToUpper();
		}

		public static string FromUrlEncode(PluginParameter args)
		{
			var str = args[0];
			try
			{
				if (Regex.IsMatch(
					HttpUtility.UrlDecode(str, Encoding.GetEncoding("iso-8859-1")),
					@"^(?:[\x00-\x7f]|[\xe0-\xef][\x80-\xbf]{2})+$"))
				{
					return HttpUtility.UrlDecode(str, Encoding.GetEncoding("UTF-8"));
				}
				return HttpUtility.UrlDecode(str, Encoding.GetEncoding("GB2312"));
			}
			catch
			{
				return "FromUrlEncode Fail";
			}
		}

		public static string ToHex(PluginParameter args)
		{
			var str = args[0];
			if (str == "")
			{
				return "";
			}
			var bytes = Encoding.UTF8.GetBytes(str);
			var tmp = "";
			for (var i = 0; i < bytes.Length; i++)
			{
				tmp = tmp + bytes[i].ToString("X");
			}
			return tmp;
		}

		public static string FromHex(PluginParameter args)
		{
			var str = args[0];
			try
			{
				str = str.Replace(" ", "");
				if ((str.Length%2) != 0)
					str += " ";
				var returnBytes = new byte[str.Length/2];
				for (var i = 0; i < returnBytes.Length; i++)
					returnBytes[i] = Convert.ToByte(str.Substring(i*2, 2), 16);
				return Encoding.UTF8.GetString(returnBytes);
			}
			catch
			{
				return "FromHex Fail";
			}
		}

		public static void RegisterService(IPlugin plugin)
		{
			PluginServiceProvider.RegisterService(plugin, "FromBase64", "Decode", Base64_Decode);
			PluginServiceProvider.RegisterService(plugin, "FromUrlEncode", "Decode", FromUrlEncode);
			PluginServiceProvider.RegisterService(plugin, "FromHex", "Decode", FromHex);

			PluginServiceProvider.RegisterService(plugin, "ToBase64", "Encode", Base64_Encode);
			PluginServiceProvider.RegisterService(plugin, "ToMd5_16", "Encode", ToMd5_16);
			PluginServiceProvider.RegisterService(plugin, "ToMd5_32", "Encode", ToMd5_32);
			PluginServiceProvider.RegisterService(plugin, "ToSha1", "Encode", ToSha1);
			PluginServiceProvider.RegisterService(plugin, "ToUrlEncode", "Encode", ToUrlEncode);
			PluginServiceProvider.RegisterService(plugin, "ToUrlEncodeAll", "Encode", ToUrlEncodeAll);
			PluginServiceProvider.RegisterService(plugin, "ToHex", "Encode", ToHex);
		}
	}
}
