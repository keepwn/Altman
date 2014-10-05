using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginFramework
{
	public interface ITest
	{
		string Msg { get; set; }

		string ShowMsg();
	}

	public interface ITestPlus:ITest
	{
		string Name { get; set; }
	}
	public interface IConfig
	{
		string Intro { get; }
	}
}