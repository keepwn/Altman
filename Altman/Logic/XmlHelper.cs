using System.Collections.Generic;
using System.IO;

namespace Altman.Logic
{
    internal class XmlHelper
    {
        /// <summary>
        /// 载入xml文件列表
        /// </summary>
        public static List<string> LoadXMlList(string basePathDir, string extension)
        {
            List<string> xmlList = new List<string>();
            DirectoryInfo theFolder = new DirectoryInfo(basePathDir);
            FileInfo[] fileInfos = theFolder.GetFiles();
            //遍历文件夹
            foreach (FileInfo file in fileInfos)
            {
                if (file.Name.EndsWith("." + extension))
                    xmlList.Add(file.Name);
            }
            return xmlList;
        }
    }
}