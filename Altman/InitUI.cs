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
            return CustomShellTypeProvider.ShellTypeStyleContainer[shellTypeName].FuncTreeRoot.Nodes["db"].Nodes.Keys.ToArray();
        }

        public static TreeNode GetCustomShellTypeTree()
        {
            TreeNode tree = new TreeNode();
            foreach (var shelltype in CustomShellTypeProvider.ShellTypeStyleContainer)
            {
                TreeNode tmp = tree.Nodes.Add(shelltype.Key);
                foreach (var func in shelltype.Value.FuncTreeRoot.Funcs)
                {
                    tmp.Nodes.Add(func.Key);
                }
                foreach (var func in shelltype.Value.FuncTreeRoot.Nodes)
                {
                    TreeNode t = tmp.Nodes.Add(func.Key);
                    foreach (var i in func.Value.Nodes)
                    {
                        TreeNode m = t.Nodes.Add(i.Key);
                        foreach (var a in i.Value.Funcs)
                        {
                            m.Nodes.Add(a.Key);
                        }
                    }
                }
            }
            return tree;
        }

    }
}
