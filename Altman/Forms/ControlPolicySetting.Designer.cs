namespace Altman.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPolicySetting));
            this.checkBox_IsParamRandom = new System.Windows.Forms.CheckBox();
            this.checkBox_IsShowDisclaimer = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox_IsParamRandom
            // 
            resources.ApplyResources(this.checkBox_IsParamRandom, "checkBox_IsParamRandom");
            this.checkBox_IsParamRandom.Name = "checkBox_IsParamRandom";
            this.checkBox_IsParamRandom.UseVisualStyleBackColor = true;
            // 
            // checkBox_IsShowDisclaimer
            // 
            resources.ApplyResources(this.checkBox_IsShowDisclaimer, "checkBox_IsShowDisclaimer");
            this.checkBox_IsShowDisclaimer.Name = "checkBox_IsShowDisclaimer";
            this.checkBox_IsShowDisclaimer.UseVisualStyleBackColor = true;
            // 
            // ControlPolicySetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_IsShowDisclaimer);
            this.Controls.Add(this.checkBox_IsParamRandom);
            this.Name = "ControlPolicySetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_IsParamRandom;
        private System.Windows.Forms.CheckBox checkBox_IsShowDisclaimer;
    }
}
