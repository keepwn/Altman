using System;
using System.Linq;
using System.Xml;
using Altman.Plugin;
using Altman.Webshell.Model;
using Eto.Drawing;
using Eto.Forms;
using Plugin_ShellManager.Resources;

namespace Plugin_ShellManager
{
    public delegate void WebshellWatchEventHandler(object sender, EventArgs e);

    public partial class FormEditWebshell : Form
    {
        private string Id;

        private IHost _host;
        private ShellManager _shellManager = null;

        public FormEditWebshell(IHost host)
        {
            InitUi();
            
            this._host = host;
            _shellManager = new ShellManager(_host);

            //init
            ComboBox_ScriptType_Init();
            
            _buttonAdd.Enabled = true;
            _buttonAlter.Enabled = false;
        }

        public FormEditWebshell(IHost host, Shell shellArray)
        {
            InitUi();

            this._host = host;
            _shellManager = new ShellManager(_host);


            //init
            ComboBox_ScriptType_Init();

            _buttonAdd.Enabled = false;
            _buttonAlter.Enabled = true;

            this.Id = shellArray.Id;
            _textBoxName.Text = shellArray.TargetId;
            _comboBoxLevel.Items.Add(shellArray.TargetLevel);
			_comboBoxScritpType.SelectedKey = shellArray.ShellType;

            _textBoxShellPath.Text = shellArray.ShellUrl;
            _textBoxShellPass.Text = shellArray.ShellPwd;

            _textBoxRemark.Text = shellArray.Remark;

	        if (_comboBoxServerCoding.Items.FirstOrDefault(r => r.Key == shellArray.ServerCoding) == null)
	        {
				_comboBoxServerCoding.Items.Add(shellArray.ServerCoding);
	        }
			_comboBoxServerCoding.SelectedKey = shellArray.ServerCoding;

	        if (_comboBoxWebCoding.Items.FirstOrDefault(r => r.Key == shellArray.WebCoding) == null)
	        {
				_comboBoxWebCoding.Items.Add(shellArray.WebCoding);
	        }
			_comboBoxWebCoding.SelectedKey = shellArray.WebCoding;

			_richTextBoxSetting.Text = shellArray.ShellExtraString;
        }
        

		void _buttonAdvanced_Click(object sender, EventArgs e)
		{
			_panelAdvanced.Visible = !_panelAdvanced.Visible;
			ClientSize = _panelAdvanced.Visible ? new Size(500, 300) : new Size(500, 130);
		}

	    private Shell GetShellConfigFromPanel()
	    {
			var shell = new Shell();

			shell.Id = this.Id;
		    shell.TargetId = _textBoxName.Text.Trim();//*
			shell.TargetLevel = _comboBoxLevel.Text;
		    shell.ShellType = _comboBoxScritpType.SelectedKey ?? "";//*

			shell.ShellUrl = _textBoxShellPath.Text.Trim();//*
			shell.ShellPwd = _textBoxShellPass.Text.Trim();//*

			shell.ShellExtraString = _richTextBoxSetting.Text;
			shell.Remark = _textBoxRemark.Text;

		    shell.ServerCoding = _comboBoxServerCoding.SelectedKey ?? "";//*
		    shell.WebCoding = _comboBoxWebCoding.SelectedKey ?? "";//*

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

            _shellManager.Insert(shell);
            OnWebshellChange(EventArgs.Empty);
            Close();
        }
		private void _buttonAlter_Click(object sender, EventArgs e)
        {
			var shell = GetShellConfigFromPanel();
			//验证Shell是否合法
			if (!VerifyShell(shell))
				return;

            _shellManager.Update(int.Parse(shell.Id), shell);
            OnWebshellChange(EventArgs.Empty);
            Close();
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
                _comboBoxScritpType.Items.Add(type);
            }
        }
    }
}
