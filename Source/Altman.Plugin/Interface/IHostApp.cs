using System;

namespace Altman.Plugin.Interface
{
    public interface IHostApp
    {
		/// <summary>
		/// 程序版本
		/// </summary>
        Version AppVersion { get; }

		/// <summary>
		/// 程序当前目录
		/// </summary>
        string AppCurrentDir { get; }

		/// <summary>
		/// 程序插件目录
		/// </summary>
        string AppPluginDir { get; }

		/// <summary>
		/// 程序插件配置路径
		/// </summary>
        string AppPluginConfigDir { get; }

		/// <summary>
		/// 程序Bin目录
		/// </summary>
        string AppBinDir { get; }

		/// <summary>
		/// 程序多语言目录
		/// </summary>
        string AppLanguageDir { get; }
    }
}
