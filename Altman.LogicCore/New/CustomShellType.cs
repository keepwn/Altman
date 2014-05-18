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
        public class BasicSettingStruct
        {
            public string ShellTypeName;
            public string ServiceExample;
            public ParamStruct MainCodeParam;

            public BasicSettingStruct()
            {
            }

            public BasicSettingStruct(string shellTypeName, string serviceExample, ParamStruct mainCodeParam)
            {
                this.ShellTypeName = shellTypeName;
                this.ServiceExample = serviceExample;
                this.MainCodeParam = mainCodeParam;
            }
        }
        public class MainCodeSettingStruct
        {
            public string Item;
            public ParamStruct FuncCodeParam;

            public MainCodeSettingStruct()
            {
            }

            public MainCodeSettingStruct(string item, ParamStruct funcCodeParam)
            {
                this.Item = item;
                this.FuncCodeParam = funcCodeParam;
            }
        }
        public class FuncCodeSettingStruct
        {
            public string Name;
            public string Type;
            public string Item;
            public List<ParamStruct> FuncParams;

            public FuncCodeSettingStruct()
            {

            }
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
        private Dictionary<string, Dictionary<string, FuncCodeSettingStruct>> _funcCodeSettingListContainer;
        public BasicSettingStruct BasicSetting
        {
            get { return _basicSetting; }
        }
        public MainCodeSettingStruct MainCodeSetting
        {
            get { return _mainCodeSetting; }
        }

        public Dictionary<string, Dictionary<string, FuncCodeSettingStruct>> FuncCodeSettingListContainer
        {
            get { return _funcCodeSettingListContainer; } 
        }
        #endregion

        #region 构造函数
        public CustomShellType(BasicSettingStruct basicSetting,
                               MainCodeSettingStruct mainCodeSetting)
        {
            _shellTypeName = basicSetting.ShellTypeName;
            _basicSetting = basicSetting;
            _mainCodeSetting = mainCodeSetting;
            _funcCodeSettingListContainer = new Dictionary<string, Dictionary<string, FuncCodeSettingStruct>>
            {
                //初始化default子节点
                {"default", new Dictionary<string, FuncCodeSettingStruct>()}
            };
        }

        public CustomShellType(BasicSettingStruct basicSetting, 
                               MainCodeSettingStruct mainCodeSetting,
                               Dictionary<string, Dictionary<string, FuncCodeSettingStruct>> funcCodeSettingListContainer)
        {
            _shellTypeName = basicSetting.ShellTypeName;
            _basicSetting = basicSetting;
            _mainCodeSetting = mainCodeSetting;
            _funcCodeSettingListContainer = funcCodeSettingListContainer ?? new Dictionary<string, Dictionary<string, FuncCodeSettingStruct>>
            {
                //初始化default子节点
                {"default", new Dictionary<string, FuncCodeSettingStruct>()}
            }; ;
        }
        #endregion


        private void AddFuncCodeNode(string nodeName)
        {
            _funcCodeSettingListContainer.Add(nodeName, new Dictionary<string, FuncCodeSettingStruct>());
        }

        public void AddFuncCodeSettingStruct(string nodeName, FuncCodeSettingStruct funcCodeSettingStruct)
        {
            //如果nodeName为空，默认赋值为default
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                nodeName = "default";
            }
            //检查nodeName节点是否已经存在
            if (!_funcCodeSettingListContainer.ContainsKey(nodeName))
            {
                AddFuncCodeNode(nodeName);

                //注册到funcCodeName子节点
                _funcCodeSettingListContainer[nodeName].Add(funcCodeSettingStruct.Name, funcCodeSettingStruct);
            }
            else
            {
                //检查funcCodeName子节点是否被已经注册
                if (_funcCodeSettingListContainer[nodeName].ContainsKey(funcCodeSettingStruct.Name))
                {
                    throw new Exception("FuncCode " + funcCodeSettingStruct.Name + " has been registered");
                }
                //注册到funcCodeName子节点
                _funcCodeSettingListContainer[nodeName].Add(funcCodeSettingStruct.Name, funcCodeSettingStruct);
            }
        }

        public FuncCodeSettingStruct GetFuncCodeSettingStruct(string nodeName, string funcCodeSettingStructName)
        {
            //如果nodeName为空，默认赋值为default
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                nodeName = "default";
            }

            //检查nodeName节点是否已经存在
            if (_funcCodeSettingListContainer.ContainsKey(nodeName))
            {
                //检查funcCodeName子节点是否被已经注册
                if (_funcCodeSettingListContainer[nodeName].ContainsKey(funcCodeSettingStructName))
                {
                    //获取funcCodeSettingStruct
                    return _funcCodeSettingListContainer[nodeName][funcCodeSettingStructName];
                }
            }
            //其他情况均为未注册
            throw new Exception(nodeName+"/"+funcCodeSettingStructName + " hasn't been registered");         
        }
    }
}
