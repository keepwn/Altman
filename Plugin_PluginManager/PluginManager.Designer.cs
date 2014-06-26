namespace Plugin_PluginManager
{
    partial class PluginManager
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.tbx_InstalledPluginsDescription = new System.Windows.Forms.TextBox();
            this.lv_InstalledPlugins = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_Install = new System.Windows.Forms.Button();
            this.tbx_AvailablePluginsDescription = new System.Windows.Forms.TextBox();
            this.lv_AvailablePlugins = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btn_Update = new System.Windows.Forms.Button();
            this.tbx_UpdatesPluginsDescription = new System.Windows.Forms.TextBox();
            this.lv_UpdatesPlugins = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Msg = new System.Windows.Forms.Label();
            this.btn_Setting = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabControl1, 3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(589, 354);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_Remove);
            this.tabPage1.Controls.Add(this.tbx_InstalledPluginsDescription);
            this.tabPage1.Controls.Add(this.lv_InstalledPlugins);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(581, 328);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Installed";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_Remove
            // 
            this.btn_Remove.Location = new System.Drawing.Point(494, 303);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(75, 23);
            this.btn_Remove.TabIndex = 11;
            this.btn_Remove.Text = "Remove";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // tbx_InstalledPluginsDescription
            // 
            this.tbx_InstalledPluginsDescription.Location = new System.Drawing.Point(20, 225);
            this.tbx_InstalledPluginsDescription.Multiline = true;
            this.tbx_InstalledPluginsDescription.Name = "tbx_InstalledPluginsDescription";
            this.tbx_InstalledPluginsDescription.ReadOnly = true;
            this.tbx_InstalledPluginsDescription.Size = new System.Drawing.Size(549, 60);
            this.tbx_InstalledPluginsDescription.TabIndex = 10;
            this.tbx_InstalledPluginsDescription.Text = "Description";
            // 
            // lv_InstalledPlugins
            // 
            this.lv_InstalledPlugins.CheckBoxes = true;
            this.lv_InstalledPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lv_InstalledPlugins.FullRowSelect = true;
            this.lv_InstalledPlugins.GridLines = true;
            this.lv_InstalledPlugins.Location = new System.Drawing.Point(20, 10);
            this.lv_InstalledPlugins.MultiSelect = false;
            this.lv_InstalledPlugins.Name = "lv_InstalledPlugins";
            this.lv_InstalledPlugins.Size = new System.Drawing.Size(549, 208);
            this.lv_InstalledPlugins.TabIndex = 9;
            this.lv_InstalledPlugins.UseCompatibleStateImageBehavior = false;
            this.lv_InstalledPlugins.View = System.Windows.Forms.View.Details;
            this.lv_InstalledPlugins.SelectedIndexChanged += new System.EventHandler(this.lv_InstalledPlugins_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Plugin";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Author";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Installed Version";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 115;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Assembly";
            this.columnHeader4.Width = 150;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_Install);
            this.tabPage2.Controls.Add(this.tbx_AvailablePluginsDescription);
            this.tabPage2.Controls.Add(this.lv_AvailablePlugins);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(581, 328);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Available";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_Install
            // 
            this.btn_Install.Location = new System.Drawing.Point(494, 303);
            this.btn_Install.Name = "btn_Install";
            this.btn_Install.Size = new System.Drawing.Size(75, 23);
            this.btn_Install.TabIndex = 7;
            this.btn_Install.Text = "Install";
            this.btn_Install.UseVisualStyleBackColor = true;
            this.btn_Install.Click += new System.EventHandler(this.btn_Install_Click);
            // 
            // tbx_AvailablePluginsDescription
            // 
            this.tbx_AvailablePluginsDescription.Location = new System.Drawing.Point(20, 225);
            this.tbx_AvailablePluginsDescription.Multiline = true;
            this.tbx_AvailablePluginsDescription.Name = "tbx_AvailablePluginsDescription";
            this.tbx_AvailablePluginsDescription.ReadOnly = true;
            this.tbx_AvailablePluginsDescription.Size = new System.Drawing.Size(549, 60);
            this.tbx_AvailablePluginsDescription.TabIndex = 6;
            this.tbx_AvailablePluginsDescription.Text = "Description";
            // 
            // lv_AvailablePlugins
            // 
            this.lv_AvailablePlugins.CheckBoxes = true;
            this.lv_AvailablePlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.lv_AvailablePlugins.FullRowSelect = true;
            this.lv_AvailablePlugins.GridLines = true;
            this.lv_AvailablePlugins.Location = new System.Drawing.Point(20, 10);
            this.lv_AvailablePlugins.MultiSelect = false;
            this.lv_AvailablePlugins.Name = "lv_AvailablePlugins";
            this.lv_AvailablePlugins.Size = new System.Drawing.Size(549, 208);
            this.lv_AvailablePlugins.TabIndex = 5;
            this.lv_AvailablePlugins.UseCompatibleStateImageBehavior = false;
            this.lv_AvailablePlugins.View = System.Windows.Forms.View.Details;
            this.lv_AvailablePlugins.SelectedIndexChanged += new System.EventHandler(this.lv_AvailablePlugins_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Plugin";
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Author";
            this.columnHeader6.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Available Version";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader7.Width = 115;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Source Code";
            this.columnHeader8.Width = 80;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btn_Update);
            this.tabPage3.Controls.Add(this.tbx_UpdatesPluginsDescription);
            this.tabPage3.Controls.Add(this.lv_UpdatesPlugins);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(581, 328);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Updates";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(494, 303);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(75, 23);
            this.btn_Update.TabIndex = 7;
            this.btn_Update.Text = "Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // tbx_UpdatesPluginsDescription
            // 
            this.tbx_UpdatesPluginsDescription.Location = new System.Drawing.Point(20, 225);
            this.tbx_UpdatesPluginsDescription.Multiline = true;
            this.tbx_UpdatesPluginsDescription.Name = "tbx_UpdatesPluginsDescription";
            this.tbx_UpdatesPluginsDescription.ReadOnly = true;
            this.tbx_UpdatesPluginsDescription.Size = new System.Drawing.Size(549, 60);
            this.tbx_UpdatesPluginsDescription.TabIndex = 6;
            this.tbx_UpdatesPluginsDescription.Text = "Description";
            // 
            // lv_UpdatesPlugins
            // 
            this.lv_UpdatesPlugins.CheckBoxes = true;
            this.lv_UpdatesPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.lv_UpdatesPlugins.FullRowSelect = true;
            this.lv_UpdatesPlugins.GridLines = true;
            this.lv_UpdatesPlugins.Location = new System.Drawing.Point(20, 10);
            this.lv_UpdatesPlugins.MultiSelect = false;
            this.lv_UpdatesPlugins.Name = "lv_UpdatesPlugins";
            this.lv_UpdatesPlugins.Size = new System.Drawing.Size(549, 208);
            this.lv_UpdatesPlugins.TabIndex = 5;
            this.lv_UpdatesPlugins.UseCompatibleStateImageBehavior = false;
            this.lv_UpdatesPlugins.View = System.Windows.Forms.View.Details;
            this.lv_UpdatesPlugins.SelectedIndexChanged += new System.EventHandler(this.lv_UpdatesPlugins_SelectedIndexChanged);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Plugin";
            this.columnHeader9.Width = 150;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Author";
            this.columnHeader10.Width = 80;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Installed Version";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader11.Width = 115;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Available Version";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader12.Width = 115;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Source Code";
            this.columnHeader13.Width = 80;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 445F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_Msg, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Setting, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Close, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.3253F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.674699F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(595, 395);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lbl_Msg
            // 
            this.lbl_Msg.AutoSize = true;
            this.lbl_Msg.ForeColor = System.Drawing.Color.Red;
            this.lbl_Msg.Location = new System.Drawing.Point(70, 360);
            this.lbl_Msg.Name = "lbl_Msg";
            this.lbl_Msg.Size = new System.Drawing.Size(437, 24);
            this.lbl_Msg.TabIndex = 13;
            this.lbl_Msg.Text = "Newly installed/removed plugin will take effect after the program start next time" +
    ".";
            this.lbl_Msg.Visible = false;
            // 
            // btn_Setting
            // 
            this.btn_Setting.Location = new System.Drawing.Point(3, 363);
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.Size = new System.Drawing.Size(61, 23);
            this.btn_Setting.TabIndex = 1;
            this.btn_Setting.Text = "Setting";
            this.btn_Setting.UseVisualStyleBackColor = true;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(515, 363);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 2;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // PluginManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 395);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginManager";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PluginManager";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_Setting;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btn_Install;
        private System.Windows.Forms.TextBox tbx_AvailablePluginsDescription;
        private System.Windows.Forms.ListView lv_AvailablePlugins;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.TextBox tbx_UpdatesPluginsDescription;
        private System.Windows.Forms.ListView lv_UpdatesPlugins;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.TextBox tbx_InstalledPluginsDescription;
        private System.Windows.Forms.ListView lv_InstalledPlugins;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label lbl_Msg;
        private System.Windows.Forms.ColumnHeader columnHeader13;
    }
}