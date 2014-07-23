namespace MyFirstPlugin
{
    partial class MyFirstPlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyFirstPlugin));
            this.lbl_shellData = new System.Windows.Forms.Label();
            this.btn_showMsgInStatus = new System.Windows.Forms.Button();
            this.tbx_msg = new System.Windows.Forms.TextBox();
            this.tbx_messagBox = new System.Windows.Forms.TextBox();
            this.btn_showMessageBox = new System.Windows.Forms.Button();
            this.btn_createNewTabPage = new System.Windows.Forms.Button();
            this.tbx_tabPageName = new System.Windows.Forms.TextBox();
            this.tbx_shellData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_shellData
            // 
            resources.ApplyResources(this.lbl_shellData, "lbl_shellData");
            this.lbl_shellData.Name = "lbl_shellData";
            // 
            // btn_showMsgInStatus
            // 
            resources.ApplyResources(this.btn_showMsgInStatus, "btn_showMsgInStatus");
            this.btn_showMsgInStatus.Name = "btn_showMsgInStatus";
            this.btn_showMsgInStatus.UseVisualStyleBackColor = true;
            this.btn_showMsgInStatus.Click += new System.EventHandler(this.btn_showMsgInStatus_Click);
            // 
            // tbx_msg
            // 
            resources.ApplyResources(this.tbx_msg, "tbx_msg");
            this.tbx_msg.Name = "tbx_msg";
            // 
            // tbx_messagBox
            // 
            resources.ApplyResources(this.tbx_messagBox, "tbx_messagBox");
            this.tbx_messagBox.Name = "tbx_messagBox";
            // 
            // btn_showMessageBox
            // 
            resources.ApplyResources(this.btn_showMessageBox, "btn_showMessageBox");
            this.btn_showMessageBox.Name = "btn_showMessageBox";
            this.btn_showMessageBox.UseVisualStyleBackColor = true;
            this.btn_showMessageBox.Click += new System.EventHandler(this.btn_showMessageBox_Click);
            // 
            // btn_createNewTabPage
            // 
            resources.ApplyResources(this.btn_createNewTabPage, "btn_createNewTabPage");
            this.btn_createNewTabPage.Name = "btn_createNewTabPage";
            this.btn_createNewTabPage.UseVisualStyleBackColor = true;
            this.btn_createNewTabPage.Click += new System.EventHandler(this.btn_createNewTabPage_Click);
            // 
            // tbx_tabPageName
            // 
            resources.ApplyResources(this.tbx_tabPageName, "tbx_tabPageName");
            this.tbx_tabPageName.Name = "tbx_tabPageName";
            // 
            // tbx_shellData
            // 
            resources.ApplyResources(this.tbx_shellData, "tbx_shellData");
            this.tbx_shellData.Name = "tbx_shellData";
            // 
            // MyFirstPlugin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tbx_shellData);
            this.Controls.Add(this.btn_createNewTabPage);
            this.Controls.Add(this.btn_showMessageBox);
            this.Controls.Add(this.tbx_tabPageName);
            this.Controls.Add(this.tbx_messagBox);
            this.Controls.Add(this.tbx_msg);
            this.Controls.Add(this.btn_showMsgInStatus);
            this.Controls.Add(this.lbl_shellData);
            this.Name = "MyFirstPlugin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_shellData;
        private System.Windows.Forms.Button btn_showMsgInStatus;
        private System.Windows.Forms.TextBox tbx_msg;
        private System.Windows.Forms.TextBox tbx_messagBox;
        private System.Windows.Forms.Button btn_showMessageBox;
        private System.Windows.Forms.Button btn_createNewTabPage;
        private System.Windows.Forms.TextBox tbx_tabPageName;
        private System.Windows.Forms.TextBox tbx_shellData;

    }
}
