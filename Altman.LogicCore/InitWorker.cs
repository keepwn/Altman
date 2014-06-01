using System;
using System.Collections.Generic;
using Altman.Setting;

namespace Altman.LogicCore
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
            //1.注册CustomShellType
            foreach (string c in typeList)
            {
                var basicSetting = new CustomShellType.Basic();
                var mainCodeSetting = new CustomShellType.MainCode();

                //读取basicSetting,mainCodeSetting
                CustomShellTypeXmlHandle.ReadXml(c, CustomShellTypePath, ref basicSetting, ref mainCodeSetting);
                //生成customShellType
                var customShellType = new CustomShellType(basicSetting, mainCodeSetting);
                //将CustomShellType注册到全局
                CustomShellTypeProvider.AddShellType(customShellType);
            }

            //读取funcTree定义列表（.tree）       
            List<string> funcTreeList = XmlHelper.LoadXMlList(CustomShellTypePath, "tree");
            //2.初始化funcTree方法树
            foreach (string c in funcTreeList)
            {
                var treeInfoList = new List<CustomShellType.TreeInfo>();

                //读取funcCodeList
                CustomShellTypeXmlHandle.ReadXml(c, CustomShellTypePath, ref treeInfoList);
                //将func注册到CustomShellType
                foreach (CustomShellType.TreeInfo info in treeInfoList)
                {
                    /***
                     * 获取节点的类型
                     * 允许多个类型，以英文逗号分隔，如"aspx,aspx1"
                     */
                    string[] types = info.Type.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string type in types)
                    {
                        CustomShellType shellType = CustomShellTypeProvider.GetShellType(type);
                        FuncTreeNode node = shellType.AddFuncTreeNode(info.Path);
                        node.Info = info.Info;
                    }
                }
            }

            //读取funcCode列表（.func）
            List<string> funcList = XmlHelper.LoadXMlList(CustomShellTypePath,"func");
            //3.注册funcCode到functree
            foreach (string c in funcList)
            {
                var funcCodeList = new List<CustomShellType.FuncCode>();

                //读取funcCodeList
                CustomShellTypeXmlHandle.ReadXml(c,CustomShellTypePath, ref funcCodeList);
                //将func注册到CustomShellType
                foreach (CustomShellType.FuncCode func in funcCodeList)
                {
                    /***
                     * 获取func的类型
                     * type允许多个类型，以英文逗号分隔，如"aspx,aspx1"
                     */
                    string[] types = func.Type.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string type in types)
                    {
                        CustomShellType shellType = CustomShellTypeProvider.GetShellType(type);
                        //获取映射节点
                        //path为xpath形式，如"/cmder"，
                        //允许多个，以英文逗号分隔，如"/cmder,/cmder1"
                        string[] xpaths = func.Path.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string xpath in xpaths)
                        {
                            shellType.AddFuncCode(xpath, func);
                        }
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
