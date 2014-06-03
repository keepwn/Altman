namespace Plugin_FileManager
{
    partial class FormWget
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_wget = new System.Windows.Forms.Button();
            this.textBox_save = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "url:";
            // 
            // textBox_url
            // 
            this.textBox_url.Location = new System.Drawing.Point(42, 11);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(192, 21);
            this.textBox_url.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "save:";
            // 
            // button_wget
            // 
            this.button_wget.Location = new System.Drawing.Point(243, 11);
            this.button_wget.Name = "button_wget";
            this.button_wget.Size = new System.Drawing.Size(42, 51);
            this.button_wget.TabIndex = 9;
            this.button_wget.Text = "Wget";
            this.button_wget.UseVisualStyleBackColor = true;
            this.button_wget.Click += new System.EventHandler(this.button_wget_Click);
            // 
            // textBox_save
            // 
            this.textBox_save.Location = new System.Drawing.Point(42, 41);
            this.textBox_save.Name = "textBox_save";
            this.textBox_save.Size = new System.Drawing.Size(192, 21);
            this.textBox_save.TabIndex = 8;
            // 
            // FormWget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 76);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_wget);
            this.Controls.Add(this.textBox_save);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_url);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWget";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wget";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_wget;
        private System.Windows.Forms.TextBox textBox_save;
    }
}