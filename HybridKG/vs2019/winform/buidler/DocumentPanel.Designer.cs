

namespace HKG.Module.Builder
{
    partial class DocumentPanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnList = new System.Windows.Forms.Button();
            this.lvDocument = new System.Windows.Forms.ListView();
            this.btnFormatEmpty = new System.Windows.Forms.Button();
            this.btnFormatAll = new System.Windows.Forms.Button();
            this.pbBuild = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.cbFormat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnList);
            this.groupBox1.Controls.Add(this.lvDocument);
            this.groupBox1.Controls.Add(this.btnFormatEmpty);
            this.groupBox1.Controls.Add(this.btnFormatAll);
            this.groupBox1.Controls.Add(this.pbBuild);
            this.groupBox1.Location = new System.Drawing.Point(13, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 602);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "词汇表";
            // 
            // btnList
            // 
            this.btnList.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnList.Location = new System.Drawing.Point(6, 573);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(195, 23);
            this.btnList.TabIndex = 3;
            this.btnList.Text = "刷新";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // lvDocument
            // 
            this.lvDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lvDocument.HideSelection = false;
            this.lvDocument.Location = new System.Drawing.Point(6, 110);
            this.lvDocument.Name = "lvDocument";
            this.lvDocument.Size = new System.Drawing.Size(195, 457);
            this.lvDocument.TabIndex = 2;
            this.lvDocument.UseCompatibleStateImageBehavior = false;
            this.lvDocument.View = System.Windows.Forms.View.Details;
            this.lvDocument.SelectedIndexChanged += new System.EventHandler(this.lvDocument_SelectedIndexChanged);
            // 
            // btnFormatEmpty
            // 
            this.btnFormatEmpty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFormatEmpty.Location = new System.Drawing.Point(116, 52);
            this.btnFormatEmpty.Name = "btnFormatEmpty";
            this.btnFormatEmpty.Size = new System.Drawing.Size(85, 23);
            this.btnFormatEmpty.TabIndex = 2;
            this.btnFormatEmpty.Text = "增量格式化";
            this.btnFormatEmpty.UseVisualStyleBackColor = true;
            this.btnFormatEmpty.Click += new System.EventHandler(this.btnFormatEmpty_Click);
            // 
            // btnFormatAll
            // 
            this.btnFormatAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFormatAll.Location = new System.Drawing.Point(6, 52);
            this.btnFormatAll.Name = "btnFormatAll";
            this.btnFormatAll.Size = new System.Drawing.Size(85, 23);
            this.btnFormatAll.TabIndex = 0;
            this.btnFormatAll.Text = "全量格式化";
            this.btnFormatAll.UseVisualStyleBackColor = true;
            this.btnFormatAll.Click += new System.EventHandler(this.btnFormatAll_Click);
            // 
            // pbBuild
            // 
            this.pbBuild.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbBuild.Location = new System.Drawing.Point(6, 81);
            this.pbBuild.Name = "pbBuild";
            this.pbBuild.Size = new System.Drawing.Size(195, 23);
            this.pbBuild.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "格式";
            // 
            // cbFormat
            // 
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Location = new System.Drawing.Point(44, 21);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(157, 25);
            this.cbFormat.TabIndex = 5;
            // 
            // DocumentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DocumentPanel";
            this.Size = new System.Drawing.Size(740, 640);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.ListView lvDocument;
        private System.Windows.Forms.Button btnFormatEmpty;
        private System.Windows.Forms.Button btnFormatAll;
        private System.Windows.Forms.ProgressBar pbBuild;
        private System.Windows.Forms.ComboBox cbFormat;
        private System.Windows.Forms.Label label1;
    }
}
