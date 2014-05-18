using System;
using System.Collections.Generic;

namespace Altman.LogicCore.New
{
    public static class CustomShellTypeProvider
    {
        /// <summary>
        /// ShellType存储容器
        ///         root
        /// |       |       |       |       |
        /// asp     aspx    aspx2   php     ...
        /// |
        /// cmder,filemanager
        /// </summary>
        private static Dictionary<string, CustomShellType> _shellTypeStyleContainer;
        public static Dictionary<string, CustomShellType> ShellTypeStyleContainer
        {
            get
            {
                return _shellTypeStyleContainer;
            }
        }
        static CustomShellTypeProvider()
        {
            _shellTypeStyleContainer = new Dictionary<string, CustomShellType>();
        }
        public static void AddShellType(CustomShellType newShellType)
        {
            try
            {
                //检查基本字段是否被设置
                if (string.IsNullOrEmpty(newShellType.ShellTypeName))
                {
                    throw new Exception("ShellTypeName is Null Or Empty");
                }
                //检查shelltype是否被已经存储
                if (_shellTypeStyleContainer.ContainsKey(newShellType.ShellTypeName))
                {
                    throw new Exception("The ShellTypeName already exists");
                }
                //存储
                _shellTypeStyleContainer.Add(newShellType.ShellTypeName, newShellType);
            }
            catch
            {
                throw;
            }
        }
        public static void RemoveShellType(string shellTypeName)
        {
            if (string.IsNullOrEmpty(shellTypeName))
            {
                throw new Exception("ShellTypeName is Null Or Empty");
            }
            //检查shelltype是否被已经存储
            if (_shellTypeStyleContainer.ContainsKey(shellTypeName))
            {
                _shellTypeStyleContainer.Remove(shellTypeName);
            }
        }   
        public static CustomShellType GetShellType(string shellTypeName)
        {
            if (!ShellTypeStyleContainer.ContainsKey(shellTypeName))
            {
                throw new Exception("发生错误，未注册" + shellTypeName + "类型");
            }
            return ShellTypeStyleContainer[shellTypeName];
        }
        public static void AddFuncCode(string shellTypeXpath, CustomShellType.FuncCodeSettingStruct funcCode)
        {
            //分解shellTypeXpath，将"aspx/cmder"分解为aspx和cmder
            string[] xpath = shellTypeXpath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string shellTypeName = xpath[0];
            string nodeName = string.Empty;
            if (xpath.Length > 1)
            {
                nodeName = xpath[1];
            }

            CustomShellType shellType = GetShellType(shellTypeName);
            if (shellType != null)
            {
                var tmp = new CustomShellType.FuncCodeSettingStruct(funcCode.Name,
                                                                    funcCode.Type,
                                                                    funcCode.Item,
                                                                    funcCode.FuncParams);
                shellType.AddFuncCodeSettingStruct(nodeName, tmp);
            }
        }

    }
}
