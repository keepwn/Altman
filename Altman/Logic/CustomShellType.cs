using System;
using System.Collections.Generic;

namespace Altman.Logic
{
    internal class CustomShellType
    {
        #region class
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
        public class Basic
        {
            public string ShellTypeName;
            public string ServiceExample;
            public ParamStruct MainCodeParam;

            public Basic()
            {
            }

            public Basic(string shellTypeName, string serviceExample, ParamStruct mainCodeParam)
            {
                this.ShellTypeName = shellTypeName;
                this.ServiceExample = serviceExample;
                this.MainCodeParam = mainCodeParam;
            }
        }
        public class MainCode
        {
            public string Item;
            public ParamStruct FuncCodeParam;

            public MainCode()
            {
            }

            public MainCode(string item, ParamStruct funcCodeParam)
            {
                this.Item = item;
                this.FuncCodeParam = funcCodeParam;
            }
        }
        public class FuncCode
        {
            public string Name;
            public string Type;
            public string Path;
            public string Item;
            public List<ParamStruct> FuncParams;

            public FuncCode()
            {

            }
            public FuncCode(string funcName, string funcType, string funcPath, string funcItem, List<ParamStruct> funcParams)
            {
                this.Name = funcName;
                this.Type = funcType;
                this.Path = funcPath;
                this.Item = funcItem;
                this.FuncParams = funcParams;
            }
        }
        public class TreeInfo
        {
            public string Path;
            public string Type;
            public string Info;

            public TreeInfo()
            {
                
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

        private Basic _basicSetting;
        private MainCode _mainCodeSetting;

        private FuncTreeNode _funcTreeRoot;
        public FuncTreeNode FuncTreeRoot
        {
            get { return _funcTreeRoot; }
        }

        public Basic BasicSetting
        {
            get { return _basicSetting; }
        }
        public MainCode MainCodeSetting
        {
            get { return _mainCodeSetting; }
        }
        #endregion

        #region 构造函数
        public CustomShellType(Basic basicSetting,
                               MainCode mainCodeSetting)
        {
            _shellTypeName = basicSetting.ShellTypeName;
            _basicSetting = basicSetting;
            _mainCodeSetting = mainCodeSetting;

            //初始化方法树
            _funcTreeRoot = new FuncTreeNode(_shellTypeName);
        }
        #endregion

        public FuncTreeNode AddFuncTreeNode(string nodeXpath)
        {
            FuncTreeNode tmp = _funcTreeRoot.FindNodes(nodeXpath);
            //如果节点不存在
            if (tmp == null)
            {
                return _funcTreeRoot.AddNodes(nodeXpath);
            }
            return tmp;
        }

        public void AddFuncCode(string nodeXpath, FuncCode funcCode)
        {
            FuncTreeNode tmp = _funcTreeRoot.FindNodes(nodeXpath);
            //如果节点不存在
            if (tmp == null)
            {
                throw new Exception(string.Format("FuncTreeNode:[{0}/{1}] has't been defined", _shellTypeName, nodeXpath));
            }
            //如果funcCode已经注册
            if (tmp.Funcs.ContainsKey(funcCode.Name))
            {
                throw new Exception(string.Format("FuncCode:[{0}/{1}/{2}] has been registered", _shellTypeName, nodeXpath, funcCode.Name));
            }
            else
            {
                tmp.Funcs.Add(funcCode.Name, funcCode);
            }
        }

        public FuncCode GetFuncCode(string nodeXpath, string funcCodeName)
        {
            FuncTreeNode tmp = _funcTreeRoot.FindNodes(nodeXpath);
            //如果节点存在
            if (tmp != null)
            {
                //如果funcCode已经注册
                if (tmp.Funcs.ContainsKey(funcCodeName))
                {
                    return tmp.Funcs[funcCodeName];
                }
            }
            //其他情况均为未注册
            throw new Exception(string.Format("FuncCode:[{0}/{1}/{2}] hasn't been registered", _shellTypeName, nodeXpath, funcCodeName));
        }
    }
}
