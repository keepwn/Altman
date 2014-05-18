using System;
using System.Collections.Generic;

namespace Altman.LogicCore.New
{
    public class CustomShellType
    {
        #region enum
        public enum FuncCodeName
        {
            //shellcmder
            SysInfoCode,
            ExecuteCommandCode,
            //filemanager,
            WWWRootPathCode,
            FileTreeCode,
            ReadFileCode,
            WriteFileCode,
            DeleteFileOrDirCode,
            DownloadFileCode,
            UploadFileCode,
            CopyFileOrDirCode,
            RenameFileOrDirCode,
            CreateDirCode,
            ModifyFileOrDirTimeCode,
            WgetCode
        }
        public enum EncryMode
        {
            None = 0,
            Base64 = 1,
            Hex = 2
        }
        #endregion

        #region struct
        public struct ParamStruct
        {
            public string Name;
            public string Location;
            public EncryMode EncryMode;
            public ParamStruct(string name, string location, EncryMode encryMode)
            {
                Name = name;
                Location = location;
                EncryMode = encryMode;
            }
        }
        public struct BasicSettingStruct
        {
            public string ShellTypeName;
            public string ServiceExample;
            public ParamStruct MainCodeParam;
            public BasicSettingStruct(string shellTypeName, string serviceExample, ParamStruct mainCodeParam)
            {
                this.ShellTypeName = shellTypeName;
                this.ServiceExample = serviceExample;
                this.MainCodeParam = mainCodeParam;
            }
        }
        public struct MainCodeSettingStruct
        {
            public string Item;
            public ParamStruct FuncCodeParam;
            public MainCodeSettingStruct(string item, ParamStruct funcCodeParam)
            {
                this.Item = item;
                this.FuncCodeParam = funcCodeParam;
            }
        }
        public struct FuncCodeSettingStruct
        {
            public string Name;
            public string Type;
            public string Item;
            public List<ParamStruct> FuncParams;
            public FuncCodeSettingStruct(string funcName, string funcType, string funcItem, List<ParamStruct> funcParams)
            {
                this.Name = funcName;
                this.Type = funcType;
                this.Item = funcItem;
                this.FuncParams = funcParams;
            }
        }

        #endregion

        #region 字段
        private string _shellTypeName;
        public string ShellTypeName
        {
            get { return _shellTypeName; }
            set { _shellTypeName = value; }
        }

        private BasicSettingStruct _basicSetting;
        private MainCodeSettingStruct _mainCodeSetting;
        private Dictionary<string, FuncCodeSettingStruct> _funcCodeSettingStructContainer;
        public BasicSettingStruct BasicSetting
        {
            get { return _basicSetting; }
        }
        public MainCodeSettingStruct MainCodeSetting
        {
            get { return _mainCodeSetting; }
        }
        public Dictionary<string, FuncCodeSettingStruct> FuncCodeSettingStructContainer
        {
            get { return _funcCodeSettingStructContainer; }
        }

        #endregion

        #region 构造函数
        public CustomShellType(BasicSettingStruct basicSetting,
                               MainCodeSettingStruct mainCodeSetting)
        {
            _shellTypeName = basicSetting.ShellTypeName;
            _basicSetting = basicSetting;
            _mainCodeSetting = mainCodeSetting;
            _funcCodeSettingStructContainer = new Dictionary<string, FuncCodeSettingStruct>();
        }

        public CustomShellType(BasicSettingStruct basicSetting, 
                               MainCodeSettingStruct mainCodeSetting,
                               Dictionary<string, FuncCodeSettingStruct> funcCodeSettingStructContainer)
        {
            _shellTypeName = basicSetting.ShellTypeName;
            _basicSetting = basicSetting;
            _mainCodeSetting = mainCodeSetting;
            _funcCodeSettingStructContainer = funcCodeSettingStructContainer ?? new Dictionary<string, FuncCodeSettingStruct>();
        }
        #endregion

        public void AddFuncCodeSettingStruct(FuncCodeSettingStruct funcCodeSettingStruct)
        {
            //检查基本字段Name是否被设置
            if (string.IsNullOrEmpty(funcCodeSettingStruct.Name))
            {
                throw new Exception("funcCode的Name字段为空");
            }
            //检查funcCodeSettingStruct是否被已经存储
            if (_funcCodeSettingStructContainer.ContainsKey(funcCodeSettingStruct.Name))
            {
                throw new Exception("方法" + funcCodeSettingStruct.Name + "已经被注册");
            }
            //存储
            _funcCodeSettingStructContainer.Add(funcCodeSettingStruct.Name, funcCodeSettingStruct);
        }

        public FuncCodeSettingStruct GetFuncCodeSettingStruct(string funcCodeSettingStructName)
        {
            //检查funcCodeSettingStructName是否存在
            if (!_funcCodeSettingStructContainer.ContainsKey(funcCodeSettingStructName))
            {
                throw new Exception(funcCodeSettingStructName + "未被注册");
            }
            //获取funcCodeSettingStruct
            return _funcCodeSettingStructContainer[funcCodeSettingStructName];
        }
    }
}
