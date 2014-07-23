using System;
using System.Collections.Generic;

namespace Altman.Logic
{
    internal static class CustomShellTypeProvider
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
                throw new Exception(string.Format("ShellType:[{0}] hasn't been registered", shellTypeName));
            }
            return ShellTypeStyleContainer[shellTypeName];
        }
        public static void Clear()
        {
            _shellTypeStyleContainer.Clear();
        }
    }
}
