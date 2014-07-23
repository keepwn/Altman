using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace Altman.Common.AltData
{
    public class DataConvert
    {
        private static string Base64_decode(string str)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(str);
                return Encoding.Default.GetString(bytes);
            }
            catch
            {
                return "Base64 Decode Fail";
            }
        }

        private static string Base64_encode(string str)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(str));
        }

        public static string StrToBase64(string str, int type)
        {
            if (type == 1)
            {
                return Base64_encode(str);
            }
            if (type == 0)
            {
                return Base64_decode(str);
            }
            return "ToBase64 is error";
        }

        public static bool StrToBool(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            if (str == "1" || str.ToLower() == "true")
            {
                return true;
            }
            return false;
        }

        public static string StrToUrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str, Encoding.Default);
        }

        public static string StrToHex(string str)
        {
            string str2 = "";
            if (str == "")
            {
                return "";
            }
            byte[] bytes = Encoding.Default.GetBytes(str);
            for (int i = 0; i < bytes.Length; i++)
            {
                str2 = str2 + bytes[i].ToString("X");
            }
            return str2;
        }

        public static string BytesToHexStr(byte[] bytes)
        {
            StringBuilder returnStr = new StringBuilder(1024);
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr.Append(bytes[i].ToString("X2"));
                }
            }
            return returnStr.ToString();
        }

        public static byte[] HexStrToBytes(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static NameValueCollection GetParma(string dataCombined)
        {
            NameValueCollection items = new NameValueCollection();
            string[] tmp = dataCombined.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in tmp)
            {
                string[] nameValue = str.Split('=');
                if (nameValue.Length == 2)
                {
                    items.Add(nameValue[0], nameValue[1]);
                }
                else
                {
                    throw new Exception("Split data to nameValueCollection failed");
                }
            }
            return items;
        }
    }
}
