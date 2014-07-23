using Altman.Controls;

namespace Altman.Forms
{
    partial class ControlRequestHeaderSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlRequestHeaderSetting));
            this.rightMenu_Header = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.item_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.item_add = new System.Windows.Forms.ToolStripMenuItem();
            this.item_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.lv_header = new Altman.Controls.ListViewPlus();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.rightMenu_Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightMenu_Header
            // 
            resources.ApplyResources(this.rightMenu_Header, "rightMenu_Header");
            this.rightMenu_Header.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.item_edit,
            this.item_add,
            this.item_delete});
            this.rightMenu_Header.Name = "rightMenu_Header";
            this.rightMenu_Header.ShowImageMargin = false;
            // 
            // item_edit
            // 
            resources.ApplyResources(this.item_edit, "item_edit");
            this.item_edit.Name = "item_edit";
            this.item_edit.Click += new System.EventHandler(this.item_edit_Click);
            // 
            // item_add
            // 
            resources.ApplyResources(this.item_add, "item_add");
            this.item_add.Name = "item_add";
            this.item_add.Click += new System.EventHandler(this.item_add_Click);
            // 
            // item_delete
            // 
            resources.ApplyResources(this.item_delete, "item_delete");
            this.item_delete.Name = "item_delete";
            this.item_delete.Click += new System.EventHandler(this.item_delete_Click);
            // 
            // lv_header
            // 
            resources.ApplyResources(this.lv_header, "lv_header");
            this.lv_header.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lv_header.ContextMenuStrip = this.rightMenu_Header;
            this.lv_header.FullRowSelect = true;
            this.lv_header.GridLines = true;
            this.lv_header.MultiSelect = false;
            this.lv_header.Name = "lv_header";
            this.lv_header.SmallImageList = this.imageList1;
            this.lv_header.UseCompatibleStateImageBehavior = false;
            this.lv_header.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.imageList1, "imageList1");
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ControlRequestHeaderSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lv_header);
            this.Name = "ControlRequestHeaderSetting";
            this.rightMenu_Header.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip rightMenu_Header;
        private System.Windows.Forms.ToolStripMenuItem item_edit;
        private System.Windows.Forms.ToolStripMenuItem item_add;
        private System.Windows.Forms.ToolStripMenuItem item_delete;
        private ListViewPlus lv_header;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
    }
}
