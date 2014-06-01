using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Altman.LogicCore;

namespace Altman
{
    public class InitUI
    {
        public static void InitCustomShellType()
        {
            //初始化CustomShellType
            InitWorker.RegisterCustomShellType();
        }

        public static void InitGlobalSetting()
        {
            //初始化GlobalSetting
            InitWorker.InitGlobalSetting();
        }

        /// <summary>
        /// 获取CustomShellType名字列表
        /// </summary>
        public static List<string> GetCustomShellTypeNameList()
        {
            return CustomShellTypeProvider.ShellTypeStyleContainer.Keys.ToList();
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
                foreach (var child in dbNode.Nodes)
                {
                    funcCodeNameList.Add(child.Value.Info);
                }
            }
            return funcCodeNameList.ToArray();
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
