
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Windows.Documents;
using System;
using HandyControl.Controls;

namespace hkg.collector
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
            public string Keyword { get; set; }
            public string Address { get; set; }
            public string Source { get; set; }
            public string RawText { get; set; }
            public string TidyText { get; set; }
            public string CrawledAt { get; set; }
            public bool IsScrape { get; set; }
            public bool IsTidy { get; set; }
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
                    doc.Address = v["address"];
                    doc.RawText = v["rawText"];
                    doc.Keyword = v["keyword"];
                    doc.TidyText = prettyJson(v["tidyText"]);
                    doc.IsScrape = !string.IsNullOrEmpty(doc.RawText);
                    doc.IsTidy = !string.IsNullOrEmpty(doc.TidyText);
                    doc.CrawledAt = v["crawledAt"];
                    control.DocumentList.Add(doc);
                    doc.Source = doc.Address;
                    foreach (var source in control.source)
                    {
                        string address;
                        if (source.TryGetValue("address", out address))
                        {
                            if (doc.Address.StartsWith(address))
                            {
                                doc.Source = source["name"];
                                break;
                            }
                        }
                    }
                }
            }

            public void RefreshOne(Dictionary<string, string> _element)
            {
                control.txtUUID.Text = _element["uuid"];
            }

            public void RefreshQuerySourceList(List<Dictionary<string, string>> _source)
            {
                control.source = _source;
                control.cbSource.Items.Clear();
                foreach (var e in control.source)
                {
                    control.cbSource.Items.Add(e["name"]);
                }
            }

            public void RefreshQueryVocabularyList(List<Dictionary<string, string>> _vocabulary)
            {
                control.vocabulary = _vocabulary;
                control.cbVocabulary.Items.Clear();
                foreach (var e in control.vocabulary)
                {
                    control.cbVocabulary.Items.Add(e["name"]);
                }
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

            public void RefreshScrapeFinish(int _code, string _message, string _name, string _address)
            {
                if (0 != _code)
                {
                    Alert(_message);
                    return;
                }

                foreach (var doc in control.DocumentList)
                {
                    if (!doc.Name.Equals(_name))
                        continue;
                    doc.IsScrape = true;
                    break;
                }
            }

            public void RefreshQuerySchemaList(List<Dictionary<string, string>> _list)
            {
                control.schema = _list;
            }

            public void RefreshQuerySchemaRuleExpression(Dictionary<string, Dictionary<string, string>> _list)
            {
                control.rule = _list;
            }

            public void RefreshTidyFinish(int _code, string _message, string _uuid)
            {
                if (0 != _code)
                {
                    Alert(_message);
                    return;
                }

                foreach (var doc in control.DocumentList)
                {
                    if (!doc.UUID.Equals(_uuid))
                        continue;
                    doc.IsTidy = true;
                    break;
                }

            }
        }

        public DocumentFacade facade { get; set; }

        public ObservableCollection<Document> DocumentList { get; set; }
        private List<Dictionary<string, string>> source = new List<Dictionary<string, string>>();
        private List<Dictionary<string, string>> vocabulary = new List<Dictionary<string, string>>();
        private List<Dictionary<string, string>> schema = new List<Dictionary<string, string>>();
        private Dictionary<string, Dictionary<string, string>> rule = new Dictionary<string, Dictionary<string, string>>();

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
            txtAddress.Text = item.Address;
            txtKeyword.Text = item.Keyword;
            txtCrawledAt.Text = item.CrawledAt;
            string header = @"<head><meta content='text/html;charset=UTF-8'></head>";
            wbRaw.NavigateToString(header + item.RawText);
            Paragraph paragraph = new Paragraph(new Run(item.TidyText));
            rtbTidy.Document.Blocks.Clear();
            rtbTidy.Document.Blocks.Add(paragraph);
        }

        private void onScrapeClick(object sender, System.Windows.RoutedEventArgs e)
        {
            dgDocument.SelectedItem = null;
            drawerScrape.IsOpen = true;
        }

        private void onVocabularySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbVocabulary.Items.Clear();
            string item = cbVocabulary.SelectedItem as string;
            if (string.IsNullOrEmpty(item))
                return;

            foreach (var v in vocabulary)
            {
                if (!v["name"].Equals(item))
                    continue;
                string value = v["value"];
                if (value.StartsWith("["))
                    value = value.Remove(0, 1);
                if (value.EndsWith("]"))
                    value = value.Remove(value.Length - 1, 1);
                foreach (var str in value.Split(','))
                {
                    lbVocabulary.Items.Add(str);
                }
            }
        }

        private void onStartScrapeClick(object sender, System.Windows.RoutedEventArgs e)
        {
            DocumentList.Clear();

            var bridge = facade.getViewBridge() as IDocumentViewBridge;

            string sourceName = cbSource.SelectedItem as string;
            if (string.IsNullOrEmpty(sourceName))
                return;
            string vocabularyName = cbVocabulary.SelectedItem as string;
            if (string.IsNullOrEmpty(vocabularyName))
                return;

            string expression = "";
            string attribute = "";
            foreach (var v in source)
            {
                if (!v["name"].Equals(sourceName))
                    continue;

                if (!v.TryGetValue("expression", out expression))
                {
                    expression = "";
                }
                if (!v.TryGetValue("attribute", out attribute))
                {
                    attribute = "";
                }
            }
            string keyword = "";
            foreach (var v in vocabulary)
            {
                if (!v["name"].Equals(vocabularyName))
                    continue;

                if (!v.TryGetValue("label", out keyword))
                {
                    keyword = "";
                }
            }

            foreach (var item in lbVocabulary.SelectedItems)
            {
                string name = item as string;
                List<string> kw = new List<string>();
                string address = expression.Replace("{?}", name);
                Document doc = new Document();
                doc.Name = name;
                doc.Address = sourceName;
                doc.IsScrape = false;
                doc.IsTidy = false;
                DocumentList.Add(doc);
                if (keyword.StartsWith("["))
                    keyword = keyword.Remove(0, 1);
                if (keyword.EndsWith("]"))
                    keyword = keyword.Remove(keyword.Length - 1, 1);
                foreach (var str in keyword.Split(','))
                {
                    kw.Add(str);
                }

                bridge.OnScrapeSubmit(name, kw.ToArray(), address, attribute);
            }
        }
        private void onTidyClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            foreach (var item in dgDocument.SelectedItems)
            {
                var doc = item as Document;
                var uri = new Uri(doc.Address);
                string host = string.Format("{0}://{1}", uri.Scheme, uri.Host);
                string sourceName = "";
                foreach (var s in source)
                {
                    if (doc.Address.StartsWith(s["address"]))
                    {
                        sourceName = s["name"];
                        break;
                    }
                }
                if (string.IsNullOrEmpty(sourceName))
                    continue;

                Dictionary<string, string> r;
                if (!rule.TryGetValue(sourceName, out r))
                    continue;

                bridge.OnTidySubmit(doc.UUID, host, r);
            }
        }
    }
}
