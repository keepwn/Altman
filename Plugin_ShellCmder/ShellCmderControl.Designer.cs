using Altman.Controls;

namespace Plugin_ShellCmder
{
    partial class ShellCmderControl
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
            this.shellTextBox_Cmder = new Altman.Controls.TextBoxShell();
            this.SuspendLayout();
            // 
            // shellTextBox_Cmder
            // 
            this.shellTextBox_Cmder.AcceptsReturn = true;
            this.shellTextBox_Cmder.AcceptsTab = true;
            this.shellTextBox_Cmder.BackColor = System.Drawing.Color.Black;
            this.shellTextBox_Cmder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellTextBox_Cmder.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shellTextBox_Cmder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.shellTextBox_Cmder.IsWin = true;
            this.shellTextBox_Cmder.Location = new System.Drawing.Point(0, 0);
            this.shellTextBox_Cmder.MaxLength = 0;
            this.shellTextBox_Cmder.Multiline = true;
            this.shellTextBox_Cmder.Name = "shellTextBox_Cmder";
            this.shellTextBox_Cmder.Prompt = ">>> >>> >>> > > > > > > ";
            this.shellTextBox_Cmder.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.shellTextBox_Cmder.ShellTextBackColor = System.Drawing.Color.Black;
            this.shellTextBox_Cmder.ShellTextFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shellTextBox_Cmder.ShellTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.shellTextBox_Cmder.Size = new System.Drawing.Size(544, 293);
            this.shellTextBox_Cmder.TabIndex = 0;
            // 
            // ShellCmderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.shellTextBox_Cmder);
            this.Name = "ShellCmderControl";
            this.Size = new System.Drawing.Size(544, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBoxShell shellTextBox_Cmder;
    }
}
