using System;
using System.Linq;
using System.Xml;
using Altman.Model;
using Eto.Drawing;
using Eto.Forms;
using PluginFramework;

namespace Plugin_ShellManager
{
    public delegate void WebshellWatchEventHandler(object sender, EventArgs e);

    public partial class FormEditWebshell : Form
    {
        private string Id;

        private IHost _host;
        private ShellManagerService _shellManagerService = null;

        public FormEditWebshell(IHost host)
        {
            Init();
            
            this._host = host;
            _shellManagerService = new ShellManagerService(_host);

            //init
            ComboBox_ScriptType_Init();
            
            _buttonAdd.Enabled = true;
            _buttonAlter.Enabled = false;

            comboBox_IniType_Init();
        }

        public FormEditWebshell(IHost host, Shell shellArray)
        {
            Init();

            this._host = host;
            _shellManagerService = new ShellManagerService(_host);


            //init
            ComboBox_ScriptType_Init();
			comboBox_IniType_Init();

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
			//_p12.Panel2.Visible = !_p12.Panel2.Visible;
			_p12.Panel2.Visible = true;
			_p12.Panel1.Visible = false;
		}

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var shell = new Shell();

            shell.Id = this.Id;
            shell.TargetId = _textBoxName.Text.Trim();
            shell.TargetLevel = _comboBoxLevel.SelectedKey.Trim();

            shell.ShellUrl = _textBoxShellPath.Text.Trim();
            shell.ShellPwd = _textBoxShellPass.Text.Trim();

            shell.ShellExtraString = _richTextBoxSetting.Text.Trim();
            shell.Remark = _textBoxRemark.Text.Trim();

            shell.ShellType = _comboBoxScritpType.SelectedKey.Trim();
            shell.ServerCoding = _comboBoxServerCoding.SelectedKey.Trim();
            shell.WebCoding = _comboBoxWebCoding.SelectedKey.Trim();

            string time = DateTime.Now.Date.ToShortDateString();
            if (time.Contains("/"))
            {
                time = time.Replace("/", "-");
            }
            shell.AddTime = time;

            //验证ExtraSettingXml是否合法
            if (!VerifyXml(shell.ShellExtraString))
                return;

            _shellManagerService.Insert(shell);
            OnWebshellChange(EventArgs.Empty);
            Close();
        }
		private void _buttonAlter_Click(object sender, EventArgs e)
        {
            var shell = new Shell();

            shell.Id = this.Id;
            shell.TargetId = _textBoxName.Text.Trim();
            shell.TargetLevel = _comboBoxLevel.SelectedKey.Trim();

            shell.ShellUrl = _textBoxShellPath.Text.Trim();
            shell.ShellPwd = _textBoxShellPass.Text.Trim();

            shell.ShellExtraString = _richTextBoxSetting.Text.Trim();
            shell.Remark = _textBoxRemark.Text.Trim();

            shell.ShellType = _comboBoxScritpType.SelectedKey.Trim();
            shell.ServerCoding = _comboBoxServerCoding.SelectedKey.Trim();
            shell.WebCoding = _comboBoxWebCoding.SelectedKey.Trim();

            string time = DateTime.Now.Date.ToShortDateString();
            if (time.Contains("/"))
            {
                time = time.Replace("/", "-");
            }
            shell.AddTime = time;

            //验证ExtraSettingXml是否合法
            if (!VerifyXml(shell.ShellExtraString))
                return;

            _shellManagerService.Update(int.Parse(shell.Id), shell);
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

        private bool VerifyXml(string xml)
        {
            bool isVaild = false;
            try
            {
                XmlNode tmp = new XmlDocument().CreateElement("Root");
                tmp.InnerXml = xml;
                isVaild = true;
            }
            catch
            {
                isVaild = false;
                MessageBox.Show("Sorry,the ini is not legal xml.Please edit the ini.");
            }
            return isVaild;
        }

        /// <summary>
        /// 初始化可选择的脚本类型
        /// </summary>
        private void ComboBox_ScriptType_Init()
        {
            //获取可用的CustomShellType
            foreach (string type in _host.Core.GetCustomShellTypeNameList())
            {
                _comboBoxScritpType.Items.Add(type);
            }
        }
        /// <summary>
        /// 初始化可选择的配置类型
        /// </summary>
        private void comboBox_IniType_Init()
        {
            string[] types = { "DbConnStr", "PostData"};
            //comboBox_IniType.Items.AddRange(types);
            //comboBox_IniType.SelectedIndex = 0;

        }
        /// <summary>
        /// 选择配置类型事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_IniType_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            comboBox_Items.Items.Clear();
            //如果选择了数据库连接配置类型
            if (comboBox_IniType.Text == "DbConnStr")
            {
                comboBox_Items.Items.AddRange(_host.Core.GetDbNodeFuncCodeNameList(comboBox_ScritpType.Text));
            }
             */
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            /*
            string selectedStr = comboBox_Items.Text;
            if (string.IsNullOrEmpty(selectedStr))
            {
                MessageBox.Show("please select one firstly");
            }
            else
            {
                richTextBox_Setting.AppendText("\r\n");
                richTextBox_Setting.AppendText(selectedStr);
            }
            */
        }
    }
}
