using Controls;

namespace Plugin_FileManager
{
    partial class FileManagerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileManagerControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView_Dirs = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView_File = new Controls.ListViewPlus();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rightMenu_FileManager = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.item_refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.item_upload = new System.Windows.Forms.ToolStripMenuItem();
            this.item_download = new System.Windows.Forms.ToolStripMenuItem();
            this.item_downloadToServer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.item_del = new System.Windows.Forms.ToolStripMenuItem();
            this.item_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.item_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.item_paste = new System.Windows.Forms.ToolStripMenuItem();
            this.item_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.item_modifyTime = new System.Windows.Forms.ToolStripMenuItem();
            this.item_add = new System.Windows.Forms.ToolStripMenuItem();
            this.item_createFile = new System.Windows.Forms.ToolStripMenuItem();
            this.item_createDir = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Dir = new System.Windows.Forms.Button();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.rightMenu_FileManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Dir, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_url, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(595, 387);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.splitContainer1, 2);
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 32);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_Dirs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView_File);
            this.splitContainer1.Size = new System.Drawing.Size(589, 352);
            this.splitContainer1.SplitterDistance = 155;
            this.splitContainer1.TabIndex = 4;
            // 
            // treeView_Dirs
            // 
            this.treeView_Dirs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Dirs.HideSelection = false;
            this.treeView_Dirs.ImageIndex = 0;
            this.treeView_Dirs.ImageList = this.imageList1;
            this.treeView_Dirs.Location = new System.Drawing.Point(0, 0);
            this.treeView_Dirs.Name = "treeView_Dirs";
            this.treeView_Dirs.PathSeparator = "/";
            this.treeView_Dirs.SelectedImageIndex = 0;
            this.treeView_Dirs.Size = new System.Drawing.Size(155, 352);
            this.treeView_Dirs.TabIndex = 2;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "disk.ico");
            this.imageList1.Images.SetKeyName(1, "folder.ico");
            this.imageList1.Images.SetKeyName(2, "file.ico");
            // 
            // listView_File
            // 
            this.listView_File.AllowDrop = true;
            this.listView_File.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView_File.ContextMenuStrip = this.rightMenu_FileManager;
            this.listView_File.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_File.FullRowSelect = true;
            this.listView_File.GridLines = true;
            this.listView_File.Location = new System.Drawing.Point(0, 0);
            this.listView_File.MultiSelect = false;
            this.listView_File.Name = "listView_File";
            this.listView_File.Size = new System.Drawing.Size(430, 352);
            this.listView_File.SmallImageList = this.imageList1;
            this.listView_File.TabIndex = 4;
            this.listView_File.UseCompatibleStateImageBehavior = false;
            this.listView_File.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Name";
            this.columnHeader5.Width = 200;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Time";
            this.columnHeader6.Width = 200;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Size";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Attribute";
            // 
            // rightMenu_FileManager
            // 
            this.rightMenu_FileManager.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.item_refresh,
            this.toolStripSeparator1,
            this.item_upload,
            this.item_download,
            this.item_downloadToServer,
            this.toolStripSeparator2,
            this.item_del,
            this.item_edit,
            this.item_copy,
            this.item_paste,
            this.item_rename,
            this.item_modifyTime,
            this.item_add});
            this.rightMenu_FileManager.Name = "rightMenu_FileManager";
            this.rightMenu_FileManager.ShowImageMargin = false;
            this.rightMenu_FileManager.Size = new System.Drawing.Size(166, 258);
            // 
            // item_refresh
            // 
            this.item_refresh.Name = "item_refresh";
            this.item_refresh.Size = new System.Drawing.Size(165, 22);
            this.item_refresh.Text = "refresh";
            this.item_refresh.Click += new System.EventHandler(this.item_refresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // item_upload
            // 
            this.item_upload.Name = "item_upload";
            this.item_upload.Size = new System.Drawing.Size(165, 22);
            this.item_upload.Text = "upload";
            this.item_upload.Click += new System.EventHandler(this.item_upload_Click);
            // 
            // item_download
            // 
            this.item_download.Name = "item_download";
            this.item_download.Size = new System.Drawing.Size(165, 22);
            this.item_download.Text = "download";
            this.item_download.Click += new System.EventHandler(this.item_download_Click);
            // 
            // item_downloadToServer
            // 
            this.item_downloadToServer.Name = "item_downloadToServer";
            this.item_downloadToServer.Size = new System.Drawing.Size(165, 22);
            this.item_downloadToServer.Text = "download to server";
            this.item_downloadToServer.Click += new System.EventHandler(this.item_downloadToServer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(162, 6);
            // 
            // item_del
            // 
            this.item_del.Name = "item_del";
            this.item_del.Size = new System.Drawing.Size(165, 22);
            this.item_del.Text = "delete";
            this.item_del.Click += new System.EventHandler(this.item_del_Click);
            // 
            // item_edit
            // 
            this.item_edit.Name = "item_edit";
            this.item_edit.Size = new System.Drawing.Size(165, 22);
            this.item_edit.Text = "edit";
            this.item_edit.Click += new System.EventHandler(this.item_edit_Click);
            // 
            // item_copy
            // 
            this.item_copy.Name = "item_copy";
            this.item_copy.Size = new System.Drawing.Size(165, 22);
            this.item_copy.Text = "copy";
            this.item_copy.Click += new System.EventHandler(this.item_copy_Click);
            // 
            // item_paste
            // 
            this.item_paste.Name = "item_paste";
            this.item_paste.Size = new System.Drawing.Size(165, 22);
            this.item_paste.Text = "paste";
            this.item_paste.Click += new System.EventHandler(this.item_paste_Click);
            // 
            // item_rename
            // 
            this.item_rename.Name = "item_rename";
            this.item_rename.Size = new System.Drawing.Size(165, 22);
            this.item_rename.Text = "rename";
            this.item_rename.Click += new System.EventHandler(this.item_rename_Click);
            // 
            // item_modifyTime
            // 
            this.item_modifyTime.Name = "item_modifyTime";
            this.item_modifyTime.Size = new System.Drawing.Size(165, 22);
            this.item_modifyTime.Text = "modify time";
            this.item_modifyTime.Click += new System.EventHandler(this.item_modifyTime_Click);
            // 
            // item_add
            // 
            this.item_add.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.item_createFile,
            this.item_createDir});
            this.item_add.Name = "item_add";
            this.item_add.Size = new System.Drawing.Size(165, 22);
            this.item_add.Text = "add";
            // 
            // item_createFile
            // 
            this.item_createFile.Name = "item_createFile";
            this.item_createFile.Size = new System.Drawing.Size(93, 22);
            this.item_createFile.Text = "file";
            this.item_createFile.Click += new System.EventHandler(this.item_createFile_Click);
            // 
            // item_createDir
            // 
            this.item_createDir.Name = "item_createDir";
            this.item_createDir.Size = new System.Drawing.Size(93, 22);
            this.item_createDir.Text = "dir";
            this.item_createDir.Click += new System.EventHandler(this.item_createDir_Click);
            // 
            // btn_Dir
            // 
            this.btn_Dir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Dir.Location = new System.Drawing.Point(538, 3);
            this.btn_Dir.Name = "btn_Dir";
            this.btn_Dir.Size = new System.Drawing.Size(54, 23);
            this.btn_Dir.TabIndex = 1;
            this.btn_Dir.Text = "Dir";
            this.btn_Dir.UseVisualStyleBackColor = true;
            this.btn_Dir.Click += new System.EventHandler(this.btn_Dir_Click);
            // 
            // textBox_url
            // 
            this.textBox_url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_url.Location = new System.Drawing.Point(3, 3);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(529, 21);
            this.textBox_url.TabIndex = 0;
            // 
            // FileManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FileManagerControl";
            this.Size = new System.Drawing.Size(595, 387);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.rightMenu_FileManager.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Button btn_Dir;
        private System.Windows.Forms.TreeView treeView_Dirs;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip rightMenu_FileManager;
        private System.Windows.Forms.ToolStripMenuItem item_del;
        private System.Windows.Forms.ToolStripMenuItem item_edit;
        private System.Windows.Forms.ToolStripMenuItem item_upload;
        private System.Windows.Forms.ToolStripMenuItem item_download;
        private System.Windows.Forms.ToolStripMenuItem item_downloadToServer;
        private System.Windows.Forms.ToolStripMenuItem item_refresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem item_copy;
        private System.Windows.Forms.ToolStripMenuItem item_paste;
        private System.Windows.Forms.ToolStripMenuItem item_rename;
        private System.Windows.Forms.ToolStripMenuItem item_modifyTime;
        private System.Windows.Forms.ToolStripMenuItem item_add;
        private System.Windows.Forms.ToolStripMenuItem item_createFile;
        private System.Windows.Forms.ToolStripMenuItem item_createDir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private ListViewPlus listView_File;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
    }
}
