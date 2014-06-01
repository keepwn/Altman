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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tbx_sql = new System.Windows.Forms.TextBox();
            this.dataGridView_result = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBox_dbs = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_run = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_connect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_disconnect = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_result)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(511, 339);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 32);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_Dbs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(501, 302);
            this.splitContainer1.SplitterDistance = 134;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView_Dbs
            // 
            this.treeView_Dbs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Dbs.ImageIndex = 0;
            this.treeView_Dbs.ImageList = this.imageList1;
            this.treeView_Dbs.Location = new System.Drawing.Point(0, 0);
            this.treeView_Dbs.Name = "treeView_Dbs";
            this.treeView_Dbs.SelectedImageIndex = 0;
            this.treeView_Dbs.ShowLines = false;
            this.treeView_Dbs.ShowRootLines = false;
            this.treeView_Dbs.Size = new System.Drawing.Size(134, 302);
            this.treeView_Dbs.TabIndex = 0;
            this.treeView_Dbs.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Dbs_NodeMouseDoubleClick);
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
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tbx_sql);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView_result);
            this.splitContainer2.Size = new System.Drawing.Size(366, 302);
            this.splitContainer2.SplitterDistance = 110;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // tbx_sql
            // 
            this.tbx_sql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_sql.Location = new System.Drawing.Point(0, 0);
            this.tbx_sql.Multiline = true;
            this.tbx_sql.Name = "tbx_sql";
            this.tbx_sql.Size = new System.Drawing.Size(366, 110);
            this.tbx_sql.TabIndex = 0;
            // 
            // dataGridView_result
            // 
            this.dataGridView_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_result.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_result.Name = "dataGridView_result";
            this.dataGridView_result.RowHeadersWidth = 40;
            this.dataGridView_result.Size = new System.Drawing.Size(366, 191);
            this.dataGridView_result.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_connect,
            this.toolStripButton_disconnect,
            this.toolStripComboBox_dbs,
            this.toolStripSeparator1,
            this.toolStripButton_run,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(2, 2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(507, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripComboBox_dbs
            // 
            this.toolStripComboBox_dbs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox_dbs.Name = "toolStripComboBox_dbs";
            this.toolStripComboBox_dbs.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_run
            // 
            this.toolStripButton_run.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_run.Image")));
            this.toolStripButton_run.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_run.Name = "toolStripButton_run";
            this.toolStripButton_run.Size = new System.Drawing.Size(50, 22);
            this.toolStripButton_run.Text = "Run";
            this.toolStripButton_run.Click += new System.EventHandler(this.toolStripButton_run_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(89, 22);
            this.toolStripButton2.Text = "ShowConnStr";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton_connect
            // 
            this.toolStripButton_connect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_connect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_connect.Image")));
            this.toolStripButton_connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_connect.Name = "toolStripButton_connect";
            this.toolStripButton_connect.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_connect.Text = "Connect";
            this.toolStripButton_connect.Click += new System.EventHandler(this.toolStripButton_connect_Click);
            // 
            // toolStripButton_disconnect
            // 
            this.toolStripButton_disconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_disconnect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_disconnect.Image")));
            this.toolStripButton_disconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_disconnect.Name = "toolStripButton_disconnect";
            this.toolStripButton_disconnect.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_disconnect.Text = "Disconnect";
            this.toolStripButton_disconnect.Click += new System.EventHandler(this.toolStripButton_disconnect_Click);
            // 
            // DbManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DbManagerControl";
            this.Size = new System.Drawing.Size(511, 339);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_result)).EndInit();
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
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton_connect;
        private System.Windows.Forms.ToolStripButton toolStripButton_disconnect;
    }
}
