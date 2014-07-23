using System;
using System.Windows.Forms;
using System.Xml;
using Altman.Model;
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
            InitializeComponent();
            
            this._host = host;
            _shellManagerService = new ShellManagerService(_host);

            //init
            ComboBox_ScriptType_Init();
            
            this.button_Add.Enabled = true;
            this.button_Alter.Enabled = false;

            comboBox_IniType_Init();
        }

        public FormEditWebshell(IHost host, Shell shellArray)
        {
            InitializeComponent();           

            this._host = host;
            _shellManagerService = new ShellManagerService(_host);


            //init
            ComboBox_ScriptType_Init();

            this.button_Add.Enabled = false;
            this.button_Alter.Enabled = true;

            this.Id = shellArray.Id;
            this.textBox_TargetID.Text = shellArray.TargetId;
            this.comboBox_TargetLevel.Text = shellArray.TargetLevel;

            this.textBox_ShellPath.Text = shellArray.ShellUrl;
            this.textBox_ShellPass.Text = shellArray.ShellPwd;

            this.richTextBox_Setting.Text = shellArray.ShellExtraString;

            this.textBox_Remark.Text = shellArray.Remark;

            this.comboBox_ScritpType.Text = shellArray.ShellType;
            this.comboBox_ServerCoding.Text = shellArray.ServerCoding;
            this.comboBox_WebCoding.Text = shellArray.WebCoding;
            this.comboBox_Area.Text = shellArray.Area;

            comboBox_IniType_Init();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            Shell shell = new Shell();

            shell.Id = this.Id;
            shell.TargetId = this.textBox_TargetID.Text.Trim();
            shell.TargetLevel = this.comboBox_TargetLevel.Text.Trim();

            shell.ShellUrl = this.textBox_ShellPath.Text.Trim();
            shell.ShellPwd = this.textBox_ShellPass.Text.Trim();

            shell.ShellExtraString = this.richTextBox_Setting.Text.Trim();
            shell.Remark = this.textBox_Remark.Text.Trim();

            shell.ShellType = this.comboBox_ScritpType.Text.Trim();
            shell.ServerCoding = this.comboBox_ServerCoding.Text.Trim();
            shell.WebCoding = this.comboBox_WebCoding.Text.Trim();
            shell.Area = this.comboBox_Area.Text.Trim();

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
        }
        private void button_Alter_Click(object sender, EventArgs e)
        {
            Shell shell = new Shell();

            shell.Id = this.Id;
            shell.TargetId = this.textBox_TargetID.Text.Trim();
            shell.TargetLevel = this.comboBox_TargetLevel.Text.Trim();

            shell.ShellUrl = this.textBox_ShellPath.Text.Trim();
            shell.ShellPwd = this.textBox_ShellPass.Text.Trim();

            shell.ShellExtraString = this.richTextBox_Setting.Text.Trim();
            shell.Remark = this.textBox_Remark.Text.Trim();

            shell.ShellType = this.comboBox_ScritpType.Text.Trim();
            shell.ServerCoding = this.comboBox_ServerCoding.Text.Trim();
            shell.WebCoding = this.comboBox_WebCoding.Text.Trim();
            shell.Area = this.comboBox_Area.Text.Trim();

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
            comboBox_IniType.Items.AddRange(types);
            //comboBox_IniType.SelectedIndex = 0;

        }
        /// <summary>
        /// 选择配置类型事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_IniType_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Items.Items.Clear();
            //如果选择了数据库连接配置类型
            if (comboBox_IniType.Text == "DbConnStr")
            {
                comboBox_Items.Items.AddRange(_host.Core.GetDbNodeFuncCodeNameList(comboBox_ScritpType.Text));
            }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
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

        }
    }
}
