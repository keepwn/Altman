using System.Windows.Forms;

namespace Plugin_ShellManager
{
    partial class ShellManagerControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellManagerControl));
            this.rightMenu_Webshell = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.item_refreshStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.item_copyServerCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.item_add = new System.Windows.Forms.ToolStripMenuItem();
            this.item_alter = new System.Windows.Forms.ToolStripMenuItem();
            this.item_del = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lv_shell = new System.Windows.Forms.ListView();
            this.columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.rightMenu_Webshell.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightMenu_Webshell
            // 
            resources.ApplyResources(this.rightMenu_Webshell, "rightMenu_Webshell");
            this.rightMenu_Webshell.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.item_refreshStatus,
            this.item_copyServerCode,
            this.toolStripSeparator1,
            this.item_add,
            this.item_alter,
            this.item_del,
            this.toolStripSeparator2});
            this.rightMenu_Webshell.Name = "rightMenu_WebShell";
            // 
            // item_refreshStatus
            // 
            resources.ApplyResources(this.item_refreshStatus, "item_refreshStatus");
            this.item_refreshStatus.Name = "item_refreshStatus";
            this.item_refreshStatus.Click += new System.EventHandler(this.item_refreshStatus_Click);
            // 
            // item_copyServerCode
            // 
            resources.ApplyResources(this.item_copyServerCode, "item_copyServerCode");
            this.item_copyServerCode.Name = "item_copyServerCode";
            this.item_copyServerCode.Click += new System.EventHandler(this.item_copyServerCode_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // item_add
            // 
            resources.ApplyResources(this.item_add, "item_add");
            this.item_add.Name = "item_add";
            this.item_add.Click += new System.EventHandler(this.item_add_Click);
            // 
            // item_alter
            // 
            resources.ApplyResources(this.item_alter, "item_alter");
            this.item_alter.Name = "item_alter";
            this.item_alter.Click += new System.EventHandler(this.item_alter_Click);
            // 
            // item_del
            // 
            resources.ApplyResources(this.item_del, "item_del");
            this.item_del.Name = "item_del";
            this.item_del.Click += new System.EventHandler(this.item_del_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // lv_shell
            // 
            resources.ApplyResources(this.lv_shell, "lv_shell");
            this.lv_shell.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.lv_shell.FullRowSelect = true;
            this.lv_shell.GridLines = true;
            this.lv_shell.MultiSelect = false;
            this.lv_shell.Name = "lv_shell";
            this.lv_shell.SmallImageList = this.imageList1;
            this.lv_shell.UseCompatibleStateImageBehavior = false;
            this.lv_shell.View = System.Windows.Forms.View.Details;
            this.lv_shell.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lv_shell_MouseDoubleClick);
            this.lv_shell.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lv_shell_MouseUp);
            // 
            // columnHeader0
            // 
            resources.ApplyResources(this.columnHeader0, "columnHeader0");
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
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // columnHeader7
            // 
            resources.ApplyResources(this.columnHeader7, "columnHeader7");
            // 
            // columnHeader8
            // 
            resources.ApplyResources(this.columnHeader8, "columnHeader8");
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
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.imageList1, "imageList1");
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ShellManagerControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lv_shell);
            this.Name = "ShellManagerControl";
            this.rightMenu_Webshell.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip rightMenu_Webshell;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem item_add;
        private System.Windows.Forms.ToolStripMenuItem item_alter;
        private System.Windows.Forms.ToolStripMenuItem item_del;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ColumnHeader columnHeader0;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ListView lv_shell;
        private ToolStripMenuItem item_refreshStatus;
        private ToolStripMenuItem item_copyServerCode;
        private ImageList imageList1;
    }
}
