
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using XTC.oelMVCS;

namespace HKG.Module.Collector
{
    public partial class DocumentPanel : UserControl
    {
        public class DocumentUiBridge : IDocumentUiBridge
        {
            public DocumentPanel panel { get; set; }

            public object getRootPanel()
            {
                return panel;
            }

            public void Alert(string _message)
            {
                MessageBox.Show(_message);
            }

            public void RefreshList(long _total, object _result)
            {
                panel.btnScrapeAll.Enabled = true;
                panel.btnScrapeNew.Enabled = true;

                panel.btnList.Enabled = true;
                panel.lvDocument.Clear();
                var col = panel.lvDocument.Columns.Add("");
                col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                //panel.lTotal.Text = _total.ToString();
                var result = (Dictionary<string, string>)_result;
                foreach(string key in result.Keys)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = key;
                    item.SubItems[0].Name = result[key];
                    panel.lvDocument.Items.Add(item);
                }
            }

            public void RefreshScrapeProgress(float _value)
            {
                panel.pbScrape.Value = (int)(_value*100);
            }

            public void RefreshScrapeFinish()
            {
                panel.pbScrape.Value = 100;
                panel.btnScrapeAll.Enabled = true;
                panel.btnScrapeNew.Enabled = true;
            }

            public void RefreshDocument(string _rawText)
            {
                panel.wbDocument.DocumentText = _rawText;
            }
        }

        public DocumentFacade facade { get; set; }
        public CrossFacade crossFacade { get; set; }

        public DocumentPanel()
        {
            InitializeComponent();

            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "rootPanel";
            this.TabIndex = 0;
        }

        private void btnScrapeAll_Click(object sender, EventArgs e)
        {
            this.btnScrapeAll.Enabled = false;
            this.btnScrapeNew.Enabled = false;
            var bridge = crossFacade.getViewBridge() as ICrossViewBridge;
            bridge.OnScrapeFromMetatableSubmit();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            this.btnList.Enabled = false;
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnListSubmit(0, 0);
        }

        private void btnScrapeNew_Click(object sender, EventArgs e)
        {
            this.btnScrapeAll.Enabled = false;
            this.btnScrapeNew.Enabled = false;
            MessageBox.Show("暂未实现的功能");
            this.btnScrapeAll.Enabled = true;
            this.btnScrapeNew.Enabled = true;
        }

        private void lvDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvDocument.SelectedItems.Count == 0)
                return;

            string uuid = this.lvDocument.SelectedItems[0].Name;
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnDocumentSelected(uuid);
        }

    }
}
