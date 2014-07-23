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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWget));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_wget = new System.Windows.Forms.Button();
            this.textBox_save = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox_url
            // 
            resources.ApplyResources(this.textBox_url, "textBox_url");
            this.textBox_url.Name = "textBox_url";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button_wget
            // 
            resources.ApplyResources(this.button_wget, "button_wget");
            this.button_wget.Name = "button_wget";
            this.button_wget.UseVisualStyleBackColor = true;
            this.button_wget.Click += new System.EventHandler(this.button_wget_Click);
            // 
            // textBox_save
            // 
            resources.ApplyResources(this.textBox_save, "textBox_save");
            this.textBox_save.Name = "textBox_save";
            // 
            // FormWget
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_wget);
            this.Controls.Add(this.textBox_save);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_url);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWget";
            this.ShowIcon = false;
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