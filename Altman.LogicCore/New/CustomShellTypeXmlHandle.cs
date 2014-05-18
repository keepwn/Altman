using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Altman.LogicCore.New
{
    public class CustomShellTypeXmlHandle
    {
        /// <summary>
        /// 写xml文件
        /// </summary>
        public static void WriteXml(string shellTypeName,
                                    string basePathDir,
                                    CustomShellType.BasicSettingStruct basicSetting,
                                    CustomShellType.MainCodeSettingStruct mainCode,
                                    List<CustomShellType.FuncCodeSettingStruct> funcCodeList)
        {
            XmlTextWriter xmlWriter = null;
            string filename = string.Format(basePathDir + "{0}", shellTypeName);
            try
            {
                xmlWriter = new XmlTextWriter(filename, Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement("CustomShellType");
                {
                    //BasicSetting
                    xmlWriter.WriteStartElement("BasicSetting");
                    {
                        xmlWriter.WriteStartElement("name");
                        xmlWriter.WriteString(shellTypeName.Substring(0, shellTypeName.Length - 4));
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("serviceExample");
                        xmlWriter.WriteCData(basicSetting.ServiceExample);
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("MainCodeParam");
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
                    xmlWriter.WriteStartElement("MainCodeSetting");
                    {
                        xmlWriter.WriteStartElement("FuncCodeParam");
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
                    xmlWriter.WriteStartElement("FuncCodeSetting");
                    {
                        foreach (CustomShellType.FuncCodeSettingStruct func in funcCodeList)
                        {
                            xmlWriter.WriteStartElement("Func");
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
                                   ref CustomShellType.BasicSettingStruct basicSetting,
                                   ref CustomShellType.MainCodeSettingStruct mainCode,
                                   ref List<CustomShellType.FuncCodeSettingStruct> funcCodeList)
        {
            string filePath = basePathDir + fileName;
            try
            {
                XmlDocument xml = new XmlDocument(); //初始化一个xml实例
                xml.Load(filePath); //导入指定xml文件
                XmlNode root = xml.SelectSingleNode("/CustomShellType"); //指定一个节点
                XmlNodeList childlist = root.ChildNodes; //获取节点下所有直接子节点
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "BasicSetting")
                    {
                        basicSetting = ReadBasicSettingNode(child);
                    }
                    else if (child.Name == "MainCodeSetting")
                    {
                        mainCode = ReadMainCodeSettingNode(child);
                    }
                    else if (child.Name == "FuncCodeSetting")
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
                                   ref CustomShellType.BasicSettingStruct basicSetting,
                                   ref CustomShellType.MainCodeSettingStruct mainCode)
        {
            string filePath = basePathDir + fileName;
            try
            {
                XmlDocument xml = new XmlDocument(); //初始化一个xml实例
                xml.Load(filePath); //导入指定xml文件
                XmlNode root = xml.SelectSingleNode("/CustomShellType"); //指定一个节点
                XmlNodeList childlist = root.ChildNodes; //获取节点下所有直接子节点
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "BasicSetting")
                    {
                        basicSetting = ReadBasicSettingNode(child);
                    }
                    else if (child.Name == "MainCodeSetting")
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
                           ref List<CustomShellType.FuncCodeSettingStruct> funcCodeList)
        {
            string filePath = basePathDir + fileName;
            try
            {
                XmlDocument xml = new XmlDocument(); //初始化一个xml实例
                xml.Load(filePath); //导入指定xml文件
                XmlNode root = xml.SelectSingleNode("/CustomShellType"); //指定一个节点
                XmlNodeList childlist = root.ChildNodes; //获取节点下所有直接子节点
                foreach (XmlNode child in childlist)
                {
                    if (child.Name == "FuncCodeSetting")
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

        private static CustomShellType.BasicSettingStruct ReadBasicSettingNode(XmlNode child)
        {
            CustomShellType.BasicSettingStruct basicSetting = new CustomShellType.BasicSettingStruct();
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
        private static CustomShellType.MainCodeSettingStruct ReadMainCodeSettingNode(XmlNode child)
        {
            CustomShellType.MainCodeSettingStruct mainCode = new CustomShellType.MainCodeSettingStruct();
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
        private static List<CustomShellType.FuncCodeSettingStruct> ReadFuncCodeSettingNode(XmlNode child)
        {
            List<CustomShellType.FuncCodeSettingStruct> funcCodeList = new List<CustomShellType.FuncCodeSettingStruct>();
            foreach (XmlNode c in child.ChildNodes)
            {
                if (c.Name == "Func")
                {
                    CustomShellType.FuncCodeSettingStruct funcCode = new CustomShellType.FuncCodeSettingStruct();
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
        private static CustomShellType.ParamStruct ReadParamNode(XmlNode child)
        {
            CustomShellType.ParamStruct par=new CustomShellType.ParamStruct();
            par.Name = child.InnerText;
            par.Location = child.Attributes["location"].Value;
            string encrymode = child.Attributes["encrymode"].Value;
            par.EncryMode = (CustomShellType.EncryMode)Enum.Parse(typeof(CustomShellType.EncryMode), encrymode);
            return par;
        }
    }
}
