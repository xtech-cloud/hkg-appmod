

namespace HKG.Module.Metatable
{
    partial class FormatPanel
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
            this.tbFrom = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tvFormat = new System.Windows.Forms.TreeView();
            this.btnImportYaml = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbFrom
            // 
            this.tbFrom.Location = new System.Drawing.Point(367, 28);
            this.tbFrom.Name = "tbFrom";
            this.tbFrom.Size = new System.Drawing.Size(246, 23);
            this.tbFrom.TabIndex = 28;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(21, 597);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(219, 23);
            this.btnRefresh.TabIndex = 25;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // tvBranch
            // 
            this.tvFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvFormat.Location = new System.Drawing.Point(21, 50);
            this.tvFormat.Name = "tvBranch";
            this.tvFormat.PathSeparator = "/";
            this.tvFormat.Size = new System.Drawing.Size(219, 541);
            this.tvFormat.TabIndex = 24;
            this.tvFormat.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvBranch_AfterSelect);
            // 
            // btnImportYaml
            // 
            this.btnImportYaml.Location = new System.Drawing.Point(21, 23);
            this.btnImportYaml.Name = "btnImportYaml";
            this.btnImportYaml.Size = new System.Drawing.Size(219, 23);
            this.btnImportYaml.TabIndex = 23;
            this.btnImportYaml.Text = "导入YAML文件";
            this.btnImportYaml.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 26;
            this.label1.Text = "别名列表";
            // 
            // BranchPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tvFormat);
            this.Controls.Add(this.btnImportYaml);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BranchPanel";
            this.Size = new System.Drawing.Size(740, 640);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TreeView tvFormat;
        private System.Windows.Forms.Button btnImportYaml;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFrom;
    }
}
