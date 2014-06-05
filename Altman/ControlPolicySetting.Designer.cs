namespace Altman
{
    partial class ControlPolicySetting
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
            this.checkBox_IsParamRandom = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_IsShowDisclaimer = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox_IsParamRandom
            // 
            this.checkBox_IsParamRandom.AutoSize = true;
            this.checkBox_IsParamRandom.Location = new System.Drawing.Point(27, 58);
            this.checkBox_IsParamRandom.Name = "checkBox_IsParamRandom";
            this.checkBox_IsParamRandom.Size = new System.Drawing.Size(180, 16);
            this.checkBox_IsParamRandom.TabIndex = 0;
            this.checkBox_IsParamRandom.Text = "发包的参数自动改为随机字符";
            this.checkBox_IsParamRandom.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "发包策略";
            // 
            // checkBox_IsShowDisclaimer
            // 
            this.checkBox_IsShowDisclaimer.AutoSize = true;
            this.checkBox_IsShowDisclaimer.Location = new System.Drawing.Point(27, 134);
            this.checkBox_IsShowDisclaimer.Name = "checkBox_IsShowDisclaimer";
            this.checkBox_IsShowDisclaimer.Size = new System.Drawing.Size(120, 16);
            this.checkBox_IsShowDisclaimer.TabIndex = 2;
            this.checkBox_IsShowDisclaimer.Text = "是否显示免责声明";
            this.checkBox_IsShowDisclaimer.UseVisualStyleBackColor = true;
            // 
            // ControlPolicySetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_IsShowDisclaimer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_IsParamRandom);
            this.Name = "ControlPolicySetting";
            this.Size = new System.Drawing.Size(276, 264);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_IsParamRandom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_IsShowDisclaimer;
    }
}
