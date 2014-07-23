namespace Altman.Forms
{
    partial class FormGlobalSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGlobalSetting));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.controlUserAgentSetting1 = new ControlUserAgentSetting();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.controlRequestHeaderSetting1 = new ControlRequestHeaderSetting();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.controlProxySetting1 = new ControlProxySetting();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_SaveSetting = new System.Windows.Forms.Button();
            this.controlPolicySetting1 = new ControlPolicySetting();
            this.tabControl1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.ItemSize = new System.Drawing.Size(60, 18);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(401, 319);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.controlUserAgentSetting1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(393, 293);
            this.tabPage5.TabIndex = 3;
            this.tabPage5.Text = "UserAgent";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // controlUserAgentSetting1
            // 
            this.controlUserAgentSetting1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlUserAgentSetting1.Location = new System.Drawing.Point(0, 0);
            this.controlUserAgentSetting1.Name = "controlUserAgentSetting1";
            this.controlUserAgentSetting1.Size = new System.Drawing.Size(393, 293);
            this.controlUserAgentSetting1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.controlRequestHeaderSetting1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(393, 293);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "HttpHeader";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // controlRequestHeaderSetting1
            // 
            this.controlRequestHeaderSetting1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlRequestHeaderSetting1.Location = new System.Drawing.Point(3, 3);
            this.controlRequestHeaderSetting1.Name = "controlRequestHeaderSetting1";
            this.controlRequestHeaderSetting1.Size = new System.Drawing.Size(387, 287);
            this.controlRequestHeaderSetting1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.controlPolicySetting1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(393, 293);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Policy";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.controlProxySetting1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(393, 293);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Proxy";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // controlProxySetting1
            // 
            this.controlProxySetting1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlProxySetting1.Location = new System.Drawing.Point(3, 3);
            this.controlProxySetting1.Name = "controlProxySetting1";
            this.controlProxySetting1.Size = new System.Drawing.Size(387, 287);
            this.controlProxySetting1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(393, 293);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Contiue";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.Location = new System.Drawing.Point(348, 332);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_SaveSetting
            // 
            this.button_SaveSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SaveSetting.Location = new System.Drawing.Point(238, 332);
            this.button_SaveSetting.Name = "button_SaveSetting";
            this.button_SaveSetting.Size = new System.Drawing.Size(104, 23);
            this.button_SaveSetting.TabIndex = 4;
            this.button_SaveSetting.Text = "SaveAndExit";
            this.button_SaveSetting.UseVisualStyleBackColor = true;
            this.button_SaveSetting.Click += new System.EventHandler(this.button_SaveSetting_Click);
            // 
            // controlPolicySetting1
            // 
            this.controlPolicySetting1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPolicySetting1.Location = new System.Drawing.Point(0, 0);
            this.controlPolicySetting1.Name = "controlPolicySetting1";
            this.controlPolicySetting1.Size = new System.Drawing.Size(393, 293);
            this.controlPolicySetting1.TabIndex = 0;
            // 
            // FormGlobalSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 357);
            this.Controls.Add(this.button_SaveSetting);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGlobalSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGlobalSetting";
            this.tabControl1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button_SaveSetting;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TabPage tabPage3;
        private ControlRequestHeaderSetting controlRequestHeaderSetting1;
        private ControlProxySetting controlProxySetting1;
        private System.Windows.Forms.TabPage tabPage5;
        private ControlUserAgentSetting controlUserAgentSetting1;
        private System.Windows.Forms.TabPage tabPage4;
        private ControlPolicySetting controlPolicySetting1;
    }
}