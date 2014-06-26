namespace Altman
{
    partial class ControlUserAgentSetting
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
            this.checkBox_IsRandom = new System.Windows.Forms.CheckBox();
            this.comboBox_UserAgentList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox_IsRandom
            // 
            this.checkBox_IsRandom.AutoSize = true;
            this.checkBox_IsRandom.Location = new System.Drawing.Point(18, 17);
            this.checkBox_IsRandom.Name = "checkBox_IsRandom";
            this.checkBox_IsRandom.Size = new System.Drawing.Size(120, 16);
            this.checkBox_IsRandom.TabIndex = 0;
            this.checkBox_IsRandom.Text = "User-Agent随机化";
            this.checkBox_IsRandom.UseVisualStyleBackColor = true;
            // 
            // comboBox_UserAgentList
            // 
            this.comboBox_UserAgentList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_UserAgentList.FormattingEnabled = true;
            this.comboBox_UserAgentList.Location = new System.Drawing.Point(18, 80);
            this.comboBox_UserAgentList.Name = "comboBox_UserAgentList";
            this.comboBox_UserAgentList.Size = new System.Drawing.Size(328, 24);
            this.comboBox_UserAgentList.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "User-Agent列表：";
            // 
            // ControlUserAgentSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_UserAgentList);
            this.Controls.Add(this.checkBox_IsRandom);
            this.Name = "ControlUserAgentSetting";
            this.Size = new System.Drawing.Size(365, 286);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_IsRandom;
        private System.Windows.Forms.ComboBox comboBox_UserAgentList;
        private System.Windows.Forms.Label label1;
    }
}
