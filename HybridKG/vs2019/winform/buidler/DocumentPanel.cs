
using System;
using System.Text.Json;
using System.Windows.Forms;
using System.Collections.Generic;
using XTC.oelMVCS;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace HKG.Module.Builder
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
                panel.btnFormatAll.Enabled = true;
                panel.btnFormatEmpty.Enabled = true;

                panel.btnList.Enabled = true;
                panel.lvDocument.Clear();
                var col = panel.lvDocument.Columns.Add("");
                col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                //panel.lTotal.Text = _total.ToString();
                var result = (Dictionary<string, string>)_result;
                foreach (string key in result.Keys)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = key;
                    item.SubItems[0].Name = result[key];
                    panel.lvDocument.Items.Add(item);
                }
            }

            public void RefreshProgress(float _value)
            {
                panel.pbBuild.Value = (int)(_value * 100);
            }

            public void RefreshFinish()
            {
                panel.pbBuild.Value = 100;
                panel.btnFormatAll.Enabled = true;
                panel.btnFormatEmpty.Enabled = true;
            }

            public void RefreshDocument(object _doc)
            {
                Dictionary<string, string> document = (Dictionary<string, string>)_doc;
                string html = HtmlTemplate.RenderTable(document["text"], (_ex) =>
                {
                });
                panel.lTime.Text = document["updatedAt"];
                panel.lLabel.Text = document["label"];
                panel.wbDocument.DocumentText = html;
                panel.rtbText.Text = prettyJson(document["text"]);
            }

            private string prettyJson(string unPrettyJson)
            {
                if (string.IsNullOrEmpty(unPrettyJson))
                    return "";
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };
                options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

                var jsonElement = JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

                return JsonSerializer.Serialize(jsonElement, options);
            }

            public void RefreshFormatList(object _formats)
            {
                List<string> formats = (List<string>) _formats;
                panel.cbFormat.Items.Clear();
                panel.cbFormat.Items.AddRange(formats.ToArray());
                panel.cbFormat.SelectedIndex = 0;
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

        private void btnFormatAll_Click(object sender, EventArgs e)
        {
            this.btnFormatAll.Enabled = false;
            this.btnFormatEmpty.Enabled = false;
            var bridge = crossFacade.getViewBridge() as ICrossViewBridge;
            bridge.OnMergeFromMetatableSubmit(this.cbFormat.Text);
        }

        private void btnFormatEmpty_Click(object sender, EventArgs e)
        {
            this.btnFormatAll.Enabled = false;
            this.btnFormatEmpty.Enabled = false;
            MessageBox.Show("暂未实现的功能");
            this.btnFormatAll.Enabled = true;
            this.btnFormatEmpty.Enabled = true;
        }

        private void lvDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvDocument.SelectedItems.Count == 0)
                return;

            string uuid = this.lvDocument.SelectedItems[0].Name;
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnDocumentSelected(uuid);
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            this.btnList.Enabled = false;
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnListSubmit(0, int.MaxValue);
        }
    }
}
