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



        private Button button_Add;
        private Button button_Alter;
        private TextBox textBox_Name;
        private ComboBox comboBox_Level;
        private TextBox textBox_ShellPath;
        private TextBox textBox_ShellPass;
        private TextArea richTextBox_Setting;
        private TextBox textBox_Remark;
        private ComboBox comboBox_ScritpType;
        private ComboBox comboBox_ServerCoding;
        private ComboBox comboBox_WebCoding;
        private Splitter p1_2;

        public FormEditWebshell(IHost host)
        {
            Init();
            
            this._host = host;
            _shellManagerService = new ShellManagerService(_host);

            //init
            ComboBox_ScriptType_Init();
            
            button_Add.Enabled = true;
            button_Alter.Enabled = false;

            comboBox_IniType_Init();
        }

        public FormEditWebshell(IHost host, Shell shellArray)
        {
            Init();

            this._host = host;
            _shellManagerService = new ShellManagerService(_host);


            //init
            ComboBox_ScriptType_Init();

            this.button_Add.Enabled = false;
            this.button_Alter.Enabled = true;

            this.Id = shellArray.Id;
            this.textBox_Name.Text = shellArray.TargetId;
            this.comboBox_Level.SelectedKey = shellArray.TargetLevel;

            this.textBox_ShellPath.Text = shellArray.ShellUrl;
            this.textBox_ShellPass.Text = shellArray.ShellPwd;

            this.richTextBox_Setting.Text = shellArray.ShellExtraString;

            this.textBox_Remark.Text = shellArray.Remark;

            this.comboBox_ScritpType.SelectedKey = shellArray.ShellType;
            this.comboBox_ServerCoding.SelectedKey = shellArray.ServerCoding;
            this.comboBox_WebCoding.SelectedKey = shellArray.WebCoding;

            comboBox_IniType_Init();
        }

        void Init()
        {
            button_Add = new Button { Text = "Add" };
            button_Add.Click +=button_Add_Click;

            button_Alter = new Button { Text = "Alter" };

            textBox_Name = new TextBox { PlaceholderText = "Name", Size = new Size(150, 21) };
            comboBox_Level = new ComboBox{ Size = new Size(50, 21) };
            comboBox_ScritpType = new ComboBox { Size = new Size(120, 21) };
            textBox_ShellPath = new TextBox { PlaceholderText = "Shell Url", Size = new Size(300, 21) };
            textBox_ShellPass = new TextBox {PlaceholderText = "Pass" };
            richTextBox_Setting = new TextArea { Size = new Size(100, 100) };
            textBox_Remark = new TextBox {PlaceholderText = "Remark"};

            
            var panel1 = new DynamicLayout();
            panel1.AddSeparateRow(textBox_Name,comboBox_Level,comboBox_ScritpType);
            panel1.AddSeparateRow(textBox_ShellPath, textBox_ShellPass);
            panel1.AddSeparateRow(textBox_Remark);
            panel1.AddSeparateRow(null, button_Add, button_Alter);
            panel1.AddRow(null);
            var panel2 = new DynamicLayout();
            panel2.AddRow(richTextBox_Setting);
            p1_2 = new Splitter { Panel1 = panel1, Panel2 = panel2, Orientation = SplitterOrientation.Horizontal, Position=300};
            p1_2.Panel2.Visible = false;

            var layout = new DynamicLayout();
            layout.AddRow(p1_2);


            this.Content = layout;
            this.ClientSize = new Size(500,300);
            this.Title = "Edit Shell";
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            p1_2.Panel2.Visible = true;
            this.ClientSize = new Size(600, 200);
            /*
            Shell shell = new Shell();

            shell.Id = this.Id;
            shell.TargetId = this.textBox_Name.Text.Trim();
            shell.TargetLevel = this.comboBox_Level.SelectedKey.Trim();

            shell.ShellUrl = this.textBox_ShellPath.Text.Trim();
            shell.ShellPwd = this.textBox_ShellPass.Text.Trim();

            shell.ShellExtraString = this.richTextBox_Setting.Text.Trim();
            shell.Remark = this.textBox_Remark.Text.Trim();

            shell.ShellType = this.comboBox_ScritpType.SelectedKey.Trim();
            shell.ServerCoding = this.comboBox_ServerCoding.SelectedKey.Trim();
            shell.WebCoding = this.comboBox_WebCoding.SelectedKey.Trim();

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
            this.Close();
             */
        }
        private void button_Alter_Click(object sender, EventArgs e)
        {
            Shell shell = new Shell();

            shell.Id = this.Id;
            shell.TargetId = this.textBox_Name.Text.Trim();
            shell.TargetLevel = this.comboBox_Level.SelectedKey.Trim();

            shell.ShellUrl = this.textBox_ShellPath.Text.Trim();
            shell.ShellPwd = this.textBox_ShellPass.Text.Trim();

            shell.ShellExtraString = this.richTextBox_Setting.Text.Trim();
            shell.Remark = this.textBox_Remark.Text.Trim();

            shell.ShellType = this.comboBox_ScritpType.SelectedKey.Trim();
            shell.ServerCoding = this.comboBox_ServerCoding.SelectedKey.Trim();
            shell.WebCoding = this.comboBox_WebCoding.SelectedKey.Trim();

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
            this.Close();
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
                comboBox_ScritpType.Items.Add(type);
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
