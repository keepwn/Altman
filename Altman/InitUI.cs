using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altman.LogicCore.New;

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
    }
}
