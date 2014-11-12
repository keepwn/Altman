using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Plugin_FileManager.Interface;
using Plugin_FileManager.Resources;

namespace Plugin_FileManager.Model
{
	public class FileInfoView
	{
		public enum FileType
		{
			unknow = 0,
			folder = 1,
			//可执行文件
			exe = 10,
			dll,
			bat,
			bin,
			//压缩文件
			zip = 20,
			rar,
			gz,
			tgz,
			cab,
			//图像文件
			bmp = 30,
			gif,
			jpeg,
			jpg,
			png,
			//媒体文件
			rm = 40,
			rmvb,
			mov,
			mp4,
			mpeg,
			mpg,
			flv,
			swf,
			//文档文件
			dat = 50,
			doc,
			docx,
			eml,
			ini,
			log,
			mdb,
			pdf,
			ppt,
			pptx,
			psd,
			rtf,
			txt,
			wps,
			xls,
			xlsx,
			//语言文件        
			asa = 80,
			asp,
			aspx,
			c,
			cpp,
			cs,
			h,
			htm,
			html,
			jar,
			php,
			ruby,
			sln,
			//其他
			iso = 100,
			chm,
		}
		public string Name { get; set; }
		public string FullName { get; set; }
		public bool IsDir { get; set; }
		public bool IsCreateing { get; set; }
		public string FileMTime { get; set; }
		public string FileSize { get; set; }
		public string FileSizeInt { get; set; }
		public string FileAttributes { get; set; }
		public FileType Type { get; set; }
		public Image Image { get; set; }

		public FileInfoView(string name, string fullname, bool isDir, string mtime, string fileSize, string attribute)
		{
			Name = name;
			FullName = fullname;
			IsDir = isDir;

			//Type
			Type = GetFileTypeByName(name, isDir);

			//Image
			Image = GetImageByFileType(Type);

			//FileMTime
			FileMTime = mtime;

			//FileSizeInt
			FileSizeInt = fileSize;

			//FileSize
			FileSize = ConvertSize(fileSize);

			//Attribute
			FileAttributes = attribute;
		}

		private FileType GetFileTypeByName(string name, bool isDir)
		{
			var fileType = FileType.unknow;
			if (IsDir)
			{
				fileType = FileType.folder;
			}
			else
			{
				var type = Path.GetExtension(name).ToLower();
				if (type.StartsWith("."))
					type = type.TrimStart('.');

				if (!Enum.TryParse<FileType>(type, out fileType))
				{
					fileType = FileType.unknow;
				}
			}
			return fileType;
		}

		private Image GetImageByFileType(FileType type)
		{
			string tmpName = string.Format("{0}_{1}_32.ico", (int)type, type.ToString());

			return Icons.FileType.GetIcon(tmpName);
		}

		private const Decimal OneKByte = 1024M;
		private const Decimal OneMByte = OneKByte * 1024M;
		private const Decimal OneGByte = OneMByte * 1024M;
		private string ConvertSize(string fileSize)
		{
			Decimal size;

			try
			{
				size = Convert.ToDecimal(fileSize);
			}
			catch
			{
				return fileSize+"B";
			}

			if (size == 0)
				return "0";

			string suffix;
			if (size > OneGByte)
			{
				size /= OneGByte;
				suffix = "GB";
			}
			else if (size > OneMByte)
			{
				size /= OneMByte;
				suffix = "MB";
			}
			else if (size > OneKByte)
			{
				size /= OneKByte;
				suffix = "KB";
			}
			else
			{
				suffix = "B";
			}

			return String.Format("{0:N2}{1}", size, suffix);  
		}
	}
}
