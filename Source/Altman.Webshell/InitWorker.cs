using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Altman.Webshell.Core;

namespace Altman.Webshell
{
	public class InitWorker
	{
		/// <summary>
		/// 注册CustomShellType
		/// </summary>
		public static void RegisterCustomShellType(string customShellTypePath)
		{
			//清空CustomShellTypeProvider
			CustomShellTypeProvider.Clear();

			//读取shelltype列表（.type）
			List<string> typeList = XmlHelper.LoadXMlList(customShellTypePath, "type");
			//1.注册CustomShellType
			foreach (string c in typeList)
			{
				var basicSetting = new CustomShellType.Basic();
				var mainCodeSetting = new CustomShellType.MainCode();

				//读取basicSetting,mainCodeSetting
				CustomShellTypeXmlHandle.ReadXml(c, customShellTypePath, ref basicSetting, ref mainCodeSetting);
				//生成customShellType
				var customShellType = new CustomShellType(basicSetting, mainCodeSetting);
				//将CustomShellType注册到全局
				CustomShellTypeProvider.AddShellType(customShellType);
			}

			//读取funcTree定义列表（.tree）       
			List<string> funcTreeList = XmlHelper.LoadXMlList(customShellTypePath, "tree");
			//2.初始化funcTree方法树
			foreach (string c in funcTreeList)
			{
				var treeInfoList = new List<CustomShellType.TreeInfo>();

				//读取funcCodeList
				CustomShellTypeXmlHandle.ReadXml(c, customShellTypePath, ref treeInfoList);
				//将func注册到CustomShellType
				foreach (CustomShellType.TreeInfo info in treeInfoList)
				{
					/***
					 * 获取节点的类型
					 * 允许多个类型，以英文逗号分隔，如"aspx,aspx1"
					 */
					string[] types = info.Type.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string type in types)
					{
						CustomShellType shellType = CustomShellTypeProvider.GetShellType(type);
						FuncTreeNode node = shellType.AddFuncTreeNode(info.Path);
						node.Info = info.Info;
					}
				}
			}

			//读取funcCode列表（.func）
			List<string> funcList = XmlHelper.LoadXMlList(customShellTypePath, "func");
			//3.注册funcCode到functree
			foreach (string c in funcList)
			{
				var funcCodeList = new List<CustomShellType.FuncCode>();

				//读取funcCodeList
				CustomShellTypeXmlHandle.ReadXml(c, customShellTypePath, ref funcCodeList);
				//将func注册到CustomShellType
				foreach (CustomShellType.FuncCode func in funcCodeList)
				{
					/***
					 * 获取func的类型
					 * type允许多个类型，以英文逗号分隔，如"aspx,aspx1"
					 */
					string[] types = func.Type.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string type in types)
					{
						CustomShellType shellType = CustomShellTypeProvider.GetShellType(type);
						//获取映射节点
						//path为xpath形式，如"/cmder"，
						//允许多个，以英文逗号分隔，如"/cmder,/cmder1"
						string[] xpaths = func.Path.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
						foreach (string xpath in xpaths)
						{
							shellType.AddFuncCode(xpath, func);
						}
					}
				}
			}
		}

		///// <summary>
		///// 显示树节点
		///// </summary>
		///// <param name="node"></param>
		///// <returns></returns>
		//public static TreeItem GetTreeNodes(FuncTreeNode node)
		//{
		//	var tree = new TreeItem { Text = node.Name };
		//	foreach (var func in node.Funcs.Keys)
		//	{
		//		var tmp = new TreeItem { Text = func };
		//		tree.Children.Add(tmp);
		//	}
		//	if (node.Nodes.Count > 0)
		//	{
		//		foreach (var child in node.Nodes)
		//		{
		//			tree.Children.Add(GetTreeNodes(child.Value));
		//		}
		//	}
		//	return tree;
		//}
		//public static TreeItem GetCustomShellTypeTree()
		//{
		//	var tree = new TreeItem();
		//	foreach (var shelltype in CustomShellTypeProvider.ShellTypeStyleContainer)
		//	{
		//		tree.Children.Add(GetTreeNodes(shelltype.Value.FuncTreeRoot));
		//	}
		//	return tree;
		//}
	}
}
