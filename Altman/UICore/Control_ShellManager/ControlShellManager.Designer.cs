using System.Windows.Forms;

namespace Altman.UICore.Control_ShellManager
{
    partial class ControlShellManager
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
            this.rightMenu_Webshell = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.rightMenu_Webshell.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightMenu_Webshell
            // 
            this.rightMenu_Webshell.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.item_add,
            this.item_alter,
            this.item_del,
            this.toolStripSeparator2});
            this.rightMenu_Webshell.Name = "rightMenu_WebShell";
            this.rightMenu_Webshell.Size = new System.Drawing.Size(114, 82);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(110, 6);
            // 
            // item_add
            // 
            this.item_add.Name = "item_add";
            this.item_add.Size = new System.Drawing.Size(113, 22);
            this.item_add.Text = "Add";
            this.item_add.Click += new System.EventHandler(this.item_add_Click);
            // 
            // item_alter
            // 
            this.item_alter.Name = "item_alter";
            this.item_alter.Size = new System.Drawing.Size(113, 22);
            this.item_alter.Text = "Edit";
            this.item_alter.Click += new System.EventHandler(this.item_alter_Click);
            // 
            // item_del
            // 
            this.item_del.Name = "item_del";
            this.item_del.Size = new System.Drawing.Size(113, 22);
            this.item_del.Text = "Delete";
            this.item_del.Click += new System.EventHandler(this.item_del_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(110, 6);
            // 
            // lv_shell
            // 
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
            this.lv_shell.ContextMenuStrip = this.rightMenu_Webshell;
            this.lv_shell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_shell.FullRowSelect = true;
            this.lv_shell.GridLines = true;
            this.lv_shell.Location = new System.Drawing.Point(0, 0);
            this.lv_shell.MultiSelect = false;
            this.lv_shell.Name = "lv_shell";
            this.lv_shell.Size = new System.Drawing.Size(847, 405);
            this.lv_shell.TabIndex = 1;
            this.lv_shell.UseCompatibleStateImageBehavior = false;
            this.lv_shell.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader0
            // 
            this.columnHeader0.Text = "Id";
            this.columnHeader0.Width = 0;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "TargetNo";
            this.columnHeader2.Width = 98;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Level";
            this.columnHeader3.Width = 45;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Status";
            this.columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ShellUrl";
            this.columnHeader5.Width = 200;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Type";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "ServerCode";
            this.columnHeader8.Width = 75;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Area";
            this.columnHeader9.Width = 58;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Remark";
            this.columnHeader10.Width = 100;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "AddTime";
            this.columnHeader11.Width = 66;
            // 
            // ControlShellManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lv_shell);
            this.Name = "ControlShellManager";
            this.Size = new System.Drawing.Size(847, 405);
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
    }
}
