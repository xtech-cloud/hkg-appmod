

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
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.rtbTidyText = new System.Windows.Forms.RichTextBox();
            this.tabPageWeb = new System.Windows.Forms.TabPage();
            this.wbDocument = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.lTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.llAddress = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.lLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageText.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tabPageText);
            this.tabControl1.Controls.Add(this.tabPageWeb);
            this.tabControl1.Location = new System.Drawing.Point(244, 83);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(484, 529);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPageText
            // 
            this.tabPageText.Controls.Add(this.rtbTidyText);
            this.tabPageText.Location = new System.Drawing.Point(4, 26);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageText.Size = new System.Drawing.Size(476, 499);
            this.tabPageText.TabIndex = 1;
            this.tabPageText.Text = "结构化文本格式";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // rtbTidyText
            // 
            this.rtbTidyText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbTidyText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbTidyText.Location = new System.Drawing.Point(3, 3);
            this.rtbTidyText.Name = "rtbTidyText";
            this.rtbTidyText.Size = new System.Drawing.Size(470, 491);
            this.rtbTidyText.TabIndex = 0;
            this.rtbTidyText.Text = "";
            // 
            // tabPageWeb
            // 
            this.tabPageWeb.Controls.Add(this.wbDocument);
            this.tabPageWeb.Location = new System.Drawing.Point(4, 26);
            this.tabPageWeb.Name = "tabPageWeb";
            this.tabPageWeb.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWeb.Size = new System.Drawing.Size(476, 499);
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
            this.wbDocument.Size = new System.Drawing.Size(469, 487);
            this.wbDocument.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "采集时间";
            // 
            // lTime
            // 
            this.lTime.AutoSize = true;
            this.lTime.Location = new System.Drawing.Point(307, 10);
            this.lTime.Name = "lTime";
            this.lTime.Size = new System.Drawing.Size(43, 17);
            this.lTime.TabIndex = 8;
            this.lTime.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "采集地址";
            // 
            // llAddress
            // 
            this.llAddress.AutoSize = true;
            this.llAddress.Location = new System.Drawing.Point(307, 31);
            this.llAddress.Name = "llAddress";
            this.llAddress.Size = new System.Drawing.Size(66, 17);
            this.llAddress.TabIndex = 10;
            this.llAddress.TabStop = true;
            this.llAddress.Text = "linkLabel1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "采集标签";
            // 
            // lLabel
            // 
            this.lLabel.AutoSize = true;
            this.lLabel.Location = new System.Drawing.Point(307, 52);
            this.lLabel.Name = "lLabel";
            this.lLabel.Size = new System.Drawing.Size(43, 17);
            this.lLabel.TabIndex = 12;
            this.lLabel.Text = "label5";
            // 
            // DocumentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.llAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DocumentPanel";
            this.Size = new System.Drawing.Size(740, 640);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageText.ResumeLayout(false);
            this.tabPageWeb.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.RichTextBox rtbTidyText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lLabel;
    }
}
