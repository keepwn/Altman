namespace Plugin_DbManager
{
    partial class DbManagerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbManagerControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView_Dbs = new System.Windows.Forms.TreeView();
            this.rightMenu_TreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Tsmi_ViewTable = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_CopyName = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tbx_sql = new System.Windows.Forms.TextBox();
            this.dataGridView_result = new System.Windows.Forms.DataGridView();
            this.rightMenu_DataTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Tsmi_SaveAsCsv = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_connect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_disconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox_dbs = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_run = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.rightMenu_TreeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_result)).BeginInit();
            this.rightMenu_DataTable.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.treeView_Dbs);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            // 
            // treeView_Dbs
            // 
            resources.ApplyResources(this.treeView_Dbs, "treeView_Dbs");
            this.treeView_Dbs.ContextMenuStrip = this.rightMenu_TreeView;
            this.treeView_Dbs.ImageList = this.imageList1;
            this.treeView_Dbs.Name = "treeView_Dbs";
            this.treeView_Dbs.ShowLines = false;
            this.treeView_Dbs.ShowRootLines = false;
            this.treeView_Dbs.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Dbs_NodeMouseClick);
            this.treeView_Dbs.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Dbs_NodeMouseDoubleClick);
            // 
            // rightMenu_TreeView
            // 
            resources.ApplyResources(this.rightMenu_TreeView, "rightMenu_TreeView");
            this.rightMenu_TreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tsmi_ViewTable,
            this.Tsmi_CopyName});
            this.rightMenu_TreeView.Name = "rightMenu_TreeView";
            // 
            // Tsmi_ViewTable
            // 
            resources.ApplyResources(this.Tsmi_ViewTable, "Tsmi_ViewTable");
            this.Tsmi_ViewTable.Name = "Tsmi_ViewTable";
            this.Tsmi_ViewTable.Click += new System.EventHandler(this.Tsmi_ViewTable_Click);
            // 
            // Tsmi_CopyName
            // 
            resources.ApplyResources(this.Tsmi_CopyName, "Tsmi_CopyName");
            this.Tsmi_CopyName.Name = "Tsmi_CopyName";
            this.Tsmi_CopyName.Click += new System.EventHandler(this.Tsmi_CopyName_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "0.database_start.ico");
            this.imageList1.Images.SetKeyName(1, "1.database_failed.ico");
            this.imageList1.Images.SetKeyName(2, "2.database.ico");
            this.imageList1.Images.SetKeyName(3, "3.table.ico");
            this.imageList1.Images.SetKeyName(4, "4.table_failed.ico");
            this.imageList1.Images.SetKeyName(5, "5.column.ico");
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            resources.ApplyResources(this.splitContainer2.Panel1, "splitContainer2.Panel1");
            this.splitContainer2.Panel1.Controls.Add(this.tbx_sql);
            // 
            // splitContainer2.Panel2
            // 
            resources.ApplyResources(this.splitContainer2.Panel2, "splitContainer2.Panel2");
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView_result);
            // 
            // tbx_sql
            // 
            resources.ApplyResources(this.tbx_sql, "tbx_sql");
            this.tbx_sql.Name = "tbx_sql";
            // 
            // dataGridView_result
            // 
            resources.ApplyResources(this.dataGridView_result, "dataGridView_result");
            this.dataGridView_result.AllowUserToAddRows = false;
            this.dataGridView_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_result.ContextMenuStrip = this.rightMenu_DataTable;
            this.dataGridView_result.Name = "dataGridView_result";
            // 
            // rightMenu_DataTable
            // 
            resources.ApplyResources(this.rightMenu_DataTable, "rightMenu_DataTable");
            this.rightMenu_DataTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tsmi_SaveAsCsv});
            this.rightMenu_DataTable.Name = "rightMenu_DataTable";
            // 
            // Tsmi_SaveAsCsv
            // 
            resources.ApplyResources(this.Tsmi_SaveAsCsv, "Tsmi_SaveAsCsv");
            this.Tsmi_SaveAsCsv.Name = "Tsmi_SaveAsCsv";
            this.Tsmi_SaveAsCsv.Click += new System.EventHandler(this.Tsmi_SaveAsCsv_Click);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_connect,
            this.toolStripButton_disconnect,
            this.toolStripComboBox_dbs,
            this.toolStripSeparator1,
            this.toolStripButton_run});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton_connect
            // 
            resources.ApplyResources(this.toolStripButton_connect, "toolStripButton_connect");
            this.toolStripButton_connect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_connect.Name = "toolStripButton_connect";
            this.toolStripButton_connect.Click += new System.EventHandler(this.toolStripButton_connect_Click);
            // 
            // toolStripButton_disconnect
            // 
            resources.ApplyResources(this.toolStripButton_disconnect, "toolStripButton_disconnect");
            this.toolStripButton_disconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_disconnect.Name = "toolStripButton_disconnect";
            this.toolStripButton_disconnect.Click += new System.EventHandler(this.toolStripButton_disconnect_Click);
            // 
            // toolStripComboBox_dbs
            // 
            resources.ApplyResources(this.toolStripComboBox_dbs, "toolStripComboBox_dbs");
            this.toolStripComboBox_dbs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox_dbs.Name = "toolStripComboBox_dbs";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripButton_run
            // 
            resources.ApplyResources(this.toolStripButton_run, "toolStripButton_run");
            this.toolStripButton_run.Name = "toolStripButton_run";
            this.toolStripButton_run.Click += new System.EventHandler(this.toolStripButton_run_Click);
            // 
            // DbManagerControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DbManagerControl";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.rightMenu_TreeView.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_result)).EndInit();
            this.rightMenu_DataTable.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_run;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView_Dbs;
        private System.Windows.Forms.TextBox tbx_sql;
        private System.Windows.Forms.DataGridView dataGridView_result;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_dbs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_connect;
        private System.Windows.Forms.ToolStripButton toolStripButton_disconnect;
        private System.Windows.Forms.ContextMenuStrip rightMenu_DataTable;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_SaveAsCsv;
        private System.Windows.Forms.ContextMenuStrip rightMenu_TreeView;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_ViewTable;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_CopyName;
    }
}
