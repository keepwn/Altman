namespace Altman
{
    partial class Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn_no = new System.Windows.Forms.Button();
            this.btn_yes = new System.Windows.Forms.Button();
            this.checkBox_noDisplay = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(13, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(382, 277);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // btn_no
            // 
            this.btn_no.Location = new System.Drawing.Point(228, 296);
            this.btn_no.Name = "btn_no";
            this.btn_no.Size = new System.Drawing.Size(75, 23);
            this.btn_no.TabIndex = 1;
            this.btn_no.Text = "No";
            this.btn_no.UseVisualStyleBackColor = true;
            this.btn_no.Click += new System.EventHandler(this.btn_no_Click);
            // 
            // btn_yes
            // 
            this.btn_yes.Location = new System.Drawing.Point(320, 296);
            this.btn_yes.Name = "btn_yes";
            this.btn_yes.Size = new System.Drawing.Size(75, 23);
            this.btn_yes.TabIndex = 2;
            this.btn_yes.Text = "Yes";
            this.btn_yes.UseVisualStyleBackColor = true;
            this.btn_yes.Click += new System.EventHandler(this.btn_yes_Click);
            // 
            // checkBox_noDisplay
            // 
            this.checkBox_noDisplay.AutoSize = true;
            this.checkBox_noDisplay.Location = new System.Drawing.Point(13, 300);
            this.checkBox_noDisplay.Name = "checkBox_noDisplay";
            this.checkBox_noDisplay.Size = new System.Drawing.Size(138, 16);
            this.checkBox_noDisplay.TabIndex = 3;
            this.checkBox_noDisplay.Text = "don\'t display again";
            this.checkBox_noDisplay.UseVisualStyleBackColor = true;
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 331);
            this.ControlBox = false;
            this.Controls.Add(this.checkBox_noDisplay);
            this.Controls.Add(this.btn_yes);
            this.Controls.Add(this.btn_no);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Welcome";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "免责申明";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn_no;
        private System.Windows.Forms.Button btn_yes;
        private System.Windows.Forms.CheckBox checkBox_noDisplay;
    }
}