using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using PluginFramework;

namespace Plugin_Encoder
{
	public class EncoderService
	{
		private static string Base64_Decode(string str)
		{
			try
			{
				var bytes = Convert.FromBase64String(str);
				return Encoding.Default.GetString(bytes);
			}
			catch
			{
				return "Base64 Decode Fail";
			}
		}

		private static string Base64_Encode(string str)
		{
			return Convert.ToBase64String(Encoding.Default.GetBytes(str));
		}

		public static string ToBase64(string str, bool encodeOrDecode)
		{
			return encodeOrDecode ? Base64_Encode(str) : Base64_Decode(str);
		}

		public static string ToMd5_16(string str)
		{
			var mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			var tmp = BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(str)), 4, 8);
			tmp = tmp.Replace("-", "");
			return tmp;
		}

		public static string ToMd5_32(string str)
		{
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

		public static void RegisterService(string pluginName)
		{
			PluginServiceProvider.RegisterService<Func<string, bool, string>>
				(pluginName, "ToBase64", "Encode", ToBase64);
			PluginServiceProvider.RegisterService<Func<string, string>>
				(pluginName, "ToMd5_16", "Convert", ToMd5_16);
			PluginServiceProvider.RegisterService<Func<string, string>>
				(pluginName, "ToMd5_32", "Convert", ToMd5_32);
		}
	}
}
