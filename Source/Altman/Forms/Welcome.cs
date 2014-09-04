using System;
using Altman.Desktop.Logic;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Desktop.Forms
{
    public class Welcome : Dialog
    {
        private TextArea _textAreaWelcome;
        private CheckBox _checkBoxNoDisplay;
        private Button _noButton;
        private Button _yesButton;

        private Setting.Setting _setting;
        
        public Welcome(Setting.Setting setting)
        {
            Init();
            this._setting = setting;
        }
        void Init()
        {
            //_textAreaWelcome
            _textAreaWelcome = new TextArea()
            {
                Size = new Size(418, 277),
                Text = "欢迎您使用Altman网站管理工具，以下简称Altman工具！\r\n" +
                       "请您仔细阅读以下声明，您在使用Altman工具时，表明您对以下内容的接受：\r\n" +
                       "第一条\r\n" +
                       "Altman工具是实验性产品。软件作者针对现有的网站管理工具，对其进行了分析，研究和改进，写出了本工具。\r\n" +
                       "第二条\r\n" +
                       "Altman工具支持插件扩展，作为插件的开发者，需严格遵循本声明以及国家相关法律法规，软件作者不承担任何法律及连带责任。\r\n" +
                       "第三条\r\n" +
                       "本程序(附带插件)部分功能可能带有攻击性，仅供安全研究与教学之用！请使用者遵守国家相关法律法规！\r\n" +
                       "使用者将本程序（附带插件）做其他用途的，由使用者承担全部法律及连带责任，软件作者不承担任何法律及连带责任。\r\n" +
                       "本声明适用当地法律，使用者和软件作者一致同意服从当地法律。我们保留随时更改上述免责及其他条款的权利。\r\n",
            };

            //_checkBoxNoDisplay
            _checkBoxNoDisplay = new CheckBox()
            {
                Text = "don't display again",
            };

            //_noButton
            _noButton = new Button()
            {
                Text = "No",
            };
            _noButton.Click += delegate
            {
                if (_checkBoxNoDisplay.Checked == true)
                {
                    _setting.IsShowDisclaimer = false;

                    //保存Setting到xml
                    InitWorker.SaveSettingToXml(_setting);
                    //重新初始化GlobalSetting
                    InitWorker.InitGlobalSetting();
                }
                Environment.Exit(0);
            };

            //_yesButton
            _yesButton = new Button()
            {
                Text = "Yes",
            };
            _yesButton.Click += delegate
            {
                if (_checkBoxNoDisplay.Checked == true)
                {
                    _setting.IsShowDisclaimer = false;
                    //保存Setting到xml
                    InitWorker.SaveSettingToXml(_setting);
                    //重新初始化GlobalSetting
                    InitWorker.InitGlobalSetting();
                }
                this.Close();
            };

            var layout = new DynamicLayout();
            layout.AddRow(_textAreaWelcome);
            layout.AddSeparateRow(_checkBoxNoDisplay,null, _noButton, _yesButton);
            layout.AddRow(null);

            this.Content = layout;
            this.Size = new Size(460,370);
            this.ShowInTaskbar = true;
            this.Title = "免责声明";
        }
    }
}
