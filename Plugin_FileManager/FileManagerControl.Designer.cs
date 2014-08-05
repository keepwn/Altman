using Altman.Controls;

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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView_Dirs = new System.Windows.Forms.TreeView();
            this.imageList_TreeType = new System.Windows.Forms.ImageList(this.components);
            this.imageList_FileType = new System.Windows.Forms.ImageList(this.components);
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Dir = new System.Windows.Forms.Button();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.listView_File = new Altman.Controls.ListViewPlus();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.rightMenu_FileManager.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.tableLayoutPanel1.SetColumnSpan(this.splitContainer1, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.treeView_Dirs);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.listView_File);
            // 
            // treeView_Dirs
            // 
            resources.ApplyResources(this.treeView_Dirs, "treeView_Dirs");
            this.treeView_Dirs.HideSelection = false;
            this.treeView_Dirs.ImageList = this.imageList_TreeType;
            this.treeView_Dirs.Name = "treeView_Dirs";
            this.treeView_Dirs.PathSeparator = "/";
            // 
            // imageList_TreeType
            // 
            this.imageList_TreeType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_TreeType.ImageStream")));
            this.imageList_TreeType.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_TreeType.Images.SetKeyName(0, "drive.ico");
            this.imageList_TreeType.Images.SetKeyName(1, "floder.ico");
            // 
            // imageList_FileType
            // 
            this.imageList_FileType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_FileType.ImageStream")));
            this.imageList_FileType.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_FileType.Images.SetKeyName(0, "0_unknow_32.ico");
            this.imageList_FileType.Images.SetKeyName(1, "1_folder_32.ico");
            this.imageList_FileType.Images.SetKeyName(2, "10_exe_32.ico");
            this.imageList_FileType.Images.SetKeyName(3, "11_dll_32.ico");
            this.imageList_FileType.Images.SetKeyName(4, "12_bat_32.ico");
            this.imageList_FileType.Images.SetKeyName(5, "13_bin_32.ico");
            this.imageList_FileType.Images.SetKeyName(6, "20_zip_32.ico");
            this.imageList_FileType.Images.SetKeyName(7, "21_rar_32.ico");
            this.imageList_FileType.Images.SetKeyName(8, "22_gz_32.ico");
            this.imageList_FileType.Images.SetKeyName(9, "23_tgz_32.ico");
            this.imageList_FileType.Images.SetKeyName(10, "24_cab_32.ico");
            this.imageList_FileType.Images.SetKeyName(11, "30_bmp_32.ico");
            this.imageList_FileType.Images.SetKeyName(12, "31_gif_32.ico");
            this.imageList_FileType.Images.SetKeyName(13, "32_jpeg_32.ico");
            this.imageList_FileType.Images.SetKeyName(14, "33_jpg_32.ico");
            this.imageList_FileType.Images.SetKeyName(15, "34_png_32.ico");
            this.imageList_FileType.Images.SetKeyName(16, "40_rm_32.ico");
            this.imageList_FileType.Images.SetKeyName(17, "41_rmvb_32.ico");
            this.imageList_FileType.Images.SetKeyName(18, "42_mov_32.ico");
            this.imageList_FileType.Images.SetKeyName(19, "43_mp4_32.ico");
            this.imageList_FileType.Images.SetKeyName(20, "44_mpeg_32.ico");
            this.imageList_FileType.Images.SetKeyName(21, "45_mpg_32.ico");
            this.imageList_FileType.Images.SetKeyName(22, "46_flv_32.ico");
            this.imageList_FileType.Images.SetKeyName(23, "47_swf_32.ico");
            this.imageList_FileType.Images.SetKeyName(24, "50_dat_32.ico");
            this.imageList_FileType.Images.SetKeyName(25, "51_doc_32.ico");
            this.imageList_FileType.Images.SetKeyName(26, "52_docx_32.ico");
            this.imageList_FileType.Images.SetKeyName(27, "53_eml_32.ico");
            this.imageList_FileType.Images.SetKeyName(28, "54_ini_32.ico");
            this.imageList_FileType.Images.SetKeyName(29, "55_log_32.ico");
            this.imageList_FileType.Images.SetKeyName(30, "56_mdb_32.ico");
            this.imageList_FileType.Images.SetKeyName(31, "57pdf_32.ico");
            this.imageList_FileType.Images.SetKeyName(32, "58_ppt_32.ico");
            this.imageList_FileType.Images.SetKeyName(33, "59_pptx_32.ico");
            this.imageList_FileType.Images.SetKeyName(34, "60_psd_32.ico");
            this.imageList_FileType.Images.SetKeyName(35, "61_rtf_32.ico");
            this.imageList_FileType.Images.SetKeyName(36, "62_txt_32.ico");
            this.imageList_FileType.Images.SetKeyName(37, "63_wps_32.ico");
            this.imageList_FileType.Images.SetKeyName(38, "64_xls_32.ico");
            this.imageList_FileType.Images.SetKeyName(39, "65_xlsx_32.ico");
            this.imageList_FileType.Images.SetKeyName(40, "80_asa_32.ico");
            this.imageList_FileType.Images.SetKeyName(41, "81_asp_32.ico");
            this.imageList_FileType.Images.SetKeyName(42, "82_aspx_32.ico");
            this.imageList_FileType.Images.SetKeyName(43, "83_c_32.ico");
            this.imageList_FileType.Images.SetKeyName(44, "84_cpp_32.ico");
            this.imageList_FileType.Images.SetKeyName(45, "85_cs.ico");
            this.imageList_FileType.Images.SetKeyName(46, "86_h_32.ico");
            this.imageList_FileType.Images.SetKeyName(47, "87_htm_32.ico");
            this.imageList_FileType.Images.SetKeyName(48, "88_html_32.ico");
            this.imageList_FileType.Images.SetKeyName(49, "89_jar_32.ico");
            this.imageList_FileType.Images.SetKeyName(50, "90_php_32.ico");
            this.imageList_FileType.Images.SetKeyName(51, "91_ruby_32.ico");
            this.imageList_FileType.Images.SetKeyName(52, "92_sln_32.ico");
            this.imageList_FileType.Images.SetKeyName(53, "100_chm_32.ico");
            this.imageList_FileType.Images.SetKeyName(54, "101_iso_32.ico");
            // 
            // rightMenu_FileManager
            // 
            resources.ApplyResources(this.rightMenu_FileManager, "rightMenu_FileManager");
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
            this.rightMenu_FileManager.ShowCheckMargin = true;
            this.rightMenu_FileManager.ShowImageMargin = false;
            // 
            // item_refresh
            // 
            resources.ApplyResources(this.item_refresh, "item_refresh");
            this.item_refresh.Name = "item_refresh";
            this.item_refresh.Click += new System.EventHandler(this.item_refresh_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // item_upload
            // 
            resources.ApplyResources(this.item_upload, "item_upload");
            this.item_upload.Name = "item_upload";
            this.item_upload.Click += new System.EventHandler(this.item_upload_Click);
            // 
            // item_download
            // 
            resources.ApplyResources(this.item_download, "item_download");
            this.item_download.Name = "item_download";
            this.item_download.Click += new System.EventHandler(this.item_download_Click);
            // 
            // item_downloadToServer
            // 
            resources.ApplyResources(this.item_downloadToServer, "item_downloadToServer");
            this.item_downloadToServer.Name = "item_downloadToServer";
            this.item_downloadToServer.Click += new System.EventHandler(this.item_downloadToServer_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // item_del
            // 
            resources.ApplyResources(this.item_del, "item_del");
            this.item_del.Name = "item_del";
            this.item_del.Click += new System.EventHandler(this.item_del_Click);
            // 
            // item_edit
            // 
            resources.ApplyResources(this.item_edit, "item_edit");
            this.item_edit.Name = "item_edit";
            this.item_edit.Click += new System.EventHandler(this.item_edit_Click);
            // 
            // item_copy
            // 
            resources.ApplyResources(this.item_copy, "item_copy");
            this.item_copy.Name = "item_copy";
            this.item_copy.Click += new System.EventHandler(this.item_copy_Click);
            // 
            // item_paste
            // 
            resources.ApplyResources(this.item_paste, "item_paste");
            this.item_paste.Name = "item_paste";
            this.item_paste.Click += new System.EventHandler(this.item_paste_Click);
            // 
            // item_rename
            // 
            resources.ApplyResources(this.item_rename, "item_rename");
            this.item_rename.Name = "item_rename";
            this.item_rename.Click += new System.EventHandler(this.item_rename_Click);
            // 
            // item_modifyTime
            // 
            resources.ApplyResources(this.item_modifyTime, "item_modifyTime");
            this.item_modifyTime.Name = "item_modifyTime";
            this.item_modifyTime.Click += new System.EventHandler(this.item_modifyTime_Click);
            // 
            // item_add
            // 
            resources.ApplyResources(this.item_add, "item_add");
            this.item_add.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.item_createFile,
            this.item_createDir});
            this.item_add.Name = "item_add";
            // 
            // item_createFile
            // 
            resources.ApplyResources(this.item_createFile, "item_createFile");
            this.item_createFile.Name = "item_createFile";
            this.item_createFile.Click += new System.EventHandler(this.item_createFile_Click);
            // 
            // item_createDir
            // 
            resources.ApplyResources(this.item_createDir, "item_createDir");
            this.item_createDir.Name = "item_createDir";
            this.item_createDir.Click += new System.EventHandler(this.item_createDir_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Dir, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_url, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // btn_Dir
            // 
            resources.ApplyResources(this.btn_Dir, "btn_Dir");
            this.btn_Dir.Name = "btn_Dir";
            this.btn_Dir.UseVisualStyleBackColor = true;
            this.btn_Dir.Click += new System.EventHandler(this.btn_Dir_Click);
            // 
            // textBox_url
            // 
            resources.ApplyResources(this.textBox_url, "textBox_url");
            this.textBox_url.Name = "textBox_url";
            // 
            // listView_File
            // 
            resources.ApplyResources(this.listView_File, "listView_File");
            this.listView_File.AllowDrop = true;
            this.listView_File.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView_File.FullRowSelect = true;
            this.listView_File.GridLines = true;
            this.listView_File.MultiSelect = false;
            this.listView_File.Name = "listView_File";
            this.listView_File.SmallImageList = this.imageList_FileType;
            this.listView_File.UseCompatibleStateImageBehavior = false;
            this.listView_File.View = System.Windows.Forms.View.Details;
            this.listView_File.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_File_MouseUp);
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
            // FileManagerControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FileManagerControl";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.rightMenu_FileManager.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Button btn_Dir;
        private System.Windows.Forms.TreeView treeView_Dirs;
        private System.Windows.Forms.ImageList imageList_TreeType;
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
        private System.Windows.Forms.ImageList imageList_FileType;
    }
}
