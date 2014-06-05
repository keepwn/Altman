using System;
using System.Windows.Forms;
using Altman.LogicCore;
using Altman.ModelCore;

namespace Altman.UICore.Control_ShellManager
{
    public delegate void WebshellWatchEventHandler(object sender, EventArgs e);

    public partial class FormEditWebshell : Form
    {
        private string Id;
        private ShellManager _shellManager = null;
        public FormEditWebshell()
        {
            InitializeComponent();
            ComboBox_ScriptType_Init();

            _shellManager = new ShellManager();

            this.button_Add.Enabled = true;
            this.button_Alter.Enabled = false;

            comboBox_IniType_Init();
        }

        public FormEditWebshell(ShellStruct shellStructArray)
        {
            InitializeComponent();
            ComboBox_ScriptType_Init();

            _shellManager = new ShellManager();

            this.button_Add.Enabled = false;
            this.button_Alter.Enabled = true;

            this.Id = shellStructArray.Id;
            this.textBox_TargetID.Text = shellStructArray.TargetId;
            this.comboBox_TargetLevel.Text = shellStructArray.TargetLevel;

            this.textBox_ShellPath.Text = shellStructArray.ShellUrl;
            this.textBox_ShellPass.Text = shellStructArray.ShellPwd;

            this.richTextBox_Setting.Text = shellStructArray.ShellExtraSetting;

            this.textBox_Remark.Text = shellStructArray.Remark;

            this.comboBox_ScritpType.Text = shellStructArray.ShellType;
            this.comboBox_ServerCoding.Text = shellStructArray.ServerCoding;
            this.comboBox_WebCoding.Text = shellStructArray.WebCoding;
            this.comboBox_Area.Text = shellStructArray.Area;

            comboBox_IniType_Init();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            ShellStruct shellStruct = new ShellStruct();

            shellStruct.Id = this.Id;
            shellStruct.TargetId = this.textBox_TargetID.Text.Trim();
            shellStruct.TargetLevel = this.comboBox_TargetLevel.Text.Trim();

            shellStruct.ShellUrl = this.textBox_ShellPath.Text.Trim();
            shellStruct.ShellPwd = this.textBox_ShellPass.Text.Trim();

            shellStruct.ShellExtraSetting = this.richTextBox_Setting.Text.Trim();
            shellStruct.Remark = this.textBox_Remark.Text.Trim();

            shellStruct.ShellType = this.comboBox_ScritpType.Text.Trim();
            shellStruct.ServerCoding = this.comboBox_ServerCoding.Text.Trim();
            shellStruct.WebCoding = this.comboBox_WebCoding.Text.Trim();
            shellStruct.Area = this.comboBox_Area.Text.Trim();

            string time = DateTime.Now.Date.ToShortDateString();
            if (time.Contains("/"))
            {
                time = time.Replace("/", "-");
            }
            shellStruct.AddTime = time;

            _shellManager.Insert(shellStruct);
            OnWebshellChange(EventArgs.Empty);
            this.Close();
        }

        private void button_Alter_Click(object sender, EventArgs e)
        {
            ShellStruct shellStruct = new ShellStruct();

            shellStruct.Id = this.Id;
            shellStruct.TargetId = this.textBox_TargetID.Text.Trim();
            shellStruct.TargetLevel = this.comboBox_TargetLevel.Text.Trim();

            shellStruct.ShellUrl = this.textBox_ShellPath.Text.Trim();
            shellStruct.ShellPwd = this.textBox_ShellPass.Text.Trim();

            shellStruct.ShellExtraSetting = this.richTextBox_Setting.Text.Trim();
            shellStruct.Remark = this.textBox_Remark.Text.Trim();

            shellStruct.ShellType = this.comboBox_ScritpType.Text.Trim();
            shellStruct.ServerCoding = this.comboBox_ServerCoding.Text.Trim();
            shellStruct.WebCoding = this.comboBox_WebCoding.Text.Trim();
            shellStruct.Area = this.comboBox_Area.Text.Trim();

            string time = DateTime.Now.Date.ToShortDateString();
            if (time.Contains("/"))
            {
                time = time.Replace("/", "-");
            }
            shellStruct.AddTime = time;


            _shellManager.Update(int.Parse(shellStruct.Id), shellStruct);
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

        /// <summary>
        /// 初始化可选择的脚本类型
        /// </summary>
        private void ComboBox_ScriptType_Init()
        {
            //获取可用的CustomShellType
            foreach (string type in InitUi.GetCustomShellTypeNameList())
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
            comboBox_IniType.SelectedIndex = 0;

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
                comboBox_Items.Items.AddRange(InitUi.GetDbNodeFuncCodeNameList(comboBox_ScritpType.Text));
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
