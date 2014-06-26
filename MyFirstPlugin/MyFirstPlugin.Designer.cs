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
            this.lbl_shellData.AutoSize = true;
            this.lbl_shellData.Location = new System.Drawing.Point(24, 16);
            this.lbl_shellData.Name = "lbl_shellData";
            this.lbl_shellData.Size = new System.Drawing.Size(65, 12);
            this.lbl_shellData.TabIndex = 0;
            this.lbl_shellData.Text = "ShellData:";
            // 
            // btn_showMsgInStatus
            // 
            this.btn_showMsgInStatus.Location = new System.Drawing.Point(411, 30);
            this.btn_showMsgInStatus.Name = "btn_showMsgInStatus";
            this.btn_showMsgInStatus.Size = new System.Drawing.Size(131, 23);
            this.btn_showMsgInStatus.TabIndex = 1;
            this.btn_showMsgInStatus.Text = "ShowMsgInStatusBar";
            this.btn_showMsgInStatus.UseVisualStyleBackColor = true;
            this.btn_showMsgInStatus.Click += new System.EventHandler(this.btn_showMsgInStatus_Click);
            // 
            // tbx_msg
            // 
            this.tbx_msg.Location = new System.Drawing.Point(287, 32);
            this.tbx_msg.Name = "tbx_msg";
            this.tbx_msg.Size = new System.Drawing.Size(100, 21);
            this.tbx_msg.TabIndex = 2;
            // 
            // tbx_messagBox
            // 
            this.tbx_messagBox.Location = new System.Drawing.Point(287, 75);
            this.tbx_messagBox.Name = "tbx_messagBox";
            this.tbx_messagBox.Size = new System.Drawing.Size(100, 21);
            this.tbx_messagBox.TabIndex = 3;
            // 
            // btn_showMessageBox
            // 
            this.btn_showMessageBox.Location = new System.Drawing.Point(411, 73);
            this.btn_showMessageBox.Name = "btn_showMessageBox";
            this.btn_showMessageBox.Size = new System.Drawing.Size(131, 23);
            this.btn_showMessageBox.TabIndex = 4;
            this.btn_showMessageBox.Text = "ShowMsgInAppDialog";
            this.btn_showMessageBox.UseVisualStyleBackColor = true;
            this.btn_showMessageBox.Click += new System.EventHandler(this.btn_showMessageBox_Click);
            // 
            // btn_createNewTabPage
            // 
            this.btn_createNewTabPage.Location = new System.Drawing.Point(411, 120);
            this.btn_createNewTabPage.Name = "btn_createNewTabPage";
            this.btn_createNewTabPage.Size = new System.Drawing.Size(131, 23);
            this.btn_createNewTabPage.TabIndex = 5;
            this.btn_createNewTabPage.Text = "CreateNewTabPage";
            this.btn_createNewTabPage.UseVisualStyleBackColor = true;
            this.btn_createNewTabPage.Click += new System.EventHandler(this.btn_createNewTabPage_Click);
            // 
            // tbx_tabPageName
            // 
            this.tbx_tabPageName.Location = new System.Drawing.Point(287, 122);
            this.tbx_tabPageName.Name = "tbx_tabPageName";
            this.tbx_tabPageName.Size = new System.Drawing.Size(100, 21);
            this.tbx_tabPageName.TabIndex = 3;
            // 
            // tbx_shellData
            // 
            this.tbx_shellData.Location = new System.Drawing.Point(26, 32);
            this.tbx_shellData.Multiline = true;
            this.tbx_shellData.Name = "tbx_shellData";
            this.tbx_shellData.Size = new System.Drawing.Size(217, 270);
            this.tbx_shellData.TabIndex = 6;
            // 
            // MyFirstPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
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
            this.Size = new System.Drawing.Size(570, 323);
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
