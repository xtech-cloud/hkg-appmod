

namespace HKG.Module.Metatable
{
    partial class SchemaPanel
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
            this.tvSchema = new System.Windows.Forms.TreeView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbField = new System.Windows.Forms.TextBox();
            this.tbText = new System.Windows.Forms.TextBox();
            this.tbElement = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnImportYaml
            // 
            this.btnImportYaml.Location = new System.Drawing.Point(21, 23);
            this.btnImportYaml.Name = "btnImportYaml";
            this.btnImportYaml.Size = new System.Drawing.Size(219, 23);
            this.btnImportYaml.TabIndex = 8;
            this.btnImportYaml.Text = "导入YAML文件";
            this.btnImportYaml.UseVisualStyleBackColor = true;
            // 
            // tvSchema
            // 
            this.tvSchema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvSchema.Location = new System.Drawing.Point(21, 52);
            this.tvSchema.Name = "tvSchema";
            this.tvSchema.PathSeparator = "/";
            this.tvSchema.Size = new System.Drawing.Size(219, 541);
            this.tvSchema.TabIndex = 9;
            this.tvSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSchema_AfterSelect);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(21, 599);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(219, 23);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "存储字段";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(276, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "显示文本";
            // 
            // tbField
            // 
            this.tbField.Location = new System.Drawing.Point(374, 28);
            this.tbField.Name = "tbField";
            this.tbField.Size = new System.Drawing.Size(299, 23);
            this.tbField.TabIndex = 13;
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(374, 58);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(299, 23);
            this.tbText.TabIndex = 14;
            // 
            // tbElement
            // 
            this.tbElement.Location = new System.Drawing.Point(374, 88);
            this.tbElement.Name = "tbElement";
            this.tbElement.Size = new System.Drawing.Size(299, 23);
            this.tbElement.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(276, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "原始文本标签";
            // 
            // SchemaPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbElement);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.tbField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tvSchema);
            this.Controls.Add(this.btnImportYaml);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SchemaPanel";
            this.Size = new System.Drawing.Size(740, 640);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnImportYaml;
        private System.Windows.Forms.TreeView tvSchema;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbField;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.TextBox tbElement;
        private System.Windows.Forms.Label label3;
    }
}
