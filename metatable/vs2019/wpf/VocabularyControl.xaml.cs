using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace hkg.metatable
{
    public partial class VocabularyControl : UserControl
    {
        public class TagItem
        {
            public string Name { get; set; }
        }
        public class VocabularyUiBridge : IVocabularyUiBridge
        {
            public VocabularyControl control { get; set; }

            public object getRootPanel()
            {
                return control;
            }

            public void Alert(string _message)
            {
                HandyControl.Controls.Growl.Warning(_message, "StatusGrowl");
            }

            public void RefreshList(List<Dictionary<string, string>> _list)
            {
                control.lbVocabulary.Items.Clear();
                foreach (var v in _list)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Uid = v["uuid"];
                    item.Content = v["name"];
                    control.lbVocabulary.Items.Add(item);
                }
            }

            public void RefreshOne(Dictionary<string, string> _element)
            {
                control.txtUUID.Text = _element["uuid"];

                control.ValueList.Clear();
                string vstr = _element["value"];
                if (vstr.StartsWith("["))
                    vstr = vstr.Remove(0, 1);
                if (vstr.EndsWith("]"))
                    vstr = vstr.Remove(vstr.Length - 1, 1);
                foreach (var v in vstr.Split(","))
                {
                    TagItem tag = new TagItem();
                    tag.Name = v.Remove(v.Length - 1, 1).Remove(0, 1);
                    control.ValueList.Add(tag);
                }

                control.LabelList.Clear();
                vstr = _element["label"];
                if (vstr.StartsWith("["))
                    vstr = vstr.Remove(0, 1);
                if (vstr.EndsWith("]"))
                    vstr = vstr.Remove(vstr.Length - 1, 1);
                foreach (var v in vstr.Split(","))
                {
                    TagItem tag = new TagItem();
                    tag.Name = v.Remove(v.Length - 1, 1).Remove(0, 1);
                    control.LabelList.Add(tag);
                }
            }
        }

        public VocabularyFacade facade { get; set; }

        public ObservableCollection<TagItem> ValueList { get; set; }
        public ObservableCollection<TagItem> LabelList { get; set; }

        public VocabularyControl()
        {
            ValueList = new ObservableCollection<TagItem>();
            LabelList = new ObservableCollection<TagItem>();
            InitializeComponent();
            spRenameVocabulary.Visibility = System.Windows.Visibility.Collapsed;
            pageDetail.Visibility = System.Windows.Visibility.Hidden;
        }

        private void onImportVocabularyClick(object sender, System.Windows.RoutedEventArgs e)
        {
            string path = string.Empty;
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "YAML(*.yml)|*.yml"
            };

            bool result = (bool)openFileDialog.ShowDialog();
            if (!result)
                return;

            string yaml = File.ReadAllText(openFileDialog.FileName);
            var bridge = facade.getViewBridge() as IVocabularyViewBridge;
            bridge.OnImportYamlSubmit(yaml);
        }

        private void onVocabularySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = lbVocabulary.SelectedItem as ListBoxItem;
            pageDetail.Visibility = null == item ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
            if (null == item)
                return;

            var bridge = facade.getViewBridge() as IVocabularyViewBridge;
            bridge.OnGetSubmit(item.Uid);
        }

        private void onPublicChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IVocabularyViewBridge;
            bridge.OnLocationChanged("public");
        }

        private void onPrivateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IVocabularyViewBridge;
            bridge.OnLocationChanged("private");
        }

        private void onLocalChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IVocabularyViewBridge;
            bridge.OnLocationChanged("local");
        }

        private void onDeleteVocabularyClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ListBoxItem item = lbVocabulary.SelectedItem as ListBoxItem;
            if (null == item)
                return;

            var bridge = facade.getViewBridge() as IVocabularyViewBridge;
            bridge.OnDeleteSubmit(item.Uid);
        }

        private void onSearchValueStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            foreach (var v in ValueList)
            {
                //v.IsSelected = v.Name.Contains(sbValue.Text);
            }
        }
    }
}
