using System;
using System.Collections.Generic;
using System.Linq;
using Altman.Desktop.Dialogs;
using Altman.Desktop.Forms;
using Altman.Logic;
using Altman.Setting;
using Eto.Forms;

namespace Altman.Desktop
{
    internal class InitUi
    {
		public static void InitCustomShellType(string customShellTypePath)
        {
            try
            {
                //初始化CustomShellType
				InitWorker.RegisterCustomShellType(customShellTypePath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		public static void InitGlobalSetting(string settingXmlPath)
        {
            try
            {
                //初始化GlobalSetting
				InitWorker.InitGlobalSetting(settingXmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void InitWelcome()
        {
            try
            {
                //获取setting
                Setting.Setting tmp = (Setting.Setting)GlobalSetting.Setting;
                if (tmp.IsShowDisclaimer)
                {
                    var w = new Welcome(tmp);
                    w.ShowModal(Application.Instance.MainForm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 获取CustomShellType名字列表
        /// </summary>
        public static string[] GetCustomShellTypeNameList()
        {
            return CustomShellTypeProvider.ShellTypeStyleContainer.Keys.ToArray();
        }
        /// <summary>
		/// 获取CustomShellType/DbManager子节点的Info列表
        /// </summary>
        /// <param name="shellTypeName"></param>
        /// <returns></returns>
        public static string[] GetDbNodeFuncCodeNameList(string shellTypeName)
        {
            List<string> funcCodeNameList = new List<string>();
            if (CustomShellTypeProvider.ShellTypeStyleContainer.ContainsKey(shellTypeName))
            {
                CustomShellType shelltype = CustomShellTypeProvider.ShellTypeStyleContainer[shellTypeName];
                FuncTreeNode dbNode = shelltype.FuncTreeRoot.FindNodes("/DbManager");
                if (dbNode != null)
                {
                    foreach (var child in dbNode.Nodes)
                    {
                        funcCodeNameList.Add(child.Value.Info);
                    }
                }
            }
            return funcCodeNameList.ToArray();
        }

        public static string GetCustomShellTypeServerCode(string shellTypeName)
        {
            if (CustomShellTypeProvider.ShellTypeStyleContainer.ContainsKey(shellTypeName))
            {
                CustomShellType shelltype = CustomShellTypeProvider.ShellTypeStyleContainer[shellTypeName];
                return shelltype.BasicSetting.ServiceExample;
            }
            return null;
        }

        /// <summary>
        /// 显示树节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TreeItem GetTreeNodes(FuncTreeNode node)
        {
            var tree = new TreeItem {Text = node.Name};
            foreach (var func in node.Funcs.Keys)
            {
                var tmp = new TreeItem {Text = func};
                tree.Children.Add(tmp);
            }
            if (node.Nodes.Count > 0)
            {
                foreach (var child in node.Nodes)
                {
                    tree.Children.Add(GetTreeNodes(child.Value));
                }
            }
            return tree;
        }
        public static TreeItem GetCustomShellTypeTree()
        {
            var tree = new TreeItem();
            foreach (var shelltype in CustomShellTypeProvider.ShellTypeStyleContainer)
            {
                tree.Children.Add(GetTreeNodes(shelltype.Value.FuncTreeRoot));
            }
            return tree;
        }
    }
}
