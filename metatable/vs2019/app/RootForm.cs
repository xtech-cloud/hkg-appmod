
using System.Collections.Generic;
using System.Windows.Forms;

namespace app
{
    public class RootForm: Form
    {
        public RootForm()
        {
            InitializeComponent();
        }

        public RichTextBox getLoggerUi()
        {
            return this.rtbLog;
        }

        public TextBox getDomainUi()
        {
            return this.tbDomain;
        }

        private TabControl tcPages;
        private TreeView tvPages;
        private Dictionary<string, TabPage> pages = new Dictionary<string, TabPage>();
        private RichTextBox rtbLog;
        private TextBox tbDomain;

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

        public void AddPath(string _path, object _page)
        {
            ContainerControl page = _page as ContainerControl;
            //this.Controls.Add(panel);
            string[] sections = _path.Split("/");
            var nodes = this.tvPages.Nodes;
            foreach(string section in sections)
            {
                if(string.IsNullOrEmpty(section))
                    continue;
                var found = nodes.Find(section, false);
                if(found.Length == 0)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Name = section;
                    newNode.Text = section;
                    nodes.Add(newNode);
                    found = new TreeNode[]{newNode};
                }
                nodes = found[0].Nodes;
            }
            TabPage tabPage = new TabPage();
            tabPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            tabPage.Location = new System.Drawing.Point(10, 10);
            tabPage.Name = _path;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(760, 660);
            tabPage.TabIndex = 0;
            tabPage.Text = _path;
            tabPage.UseVisualStyleBackColor = true;
            tabPage.Controls.Add(page);
            this.tcPages.Controls.Add(tabPage);
            this.pages[_path] = tabPage;
            this.tvPages.ExpandAll();
        }

        private void InitializeComponent()
        {
            this.tcPages = new System.Windows.Forms.TabControl();
            this.tvPages = new System.Windows.Forms.TreeView();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.tbDomain = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tcPages
            // 
            this.tcPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcPages.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcPages.ItemSize = new System.Drawing.Size(0, 1);
            this.tcPages.Location = new System.Drawing.Point(216, 61);
            this.tcPages.Name = "tcPages";
            this.tcPages.SelectedIndex = 0;
            this.tcPages.Size = new System.Drawing.Size(780, 468);
            this.tcPages.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcPages.TabIndex = 1;
            // 
            // tvPages
            // 
            this.tvPages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvPages.Location = new System.Drawing.Point(13, 20);
            this.tvPages.Name = "tvPages";
            this.tvPages.PathSeparator = "/";
            this.tvPages.Size = new System.Drawing.Size(188, 509);
            this.tvPages.TabIndex = 2;
            this.tvPages.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPages_AfterSelect);
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.Location = new System.Drawing.Point(13, 547);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(983, 170);
            this.rtbLog.TabIndex = 3;
            this.rtbLog.Text = "";
            // 
            // tbDomain
            // 
            this.tbDomain.Location = new System.Drawing.Point(231, 20);
            this.tbDomain.Name = "tbDomain";
            this.tbDomain.Size = new System.Drawing.Size(416, 23);
            this.tbDomain.TabIndex = 4;
            this.tbDomain.Text = "http://localhost:8080";
            // 
            // RootForm
            // 
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.tbDomain);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.tvPages);
            this.Controls.Add(this.tcPages);
            this.Name = "RootForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void tvPages_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count > 0)
                return;
            string fullpath = "/" + node.FullPath;
            TabPage page;
            if (!this.pages.TryGetValue(fullpath, out page))
                return;
            this.tcPages.SelectedTab = page;
        }
    }

}
