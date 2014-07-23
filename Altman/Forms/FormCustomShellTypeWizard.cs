using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Altman.Logic;

namespace Altman.Forms
{
    public partial class FormCustomShellTypeWizard : Form
    {
        private readonly string _basePathDir = Application.StartupPath + "/CustomType/";
        private string _selectedXmlFileName = "";

        private CustomShellType.Basic _basicSetting;
        private CustomShellType.MainCode _mainCode;
        private List<CustomShellType.FuncCode> _funcCodeList;
        public FormCustomShellTypeWizard()
        {
            InitializeComponent();

            this.MouseClick += FormCustomShellTypeWizard_MouseClick;
            this.panel1.MouseClick += panel1_MouseClick;

            //==界面初始化indexpage            
            //载入xml文件
            LoadXMlList();  

            //==界面初始化basicpage

            //==界面初始化mainpage

            //==界面初始化funcpage

            //添加元素到cb中
            foreach (var it in Enum.GetValues(typeof(CustomShellType.FuncCode)))
            {
                cb_Name.Items.Add(it.ToString());
            }
            foreach (var it in Enum.GetValues(typeof(EncryMode)))
            {
                cb_ParmaEncry.Items.Add(it.ToString());
            }

        }

        #region Event
        private void FormCustomShellTypeWizard_MouseClick(object sender, MouseEventArgs e)
        {
            this.panel1.Focus();
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            this.panel1.Focus();
        }
        /// <summary>
        /// 上一步
        /// </summary>
        private void btn_back_Click(object sender, EventArgs e)
        {
            if (tabControl_WizPage.SelectedIndex > 0 && tabControl_WizPage.SelectedIndex < 4)
            {
                tabControl_WizPage.SelectedIndex = tabControl_WizPage.SelectedIndex - 1;
            }
        }
        /// <summary>
        /// 下一步
        /// </summary>
        private void btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                int selectPage = tabControl_WizPage.SelectedIndex;
                switch (selectPage)
                {
                    case 0:
                        //如果未选择文件，则为创建模式；反之则读取xml文件，编辑模式
                        if (lv_XmlList.SelectedItems.Count == 1)
                        {
                            //数据初始化
                            InitData();
                            _selectedXmlFileName = lv_XmlList.SelectedItems[0].Text;
                            CustomShellTypeXmlHandle.ReadXml(_selectedXmlFileName,
                                _basePathDir,
                                ref _basicSetting,
                                ref _mainCode,
                                ref _funcCodeList);
                        }
                        LoadBasic();
                        tabControl_WizPage.SelectedIndex = tabControl_WizPage.SelectedIndex + 1;
                        break;
                    case 1:
                        LoadMainCode();
                        tabControl_WizPage.SelectedIndex = tabControl_WizPage.SelectedIndex + 1;
                        break;
                    case 2:
                        LoadFuncCode();
                        tabControl_WizPage.SelectedIndex = tabControl_WizPage.SelectedIndex + 1;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 确认保存
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                //保存之前数据初始化
                InitData();
                //保存数据
                SaveBasic();
                SaveMainCode();
                SaveFuncCode();
                //将数据写到xml文件中
                CustomShellTypeXmlHandle.WriteXml(_basicSetting.ShellTypeName,
                                                         _basePathDir,
                                                         _basicSetting,
                                                         _mainCode,
                                                         _funcCodeList);
                MessageBox.Show("已成功生成" + _basicSetting.ShellTypeName+".xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitData()
        {
            _basicSetting = new CustomShellType.Basic();
            _mainCode = new CustomShellType.MainCode();
            _funcCodeList = new List<CustomShellType.FuncCode>();
        }
        /// <summary>
        /// 载入xml文件列表
        /// </summary>
        private void LoadXMlList()
        {
            List<string> xmlList = XmlHelper.LoadXMlList(_basePathDir,"xml");
            foreach (string xml in xmlList)
            {
                lv_XmlList.Items.Add(xml);
            }   
        }

        #region 0.BasicPage
        /// <summary>
        /// 载入基本信息
        /// </summary>
        private void LoadBasic()
        {
            try
            {
                tbx_ShellTypeName.Text = _basicSetting.ShellTypeName;
                tbx_ServiceExample.Text = _basicSetting.ServiceExample;
                //cb_MainLoaction.Text = _basicSetting.MainCodeLocation;
                //cb_MainEncry.Text = _basicSetting.MainCodeEncryMode.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 保存基本信息
        /// </summary>
        private void SaveBasic()
        {
            string msg;
            string shellTypeName = tbx_ShellTypeName.Text.Trim();
            string serviceExample = tbx_ServiceExample.Text.Trim();
            string mainLocation = cb_MainLoaction.Text.Trim();
            string mainEncry = cb_MainEncry.Text.Trim();

            //检查shellTypeName,serviceExample,mainLocation,mainEncry字段是否为空
            if (shellTypeName == "" || serviceExample == "" || mainLocation == "" || mainEncry == "")
            {
                msg = "shellTypeName,serviceExample,mainLocation,mainEncry字段不许为空";
                throw new Exception(msg);
            }
            //检查encry字段的正确性
            EncryMode encryMode;
            try
            {
                encryMode = (EncryMode)Enum.Parse(typeof(EncryMode), mainEncry);
            }
            catch
            {
                msg = "mainEncry字段不匹配";
                throw new Exception(msg);
            }

            //全部通过检查，则保存到全局变量
            _basicSetting.ServiceExample = serviceExample;
            _basicSetting.ShellTypeName = shellTypeName;
            //_basicSetting.MainCodeLocation = mainLocation;
            //_basicSetting.MainCodeEncryMode = encryMode;
        }
        #endregion

        #region 1.MainCodePage
        /// <summary>
        /// 载入maincode
        /// </summary>
        private void LoadMainCode()
        {
            try
            {
                string code = _mainCode.Item;
                //string location = _mainCode.FuncCodeLocation;
                //string encry = _mainCode.FuncCodeEncryMode.ToString();

                tb_MainCode.Text = code;
                //cb_FuncLoaction.Text = location;
                //cb_FuncEncry.Text = encry;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 保存maincode
        /// </summary>
        private void SaveMainCode()
        {
            string msg;
            string code = tb_MainCode.Text.Trim();
            string location = cb_FuncLoaction.Text.Trim();
            string encry = cb_FuncEncry.Text.Trim();

            //检查code,location,encry字段是否为空
            if (code == "" || location == "" || encry == "")
            {
                msg = "code,location,encry字段不许为空";
                throw new Exception(msg);
            }
            //检查encry字段的正确性
            EncryMode encryMode;
            try
            {
                encryMode = (EncryMode)Enum.Parse(typeof(EncryMode), encry);
            }
            catch
            {
                msg = "encry字段不匹配";
                throw new Exception(msg);
            }

            //全部通过检查，则保存
            CustomShellType.MainCode mainCode = new CustomShellType.MainCode();
            mainCode.Item = code;
            //MainCodeSetting.FuncCodeLocation = location;
            //MainCodeSetting.FuncCodeEncryMode = encryMode;

            //保存到全局变量
            _mainCode = mainCode;
        }
        #endregion

        #region 2.FuncCodePage

        #region Button Event
        private void bt_Clear_Click(object sender, EventArgs e)
        {
            ClearFuncCodeInInputControl();
        }
        private void bt_Save_Click(object sender, EventArgs e)
        {
            //保存funcode
            SaveFuncCodeToListView();
        }
        #endregion

        #region Right Event
        private void item_Edit_Click(object sender, EventArgs e)
        {
            EditFuncCodeInListView();
        }
        private void item_Delete_Click(object sender, EventArgs e)
        {
            DeleteFuncCodeInListView();
        }
        #endregion

        #region private method
        /// <summary>
        ///清空输入控件中的数据
        /// </summary>
        private void ClearFuncCodeInInputControl()
        {
            //清空内容
            cb_Name.Text = "";
            tb_FuncCode.Text = "";
            cb_ParmaLocation.Text = "";
            cb_ParmaEncry.Text = "";
        }
        /// <summary>
        /// 保存funccode
        /// </summary>
        private void SaveFuncCodeToListView()
        {
            string name = cb_Name.Text.Trim();
            string code = tb_FuncCode.Text.Trim();
            string location = cb_ParmaLocation.Text.Trim();
            string encry = cb_ParmaEncry.Text.Trim();

            //检查name字段是否已经存在
            if (lv_Func.Items.ContainsKey(name))
            {
                MessageBox.Show("在列表中已存在这个Name");
                return;
            }
            //检查code,location,encry字段是否为空
            if (code == "" || location == "" || encry == "")
            {
                MessageBox.Show("code,location,encry字段不许为空");
                return;
            }
            //检查encry字段的正确性
            EncryMode encryMode;
            try
            {
                encryMode = (EncryMode)Enum.Parse(typeof(EncryMode), encry);
            }
            catch
            {
                MessageBox.Show("encry字段不匹配");
                return;
            }

            //全部通过检查，则保存
            CustomShellType.FuncCode funcCode = new CustomShellType.FuncCode();
            funcCode.Name = name;
            funcCode.Item = code;
            //funcCode.FuncParmaLocation = location;
            //funcCode.FuncParmaEncryMode = encryMode;


            ListViewItem item = new ListViewItem(name);
            item.Name = name;//检查是否存在，为此赋值作为key
            item.Tag = funcCode;    //绑定funcode到tag
            lv_Func.Items.Add(item);
        }
        /// <summary>
        /// 删除funccode
        /// </summary>
        private void DeleteFuncCodeInListView()
        {
            if (lv_Func.SelectedItems.Count > 0)
            {
                lv_Func.SelectedItems[0].Remove();
            }
        }
        /// <summary>
        /// 编辑funccode
        /// </summary>
        private void EditFuncCodeInListView()
        {
            if (lv_Func.SelectedItems.Count > 0)
            {
                ListViewItem item = lv_Func.SelectedItems[0];
                CustomShellType.FuncCode funcCode = (CustomShellType.FuncCode)item.Tag;

                //将列表中数据重新载入控件中
                cb_Name.Text = item.Text.Trim();
                tb_FuncCode.Text = funcCode.Item;
                //cb_ParmaLocation.Text = funcCode.FuncParmaLocation;
                //cb_ParmaEncry.Text = funcCode.FuncParmaEncryMode.ToString();

                //移除列表中数据
                item.Remove();
            }
        }
        #endregion

        /// <summary>
        /// 载入FuncCode
        /// </summary>
        private void LoadFuncCode()
        {
            try
            {
                if (_funcCodeList != null && _funcCodeList.Count > 0)
                {
                    foreach (CustomShellType.FuncCode funcCode in _funcCodeList)
                    {
                        string name = funcCode.Name;
                        if (string.IsNullOrEmpty(name))
                        {
                            continue;
                        }
                        ListViewItem item = new ListViewItem(name);
                        item.Name = name;       //检查是否存在，为此赋值作为key
                        item.Tag = funcCode;    //绑定funcode到tag
                        lv_Func.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 保存FuncCode
        /// </summary>
        private void SaveFuncCode()
        {
            List<CustomShellType.FuncCode> funcCodeList = new List<CustomShellType.FuncCode>();
            if (lv_Func.Items.Count > 0)
            {
                //遍历listview
                foreach (ListViewItem item in lv_Func.Items)
                {
                    CustomShellType.FuncCode funcCode = (CustomShellType.FuncCode)item.Tag;
                    funcCodeList.Add(funcCode);
                }
            }
            //保存到全局变量
            _funcCodeList = funcCodeList;
        }
        #endregion



    }
}
