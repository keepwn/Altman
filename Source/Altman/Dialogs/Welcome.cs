using System;
using Altman.Desktop.CustomControls;
using Altman.Desktop.Resources;
using Altman.Logic;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Desktop.Dialogs
{
	public class Welcome : Dialog
    {
        private TextArea _textAreaWelcome;
        private CheckBox _checkBoxNoDisplay;
        private Button _buttonNo;
        private Button _buttonYes;

        private Setting.Setting _setting;
        
        public Welcome(Setting.Setting setting)
        {
            Init();
            this._setting = setting;
        }
        void Init()
        {
            //_textAreaWelcome
	        _textAreaWelcome = new TextArea() {Size = new Size(418, 277), Text = AltStrRes.Disclaimer};

            //_checkBoxNoDisplay
			_checkBoxNoDisplay = new CheckBox() { Text = AltStrRes.DontDisplayAgain};

            //_buttonNo
			_buttonNo = new Button() { Text = AltStrRes.No};
            _buttonNo.Click += delegate
            {
                if (_checkBoxNoDisplay.Checked == true)
                {
                    _setting.IsShowDisclaimer = false;

                    //保存Setting到xml
					InitWorker.SaveSettingToXml(AppEnvironment.AppPath, _setting);
                    //重新初始化GlobalSetting
					InitWorker.InitGlobalSetting(AppEnvironment.AppPath);
                }
				//Application.Instance.Quit();
				Environment.Exit(0);
            };

            //_buttonYes
	        _buttonYes = new Button() {Text = AltStrRes.Yes};
            _buttonYes.Click += delegate
            {
                if (_checkBoxNoDisplay.Checked == true)
                {
                    _setting.IsShowDisclaimer = false;
                    //保存Setting到xml
					InitWorker.SaveSettingToXml(AppEnvironment.AppPath, _setting);
                    //重新初始化GlobalSetting
					InitWorker.InitGlobalSetting(AppEnvironment.AppPath);
                }
                Close();
            };

            var layout = new DynamicLayout();
            layout.AddRow(_textAreaWelcome);
            layout.AddSeparateRow(_checkBoxNoDisplay,null, _buttonNo, _buttonYes);
            layout.AddRow(null);

            Content = layout;
            Size = new Size(460,370);
			Icon = Icons.AltmanIcon;
			ShowInTaskbar = true;
            Title = AltStrRes.Welcome;
        }
    }
}
