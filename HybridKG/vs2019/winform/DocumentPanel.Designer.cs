

namespace HKG.Module.Collector
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
            this.btnScrape = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lvDocument = new System.Windows.Forms.ListView();
            this.btnList = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageWeb = new System.Windows.Forms.TabPage();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnScrape
            // 
            this.btnScrape.Location = new System.Drawing.Point(6, 22);
            this.btnScrape.Name = "btnScrape";
            this.btnScrape.Size = new System.Drawing.Size(195, 23);
            this.btnScrape.TabIndex = 0;
            this.btnScrape.Text = "重新采集";
            this.btnScrape.UseVisualStyleBackColor = true;
            this.btnScrape.Click += new System.EventHandler(this.btnScrape_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 51);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(195, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // lvDocument
            // 
            this.lvDocument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvDocument.HideSelection = false;
            this.lvDocument.Location = new System.Drawing.Point(6, 22);
            this.lvDocument.Name = "lvDocument";
            this.lvDocument.Size = new System.Drawing.Size(195, 432);
            this.lvDocument.TabIndex = 2;
            this.lvDocument.UseCompatibleStateImageBehavior = false;
            this.lvDocument.View = System.Windows.Forms.View.Details;
            // 
            // btnList
            // 
            this.btnList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnList.Location = new System.Drawing.Point(6, 460);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(195, 23);
            this.btnList.TabIndex = 3;
            this.btnList.Text = "刷新";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnScrape);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Location = new System.Drawing.Point(13, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 89);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "词汇元表";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.lvDocument);
            this.groupBox2.Controls.Add(this.btnList);
            this.groupBox2.Location = new System.Drawing.Point(13, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 489);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "词汇原始文本";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageWeb);
            this.tabControl1.Controls.Add(this.tabPageText);
            this.tabControl1.Location = new System.Drawing.Point(244, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(484, 602);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPageWeb
            // 
            this.tabPageWeb.Location = new System.Drawing.Point(4, 26);
            this.tabPageWeb.Name = "tabPageWeb";
            this.tabPageWeb.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWeb.Size = new System.Drawing.Size(476, 572);
            this.tabPageWeb.TabIndex = 0;
            this.tabPageWeb.Text = "网页格式";
            this.tabPageWeb.UseVisualStyleBackColor = true;
            // 
            // tabPageText
            // 
            this.tabPageText.Location = new System.Drawing.Point(4, 26);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageText.Size = new System.Drawing.Size(476, 572);
            this.tabPageText.TabIndex = 1;
            this.tabPageText.Text = "纯文本格式";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // DocumentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DocumentPanel";
            this.Size = new System.Drawing.Size(740, 640);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScrape;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListView lvDocument;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageWeb;
        private System.Windows.Forms.TabPage tabPageText;
    }
}
