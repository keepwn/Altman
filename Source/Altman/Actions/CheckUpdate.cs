using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;
using Altman.Resources;
using Eto.Forms;

namespace Altman.Actions
{
    public class CheckUpdate : Command
    {
        public CheckUpdate()
        {
            MenuText = AltStrRes.CheckUpdate;
            Shortcut = Keys.U | Application.Instance.CommonModifier;
            Executed += CheckUpdate_Executed;
        }

        private void CheckUpdate_Executed(object sender, EventArgs e)
        {
            Check();
        }

        public void Check()
        {
            var updateUrl = "http://altman.keepwn.com/update.xml";
            try
            {
                var newVerson = "";
                var downloadUrl = "";

                var client = new WebClient();
                var stream = client.OpenRead(updateUrl);
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);

                var list = xmlDoc.SelectSingleNode("update");
                foreach (XmlNode node in list)
                {
                    if (node.Name == "soft" && node.Attributes["name"].Value.ToLower() == "altman")
                    {
                        foreach (XmlNode xml in node)
                        {
                            switch (xml.Name)
                            {
                                case "version":
                                    newVerson = xml.Attributes["number"].Value;
                                    break;
                                case "download":
                                    downloadUrl = xml.InnerText;
                                    break;
                            }
                        }
                    }
                }

                var ver = new Version(newVerson);
                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                if (currentVersion.CompareTo(ver) < 0)
                {
                    var msg = string.Format("Found new version: {0}, do you want to download now ?", newVerson);
                    var result = MessageBox.Show(Application.Instance.MainForm, msg, MessageBoxButtons.YesNo, MessageBoxType.Question);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(downloadUrl);
                    }
                }
                else
                {
                    var msg = "Not found new version, the current version is the latest.";
                    MessageBox.Show(Application.Instance.MainForm, msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.Instance.MainForm, "Couldn't download update xml, please try again later.",
                    MessageBoxType.Error);
            }
        }
    }
}
