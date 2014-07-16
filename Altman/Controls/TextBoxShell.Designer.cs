using System.Windows.Forms;

namespace Altman.Controls
{
    partial class TextBoxShell
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
            this.SuspendLayout();
            // 
            // shellTextBox
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ForeColor = System.Drawing.Color.LawnGreen;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaxLength = 0;
            this.Multiline = true;
            this.Name = "ShellTextBox";
            this.AcceptsTab = true;
            this.AcceptsReturn = true;
            this.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Size = new System.Drawing.Size(400, 176);
            this.TabIndex = 0;
            this.Text = "";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ShellTextBox_KeyPress);
            this.KeyDown += new KeyEventHandler(ShellControl_KeyDown);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
