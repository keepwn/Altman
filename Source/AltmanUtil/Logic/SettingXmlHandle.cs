using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Altman.Common.AltData;

namespace Altman.Logic
{
    internal class SettingXmlHandle
    {
        public static void WriteXml(string fileName, string basePathDir, Setting.Setting setting)
        {
            XmlTextWriter xmlWriter = null;
            string filename = string.Format(basePathDir + "{0}", fileName);
            try
            {
                xmlWriter = new XmlTextWriter(filename, Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement("AltmanSetting");
                {
                    //UserAgent
                    xmlWriter.WriteStartElement("UserAgent");
                    {
                        xmlWriter.WriteStartElement("type");
                        xmlWriter.WriteString(Convert.ToInt32(setting.GetUserAgentStruct.IsRandom).ToString());
                        xmlWriter.WriteEndElement();

                        foreach (var i in setting.GetUserAgentStruct.UserAgentList)
                        {
                            xmlWriter.WriteStartElement("option");
                            {
                                //添加name属性
                                xmlWriter.WriteStartAttribute("name");
                                xmlWriter.WriteString(i.Key);
                                xmlWriter.WriteEndAttribute();

                                //如果name和selected中保存的一致，则添加selected属性
                                if (i.Key == setting.GetUserAgentStruct.Selected.Key)
                                {
                                    xmlWriter.WriteStartAttribute("selected");
                                    xmlWriter.WriteString("True");
                                    xmlWriter.WriteEndAttribute();
                                }
                                //添加value
                                xmlWriter.WriteString(i.Value);
                            }
                            xmlWriter.WriteEndElement();
                        }
                    }
                    xmlWriter.WriteEndElement();

                    //HttpHeader
                    xmlWriter.WriteStartElement("HttpHeader");
                    {
                        foreach (var i in setting.GetHttpHeaderStruct.HttpHeaderList)
                        {
                            xmlWriter.WriteStartElement("key");
                            {
                                //添加name属性
                                xmlWriter.WriteStartAttribute("name");
                                xmlWriter.WriteString(i.Key);
                                xmlWriter.WriteEndAttribute();

                                //添加value
                                xmlWriter.WriteString(i.Value);
                            }
                            xmlWriter.WriteEndElement();
                        }
                    }
                    xmlWriter.WriteEndElement();

                    //Policy
                    xmlWriter.WriteStartElement("Policy");
                    {
                        xmlWriter.WriteStartElement("isParamRandom");
                        xmlWriter.WriteString(Convert.ToInt32(setting.GetPolicyStruct.IsParamRandom).ToString());
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("isShowDisclaimer");
                        xmlWriter.WriteString(Convert.ToInt32(setting.GetPolicyStruct.IsShowDisclaimer).ToString());
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();

                    //Proxy
                    xmlWriter.WriteStartElement("Proxy");
                    {
                        xmlWriter.WriteStartElement("type");
                        xmlWriter.WriteString(setting.GetProxyStruct.IsNoOrIeOrCustomProxy.ToString());
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("customProxy");
                        {
                            //添加address属性
                            xmlWriter.WriteStartAttribute("address");
                            xmlWriter.WriteString(setting.GetProxyStruct.ProxyAddr);
                            xmlWriter.WriteEndAttribute();

                            //添加port属性
                            xmlWriter.WriteStartAttribute("port");
                            xmlWriter.WriteString(setting.GetProxyStruct.ProxyPort);
                            xmlWriter.WriteEndAttribute();

                            //添加user属性
                            xmlWriter.WriteStartAttribute("user");
                            xmlWriter.WriteString(setting.GetProxyStruct.ProxyUser);
                            xmlWriter.WriteEndAttribute();

                            //添加password属性
                            xmlWriter.WriteStartAttribute("password");
                            xmlWriter.WriteString(setting.GetProxyStruct.ProxyPassword);
                            xmlWriter.WriteEndAttribute();

                            //添加domain属性
                            xmlWriter.WriteStartAttribute("domain");
                            xmlWriter.WriteString(setting.GetProxyStruct.ProxyDomain);
                            xmlWriter.WriteEndAttribute();

                        }                
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                xmlWriter.Close();
            }
        }


        /// <summary>
        /// 读xml文件
        /// </summary>
        public static Setting.Setting ReadXml(string fileName, string basePathDir)
        {
            string filePath = basePathDir + fileName;
            Setting.Setting.UserAgentStruct userAgent=new Setting.Setting.UserAgentStruct();
            Setting.Setting.HttpHeaderStruct httpHeader=new Setting.Setting.HttpHeaderStruct();
            Setting.Setting.PolicyStruct policy=new Setting.Setting.PolicyStruct();
            Setting.Setting.ProxyStruct proxy=new Setting.Setting.ProxyStruct();
            try
            {
                //Setting setting = new Setting();
                XmlDocument xml = new XmlDocument(); //初始化一个xml实例
                xml.Load(filePath); //导入指定xml文件
                XmlNode root = xml.SelectSingleNode("/AltmanSetting"); //指定一个节点
                XmlNodeList childlist = root.ChildNodes; //获取节点下所有直接子节点
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "UserAgent")
                    {
                        userAgent = ReadUserAgentNode(child);
                    }
                    else if (child.Name == "HttpHeader")
                    {
                        httpHeader = ReadHttpHeaderNode(child);
                    }
                    else if (child.Name == "Policy")
                    {
                        policy = ReadPolicyNode(child);
                    }
                    else if (child.Name == "Proxy")
                    {
                        proxy = ReadProxyNode(child);
                    }
                }
                Setting.Setting setting = new Setting.Setting(userAgent, httpHeader,policy,proxy);
                return setting;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private static Setting.Setting.UserAgentStruct ReadUserAgentNode(XmlNode child)
        {
            //初始化一下userAgent
            Setting.Setting.UserAgentStruct userAgent = new Setting.Setting.UserAgentStruct();
            userAgent.Selected = new KeyValuePair<string, string>();
            userAgent.UserAgentList = new Dictionary<string, string>();
            foreach (XmlNode c in child.ChildNodes)
            {
                if (c.Name == "type")
                {
                    userAgent.IsRandom = DataConvert.StrToBool(c.InnerText);
                }
                else if (c.Name == "option")
                {
                    //判断name属性是否存在
                    if(c.Attributes["name"]==null) continue;
                    string name = c.Attributes["name"].Value;
                    string value = c.InnerText;

                    if (!string.IsNullOrWhiteSpace(name) && !userAgent.UserAgentList.ContainsKey(name))
                    {
                        userAgent.UserAgentList.Add(name, value);
                        //判断selected属性是否存在
                        if (c.Attributes["selected"] == null) continue;
                        if (DataConvert.StrToBool(c.Attributes["selected"].Value))
                        {
                            userAgent.Selected = new KeyValuePair<string, string>(name, value);
                        }
                    }
                }
            }
            return userAgent;
        }
        private static Setting.Setting.HttpHeaderStruct ReadHttpHeaderNode(XmlNode child)
        {
            Setting.Setting.HttpHeaderStruct httpHeader = new Setting.Setting.HttpHeaderStruct();
            httpHeader.HttpHeaderList = new Dictionary<string, string>();
            foreach (XmlNode c in child.ChildNodes)
            {
                if (c.Name == "key")
                {
                    //判断name属性是否存在
                    if (c.Attributes["name"] == null) continue;
                    string name = c.Attributes["name"].Value;
                    string value = c.InnerText;

                    if (!string.IsNullOrWhiteSpace(name) && !httpHeader.HttpHeaderList.ContainsKey(name))
                    {
                        httpHeader.HttpHeaderList.Add(name, value);
                    }
                }
            }
            return httpHeader;
        }
        private static Setting.Setting.PolicyStruct ReadPolicyNode(XmlNode child)
        {
            Setting.Setting.PolicyStruct policy = new Setting.Setting.PolicyStruct();
            foreach (XmlNode c in child.ChildNodes)
            {
                if (c.Name == "isParamRandom")
                {
                    policy.IsParamRandom = DataConvert.StrToBool(c.InnerText);
                }
                if (c.Name == "isShowDisclaimer")
                {
                    policy.IsShowDisclaimer = DataConvert.StrToBool(c.InnerText);
                }
            }
            return policy;
        }
        private static Setting.Setting.ProxyStruct ReadProxyNode(XmlNode child)
        {
            Setting.Setting.ProxyStruct proxy = new Setting.Setting.ProxyStruct();
            foreach (XmlNode c in child.ChildNodes)
            {
                if (c.Name == "type")
                {
                    int tmp;
                    string type = c.InnerText;
                    switch (type)
                    {
                        case "1":
                            tmp = 1;
                            break;
                        case "2":
                            tmp = 2;
                            break;
                        default:
                            tmp = 0;
                            break;
                    }
                    proxy.IsNoOrIeOrCustomProxy = tmp;
                }
                else if (c.Name == "customProxy")
                {
                    //判断属性是否存在
                    if (c.Attributes["address"] == null
                        ||c.Attributes["port"] == null
                        ||c.Attributes["user"] == null
                        ||c.Attributes["password"] == null
                        ||c.Attributes["domain"] == null) continue;

                    string address = c.Attributes["address"].Value;
                    string port = c.Attributes["port"].Value;
                    string user = c.Attributes["user"].Value;
                    string password = c.Attributes["password"].Value;
                    string domain = c.Attributes["domain"].Value;

                    proxy.ProxyAddr = address;
                    proxy.ProxyPort = port;
                    proxy.ProxyUser = user;
                    proxy.ProxyPassword = password;
                    proxy.ProxyDomain = domain;
                }
            }
            return proxy;
        }
    }
}
