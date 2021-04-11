
using System;
using System.Collections.Generic;
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
                    panel.lvDocument.Items.Add(item);
                }
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

        private void btnScrape_Click(object sender, EventArgs e)
        {
            var bridge = crossFacade.getViewBridge() as ICrossViewBridge;
            bridge.OnScrapeFromMetatableSubmit();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            this.btnList.Enabled = false;
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnListSubmit(0, 0);
        }
    }
}
