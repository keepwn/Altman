using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Altman.Logic
{
    internal class CustomShellTypeXmlHandle
    {
        /// <summary>
        /// 写xml文件
        /// </summary>
        public static void WriteXml(string shellTypeName,
                                    string basePathDir,
                                    CustomShellType.Basic basicSetting,
                                    CustomShellType.MainCode mainCode,
                                    List<CustomShellType.FuncCode> funcCodeList)
        {
            XmlTextWriter xmlWriter = null;
            string filename = string.Format(basePathDir + "{0}", shellTypeName);
            try
            {
                xmlWriter = new XmlTextWriter(filename, Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement("customShellType");
                {
                    //BasicSetting
                    xmlWriter.WriteStartElement("basicSetting");
                    {
                        xmlWriter.WriteStartElement("name");
                        xmlWriter.WriteString(shellTypeName.Substring(0, shellTypeName.Length - 4));
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("serviceExample");
                        xmlWriter.WriteCData(basicSetting.ServiceExample);
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("mainCodeParam");
                        {
                            xmlWriter.WriteStartAttribute("location");
                            xmlWriter.WriteString(basicSetting.MainCodeParam.Location);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute("encrymode");
                            xmlWriter.WriteString(basicSetting.MainCodeParam.EncryMode.ToString());
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteString(basicSetting.MainCodeParam.Name);
                        }
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();

                    //MainCodeSetting
                    xmlWriter.WriteStartElement("mainCodeSetting");
                    {
                        xmlWriter.WriteStartElement("funcCodeParam");
                        {
                            xmlWriter.WriteStartAttribute("location");
                            xmlWriter.WriteString(mainCode.FuncCodeParam.Location);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute("encrymode");
                            xmlWriter.WriteString(mainCode.FuncCodeParam.EncryMode.ToString());
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteString(mainCode.FuncCodeParam.Name);
                        }
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("item");
                        xmlWriter.WriteCData(mainCode.Item);
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();

                    //FuncCodeSetting
                    xmlWriter.WriteStartElement("funcCodeSetting");
                    {
                        foreach (CustomShellType.FuncCode func in funcCodeList)
                        {
                            xmlWriter.WriteStartElement("func");
                            {
                                xmlWriter.WriteStartElement("name");
                                xmlWriter.WriteString(func.Name);
                                xmlWriter.WriteEndElement();

                                foreach (CustomShellType.ParamStruct par in func.FuncParams)
                                {
                                    xmlWriter.WriteStartElement("funcParameter");
                                    {
                                        xmlWriter.WriteStartAttribute("location");
                                        xmlWriter.WriteString(par.Location);
                                        xmlWriter.WriteEndAttribute();

                                        xmlWriter.WriteStartAttribute("encrymode");
                                        xmlWriter.WriteString(par.EncryMode.ToString());
                                        xmlWriter.WriteEndAttribute();

                                        xmlWriter.WriteString(par.Name);
                                    }
                                    xmlWriter.WriteEndElement();
                                }

                                xmlWriter.WriteStartElement("item");
                                xmlWriter.WriteCData(func.Item);
                                xmlWriter.WriteEndElement();
                            }
                            xmlWriter.WriteEndElement();
                        }
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
        public static void ReadXml(string fileName,
                                   string basePathDir,
                                   ref CustomShellType.Basic basicSetting,
                                   ref CustomShellType.MainCode mainCode,
                                   ref List<CustomShellType.FuncCode> funcCodeList)
        {
            string filePath = basePathDir + fileName;
            try
            {
                XmlDocument xml = new XmlDocument(); //初始化一个xml实例
                xml.Load(filePath); //导入指定xml文件
                XmlNode root = xml.SelectSingleNode("/customShellType"); //指定一个节点
                XmlNodeList childlist = root.ChildNodes; //获取节点下所有直接子节点
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "basicSetting")
                    {
                        basicSetting = ReadBasicSettingNode(child);
                    }
                    else if (child.Name == "mainCodeSetting")
                    {
                        mainCode = ReadMainCodeSettingNode(child);
                    }
                    else if (child.Name == "funcCodeSetting")
                    {
                        funcCodeList = ReadFuncCodeSettingNode(child);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void ReadXml(string fileName,
                                   string basePathDir,
                                   ref CustomShellType.Basic basicSetting,
                                   ref CustomShellType.MainCode mainCode)
        {
            string filePath = basePathDir + fileName;
            try
            {
                XmlDocument xml = new XmlDocument(); //初始化一个xml实例
                xml.Load(filePath); //导入指定xml文件
                XmlNode root = xml.SelectSingleNode("/customShellType"); //指定一个节点
                XmlNodeList childlist = root.ChildNodes; //获取节点下所有直接子节点
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "basicSetting")
                    {
                        basicSetting = ReadBasicSettingNode(child);
                    }
                    else if (child.Name == "mainCodeSetting")
                    {
                        mainCode = ReadMainCodeSettingNode(child);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void ReadXml(string fileName,
                           string basePathDir,
                           ref List<CustomShellType.FuncCode> funcCodeList)
        {
            string filePath = basePathDir + fileName;
            try
            {
                XmlDocument xml = new XmlDocument(); //初始化一个xml实例
                xml.Load(filePath); //导入指定xml文件
                XmlNode root = xml.SelectSingleNode("/customShellType"); //指定一个节点
                XmlNodeList childlist = root.ChildNodes; //获取节点下所有直接子节点
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "funcCodeSetting")
                    {
                        funcCodeList = ReadFuncCodeSettingNode(child);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void ReadXml(string fileName,
                   string basePathDir,
                   ref List<CustomShellType.TreeInfo> infoList)
        {
            string filePath = basePathDir + fileName;
            try
            {
                XmlDocument xml = new XmlDocument(); //初始化一个xml实例
                xml.Load(filePath); //导入指定xml文件
                XmlNode root = xml.SelectSingleNode("/customShellType"); //指定一个节点
                XmlNodeList childlist = root.ChildNodes; //获取节点下所有直接子节点
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "funcTreeInfo")
                    {
                        infoList = ReadTreeInfoNode(child);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static CustomShellType.Basic ReadBasicSettingNode(XmlNode child)
        {
            CustomShellType.Basic basicSetting = new CustomShellType.Basic();
            foreach (XmlNode c in child.ChildNodes)
            {
                if (c.Name == "name")
                {
                    basicSetting.ShellTypeName = c.InnerText;
                }
                else if (c.Name == "serviceExample")
                {
                    basicSetting.ServiceExample = c.InnerText;
                }
                else if (c.Name == "mainCodeParam")
                {
                    basicSetting.MainCodeParam = ReadParamNode(c);
                }
            }
            return basicSetting;
        }
        private static CustomShellType.MainCode ReadMainCodeSettingNode(XmlNode child)
        {
            CustomShellType.MainCode mainCode = new CustomShellType.MainCode();
            foreach (XmlNode c in child.ChildNodes)
            {
                if (c.Name == "item")
                {
                    mainCode.Item = c.InnerText;
                }
                else if (c.Name == "funcCodeParam")
                {
                    mainCode.FuncCodeParam = ReadParamNode(c);
                }
            }
            return mainCode;
        }
        private static List<CustomShellType.FuncCode> ReadFuncCodeSettingNode(XmlNode child)
        {
            List<CustomShellType.FuncCode> funcCodeList = new List<CustomShellType.FuncCode>();
            foreach (XmlNode c in child.ChildNodes)
            {
                if (c.Name == "func")
                {
                    CustomShellType.FuncCode funcCode = new CustomShellType.FuncCode();
                    //初始化一下Parmas
                    funcCode.FuncParams = new List<CustomShellType.ParamStruct>();
                    foreach (XmlNode cc in c.ChildNodes)
                    {
                        if (cc.Name == "name")
                        {
                            funcCode.Name = cc.InnerText;
                        }
                        else if (cc.Name == "type")
                        {
                            funcCode.Type = cc.InnerText;
                        }
                        else if (cc.Name == "path")
                        {
                            funcCode.Path = cc.InnerText;
                        }
                        else if (cc.Name == "item")
                        {
                            funcCode.Item = cc.InnerText;
                        }
                        else if (cc.Name == "funcParam")
                        {                       
                            funcCode.FuncParams.Add(ReadParamNode(cc));
                        }
                    }
                    funcCodeList.Add(funcCode);
                }
            }
            return funcCodeList;
        }
        private static List<CustomShellType.TreeInfo> ReadTreeInfoNode(XmlNode child)
        {
            var infoList = new List<CustomShellType.TreeInfo>();
            foreach (XmlNode c in child.ChildNodes)
            {
                if (c.Name == "node")
                {
                    var info = new CustomShellType.TreeInfo();
                    foreach (XmlNode cc in c.ChildNodes)
                    {
                        if (cc.Name == "path")
                        {
                            info.Path = cc.InnerText;
                        }
                        else if (cc.Name == "type")
                        {
                            info.Type = cc.InnerText;
                        }
                        else if (cc.Name == "info")
                        {
                            info.Info = cc.InnerText;
                        }
                    }
                    infoList.Add(info);
                }
            }
            return infoList;
        }
        private static CustomShellType.ParamStruct ReadParamNode(XmlNode child)
        {
            CustomShellType.ParamStruct par=new CustomShellType.ParamStruct();
            par.Name = child.InnerText;
            par.Location = child.Attributes["location"].Value;
            string encrymode = child.Attributes["encrymode"].Value;
            par.EncryMode = (EncryMode)Enum.Parse(typeof(EncryMode), encrymode);
            return par;
        }
    }
}
