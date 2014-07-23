namespace Altman.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlUserAgentSetting));
            this.checkBox_IsRandom = new System.Windows.Forms.CheckBox();
            this.comboBox_UserAgentList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox_IsRandom
            // 
            resources.ApplyResources(this.checkBox_IsRandom, "checkBox_IsRandom");
            this.checkBox_IsRandom.Name = "checkBox_IsRandom";
            this.checkBox_IsRandom.UseVisualStyleBackColor = true;
            // 
            // comboBox_UserAgentList
            // 
            resources.ApplyResources(this.comboBox_UserAgentList, "comboBox_UserAgentList");
            this.comboBox_UserAgentList.FormattingEnabled = true;
            this.comboBox_UserAgentList.Name = "comboBox_UserAgentList";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ControlUserAgentSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_UserAgentList);
            this.Controls.Add(this.checkBox_IsRandom);
            this.Name = "ControlUserAgentSetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_IsRandom;
        private System.Windows.Forms.ComboBox comboBox_UserAgentList;
        private System.Windows.Forms.Label label1;
    }
}
