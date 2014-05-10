using System;
using System.Collections.Generic;
using Altman.Setting;

namespace Altman.LogicCore.New
{
    public class InitWorker
    {
        private static readonly string CustomShellTypePath = Environment.CurrentDirectory + "/CustomType/";
        private static readonly string SettingXmlPath = Environment.CurrentDirectory + "/";

        /// <summary>
        /// 注册CustomShellType
        /// </summary>
        public static void RegisterCustomShellType()
        {
            //读取shelltype列表（.type）
            List<string> typeList = XmlHelper.LoadXMlList(CustomShellTypePath, "type");
            //注册CustomShellType
            foreach (string c in typeList)
            {
                var basicSetting = new CustomShellType.BasicSettingStruct();
                var mainCodeSetting = new CustomShellType.MainCodeSettingStruct();

                //读取basicSetting,mainCodeSetting
                CustomShellTypeXmlHandle.ReadXml(c, CustomShellTypePath, ref basicSetting, ref mainCodeSetting);
                //生成customShellType
                var customShellType = new CustomShellType(basicSetting, mainCodeSetting);
                //将CustomShellType注册到全局
                CustomShellTypeProvider.AddShellType(customShellType);
            }

            //读取func列表（.func）
            List<string> funcList = XmlHelper.LoadXMlList(CustomShellTypePath,"func");
            //注册func
            foreach (string c in funcList)
            {
                var funcCodeList = new List<CustomShellType.FuncCodeSettingStruct>();

                //读取funcCodeList
                CustomShellTypeXmlHandle.ReadXml(c,CustomShellTypePath, ref funcCodeList);
                //将func注册到CustomShellType
                foreach (CustomShellType.FuncCodeSettingStruct func in funcCodeList)
                {
                    //获取func的类型，允许多类型，以英文逗号分隔，如"aspx1,aspx2"
                    string[] types = func.Type.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string type in types)
                    {
                        CustomShellTypeProvider.AddFuncCode(type, func);
                    }
                }           
            }
        }

        public static void InitGlobalSetting()
        {
            string path = SettingXmlPath;
            Setting.Setting setting = null;
            //读取setting.xml列表（.xml）
            List<string> settingList = XmlHelper.LoadXMlList(path, "xml");
            foreach (string i in settingList)
            {
                if (i.ToLower() == "setting.xml")
                {
                    setting = SettingXmlHandle.ReadXml(i, path);
                    break;
                }
            }
            //init GlobalSetting
            GlobalSetting.Clear();
            GlobalSetting.UserAgent = setting.UserAgent;
            GlobalSetting.HttpHeader = setting.HttpHeader;
            GlobalSetting.IsParamRandom = setting.IsIsParamRandom;
            GlobalSetting.Proxy = setting.Proxy;
            GlobalSetting.Setting = setting;
        }

        public static void SaveSettingToXml(Setting.Setting setting)
        {
            SettingXmlHandle.WriteXml("setting.xml", SettingXmlPath, setting);
        }
    }
}
