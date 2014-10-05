using System;
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


	    private Button _buttonAdvanced;
        private Button _buttonAdd;
        private Button _buttonAlter;
        private TextBox _textBoxName;
		private ComboBox _comboBoxLevel;
        private TextBox _textBoxShellPath;
        private TextBox _textBoxShellPass;
        private TextArea _richTextBoxSetting;
        private TextBox _textBoxRemark;
        private ComboBox _comboBoxScritpType;
        private ComboBox _comboBoxServerCoding;
        private ComboBox _comboBoxWebCoding;
        private Splitter _p12;

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

            this._buttonAdd.Enabled = false;
            this._buttonAlter.Enabled = true;

            this.Id = shellArray.Id;
            this._textBoxName.Text = shellArray.TargetId;
            this._comboBoxLevel.Items.Add(shellArray.TargetLevel);

            this._textBoxShellPath.Text = shellArray.ShellUrl;
            this._textBoxShellPass.Text = shellArray.ShellPwd;

            this._richTextBoxSetting.Text = shellArray.ShellExtraString;

            this._textBoxRemark.Text = shellArray.Remark;

            this._comboBoxScritpType.SelectedKey = shellArray.ShellType;
            this._comboBoxServerCoding.Items.Add(shellArray.ServerCoding);
            this._comboBoxWebCoding.Items.Add(shellArray.WebCoding);          
        }

        void Init()
        {
            _buttonAdd = new Button { Text = "Add" };
            _buttonAdd.Click +=buttonAdd_Click;

            _buttonAlter = new Button { Text = "Alter"};
			_buttonAlter.Click += _buttonAlter_Click;

	        _buttonAdvanced = new Button {Text = "Advanced"};
			_buttonAdvanced.Click += _buttonAdvanced_Click;

	        _textBoxName = new TextBox {PlaceholderText = "Name", Size = new Size(200, -1)};
			_comboBoxLevel = new ComboBox { Size = new Size(50, -1), };
            _comboBoxScritpType = new ComboBox { Size = new Size(120, -1) };
			_textBoxShellPath = new TextBox { PlaceholderText = "Shell Url", Size = new Size(300, -1) };
            _textBoxShellPass = new TextBox {PlaceholderText = "Pass" };
            _richTextBoxSetting = new TextArea { Size = new Size(100, -1) };
            _textBoxRemark = new TextBox {PlaceholderText = "Remark"};
	        _comboBoxServerCoding = new ComboBox();
	        _comboBoxWebCoding = new ComboBox();


			var panel1 = new DynamicLayout { Padding = new Padding(5, 5), Spacing = new Size(5, 5) };
			panel1.BeginVertical();
			panel1.BeginHorizontal();
	        panel1.Add(_textBoxName, true);
			panel1.Add(_comboBoxLevel);
			panel1.Add(_comboBoxScritpType);
			panel1.EndHorizontal();
			panel1.EndVertical();

			panel1.BeginVertical();
			panel1.BeginHorizontal();
			panel1.Add(_textBoxShellPath, true);
            panel1.Add(_textBoxShellPass);
			panel1.EndHorizontal();
			panel1.EndVertical();
	        
	        panel1.AddRow(_textBoxRemark);

			panel1.BeginVertical();
			panel1.BeginHorizontal();
	        panel1.Add(null, true);
			panel1.Add(_buttonAdd);
			panel1.Add(_buttonAlter);
			panel1.EndHorizontal();
            panel1.Add(null);
			panel1.EndVertical();
			

            var panel2 = new DynamicLayout();
            panel2.AddRow(_richTextBoxSetting);
            _p12 = new Splitter { Panel1 = panel1, Panel2 = panel2, Orientation = SplitterOrientation.Horizontal, Position=300};
            _p12.Panel2.Visible = false;

            var layout = new DynamicLayout{Padding=new Padding(5,5),Spacing=new Size(5,5)};
            layout.AddRow(_p12);


            Content = layout;
            ClientSize = new Size(500,300);
            Title = "Edit Shell";
	        Icon = Application.Instance.MainForm.Icon;
        }

		void _buttonAdvanced_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

        private void buttonAdd_Click(object sender, EventArgs e)
        {
			//_p12.Panel2.Visible = !_p12.Panel2.Visible;
            //this.ClientSize = new Size(600, 200);
			//this.Size = new Size(600, 200);

            Shell shell = new Shell();

            shell.Id = this.Id;
            shell.TargetId = this._textBoxName.Text.Trim();
            shell.TargetLevel = this._comboBoxLevel.SelectedKey.Trim();

            shell.ShellUrl = this._textBoxShellPath.Text.Trim();
            shell.ShellPwd = this._textBoxShellPass.Text.Trim();

            shell.ShellExtraString = this._richTextBoxSetting.Text.Trim();
            shell.Remark = this._textBoxRemark.Text.Trim();

            shell.ShellType = this._comboBoxScritpType.SelectedKey.Trim();
            shell.ServerCoding = this._comboBoxServerCoding.SelectedKey.Trim();
            shell.WebCoding = this._comboBoxWebCoding.SelectedKey.Trim();

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
            Shell shell = new Shell();

            shell.Id = this.Id;
            shell.TargetId = this._textBoxName.Text.Trim();
            shell.TargetLevel = this._comboBoxLevel.SelectedKey.Trim();

            shell.ShellUrl = this._textBoxShellPath.Text.Trim();
            shell.ShellPwd = this._textBoxShellPass.Text.Trim();

            shell.ShellExtraString = this._richTextBoxSetting.Text.Trim();
            shell.Remark = this._textBoxRemark.Text.Trim();

            shell.ShellType = this._comboBoxScritpType.SelectedKey.Trim();
            shell.ServerCoding = this._comboBoxServerCoding.SelectedKey.Trim();
            shell.WebCoding = this._comboBoxWebCoding.SelectedKey.Trim();

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
