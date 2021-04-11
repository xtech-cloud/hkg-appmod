

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnList = new System.Windows.Forms.Button();
            this.lTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOffset = new System.Windows.Forms.TextBox();
            this.tbCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPage = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPage)).BeginInit();
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnList);
            this.panel1.Controls.Add(this.lTotal);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbOffset);
            this.panel1.Controls.Add(this.tbCount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(310, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(407, 92);
            this.panel1.TabIndex = 7;
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(0, 59);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(150, 23);
            this.btnList.TabIndex = 7;
            this.btnList.Text = "查询";
            this.btnList.UseVisualStyleBackColor = true;
            // 
            // lTotal
            // 
            this.lTotal.AutoSize = true;
            this.lTotal.Location = new System.Drawing.Point(86, 3);
            this.lTotal.Name = "lTotal";
            this.lTotal.Size = new System.Drawing.Size(15, 17);
            this.lTotal.TabIndex = 6;
            this.lTotal.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "数量";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "偏移值";
            // 
            // tbOffset
            // 
            this.tbOffset.Location = new System.Drawing.Point(50, 29);
            this.tbOffset.MaxLength = 10;
            this.tbOffset.Name = "tbOffset";
            this.tbOffset.Size = new System.Drawing.Size(100, 23);
            this.tbOffset.TabIndex = 3;
            this.tbOffset.Text = "0";
            // 
            // tbCount
            // 
            this.tbCount.Location = new System.Drawing.Point(229, 29);
            this.tbCount.MaxLength = 10;
            this.tbCount.Name = "tbCount";
            this.tbCount.Size = new System.Drawing.Size(100, 23);
            this.tbCount.TabIndex = 2;
            this.tbCount.Text = "50";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "云端总记录数";
            // 
            // dgvPage
            // 
            this.dgvPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPage.Location = new System.Drawing.Point(310, 122);
            this.dgvPage.Name = "dgvPage";
            this.dgvPage.RowTemplate.Height = 25;
            this.dgvPage.Size = new System.Drawing.Size(407, 498);
            this.dgvPage.TabIndex = 6;
            // 
            // SourcePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvPage);
            this.Controls.Add(this.btnImportYaml);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SourcePanel";
            this.Size = new System.Drawing.Size(740, 640);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportYaml;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Label lTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbOffset;
        private System.Windows.Forms.TextBox tbCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPage;
    }
}
