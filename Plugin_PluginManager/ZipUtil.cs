using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace Plugin_PluginManager
{
    public static class ZipUtil
    {
        /// <summary>
        /// 压缩ZIP文件
        /// </summary>
        /// <param name="fileList">待压缩的文件或目录集合</param>
        /// <param name="zipName">压缩后的文件名</param>
        /// <param name="isDirStruct">是否按目录结构压缩</param>
        public static bool Compress(List<string> fileList, string zipName, bool isDirStruct)
        {
            try
            {
                using (var zip = new ZipFile(Encoding.Default))
                {
                    foreach (string path in fileList)
                    {
                        string fileName = Path.GetFileName(path);
                        if (Directory.Exists(path))
                        {
                            //按目录结构压缩
                            if (isDirStruct)
                            {
                                zip.AddDirectory(path, fileName);
                            }
                            else//目录下文件压缩到根目录
                            {
                                zip.AddDirectory(path);
                            }
                        }
                        if (File.Exists(path))
                        {
                            zip.AddFile(path);
                        }
                    }
                    zip.Save(zipName);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 解压ZIP文件
        /// </summary>
        /// <param name="zipPath">待解压的ZIP文件</param>
        /// <param name="unZipPath">解压的目录</param>
        /// <param name="isOverWrite">是否覆盖</param>
        public static bool Decompress(string zipPath, string unZipPath, bool isOverWrite)
        {
            try
            {
                ReadOptions options = new ReadOptions();
                options.Encoding = Encoding.Default;
                using (var zip = ZipFile.Read(zipPath, options))
                {
                    foreach (ZipEntry entry in zip)
                    {
                        if (string.IsNullOrEmpty(unZipPath))
                        {
                            unZipPath = zipPath.Split('.').First();
                        }
                        entry.Extract(unZipPath,isOverWrite
                            ? ExtractExistingFileAction.OverwriteSilently
                            : ExtractExistingFileAction.DoNotOverwrite);
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}