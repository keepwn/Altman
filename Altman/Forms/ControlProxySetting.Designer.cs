namespace Altman.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlProxySetting));
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
            resources.ApplyResources(this.panel_customProxy, "panel_customProxy");
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
            this.panel_customProxy.Name = "panel_customProxy";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBox_proxyAddr
            // 
            resources.ApplyResources(this.textBox_proxyAddr, "textBox_proxyAddr");
            this.textBox_proxyAddr.Name = "textBox_proxyAddr";
            // 
            // textBox_proxyPort
            // 
            resources.ApplyResources(this.textBox_proxyPort, "textBox_proxyPort");
            this.textBox_proxyPort.Name = "textBox_proxyPort";
            // 
            // textBox_proxyPasswd
            // 
            resources.ApplyResources(this.textBox_proxyPasswd, "textBox_proxyPasswd");
            this.textBox_proxyPasswd.Name = "textBox_proxyPasswd";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBox_proxyUser
            // 
            resources.ApplyResources(this.textBox_proxyUser, "textBox_proxyUser");
            this.textBox_proxyUser.Name = "textBox_proxyUser";
            // 
            // textBox_proxyDomain
            // 
            resources.ApplyResources(this.textBox_proxyDomain, "textBox_proxyDomain");
            this.textBox_proxyDomain.Name = "textBox_proxyDomain";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // radioButton_customProxy
            // 
            resources.ApplyResources(this.radioButton_customProxy, "radioButton_customProxy");
            this.radioButton_customProxy.Name = "radioButton_customProxy";
            this.radioButton_customProxy.UseVisualStyleBackColor = true;
            this.radioButton_customProxy.CheckedChanged += new System.EventHandler(this.radioButton_customProxy_CheckedChanged);
            // 
            // radioButton_ieProxy
            // 
            resources.ApplyResources(this.radioButton_ieProxy, "radioButton_ieProxy");
            this.radioButton_ieProxy.Name = "radioButton_ieProxy";
            this.radioButton_ieProxy.UseVisualStyleBackColor = true;
            this.radioButton_ieProxy.CheckedChanged += new System.EventHandler(this.radioButton_ieProxy_CheckedChanged);
            // 
            // radioButton_noProxy
            // 
            resources.ApplyResources(this.radioButton_noProxy, "radioButton_noProxy");
            this.radioButton_noProxy.Name = "radioButton_noProxy";
            this.radioButton_noProxy.UseVisualStyleBackColor = true;
            this.radioButton_noProxy.CheckedChanged += new System.EventHandler(this.radioButton_noProxy_CheckedChanged);
            // 
            // ControlProxySetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_customProxy);
            this.Controls.Add(this.radioButton_customProxy);
            this.Controls.Add(this.radioButton_ieProxy);
            this.Controls.Add(this.radioButton_noProxy);
            this.Name = "ControlProxySetting";
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
