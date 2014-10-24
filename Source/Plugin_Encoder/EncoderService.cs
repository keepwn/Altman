using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
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
				return "Base64 Decode Fail";
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

		public static void RegisterService(IPlugin plugin)
		{
			PluginServiceProvider.RegisterService(plugin, "ToBase64", "Encode", Base64_Encode);
			PluginServiceProvider.RegisterService(plugin, "FromBase64", "Decode", Base64_Decode);
			PluginServiceProvider.RegisterService(plugin, "ToMd5_16", "Encode", ToMd5_16);
			PluginServiceProvider.RegisterService(plugin, "ToMd5_32", "Encode", ToMd5_32);
		}
	}
}
