
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Windows.Documents;
using System;
using HandyControl.Controls;
using XTC.oelMVCS;

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

        public class Task
        {
            public string Name { get; set; }
            public string Address { get; set; }
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

            public void RefreshList(long _total, List<Dictionary<string, Any>> _list)
            {
                control.DocumentList.Clear();
                control.TotalDocumentList.Clear();
                control.piDocument.MaxPageCount = _list.Count == 0 ? 1 : _list.Count / control.piDocument.DataCountPerPage;

                foreach (var v in _list)
                {
                    Document doc = new Document();
                    doc.UUID = v["uuid"].AsString();
                    doc.Name = v["name"].AsString();
                    doc.Address = v["address"].AsString();
                    doc.RawText = v["rawText"].AsString();
                    doc.Keyword = v["keyword"].AsString();
                    doc.TidyText = prettyJson(v["tidyText"].AsString());
                    doc.IsScrape = !string.IsNullOrEmpty(doc.RawText);
                    doc.IsTidy = !string.IsNullOrEmpty(doc.TidyText);
                    doc.CrawledAt = v["crawledAt"].AsString();
                    control.TotalDocumentList.Add(doc);
                    doc.Source = doc.Address;
                    foreach (var source in control.source)
                    {
                        Any address;
                        if (source.TryGetValue("address", out address))
                        {
                            if (doc.Address.StartsWith(address.AsString()))
                            {
                                doc.Source = source["name"].AsString();
                                break;
                            }
                        }
                    }
                }

                if (_list.Count > 0)
                {
                    if (_list.Count % control.piDocument.DataCountPerPage != 0)
                        control.piDocument.MaxPageCount += 1;
                    control.piDocument.PageIndex = 1;
                    for (int i = 0; i < control.piDocument.DataCountPerPage && i < control.TotalDocumentList.Count; ++i)
                    {
                        control.DocumentList.Add(control.TotalDocumentList[i]);
                    }
                }
            }

            public void RefreshOne(Dictionary<string, Any> _element)
            {
                control.txtUUID.Text = _element["uuid"].AsString();
            }

            public void RefreshQuerySourceList(List<Dictionary<string, Any>> _source)
            {
                control.source = _source;
                control.cbSource.Items.Clear();
                foreach (var e in control.source)
                {
                    control.cbSource.Items.Add(e["name"].AsString());
                }
            }

            public void RefreshQueryVocabularyList(List<Dictionary<string, Any>> _vocabulary)
            {
                control.vocabulary = _vocabulary;
                control.cbVocabulary.Items.Clear();
                foreach (var e in control.vocabulary)
                {
                    control.cbVocabulary.Items.Add(e["name"].AsString());
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

                foreach (var task in control.lbTask.Items)
                {
                    var item = task as ListBoxItem;
                    string uid = string.Format("{0}{1}", item.Content, item.Uid);
                    if (!item.Content.Equals(_name) && !item.Uid.Equals(_address))
                        continue;
                    control.lbTask.Items.Remove(item);
                    break;
                }

                if (control.lbTask.Items.Count == 0)
                {
                    control.lbVocabulary.Visibility = System.Windows.Visibility.Visible;
                    control.lbTask.Visibility = System.Windows.Visibility.Collapsed;
                    control.btnStartScrape.IsEnabled = true;
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

            public void RefreshRemovedDocument(List<string> _uuid)
            {
                control.TotalDocumentList.RemoveAll((_item) =>
                {
                    return _uuid.Contains(_item.UUID);
                });
                List<Document> found = new List<Document>();
                foreach (var doc in control.DocumentList)
                {
                    if (_uuid.Contains(doc.UUID))
                        found.Add(doc);
                }
                foreach (var doc in found)
                {
                    control.DocumentList.Remove(doc);
                }
            }

            public void RefreshActivateLocation(string _location)
            {
                if (_location.Equals("public"))
                    control.rbPublic.IsEnabled = true;
                else if (_location.Equals("private"))
                    control.rbPrivate.IsEnabled = true;
            }
        }

        public DocumentFacade facade { get; set; }

        public ObservableCollection<Document> DocumentList { get; set; }
        public List<Document> TotalDocumentList { get; set; }
        private List<Dictionary<string, Any>> source = new List<Dictionary<string, Any>>();
        private List<Dictionary<string, Any>> vocabulary = new List<Dictionary<string, Any>>();
        private List<Dictionary<string, string>> schema = new List<Dictionary<string, string>>();
        private Dictionary<string, Dictionary<string, string>> rule = new Dictionary<string, Dictionary<string, string>>();
        private string location_ { get; set; }

        public DocumentControl()
        {
            DocumentList = new ObservableCollection<Document>();
            TotalDocumentList = new List<Document>();
            InitializeComponent();
            btnScrape.Visibility = System.Windows.Visibility.Hidden;
            dpMainPage.Visibility = System.Windows.Visibility.Hidden;
            rbPrivate.IsEnabled = false;
            rbPublic.IsEnabled = false;
            lbVocabulary.Visibility = System.Windows.Visibility.Visible;
            lbTask.Visibility = System.Windows.Visibility.Collapsed;
            btnSync.Visibility = System.Windows.Visibility.Collapsed;
            btnScrape.IsEnabled = false;
        }
        private void onPublicChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnLocationChanged("public");
            location_ = "public";
            dpMainPage.Visibility = System.Windows.Visibility.Visible;
            btnScrape.Visibility = System.Windows.Visibility.Visible;
            btnSync.Visibility = System.Windows.Visibility.Visible;
        }

        private void onPrivateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnLocationChanged("private");
            location_ = "private";
            dpMainPage.Visibility = System.Windows.Visibility.Visible;
            btnScrape.Visibility = System.Windows.Visibility.Visible;
            btnSync.Visibility = System.Windows.Visibility.Visible;
        }

        private void onLocalChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnLocationChanged("local");
            location_ = "local";
            dpMainPage.Visibility = System.Windows.Visibility.Visible;
            btnScrape.Visibility = System.Windows.Visibility.Visible;
            btnSync.Visibility = System.Windows.Visibility.Visible;
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
                if (!v["name"].AsString().Equals(item))
                    continue;
                foreach (var str in v["value"].AsStringAry())
                {
                    lbVocabulary.Items.Add(str);
                }
            }
        }

        private void onStartScrapeClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IDocumentViewBridge;

            string sourceName = cbSource.SelectedItem as string;
            if (string.IsNullOrEmpty(sourceName))
                return;
            string vocabularyName = cbVocabulary.SelectedItem as string;
            if (string.IsNullOrEmpty(vocabularyName))
                return;

            Any expression = Any.FromStringAry(new string[0]);
            Any attribute = Any.FromStringAry(new string[0]);
            foreach (var v in source)
            {
                if (!v["name"].AsString().Equals(sourceName))
                    continue;

                if (!v.TryGetValue("expression", out expression))
                {
                    expression = Any.FromString("");
                }
                if (!v.TryGetValue("attribute", out attribute))
                {
                    attribute = Any.FromString("");
                }
            }
            Any keyword = Any.FromStringAry(new string[0]);
            foreach (var v in vocabulary)
            {
                if (!v["name"].AsString().Equals(vocabularyName))
                    continue;

                if (!v.TryGetValue("label", out keyword))
                {
                    keyword = Any.FromStringAry(new string[0]);
                }
            }

            lbVocabulary.Visibility = System.Windows.Visibility.Collapsed;
            lbTask.Visibility = System.Windows.Visibility.Visible;
            btnStartScrape.IsEnabled = false;
            foreach (var item in lbVocabulary.SelectedItems)
            {
                string name = item as string;
                List<string> kw = new List<string>();
                string address = expression.AsString().Replace("{?}", name);
                ListBoxItem task = new ListBoxItem();
                task.Content = name;
                task.Uid = string.Format("{0}{1}", name, address);
                lbTask.Items.Add(task);
                foreach (var str in keyword.AsStringAry())
                {
                    kw.Add(str);
                }

                bridge.OnScrapeSubmit(name, kw.ToArray(), address, attribute.AsString());
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
                    if (doc.Address.StartsWith(s["address"].AsString()))
                    {
                        sourceName = s["name"].AsString();
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

        private void onDocumentPageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            int offset = (piDocument.PageIndex - 1) * piDocument.DataCountPerPage;
            int count = piDocument.DataCountPerPage;

            DocumentList.Clear();
            for (int i = offset; i < count + offset && i < TotalDocumentList.Count; ++i)
            {
                DocumentList.Add(TotalDocumentList[i]);
            }
        }

        private void onDocumentSearchStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            if (string.IsNullOrEmpty(sbDocument.Text))
                return;
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            Dictionary<string, string> filter = new Dictionary<string, string>();
            filter["name"] = sbDocument.Text;
            bridge.OnListSubmit(0, int.MaxValue, filter);
        }

        private void onDeleteClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IDocumentViewBridge;

            List<string> uuid = new List<string>();
            foreach (var item in dgDocument.SelectedItems)
            {
                var doc = item as Document;
                uuid.Add(doc.UUID);
            }
            bridge.OnBatchDeleteSubmit(uuid.ToArray());
        }

        private void onSyncClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IDocumentViewBridge;
            bridge.OnRefreshMetatableSourceSubmit(location_);
            bridge.OnRefreshMetatableSchemaSubmit(location_);
            bridge.OnRefreshMetatableVocabularySubmit(location_);
            btnScrape.IsEnabled = true;
        }
    }
}
