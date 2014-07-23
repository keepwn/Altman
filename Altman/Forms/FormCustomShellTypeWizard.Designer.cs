namespace Altman.Forms
{
    partial class FormCustomShellTypeWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCustomShellTypeWizard));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_MainCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_FuncEncry = new System.Windows.Forms.ComboBox();
            this.cb_FuncLoaction = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl_WizPage = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lv_XmlList = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbx_ShellTypeName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbx_ServiceExample = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_MainEncry = new System.Windows.Forms.ComboBox();
            this.cb_MainLoaction = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lv_Func = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cms_RightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.item_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.item_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_FuncCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_ParmaEncry = new System.Windows.Forms.ComboBox();
            this.cb_Name = new System.Windows.Forms.ComboBox();
            this.cb_ParmaLocation = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bt_Clear = new System.Windows.Forms.Button();
            this.bt_Add = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl_WizPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.cms_RightMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tb_MainCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cb_FuncEncry);
            this.groupBox1.Controls.Add(this.cb_FuncLoaction);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(47, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 218);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MainCodeSetting";
            // 
            // tb_MainCode
            // 
            this.tb_MainCode.Location = new System.Drawing.Point(92, 29);
            this.tb_MainCode.Multiline = true;
            this.tb_MainCode.Name = "tb_MainCode";
            this.tb_MainCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_MainCode.Size = new System.Drawing.Size(293, 74);
            this.tb_MainCode.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "FuncEncry";
            // 
            // cb_FuncEncry
            // 
            this.cb_FuncEncry.FormattingEnabled = true;
            this.cb_FuncEncry.Items.AddRange(new object[] {
            "None",
            "Hex",
            "Base64"});
            this.cb_FuncEncry.Location = new System.Drawing.Point(90, 172);
            this.cb_FuncEncry.Name = "cb_FuncEncry";
            this.cb_FuncEncry.Size = new System.Drawing.Size(121, 20);
            this.cb_FuncEncry.TabIndex = 3;
            // 
            // cb_FuncLoaction
            // 
            this.cb_FuncLoaction.FormattingEnabled = true;
            this.cb_FuncLoaction.Items.AddRange(new object[] {
            "Body",
            "Cookie"});
            this.cb_FuncLoaction.Location = new System.Drawing.Point(90, 130);
            this.cb_FuncLoaction.Name = "cb_FuncLoaction";
            this.cb_FuncLoaction.Size = new System.Drawing.Size(121, 20);
            this.cb_FuncLoaction.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "FuncLocation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Code";
            // 
            // tabControl_WizPage
            // 
            this.tabControl_WizPage.Controls.Add(this.tabPage1);
            this.tabControl_WizPage.Controls.Add(this.tabPage2);
            this.tabControl_WizPage.Controls.Add(this.tabPage3);
            this.tabControl_WizPage.Controls.Add(this.tabPage4);
            this.tabControl_WizPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl_WizPage.Location = new System.Drawing.Point(0, 0);
            this.tabControl_WizPage.Name = "tabControl_WizPage";
            this.tabControl_WizPage.SelectedIndex = 0;
            this.tabControl_WizPage.Size = new System.Drawing.Size(522, 320);
            this.tabControl_WizPage.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lv_XmlList);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(514, 294);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Home Page";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lv_XmlList
            // 
            this.lv_XmlList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lv_XmlList.FullRowSelect = true;
            this.lv_XmlList.GridLines = true;
            this.lv_XmlList.Location = new System.Drawing.Point(241, 57);
            this.lv_XmlList.MultiSelect = false;
            this.lv_XmlList.Name = "lv_XmlList";
            this.lv_XmlList.Size = new System.Drawing.Size(207, 197);
            this.lv_XmlList.TabIndex = 1;
            this.lv_XmlList.UseCompatibleStateImageBehavior = false;
            this.lv_XmlList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Select One ShellType To Load";
            this.columnHeader2.Width = 192;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(239, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(185, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "The Existing Custom ShellType:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbx_ShellTypeName);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.tbx_ServiceExample);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.cb_MainEncry);
            this.tabPage2.Controls.Add(this.cb_MainLoaction);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(514, 294);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Basic Definition";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbx_ShellTypeName
            // 
            this.tbx_ShellTypeName.Location = new System.Drawing.Point(123, 19);
            this.tbx_ShellTypeName.Name = "tbx_ShellTypeName";
            this.tbx_ShellTypeName.Size = new System.Drawing.Size(125, 21);
            this.tbx_ShellTypeName.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(40, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "ShellTypeName";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(40, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(167, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "The server using the sample";
            // 
            // tbx_ServiceExample
            // 
            this.tbx_ServiceExample.Location = new System.Drawing.Point(42, 115);
            this.tbx_ServiceExample.Multiline = true;
            this.tbx_ServiceExample.Name = "tbx_ServiceExample";
            this.tbx_ServiceExample.Size = new System.Drawing.Size(420, 114);
            this.tbx_ServiceExample.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "MainLocation";
            // 
            // cb_MainEncry
            // 
            this.cb_MainEncry.FormattingEnabled = true;
            this.cb_MainEncry.Items.AddRange(new object[] {
            "None",
            "Hex",
            "Base64"});
            this.cb_MainEncry.Location = new System.Drawing.Point(337, 55);
            this.cb_MainEncry.Name = "cb_MainEncry";
            this.cb_MainEncry.Size = new System.Drawing.Size(125, 20);
            this.cb_MainEncry.TabIndex = 20;
            // 
            // cb_MainLoaction
            // 
            this.cb_MainLoaction.FormattingEnabled = true;
            this.cb_MainLoaction.Items.AddRange(new object[] {
            "Body",
            "Cookie"});
            this.cb_MainLoaction.Location = new System.Drawing.Point(123, 55);
            this.cb_MainLoaction.Name = "cb_MainLoaction";
            this.cb_MainLoaction.Size = new System.Drawing.Size(125, 20);
            this.cb_MainLoaction.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(272, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "MainEncry";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(514, 294);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "MainCodeSetting Definition";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.AutoScroll = true;
            this.tabPage4.Controls.Add(this.tableLayoutPanel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(514, 294);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "FuncCode Definition";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.40467F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.59533F));
            this.tableLayoutPanel1.Controls.Add(this.lv_Func, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.22222F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.77778F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(514, 294);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // lv_Func
            // 
            this.lv_Func.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lv_Func.ContextMenuStrip = this.cms_RightMenu;
            this.lv_Func.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_Func.FullRowSelect = true;
            this.lv_Func.GridLines = true;
            this.lv_Func.Location = new System.Drawing.Point(3, 3);
            this.lv_Func.MultiSelect = false;
            this.lv_Func.Name = "lv_Func";
            this.tableLayoutPanel1.SetRowSpan(this.lv_Func, 2);
            this.lv_Func.Size = new System.Drawing.Size(140, 288);
            this.lv_Func.TabIndex = 14;
            this.lv_Func.UseCompatibleStateImageBehavior = false;
            this.lv_Func.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "FuncName";
            this.columnHeader1.Width = 129;
            // 
            // cms_RightMenu
            // 
            this.cms_RightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.item_Edit,
            this.item_Delete});
            this.cms_RightMenu.Name = "cms_RightMenu";
            this.cms_RightMenu.ShowImageMargin = false;
            this.cms_RightMenu.Size = new System.Drawing.Size(76, 48);
            // 
            // item_Edit
            // 
            this.item_Edit.Name = "item_Edit";
            this.item_Edit.Size = new System.Drawing.Size(75, 22);
            this.item_Edit.Text = "编辑";
            this.item_Edit.Click += new System.EventHandler(this.item_Edit_Click);
            // 
            // item_Delete
            // 
            this.item_Delete.Name = "item_Delete";
            this.item_Delete.Size = new System.Drawing.Size(75, 22);
            this.item_Delete.Text = "删除";
            this.item_Delete.Click += new System.EventHandler(this.item_Delete_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tb_FuncCode);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cb_ParmaEncry);
            this.panel1.Controls.Add(this.cb_Name);
            this.panel1.Controls.Add(this.cb_ParmaLocation);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(149, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 235);
            this.panel1.TabIndex = 15;
            // 
            // tb_FuncCode
            // 
            this.tb_FuncCode.Location = new System.Drawing.Point(98, 45);
            this.tb_FuncCode.Multiline = true;
            this.tb_FuncCode.Name = "tb_FuncCode";
            this.tb_FuncCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_FuncCode.Size = new System.Drawing.Size(236, 108);
            this.tb_FuncCode.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "ParmaEncry";
            // 
            // cb_ParmaEncry
            // 
            this.cb_ParmaEncry.FormattingEnabled = true;
            this.cb_ParmaEncry.Location = new System.Drawing.Point(98, 201);
            this.cb_ParmaEncry.Name = "cb_ParmaEncry";
            this.cb_ParmaEncry.Size = new System.Drawing.Size(121, 20);
            this.cb_ParmaEncry.TabIndex = 9;
            // 
            // cb_Name
            // 
            this.cb_Name.FormattingEnabled = true;
            this.cb_Name.Location = new System.Drawing.Point(98, 14);
            this.cb_Name.Name = "cb_Name";
            this.cb_Name.Size = new System.Drawing.Size(121, 20);
            this.cb_Name.TabIndex = 7;
            // 
            // cb_ParmaLocation
            // 
            this.cb_ParmaLocation.FormattingEnabled = true;
            this.cb_ParmaLocation.Items.AddRange(new object[] {
            "Body",
            "Cookie"});
            this.cb_ParmaLocation.Location = new System.Drawing.Point(98, 173);
            this.cb_ParmaLocation.Name = "cb_ParmaLocation";
            this.cb_ParmaLocation.Size = new System.Drawing.Size(121, 20);
            this.cb_ParmaLocation.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "ParmaLocation";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Code";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bt_Clear);
            this.panel2.Controls.Add(this.bt_Add);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(149, 244);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(362, 47);
            this.panel2.TabIndex = 16;
            // 
            // bt_Clear
            // 
            this.bt_Clear.Location = new System.Drawing.Point(283, 18);
            this.bt_Clear.Name = "bt_Clear";
            this.bt_Clear.Size = new System.Drawing.Size(75, 23);
            this.bt_Clear.TabIndex = 2;
            this.bt_Clear.Text = "Reset";
            this.bt_Clear.UseVisualStyleBackColor = true;
            this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
            // 
            // bt_Add
            // 
            this.bt_Add.Location = new System.Drawing.Point(16, 18);
            this.bt_Add.Name = "bt_Add";
            this.bt_Add.Size = new System.Drawing.Size(75, 23);
            this.bt_Add.TabIndex = 1;
            this.bt_Add.Text = "Add";
            this.bt_Add.UseVisualStyleBackColor = true;
            this.bt_Add.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(245, 347);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(75, 23);
            this.btn_back.TabIndex = 14;
            this.btn_back.Text = "上一步";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_next
            // 
            this.btn_next.Location = new System.Drawing.Point(326, 347);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(75, 23);
            this.btn_next.TabIndex = 15;
            this.btn_next.Text = "下一步";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(436, 347);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 16;
            this.btn_OK.Text = "完成";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // FormCustomShellTypeWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 382);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.tabControl_WizPage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCustomShellTypeWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCustomShellTypeWizard";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl_WizPage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.cms_RightMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_MainCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_FuncEncry;
        private System.Windows.Forms.ComboBox cb_FuncLoaction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl_WizPage;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView lv_Func;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_FuncCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_ParmaEncry;
        private System.Windows.Forms.ComboBox cb_ParmaLocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_Name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bt_Clear;
        private System.Windows.Forms.Button bt_Add;
        private System.Windows.Forms.ContextMenuStrip cms_RightMenu;
        private System.Windows.Forms.ToolStripMenuItem item_Edit;
        private System.Windows.Forms.ToolStripMenuItem item_Delete;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbx_ServiceExample;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_MainEncry;
        private System.Windows.Forms.ComboBox cb_MainLoaction;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbx_ShellTypeName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListView lv_XmlList;
        private System.Windows.Forms.ColumnHeader columnHeader2;

    }
}