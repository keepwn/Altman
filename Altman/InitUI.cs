using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Altman.Forms;
using Altman.Logic;
using Altman.Setting;

namespace Altman
{
    internal class InitUi
    {
        public static void InitCustomShellType()
        {
            try
            {
                //初始化CustomShellType
                InitWorker.RegisterCustomShellType();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void InitGlobalSetting()
        {
            try
            {
                //初始化GlobalSetting
                InitWorker.InitGlobalSetting();
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
                    Welcome w = new Welcome(tmp);
                    w.ShowDialog();
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
        /// 获取CustomShellType子节点的名字列表
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
        public static TreeNode GetTreeNodes(FuncTreeNode node)
        {
            TreeNode tree = new TreeNode(node.Name);
            foreach (var func in node.Funcs.Keys)
            {
                tree.Nodes.Add(func);
            }
            if (node.Nodes.Count > 0)
            {
                foreach (var child in node.Nodes)
                {
                    tree.Nodes.Add(GetTreeNodes(child.Value));
                }
            }
            return tree;
        }
        public static TreeNode GetCustomShellTypeTree()
        {
            TreeNode tree = new TreeNode();
            foreach (var shelltype in CustomShellTypeProvider.ShellTypeStyleContainer)
            {
                tree.Nodes.Add(GetTreeNodes(shelltype.Value.FuncTreeRoot));
            }
            return tree;
        }
    }
}
