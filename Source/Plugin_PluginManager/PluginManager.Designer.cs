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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginManager));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_installed = new System.Windows.Forms.TabPage();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.tbx_InstalledPluginsDescription = new System.Windows.Forms.TextBox();
            this.lv_InstalledPlugins = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage_available = new System.Windows.Forms.TabPage();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btn_Install = new System.Windows.Forms.Button();
            this.tbx_AvailablePluginsDescription = new System.Windows.Forms.TextBox();
            this.lv_AvailablePlugins = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage_updatable = new System.Windows.Forms.TabPage();
            this.btn_Update = new System.Windows.Forms.Button();
            this.tbx_UpdatesPluginsDescription = new System.Windows.Forms.TextBox();
            this.lv_UpdatablePlugins = new System.Windows.Forms.ListView();
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
            this.tabPage_installed.SuspendLayout();
            this.tabPage_available.SuspendLayout();
            this.tabPage_updatable.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tableLayoutPanel1.SetColumnSpan(this.tabControl1, 3);
            this.tabControl1.Controls.Add(this.tabPage_installed);
            this.tabControl1.Controls.Add(this.tabPage_available);
            this.tabControl1.Controls.Add(this.tabPage_updatable);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage_installed
            // 
            resources.ApplyResources(this.tabPage_installed, "tabPage_installed");
            this.tabPage_installed.Controls.Add(this.btn_Remove);
            this.tabPage_installed.Controls.Add(this.tbx_InstalledPluginsDescription);
            this.tabPage_installed.Controls.Add(this.lv_InstalledPlugins);
            this.tabPage_installed.Name = "tabPage_installed";
            this.tabPage_installed.UseVisualStyleBackColor = true;
            // 
            // btn_Remove
            // 
            resources.ApplyResources(this.btn_Remove, "btn_Remove");
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // tbx_InstalledPluginsDescription
            // 
            resources.ApplyResources(this.tbx_InstalledPluginsDescription, "tbx_InstalledPluginsDescription");
            this.tbx_InstalledPluginsDescription.Name = "tbx_InstalledPluginsDescription";
            this.tbx_InstalledPluginsDescription.ReadOnly = true;
            // 
            // lv_InstalledPlugins
            // 
            resources.ApplyResources(this.lv_InstalledPlugins, "lv_InstalledPlugins");
            this.lv_InstalledPlugins.CheckBoxes = true;
            this.lv_InstalledPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lv_InstalledPlugins.FullRowSelect = true;
            this.lv_InstalledPlugins.GridLines = true;
            this.lv_InstalledPlugins.MultiSelect = false;
            this.lv_InstalledPlugins.Name = "lv_InstalledPlugins";
            this.lv_InstalledPlugins.UseCompatibleStateImageBehavior = false;
            this.lv_InstalledPlugins.View = System.Windows.Forms.View.Details;
            this.lv_InstalledPlugins.SelectedIndexChanged += new System.EventHandler(this.lv_InstalledPlugins_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // tabPage_available
            // 
            resources.ApplyResources(this.tabPage_available, "tabPage_available");
            this.tabPage_available.Controls.Add(this.btn_Refresh);
            this.tabPage_available.Controls.Add(this.btn_Install);
            this.tabPage_available.Controls.Add(this.tbx_AvailablePluginsDescription);
            this.tabPage_available.Controls.Add(this.lv_AvailablePlugins);
            this.tabPage_available.Name = "tabPage_available";
            this.tabPage_available.UseVisualStyleBackColor = true;
            // 
            // btn_Refresh
            // 
            resources.ApplyResources(this.btn_Refresh, "btn_Refresh");
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_Install
            // 
            resources.ApplyResources(this.btn_Install, "btn_Install");
            this.btn_Install.Name = "btn_Install";
            this.btn_Install.UseVisualStyleBackColor = true;
            this.btn_Install.Click += new System.EventHandler(this.btn_Install_Click);
            // 
            // tbx_AvailablePluginsDescription
            // 
            resources.ApplyResources(this.tbx_AvailablePluginsDescription, "tbx_AvailablePluginsDescription");
            this.tbx_AvailablePluginsDescription.Name = "tbx_AvailablePluginsDescription";
            this.tbx_AvailablePluginsDescription.ReadOnly = true;
            // 
            // lv_AvailablePlugins
            // 
            resources.ApplyResources(this.lv_AvailablePlugins, "lv_AvailablePlugins");
            this.lv_AvailablePlugins.CheckBoxes = true;
            this.lv_AvailablePlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader14});
            this.lv_AvailablePlugins.FullRowSelect = true;
            this.lv_AvailablePlugins.GridLines = true;
            this.lv_AvailablePlugins.MultiSelect = false;
            this.lv_AvailablePlugins.Name = "lv_AvailablePlugins";
            this.lv_AvailablePlugins.UseCompatibleStateImageBehavior = false;
            this.lv_AvailablePlugins.View = System.Windows.Forms.View.Details;
            this.lv_AvailablePlugins.SelectedIndexChanged += new System.EventHandler(this.lv_AvailablePlugins_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // columnHeader6
            // 
            resources.ApplyResources(this.columnHeader6, "columnHeader6");
            // 
            // columnHeader7
            // 
            resources.ApplyResources(this.columnHeader7, "columnHeader7");
            // 
            // columnHeader8
            // 
            resources.ApplyResources(this.columnHeader8, "columnHeader8");
            // 
            // columnHeader14
            // 
            resources.ApplyResources(this.columnHeader14, "columnHeader14");
            // 
            // tabPage_updatable
            // 
            resources.ApplyResources(this.tabPage_updatable, "tabPage_updatable");
            this.tabPage_updatable.Controls.Add(this.btn_Update);
            this.tabPage_updatable.Controls.Add(this.tbx_UpdatesPluginsDescription);
            this.tabPage_updatable.Controls.Add(this.lv_UpdatablePlugins);
            this.tabPage_updatable.Name = "tabPage_updatable";
            this.tabPage_updatable.UseVisualStyleBackColor = true;
            // 
            // btn_Update
            // 
            resources.ApplyResources(this.btn_Update, "btn_Update");
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // tbx_UpdatesPluginsDescription
            // 
            resources.ApplyResources(this.tbx_UpdatesPluginsDescription, "tbx_UpdatesPluginsDescription");
            this.tbx_UpdatesPluginsDescription.Name = "tbx_UpdatesPluginsDescription";
            this.tbx_UpdatesPluginsDescription.ReadOnly = true;
            // 
            // lv_UpdatablePlugins
            // 
            resources.ApplyResources(this.lv_UpdatablePlugins, "lv_UpdatablePlugins");
            this.lv_UpdatablePlugins.CheckBoxes = true;
            this.lv_UpdatablePlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.lv_UpdatablePlugins.FullRowSelect = true;
            this.lv_UpdatablePlugins.GridLines = true;
            this.lv_UpdatablePlugins.MultiSelect = false;
            this.lv_UpdatablePlugins.Name = "lv_UpdatablePlugins";
            this.lv_UpdatablePlugins.UseCompatibleStateImageBehavior = false;
            this.lv_UpdatablePlugins.View = System.Windows.Forms.View.Details;
            this.lv_UpdatablePlugins.SelectedIndexChanged += new System.EventHandler(this.lv_UpdatablePlugins_SelectedIndexChanged);
            // 
            // columnHeader9
            // 
            resources.ApplyResources(this.columnHeader9, "columnHeader9");
            // 
            // columnHeader10
            // 
            resources.ApplyResources(this.columnHeader10, "columnHeader10");
            // 
            // columnHeader11
            // 
            resources.ApplyResources(this.columnHeader11, "columnHeader11");
            // 
            // columnHeader12
            // 
            resources.ApplyResources(this.columnHeader12, "columnHeader12");
            // 
            // columnHeader13
            // 
            resources.ApplyResources(this.columnHeader13, "columnHeader13");
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lbl_Msg, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Setting, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Close, 2, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lbl_Msg
            // 
            resources.ApplyResources(this.lbl_Msg, "lbl_Msg");
            this.lbl_Msg.ForeColor = System.Drawing.Color.Red;
            this.lbl_Msg.Name = "lbl_Msg";
            // 
            // btn_Setting
            // 
            resources.ApplyResources(this.btn_Setting, "btn_Setting");
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.UseVisualStyleBackColor = true;
            // 
            // btn_Close
            // 
            resources.ApplyResources(this.btn_Close, "btn_Close");
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // PluginManager
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginManager";
            this.ShowIcon = false;
            this.tabControl1.ResumeLayout(false);
            this.tabPage_installed.ResumeLayout(false);
            this.tabPage_installed.PerformLayout();
            this.tabPage_available.ResumeLayout(false);
            this.tabPage_available.PerformLayout();
            this.tabPage_updatable.ResumeLayout(false);
            this.tabPage_updatable.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_installed;
        private System.Windows.Forms.TabPage tabPage_available;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_Setting;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TabPage tabPage_updatable;
        private System.Windows.Forms.Button btn_Install;
        private System.Windows.Forms.TextBox tbx_AvailablePluginsDescription;
        private System.Windows.Forms.ListView lv_AvailablePlugins;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.TextBox tbx_UpdatesPluginsDescription;
        private System.Windows.Forms.ListView lv_UpdatablePlugins;
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
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.Button btn_Refresh;
    }
}