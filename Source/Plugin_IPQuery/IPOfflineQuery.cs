using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Plugin_IPQuery
{
	public class IPOfflineQuery
	{
		readonly string ipBinaryFilePath = "17monipdb.dat";
        readonly byte[] dataBuffer, indexBuffer;
        readonly uint[] index = new uint[256];
        readonly int offset;
		public IPOfflineQuery()
        {
			var file = new FileInfo(ipBinaryFilePath);
			if (!file.Exists)
			{
				throw new FileNotFoundException("not find 17monipdb.dat");
			}
			dataBuffer = new byte[file.Length];
			using (var fin = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
			{
				fin.Read(dataBuffer, 0, dataBuffer.Length);
			}

			var indexLength = BytesToLong(dataBuffer[0], dataBuffer[1], dataBuffer[2], dataBuffer[3]);
			indexBuffer = new byte[indexLength];
			Array.Copy(dataBuffer, 4, indexBuffer, 0, indexLength);
			offset = (int) indexLength;

			for (int loop = 0; loop < 256; loop++)
			{
				index[loop] = BytesToLong(indexBuffer[loop*4 + 3], indexBuffer[loop*4 + 2], indexBuffer[loop*4 + 1],
					indexBuffer[loop*4]);
			}
        }
        private static uint BytesToLong(byte a, byte b, byte c, byte d)
        {
            return ((uint)a << 24) | ((uint)b << 16) | ((uint)c << 8) | (uint)d;
        }

		private bool IsValidIP(string ip)
		{
			var pattrn = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
			return Regex.IsMatch(ip, pattrn);
		}

        public string[] Find(string ip)
        {
	        try
	        {
		        if (!IsValidIP(ip))
		        {
			        throw new FormatException("Sorry, please input valid ip");
		        }
		        var ips = ip.Split('.');
		        int ipPrefixValue = int.Parse(ips[0]);
		        long ip2LongValue = BytesToLong(
			        byte.Parse(ips[0]),
			        byte.Parse(ips[1]),
			        byte.Parse(ips[2]),
			        byte.Parse(ips[3]));
		        uint start = index[ipPrefixValue];
		        int maxCompLen = offset - 1028;
		        long indexOffset = -1;
		        int indexLength = -1;
		        byte b = 0;
		        for (start = start*8 + 1024; start < maxCompLen; start += 8)
		        {
			        if (
				        BytesToLong(indexBuffer[start + 0], indexBuffer[start + 1], indexBuffer[start + 2], indexBuffer[start + 3]) >=
						ip2LongValue)
			        {
				        indexOffset = BytesToLong(b, indexBuffer[start + 6], indexBuffer[start + 5], indexBuffer[start + 4]);
				        indexLength = 0xFF & indexBuffer[start + 7];
				        break;
			        }
		        }
		        var areaBytes = new byte[indexLength];
		        Array.Copy(dataBuffer, offset + (int) indexOffset - 1024, areaBytes, 0, indexLength);
		        return Encoding.UTF8.GetString(areaBytes).Split('\t');
	        }
	        catch(Exception ex)
	        {
		        return new[] {ex.Message};
	        }
        }
	}
}
