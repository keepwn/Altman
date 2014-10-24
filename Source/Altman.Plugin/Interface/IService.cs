using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altman.Plugin.Interface
{
	public interface IServiceMeta
	{
		string Name { get; }
		string Type { get; }
	}

	public interface IService
	{
		/// <summary>
		/// 加载
		/// </summary>
		/// <returns>成功或失败</returns>
		bool Load();
	}
}
