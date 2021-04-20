

namespace HKG.Module.Metatable
{
    partial class VocabularyPanel
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnImportYaml = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tvVocabulary = new System.Windows.Forms.TreeView();
            this.lbLabel = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnImportYaml
            // 
            this.btnImportYaml.Location = new System.Drawing.Point(21, 23);
            this.btnImportYaml.Name = "btnImportYaml";
            this.btnImportYaml.Size = new System.Drawing.Size(219, 23);
            this.btnImportYaml.TabIndex = 1;
            this.btnImportYaml.Text = "导入YAML文件";
            this.btnImportYaml.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(21, 599);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(219, 23);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // tvVocabulary
            // 
            this.tvVocabulary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvVocabulary.Location = new System.Drawing.Point(21, 52);
            this.tvVocabulary.Name = "tvVocabulary";
            this.tvVocabulary.PathSeparator = "/";
            this.tvVocabulary.Size = new System.Drawing.Size(219, 541);
            this.tvVocabulary.TabIndex = 19;
            this.tvVocabulary.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvVocabulary_AfterSelect);
            // 
            // lbLabel
            // 
            this.lbLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbLabel.FormattingEnabled = true;
            this.lbLabel.ItemHeight = 17;
            this.lbLabel.Location = new System.Drawing.Point(279, 52);
            this.lbLabel.Name = "lbLabel";
            this.lbLabel.Size = new System.Drawing.Size(216, 531);
            this.lbLabel.TabIndex = 21;
            // 
            // VocabularyPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbLabel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tvVocabulary);
            this.Controls.Add(this.btnImportYaml);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VocabularyPanel";
            this.Size = new System.Drawing.Size(740, 640);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnImportYaml;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TreeView tvVocabulary;
        private System.Windows.Forms.ListBox lbLabel;
    }
}
