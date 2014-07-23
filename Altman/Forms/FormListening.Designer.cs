namespace Altman.Forms
{
    partial class FormListening
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormListening));
            this.textBox_ListenData = new System.Windows.Forms.TextBox();
            this.button_Clear = new System.Windows.Forms.Button();
            this.checkBox_Capture = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox_ListenData
            // 
            this.textBox_ListenData.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_ListenData.Location = new System.Drawing.Point(0, 0);
            this.textBox_ListenData.Multiline = true;
            this.textBox_ListenData.Name = "textBox_ListenData";
            this.textBox_ListenData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_ListenData.Size = new System.Drawing.Size(478, 278);
            this.textBox_ListenData.TabIndex = 0;
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(391, 284);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(75, 23);
            this.button_Clear.TabIndex = 1;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // checkBox_Capture
            // 
            this.checkBox_Capture.AutoSize = true;
            this.checkBox_Capture.Location = new System.Drawing.Point(12, 288);
            this.checkBox_Capture.Name = "checkBox_Capture";
            this.checkBox_Capture.Size = new System.Drawing.Size(66, 16);
            this.checkBox_Capture.TabIndex = 2;
            this.checkBox_Capture.Text = "Capture";
            this.checkBox_Capture.UseVisualStyleBackColor = true;
            this.checkBox_Capture.CheckedChanged += new System.EventHandler(this.checkBox_Capture_CheckedChanged);
            // 
            // FormListening
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 313);
            this.Controls.Add(this.checkBox_Capture);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.textBox_ListenData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormListening";
            this.Text = "FormListening";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ListenData;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.CheckBox checkBox_Capture;
    }
}