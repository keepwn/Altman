
namespace Altman.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView_func = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_index = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label_logo = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Tsmi_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_ReloadShellType = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_ReloadSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_Plugins = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_Wizard = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_Listening = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_developerMode = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_docs = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_about = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_showMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_productVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_index.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_func);
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.menuStrip1);
            // 
            // treeView_func
            // 
            resources.ApplyResources(this.treeView_func, "treeView_func");
            this.treeView_func.Name = "treeView_func";
            this.treeView_func.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_func_NodeMouseDoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_index);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage_index
            // 
            this.tabPage_index.Controls.Add(this.label1);
            this.tabPage_index.Controls.Add(this.label_logo);
            resources.ApplyResources(this.tabPage_index, "tabPage_index");
            this.tabPage_index.Name = "tabPage_index";
            this.tabPage_index.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.Name = "label1";
            // 
            // label_logo
            // 
            resources.ApplyResources(this.label_logo, "label_logo");
            this.label_logo.ForeColor = System.Drawing.Color.DarkGray;
            this.label_logo.Name = "label_logo";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tsmi_Menu,
            this.Tsmi_Setting,
            this.Tsmi_Plugins,
            this.Tsmi_Wizard,
            this.Tsmi_Listening,
            this.Tsmi_Help});
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // Tsmi_Menu
            // 
            this.Tsmi_Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tsmi_ReloadShellType,
            this.Tsmi_ReloadSetting});
            this.Tsmi_Menu.Name = "Tsmi_Menu";
            resources.ApplyResources(this.Tsmi_Menu, "Tsmi_Menu");
            // 
            // Tsmi_ReloadShellType
            // 
            this.Tsmi_ReloadShellType.Name = "Tsmi_ReloadShellType";
            resources.ApplyResources(this.Tsmi_ReloadShellType, "Tsmi_ReloadShellType");
            this.Tsmi_ReloadShellType.Click += new System.EventHandler(this.Tsmi_ReloadShellType_Click);
            // 
            // Tsmi_ReloadSetting
            // 
            this.Tsmi_ReloadSetting.Name = "Tsmi_ReloadSetting";
            resources.ApplyResources(this.Tsmi_ReloadSetting, "Tsmi_ReloadSetting");
            this.Tsmi_ReloadSetting.Click += new System.EventHandler(this.Tsmi_ReloadSetting_Click);
            // 
            // Tsmi_Setting
            // 
            resources.ApplyResources(this.Tsmi_Setting, "Tsmi_Setting");
            this.Tsmi_Setting.Name = "Tsmi_Setting";
            this.Tsmi_Setting.Click += new System.EventHandler(this.Tsmi_Setting_Click);
            // 
            // Tsmi_Plugins
            // 
            this.Tsmi_Plugins.Name = "Tsmi_Plugins";
            resources.ApplyResources(this.Tsmi_Plugins, "Tsmi_Plugins");
            // 
            // Tsmi_Wizard
            // 
            resources.ApplyResources(this.Tsmi_Wizard, "Tsmi_Wizard");
            this.Tsmi_Wizard.Name = "Tsmi_Wizard";
            this.Tsmi_Wizard.Click += new System.EventHandler(this.Tsmi_Wizard_Click);
            // 
            // Tsmi_Listening
            // 
            resources.ApplyResources(this.Tsmi_Listening, "Tsmi_Listening");
            this.Tsmi_Listening.Name = "Tsmi_Listening";
            this.Tsmi_Listening.Click += new System.EventHandler(this.Tsmi_Listening_Click);
            // 
            // Tsmi_Help
            // 
            this.Tsmi_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tsmi_developerMode,
            this.Tsmi_docs,
            this.Tsmi_about});
            this.Tsmi_Help.Name = "Tsmi_Help";
            resources.ApplyResources(this.Tsmi_Help, "Tsmi_Help");
            // 
            // Tsmi_developerMode
            // 
            this.Tsmi_developerMode.CheckOnClick = true;
            this.Tsmi_developerMode.Name = "Tsmi_developerMode";
            resources.ApplyResources(this.Tsmi_developerMode, "Tsmi_developerMode");
            this.Tsmi_developerMode.Click += new System.EventHandler(this.Tsmi_developerMode_Click);
            // 
            // Tsmi_docs
            // 
            this.Tsmi_docs.Name = "Tsmi_docs";
            resources.ApplyResources(this.Tsmi_docs, "Tsmi_docs");
            this.Tsmi_docs.Click += new System.EventHandler(this.Tsmi_docs_Click);
            // 
            // Tsmi_about
            // 
            this.Tsmi_about.Name = "Tsmi_about";
            resources.ApplyResources(this.Tsmi_about, "Tsmi_about");
            this.Tsmi_about.Click += new System.EventHandler(this.Tsmi_about_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.imageList1, "imageList1");
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_showMsg,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel_productVersion});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel_showMsg
            // 
            this.toolStripStatusLabel_showMsg.Name = "toolStripStatusLabel_showMsg";
            resources.ApplyResources(this.toolStripStatusLabel_showMsg, "toolStripStatusLabel_showMsg");
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolStripStatusLabel_productVersion
            // 
            this.toolStripStatusLabel_productVersion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel_productVersion.Name = "toolStripStatusLabel_productVersion";
            resources.ApplyResources(this.toolStripStatusLabel_productVersion, "toolStripStatusLabel_productVersion");
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // rightMenu
            // 
            this.rightMenu.Name = "rightMenu";
            resources.ApplyResources(this.rightMenu, "rightMenu");
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_index.ResumeLayout(false);
            this.tabPage_index.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_func;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_index;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_logo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_productVersion;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_Setting;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_Wizard;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_Listening;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_Help;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_Plugins;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_about;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_developerMode;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_Menu;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_ReloadShellType;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_ReloadSetting;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_docs;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_showMsg;
        private System.Windows.Forms.ContextMenuStrip rightMenu;
    }
}

