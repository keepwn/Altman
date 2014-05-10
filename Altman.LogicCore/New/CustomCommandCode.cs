using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using Altman.Common.AltData;
using Altman.Setting;

namespace Altman.LogicCore.New
{
    public class CustomCommandCode
    {
        private CustomShellType _customShellType;
        private string _pass;
        private string _coding;

        private Dictionary<string, string> _randomParam;
        public CustomCommandCode(CustomShellType customShellType, string pass)
        {
            _customShellType = customShellType;
            _pass = pass;
            _randomParam = new Dictionary<string, string>();
        }

        /**
         * Dictionary<string1,string2> 
         * string1值为：Body,Cookie,Referer(header头)等
         * string2值为：pass=MMM&z=AAA&z1=BBB ,MMM,等
         */
        private void AddItemToDic(Dictionary<string, string> dic, string key, string value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] += "&" + value;
            }
            else
            {
                dic.Add(key, value);
            }
        }
        private string EncryItem(CustomShellType.EncryMode mode, string item)
        {
            if (mode == CustomShellType.EncryMode.None)
            {
                return item;
            }
            else if (mode == CustomShellType.EncryMode.Base64)
            {
                return DataConvert.StrToBase64(item, 1);
            }
            else if (mode == CustomShellType.EncryMode.Hex)
            {
                return DataConvert.StrToHex(item);
            }
            else
            {
                return item;
            }
        }


        private string GetRandomStr(int length)
        {
            Random r = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string s = ((char)r.Next(97, 123)).ToString();
                str += s;
            }
            return str;
        }

        /// <summary>
        /// 填充参数，主要用于MainCodeSetting.item和FuncCodeSetting.func.item中的$par$=>par
        /// </summary>
        /// <param name="item"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        private string FillParams(string item, object pars)
        {
            List<string> parList = new List<string>();
            //将传入参数转换为List<string>
            if (pars is CustomShellType.ParamStruct)
            {
                parList.Add(((CustomShellType.ParamStruct)pars).Name);
            }
            else if (pars is List<CustomShellType.ParamStruct>)
            {
                List<CustomShellType.ParamStruct> tmp = (List<CustomShellType.ParamStruct>)pars;
                foreach (var i in tmp)
                {
                    parList.Add(i.Name);
                }
            }
            foreach (string par in parList)
            {
                if (GlobalSetting.IsParamRandom)
                {
                    //string newguid = Guid.NewGuid().ToString().Substring(0, 2);
                    string newguid = GetRandomStr(1);
                    while (_randomParam.ContainsValue(newguid))
                    {
                        newguid = GetRandomStr(1);
                    }
                    _randomParam.Add(par, newguid);
                    
                    item = item.Replace("$" + par + "$", newguid);
                }
                else
                {
                    item = item.Replace("$" + par + "$", par);
                }
            }
            return item;
        }


        private Dictionary<string, string> GetCode(CustomShellType customShellType,
                                                   string pass,
                                                   CustomShellType.FuncCodeSettingStruct funcCode,
                                                   string[] parmas)
        {
            DataCombine dataCombine = new DataCombine();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //MainCode
            string mainCodeString =
                FillParams(customShellType.MainCode.Item, customShellType.MainCode.FuncCodeParam);

            NameValueCollection mainCodeItem = new NameValueCollection
            {
                {pass, EncryItem(customShellType.BasicSetting.MainCodeParam.EncryMode, mainCodeString)}
            };
            AddItemToDic(dic, customShellType.BasicSetting.MainCodeParam.Location, dataCombine.CombineToStr(mainCodeItem));
            
            //FuncCode
            string funcCodeString = "";
            if (funcCode.FuncParams.Count > 0)
            {
                funcCodeString = FillParams(funcCode.Item, funcCode.FuncParams);
            }
            else
            {
                funcCodeString = funcCode.Item;
            }
            //判断是否随机参数
            string funcParamName = customShellType.MainCode.FuncCodeParam.Name;
            if (GlobalSetting.IsParamRandom)
            {
                string newguid = _randomParam[funcParamName];
                funcParamName = newguid;
            }
            NameValueCollection funcCodeItem = new NameValueCollection
            {
                {funcParamName, EncryItem(customShellType.MainCode.FuncCodeParam.EncryMode, funcCodeString)}
            };
            AddItemToDic(dic, customShellType.MainCode.FuncCodeParam.Location, dataCombine.CombineToStr(funcCodeItem));
            //FunParma
            if (parmas != null && parmas.Length > 0)
            {
                if (parmas.Length != funcCode.FuncParams.Count)
                {
                    throw new Exception("调用方法的参数个数与实现代码的参数个数不符合");
                }
                for (int i = 0; i < parmas.Length; i++)
                {
                    string parName = funcCode.FuncParams[i].Name;
                    if (GlobalSetting.IsParamRandom)
                    {
                        string newguid = _randomParam[parName];
                        parName = newguid;
                    }

                    NameValueCollection item = new NameValueCollection
                    {
                        {parName, EncryItem(funcCode.FuncParams[i].EncryMode, parmas[i])}
                    };
                    AddItemToDic(dic, funcCode.FuncParams[i].Location, dataCombine.CombineToStr(item));

                    //dataCombine.AddFuncParmaItem("z" + (i + 1), EncryItem(FuncCode.FuncParmaEncryMode, parmas[i]));                   
                }
                //AddItemToDic(dic, FuncCode.FuncParmaLocation, dataCombine.CombineToStr(dataCombine.FuncParmaItems));
            }
            return dic;
        }


        public Dictionary<string, string> GetCode(string funcCodeName, string[] parmas)
        {
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct(funcCodeName), parmas);
        }

        public Dictionary<string, string> GetWwwRootPathCode()
        {
            //string[] parmas = new string[] { dirPath };
            return GetCode(_customShellType, _coding, _customShellType.GetFuncCodeSettingStruct("WWWRootPathCode"), null);
        }
        public Dictionary<string, string> GetFileTreeCode(string dirPath)
        {
            string[] parmas = new string[] { dirPath };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("FileTreeCode"), parmas);
        }
        public Dictionary<string, string> GetReadFileCode(string filePath)
        {
            string[] parmas = new string[] { filePath };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("ReadFileCode"), parmas);
        }
        public Dictionary<string, string> GetWriteFileCode(string filePath, string fileContext)
        {
            string[] parmas = new string[] { filePath, fileContext };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("WriteFileCode"), parmas);
        }
        public Dictionary<string, string> GetDeleteFileOrDirCode(string fileOrDirPath)
        {
            string[] parmas = new string[] { fileOrDirPath };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("DeleteFileOrDirCode"), parmas);
        }
        public Dictionary<string, string> GetDownloadFileCode(string filePath)
        {
            string[] parmas = new string[] { filePath };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("DownloadFileCode"), parmas);
        }
        public Dictionary<string, string> GetUploadFileCode(string saveFilePath, string fileHexContext)
        {
            string[] parmas = new string[] { saveFilePath, fileHexContext };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("UploadFileCode"), parmas);
        }
        public Dictionary<string, string> GetCopyFileOrDirCode(string sourceFilePath, string targetFilePath)
        {
            string[] parmas = new string[] { sourceFilePath, targetFilePath };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("CopyFileOrDirCode"), parmas);
        }
        public Dictionary<string, string> GetRenameFileOrDirCode(string oldName, string newName)
        {
            string[] parmas = new string[] { oldName, newName };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("RenameFileOrDirCode"), parmas);
        }
        public Dictionary<string, string> GetCreateDirCode(string dirPath)
        {
            string[] parmas = new string[] { dirPath };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("CreateDirCode"), parmas);
        }
        public Dictionary<string, string> GetModifyFileOrDirTimeCode(string fileOrDirPath, string aTime)
        {
            string[] parmas = new string[] { fileOrDirPath, aTime };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("ModifyFileOrDirTimeCode"), parmas);
        }
        public Dictionary<string, string> GetWgetCode(string urlPath, string saveFilePath)
        {
            string[] parmas = new string[] { urlPath, saveFilePath };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("WgetCode"), parmas);
        }


        //shellcmder
        public Dictionary<string, string> GetSysInfoCode()
        {
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("SysInfoCode"), null);
        }
        public Dictionary<string, string> GetExecuteCommandCode(string cmdPath, string command)
        {
            string[] parmas = new string[] { cmdPath, command };
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCodeSettingStruct("ExecuteCommandCode"), parmas);
        }
    }
}
