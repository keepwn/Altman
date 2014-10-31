using System.Collections.Generic;
using Altman.Util.Setting;

namespace Altman.Util.Logic
{
    public class InitWorker
    {
		//private static readonly string CustomShellTypePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/CustomType/";
		//private static readonly string SettingXmlPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/";

		public static void InitGlobalSetting(string settingXmlPath)
        {
			string path = settingXmlPath;
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
            GlobalSetting.IsParamRandom = setting.PolicySetting.IsParamRandom;
            GlobalSetting.Proxy = setting.Proxy;
            GlobalSetting.Setting = setting;
        }

        public static void SaveSettingToXml(string settingXmlPath, Setting.Setting setting)
        {
			SettingXmlHandle.WriteXml("setting.xml", settingXmlPath, setting);
        }
    }
}
