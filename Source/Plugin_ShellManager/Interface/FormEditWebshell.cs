using System;
using System.Linq;
using Altman.Plugin.Interface;
using Altman.Webshell.Model;
using Eto.Drawing;
using Eto.Forms;
using Plugin_ShellManager.Resources;

namespace Plugin_ShellManager.Interface
{
    public delegate void WebshellWatchEventHandler(object sender, EventArgs e);

    public partial class FormEditWebshell : Form
    {
        private string Id;

        public FormEditWebshell()
        {
            InitUi();

            //init
            ComboBox_ScriptType_Init();
            
            _buttonAdd.Enabled = true;
            _buttonAlter.Enabled = false;
        }

        public FormEditWebshell(Shell shellArray)
        {
            InitUi();

            //init
            ComboBox_ScriptType_Init();

            _buttonAdd.Enabled = false;
            _buttonAlter.Enabled = true;

            this.Id = shellArray.Id;
            _textBoxName.Text = shellArray.TargetId;
	        _comboBoxLevel.Text = shellArray.TargetLevel;
            _comboBoxLevel.Items.Add(shellArray.TargetLevel);
			_dropDownScritpType.SelectedKey = shellArray.ShellType;

            _textBoxShellPath.Text = shellArray.ShellUrl;
            _textBoxShellPass.Text = shellArray.ShellPwd;

            _textBoxRemark.Text = shellArray.Remark;

	        if (_dropDownServerCoding.Items.FirstOrDefault(r => r.Key == shellArray.ServerCoding) == null)
	        {
				_dropDownServerCoding.Items.Add(shellArray.ServerCoding);
	        }
			_dropDownServerCoding.SelectedKey = shellArray.ServerCoding;

	        if (_dropDownWebCoding.Items.FirstOrDefault(r => r.Key == shellArray.WebCoding) == null)
	        {
				_dropDownWebCoding.Items.Add(shellArray.WebCoding);
	        }
			_dropDownWebCoding.SelectedKey = shellArray.WebCoding;

			_richTextBoxSetting.Text = shellArray.ShellExtraString;
        }

	    private Shell GetShellConfigFromPanel()
	    {
			var shell = new Shell();

			shell.Id = this.Id;
		    shell.TargetId = _textBoxName.Text.Trim();//*
			shell.TargetLevel = _comboBoxLevel.Text;
		    shell.ShellType = _dropDownScritpType.SelectedKey ?? "";//*

			shell.ShellUrl = _textBoxShellPath.Text.Trim();//*
			shell.ShellPwd = _textBoxShellPass.Text.Trim();//*

			shell.ShellExtraString = _richTextBoxSetting.Text;
			shell.Remark = _textBoxRemark.Text;

		    shell.ServerCoding = _dropDownServerCoding.SelectedKey ?? "";//*
		    shell.WebCoding = _dropDownWebCoding.SelectedKey ?? "";//*

			var time = DateTime.Now.Date.ToShortDateString();
			if (time.Contains("/"))
			{
				time = time.Replace("/", "-");
			}
			shell.AddTime = time;
		    return shell;
	    }

	    private bool VerifyShell(Shell shell)
	    {
		    var success = true;
		    if (shell.TargetId == ""
				|| shell.ShellType == ""
				|| shell.ShellUrl == ""
				|| shell.ShellPwd == ""
				|| shell.ServerCoding == ""
				|| shell.WebCoding == "")
		    {
			    success = false;
			    MessageBox.Show(
					this,
					StrRes.GetString("StrPleaseFillOutTheProjectWith*","Please fill out the project with *"),
					MessageBoxType.Error);
		    }
			return success;
	    }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
	        var shell = GetShellConfigFromPanel();
			//验证Shell是否合法
			if (!VerifyShell(shell))
		        return;

	        ShellManager.Insert(shell);
            OnWebshellChange(EventArgs.Empty);
            Close();
        }

		private void _buttonAlter_Click(object sender, EventArgs e)
        {
			var shell = GetShellConfigFromPanel();
			//验证Shell是否合法
			if (!VerifyShell(shell))
				return;

			ShellManager.Update(int.Parse(shell.Id), shell);
            OnWebshellChange(EventArgs.Empty);
            Close();
        }

		private void _buttonAdvanced_Click(object sender, EventArgs e)
		{
			_panelAdvanced.Visible = !_panelAdvanced.Visible;
			ClientSize = new Size(ClientSize.Width, _panelAdvanced.Visible ? ClientSize.Height + 200 : 130);
		}

		private void _buttonDefault_Click(object sender, EventArgs e)
		{
			if (_dropDownScritpType.SelectedIndex != -1)
			{
				var conns = Altman.Webshell.Service.GetDbNodeInfoList(_dropDownScritpType.SelectedKey);
				_richTextBoxSetting.Text = ShellExtraStringHandle.CreateDefaultIniString(conns);
			}
			else
			{
				MessageBox.Show(
					this,
					StrRes.GetString("StrPleaseSelectScriptTypeFirst", "Please select script type first"),
					MessageBoxType.Error);
			}
		}

        public event WebshellWatchEventHandler WebshellWatchEvent;
        private void OnWebshellChange(EventArgs e)
        {
            if (WebshellWatchEvent != null)
            {
                WebshellWatchEvent(this, e);
            }
        }

        /// <summary>
        /// 初始化可选择的脚本类型
        /// </summary>
        private void ComboBox_ScriptType_Init()
        {
            //获取可用的CustomShellType
			foreach (var type in Altman.Webshell.Service.GetCustomShellTypeNameList())
            {
                _dropDownScritpType.Items.Add(type);
            }
        }
    }
}
