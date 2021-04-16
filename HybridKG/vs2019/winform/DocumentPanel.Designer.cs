

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
            this.btnScrapeAll = new System.Windows.Forms.Button();
            this.pbScrape = new System.Windows.Forms.ProgressBar();
            this.lvDocument = new System.Windows.Forms.ListView();
            this.btnList = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTidyEmpty = new System.Windows.Forms.Button();
            this.btnTidyAll = new System.Windows.Forms.Button();
            this.btnScrapeEmpty = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageWeb = new System.Windows.Forms.TabPage();
            this.wbDocument = new System.Windows.Forms.WebBrowser();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageWeb.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnScrapeAll
            // 
            this.btnScrapeAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnScrapeAll.Location = new System.Drawing.Point(7, 22);
            this.btnScrapeAll.Name = "btnScrapeAll";
            this.btnScrapeAll.Size = new System.Drawing.Size(85, 23);
            this.btnScrapeAll.TabIndex = 0;
            this.btnScrapeAll.Text = "全量采集";
            this.btnScrapeAll.UseVisualStyleBackColor = true;
            this.btnScrapeAll.Click += new System.EventHandler(this.btnScrapeAll_Click);
            // 
            // pbScrape
            // 
            this.pbScrape.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbScrape.Location = new System.Drawing.Point(6, 81);
            this.pbScrape.Name = "pbScrape";
            this.pbScrape.Size = new System.Drawing.Size(195, 23);
            this.pbScrape.TabIndex = 1;
            // 
            // lvDocument
            // 
            this.lvDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lvDocument.HideSelection = false;
            this.lvDocument.Location = new System.Drawing.Point(7, 112);
            this.lvDocument.Name = "lvDocument";
            this.lvDocument.Size = new System.Drawing.Size(195, 452);
            this.lvDocument.TabIndex = 2;
            this.lvDocument.UseCompatibleStateImageBehavior = false;
            this.lvDocument.View = System.Windows.Forms.View.Details;
            this.lvDocument.SelectedIndexChanged += new System.EventHandler(this.lvDocument_SelectedIndexChanged);
            // 
            // btnList
            // 
            this.btnList.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnList.Location = new System.Drawing.Point(7, 570);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(195, 23);
            this.btnList.TabIndex = 3;
            this.btnList.Text = "刷新";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btnTidyEmpty);
            this.groupBox1.Controls.Add(this.btnTidyAll);
            this.groupBox1.Controls.Add(this.btnList);
            this.groupBox1.Controls.Add(this.lvDocument);
            this.groupBox1.Controls.Add(this.btnScrapeEmpty);
            this.groupBox1.Controls.Add(this.btnScrapeAll);
            this.groupBox1.Controls.Add(this.pbScrape);
            this.groupBox1.Location = new System.Drawing.Point(13, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 602);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "词汇表";
            // 
            // btnTidyEmpty
            // 
            this.btnTidyEmpty.Location = new System.Drawing.Point(116, 51);
            this.btnTidyEmpty.Name = "btnTidyEmpty";
            this.btnTidyEmpty.Size = new System.Drawing.Size(86, 23);
            this.btnTidyEmpty.TabIndex = 5;
            this.btnTidyEmpty.Text = "增量结构化";
            this.btnTidyEmpty.UseVisualStyleBackColor = true;
            this.btnTidyEmpty.Click += new System.EventHandler(this.btnTidyEmpty_Click);
            // 
            // btnTidyAll
            // 
            this.btnTidyAll.Location = new System.Drawing.Point(116, 22);
            this.btnTidyAll.Name = "btnTidyAll";
            this.btnTidyAll.Size = new System.Drawing.Size(85, 23);
            this.btnTidyAll.TabIndex = 4;
            this.btnTidyAll.Text = "全量结构化";
            this.btnTidyAll.UseVisualStyleBackColor = true;
            this.btnTidyAll.Click += new System.EventHandler(this.btnTidyAll_Click);
            // 
            // btnScrapeEmpty
            // 
            this.btnScrapeEmpty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnScrapeEmpty.Location = new System.Drawing.Point(7, 51);
            this.btnScrapeEmpty.Name = "btnScrapeEmpty";
            this.btnScrapeEmpty.Size = new System.Drawing.Size(85, 23);
            this.btnScrapeEmpty.TabIndex = 2;
            this.btnScrapeEmpty.Text = "增量采集";
            this.btnScrapeEmpty.UseVisualStyleBackColor = true;
            this.btnScrapeEmpty.Click += new System.EventHandler(this.btnScrapeEmpty_Click);
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
            this.tabPageWeb.Controls.Add(this.wbDocument);
            this.tabPageWeb.Location = new System.Drawing.Point(4, 26);
            this.tabPageWeb.Name = "tabPageWeb";
            this.tabPageWeb.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWeb.Size = new System.Drawing.Size(476, 572);
            this.tabPageWeb.TabIndex = 0;
            this.tabPageWeb.Text = "网页格式";
            this.tabPageWeb.UseVisualStyleBackColor = true;
            // 
            // wbDocument
            // 
            this.wbDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbDocument.Location = new System.Drawing.Point(4, 4);
            this.wbDocument.Name = "wbDocument";
            this.wbDocument.Size = new System.Drawing.Size(469, 565);
            this.wbDocument.TabIndex = 0;
            // 
            // tabPageText
            // 
            this.tabPageText.Location = new System.Drawing.Point(4, 26);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageText.Size = new System.Drawing.Size(476, 572);
            this.tabPageText.TabIndex = 1;
            this.tabPageText.Text = "结构化文本格式";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // DocumentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DocumentPanel";
            this.Size = new System.Drawing.Size(740, 640);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageWeb.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScrapeAll;
        private System.Windows.Forms.ProgressBar pbScrape;
        private System.Windows.Forms.ListView lvDocument;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageWeb;
        private System.Windows.Forms.TabPage tabPageText;
        private System.Windows.Forms.Button btnScrapeEmpty;
        private System.Windows.Forms.WebBrowser wbDocument;
        private System.Windows.Forms.Button btnTidyEmpty;
        private System.Windows.Forms.Button btnTidyAll;
    }
}
