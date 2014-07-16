namespace Altman.UI
{
    partial class ControlProxySetting
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
            this.panel_customProxy = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_proxyAddr = new System.Windows.Forms.TextBox();
            this.textBox_proxyPort = new System.Windows.Forms.TextBox();
            this.textBox_proxyPasswd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_proxyUser = new System.Windows.Forms.TextBox();
            this.textBox_proxyDomain = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButton_customProxy = new System.Windows.Forms.RadioButton();
            this.radioButton_ieProxy = new System.Windows.Forms.RadioButton();
            this.radioButton_noProxy = new System.Windows.Forms.RadioButton();
            this.panel_customProxy.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_customProxy
            // 
            this.panel_customProxy.Controls.Add(this.label4);
            this.panel_customProxy.Controls.Add(this.textBox_proxyAddr);
            this.panel_customProxy.Controls.Add(this.textBox_proxyPort);
            this.panel_customProxy.Controls.Add(this.textBox_proxyPasswd);
            this.panel_customProxy.Controls.Add(this.label7);
            this.panel_customProxy.Controls.Add(this.label6);
            this.panel_customProxy.Controls.Add(this.label3);
            this.panel_customProxy.Controls.Add(this.textBox_proxyUser);
            this.panel_customProxy.Controls.Add(this.textBox_proxyDomain);
            this.panel_customProxy.Controls.Add(this.label5);
            this.panel_customProxy.Location = new System.Drawing.Point(25, 87);
            this.panel_customProxy.Name = "panel_customProxy";
            this.panel_customProxy.Size = new System.Drawing.Size(294, 130);
            this.panel_customProxy.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "User:";
            // 
            // textBox_proxyAddr
            // 
            this.textBox_proxyAddr.Location = new System.Drawing.Point(55, 17);
            this.textBox_proxyAddr.Name = "textBox_proxyAddr";
            this.textBox_proxyAddr.Size = new System.Drawing.Size(100, 21);
            this.textBox_proxyAddr.TabIndex = 3;
            // 
            // textBox_proxyPort
            // 
            this.textBox_proxyPort.Location = new System.Drawing.Point(208, 17);
            this.textBox_proxyPort.Name = "textBox_proxyPort";
            this.textBox_proxyPort.Size = new System.Drawing.Size(75, 21);
            this.textBox_proxyPort.TabIndex = 4;
            // 
            // textBox_proxyPasswd
            // 
            this.textBox_proxyPasswd.Location = new System.Drawing.Point(208, 53);
            this.textBox_proxyPasswd.Name = "textBox_proxyPasswd";
            this.textBox_proxyPasswd.Size = new System.Drawing.Size(75, 21);
            this.textBox_proxyPasswd.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(164, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "Passwd:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(164, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Server:";
            // 
            // textBox_proxyUser
            // 
            this.textBox_proxyUser.Location = new System.Drawing.Point(55, 50);
            this.textBox_proxyUser.Name = "textBox_proxyUser";
            this.textBox_proxyUser.Size = new System.Drawing.Size(100, 21);
            this.textBox_proxyUser.TabIndex = 3;
            // 
            // textBox_proxyDomain
            // 
            this.textBox_proxyDomain.Location = new System.Drawing.Point(55, 83);
            this.textBox_proxyDomain.Name = "textBox_proxyDomain";
            this.textBox_proxyDomain.Size = new System.Drawing.Size(100, 21);
            this.textBox_proxyDomain.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Domain:";
            // 
            // radioButton_customProxy
            // 
            this.radioButton_customProxy.AutoSize = true;
            this.radioButton_customProxy.Location = new System.Drawing.Point(25, 61);
            this.radioButton_customProxy.Name = "radioButton_customProxy";
            this.radioButton_customProxy.Size = new System.Drawing.Size(173, 16);
            this.radioButton_customProxy.TabIndex = 6;
            this.radioButton_customProxy.Text = "Use Custom Proxy Settings";
            this.radioButton_customProxy.UseVisualStyleBackColor = true;
            this.radioButton_customProxy.CheckedChanged += new System.EventHandler(this.radioButton_customProxy_CheckedChanged);
            // 
            // radioButton_ieProxy
            // 
            this.radioButton_ieProxy.AutoSize = true;
            this.radioButton_ieProxy.Location = new System.Drawing.Point(25, 39);
            this.radioButton_ieProxy.Name = "radioButton_ieProxy";
            this.radioButton_ieProxy.Size = new System.Drawing.Size(173, 16);
            this.radioButton_ieProxy.TabIndex = 5;
            this.radioButton_ieProxy.Text = "Use System Proxy Settings";
            this.radioButton_ieProxy.UseVisualStyleBackColor = true;
            this.radioButton_ieProxy.CheckedChanged += new System.EventHandler(this.radioButton_ieProxy_CheckedChanged);
            // 
            // radioButton_noProxy
            // 
            this.radioButton_noProxy.AutoSize = true;
            this.radioButton_noProxy.Location = new System.Drawing.Point(25, 17);
            this.radioButton_noProxy.Name = "radioButton_noProxy";
            this.radioButton_noProxy.Size = new System.Drawing.Size(101, 16);
            this.radioButton_noProxy.TabIndex = 4;
            this.radioButton_noProxy.Text = "Not Use Proxy";
            this.radioButton_noProxy.UseVisualStyleBackColor = true;
            this.radioButton_noProxy.CheckedChanged += new System.EventHandler(this.radioButton_noProxy_CheckedChanged);
            // 
            // ControlProxySetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_customProxy);
            this.Controls.Add(this.radioButton_customProxy);
            this.Controls.Add(this.radioButton_ieProxy);
            this.Controls.Add(this.radioButton_noProxy);
            this.Name = "ControlProxySetting";
            this.Size = new System.Drawing.Size(355, 267);
            this.panel_customProxy.ResumeLayout(false);
            this.panel_customProxy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_customProxy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_proxyAddr;
        private System.Windows.Forms.TextBox textBox_proxyPort;
        private System.Windows.Forms.TextBox textBox_proxyPasswd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_proxyUser;
        private System.Windows.Forms.TextBox textBox_proxyDomain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton_customProxy;
        private System.Windows.Forms.RadioButton radioButton_ieProxy;
        private System.Windows.Forms.RadioButton radioButton_noProxy;
    }
}
