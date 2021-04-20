

namespace HKG.Module.Metatable
{
    partial class SourcePanel
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
            this.label3 = new System.Windows.Forms.Label();
            this.tbExpression = new System.Windows.Forms.TextBox();
            this.tbAttribute = new System.Windows.Forms.TextBox();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tvSource = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // btnImportYaml
            // 
            this.btnImportYaml.Location = new System.Drawing.Point(21, 23);
            this.btnImportYaml.Name = "btnImportYaml";
            this.btnImportYaml.Size = new System.Drawing.Size(219, 23);
            this.btnImportYaml.TabIndex = 2;
            this.btnImportYaml.Text = "导入YAML文件";
            this.btnImportYaml.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(276, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "目标标签";
            // 
            // tbExpression
            // 
            this.tbExpression.Location = new System.Drawing.Point(374, 88);
            this.tbExpression.Name = "tbExpression";
            this.tbExpression.Size = new System.Drawing.Size(299, 23);
            this.tbExpression.TabIndex = 23;
            // 
            // tbAttribute
            // 
            this.tbAttribute.Location = new System.Drawing.Point(374, 58);
            this.tbAttribute.Name = "tbAttribute";
            this.tbAttribute.Size = new System.Drawing.Size(299, 23);
            this.tbAttribute.TabIndex = 22;
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(374, 28);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(299, 23);
            this.tbAddress.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "语法";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "地址";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(21, 599);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(219, 23);
            this.btnRefresh.TabIndex = 18;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // tvSource
            // 
            this.tvSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvSource.Location = new System.Drawing.Point(21, 52);
            this.tvSource.Name = "tvSource";
            this.tvSource.PathSeparator = "/";
            this.tvSource.Size = new System.Drawing.Size(219, 541);
            this.tvSource.TabIndex = 17;
            this.tvSource.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSchema_AfterSelect);
            // 
            // SourcePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbExpression);
            this.Controls.Add(this.tbAttribute);
            this.Controls.Add(this.tbAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tvSource);
            this.Controls.Add(this.btnImportYaml);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SourcePanel";
            this.Size = new System.Drawing.Size(740, 640);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportYaml;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbExpression;
        private System.Windows.Forms.TextBox tbAttribute;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TreeView tvSource;
    }
}
