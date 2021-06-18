using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Windows.Documents;
using System;
using HandyControl.Controls;

namespace hkg.builder
{
    public partial class DocumentControl : UserControl
    {
        public class TagItem
        {
            public string Name { get; set; }
            public string IsSelected { get; set; }
        }

        public class Document
        {
            public string UUID { get; set; }
            public string Name { get; set; }
            public string Label { get; set; }
            public string Text { get; set; }
            public string Html { get; set; }
            public string UpdatedAt { get; set; }
        }

        public class DocumentUiBridge : IDocumentUiBridge
        {
            public DocumentControl control { get; set; }

            public object getRootPanel()
            {
                return control;
            }

            public void Alert(string _message)
            {
                Growl.Warning(_message, "StatusGrowl");
            }

            public void RefreshList(long _total, List<Dictionary<string, string>> _list)
            {
                control.DocumentList.Clear();
                foreach (var v in _list)
                {
                    Document doc = new Document();
                    doc.UUID = v["uuid"];
                    doc.Name = v["name"];
                    doc.Label = v["label"];
                    doc.Text = prettyJson(v["text"]);
                    doc.Html = v["html"];
                    doc.UpdatedAt = v["updatedAt"];
                    control.DocumentList.Add(doc);
                }
            }

            public void RefreshOne(Dictionary<string, string> _element)
            {
                control.txtUUID.Text = _element["uuid"];
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

            public void RefreshQueryCollectorDocumentList(List<Dictionary<string, string>> _list)
            {
                control.collectorDocument = _list;
                control.lbDocument.Items.Clear();
                foreach (var dict in _list)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Uid = dict["code"];
                    item.Content = dict["name"];
                    control.lbDocument.Items.Add(item);
                }
            }

            public void RefreshQueryMetatableFormatList(List<Dictionary<string, string>> _list)
            {
                control.metatableFormat = _list;
                control.cbFormat.Items.Clear();
                foreach (var dict in _list)
                {
                    control.cbFormat.Items.Add(dict["name"]);
                }
            }
        }

        public DocumentFacade facade { get; set; }

        public ObservableCollection<Document> DocumentList { get; set; }

        private List<Dictionary<string, string>> collectorDocument = new List<Dictionary<string, string>>();
        private List<Dictionary<string, string>> metatableFormat = new List<Dictionary<string, string>>();

        public DocumentControl()
        {
            DocumentList = new ObservableCollection<Document>();
            InitializeComponent();
        }

        private void onPublicChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnLocationChanged("public");
        }

        private void onPrivateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnLocationChanged("private");
        }

        private void onLocalChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnLocationChanged("local");
        }

        private void onDocumentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageDetail.Visibility = 1 == dgDocument.SelectedItems.Count ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;

            if (1 != dgDocument.SelectedItems.Count)
                return;
            var item = dgDocument.SelectedItems[0] as Document;
            txtUUID.Text = item.UUID;
            txtUpdatedAt.Text = item.UpdatedAt;
            txtLabel.Text = item.Label;
            wbText.NavigateToString(item.Html);
            Paragraph paragraph = new Paragraph(new Run(item.Text));
            rtbText.Document.Blocks.Clear();
            rtbText.Document.Blocks.Add(paragraph);
        }

        private void onFormatClick(object sender, System.Windows.RoutedEventArgs e)
        {
            dgDocument.SelectedItem = null;
            drawerFormat.IsOpen = true;
        }

        private void onStartFormatClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lbDocument.SelectedItems.Count == 0)
                return;

            var bridge = facade.getViewBridge() as IDocumentViewBridge;

            string formatName = cbFormat.SelectedItem as string;
            string paramFormat = "";
            string[] paramLabel = new string[0];
            string[] paramText = new string[0];

            bridge.BuildMergeParam(formatName, ref paramFormat, null, ref paramLabel, ref paramText);
            if (string.IsNullOrEmpty(paramFormat))
                return;


            foreach (var item in lbDocument.SelectedItems)
            {
                var lbItem = item as ListBoxItem;
                bridge.BuildMergeParam(null, ref paramFormat, lbItem.Uid, ref paramLabel, ref paramText);

                bridge.OnMergeSubmit(lbItem.Content as string, paramLabel, paramText, paramFormat);
            }
        }

        private void onFormatSelectedClick(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
