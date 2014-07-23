using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Altman.Common;
using Altman.Common.AltData;
using Altman.Setting;

namespace Altman.Logic
{
    internal class CustomCommandCode
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
        private string EncryItem(EncryMode mode, string item)
        {
            if (mode == EncryMode.None)
            {
                return item;
            }
            else if (mode == EncryMode.Base64)
            {
                return DataConvert.StrToBase64(item, 1);
            }
            else if (mode == EncryMode.Hex)
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
                    //新的uid不能等于pass且，不与已经产生的uid相同
                    while (newguid == _pass || _randomParam.ContainsValue(newguid))
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
                                                   CustomShellType.FuncCode funcCode,
                                                   string[] parmas)
        {
            DataCombine dataCombine = new DataCombine();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //MainCodeSetting
            string mainCodeString =
                FillParams(customShellType.MainCodeSetting.Item, customShellType.MainCodeSetting.FuncCodeParam);

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
            string funcParamName = customShellType.MainCodeSetting.FuncCodeParam.Name;
            if (GlobalSetting.IsParamRandom)
            {
                string newguid = _randomParam[funcParamName];
                funcParamName = newguid;
            }
            NameValueCollection funcCodeItem = new NameValueCollection
            {
                {funcParamName, EncryItem(customShellType.MainCodeSetting.FuncCodeParam.EncryMode, funcCodeString)}
            };
            AddItemToDic(dic, customShellType.MainCodeSetting.FuncCodeParam.Location, dataCombine.CombineToStr(funcCodeItem));
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


        public Dictionary<string, string> GetCode(string funcCodeNameXpath, string[] parmas)
        {
            //分解funcCodeNameXpath，将"cmder/exec"分解为cmder和exec
            List<string> list = new List<string>(funcCodeNameXpath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
            string funcCodeName = list[list.Count-1];

            list.RemoveAt(list.Count-1);
            string path = string.Join("/", list);
            return GetCode(_customShellType, _pass, _customShellType.GetFuncCode(path, funcCodeName), parmas);
        }
    }
}
