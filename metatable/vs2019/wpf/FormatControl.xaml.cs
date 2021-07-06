using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace hkg.metatable
{
    public partial class FormatControl: UserControl
    {
        public class FromTag
        {
            public string Name { get; set; }
            public bool IsSelected { get; set; }
        }
        public class PatternInfo
        {
            public string To { get; set; }
            public List<FromTag> From { get; set; }
        }

        public class FormatUiBridge : IFormatUiBridge
        {
            public FormatControl control { get; set; }

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
                control.lbFormat.Items.Clear();
                foreach(var v in _list)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Uid = v["uuid"];
                    item.Content = v["name"];
                    control.lbFormat.Items.Add(item);
                }
            }

            public void RefreshOne(Dictionary<string, string> _element)
            {
                control.txtUUID.Text = _element["uuid"];

                control.PatternList.Clear();
                int count;
                int.TryParse(_element["pattern.count"], out count);
                for(int i = 0; i < count; ++i)
                {
                    PatternInfo p = new PatternInfo();
                    p.To = _element[string.Format("pattern.{0}.to", i)];
                    p.From = new List<FromTag>();
                    string from = _element[string.Format("pattern.{0}.from", i)];
                    if (from.StartsWith("["))
                        from = from.Remove(0, 1);
                    if (from.EndsWith("]"))
                        from = from.Remove(from.Length-1, 1);
                    foreach(var v in from.Split(","))
                    {
                        FromTag tag = new FromTag();
                        tag.Name = v.Remove(v.Length-1, 1).Remove(0,1);
                        tag.IsSelected = true;
                        p.From.Add(tag);
                    }
                    control.PatternList.Add(p);
                }
            }
        }

        public FormatFacade facade { get; set; }

        public ObservableCollection<PatternInfo> PatternList { get; set; }

        public FormatControl()
        {
            PatternList = new ObservableCollection<PatternInfo>();
            InitializeComponent();
            spRenameFormat.Visibility = System.Windows.Visibility.Collapsed;
            pageDetail.Visibility = System.Windows.Visibility.Hidden;
        }

        private void onImportFormatClick(object sender, System.Windows.RoutedEventArgs e)
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
            var bridge = facade.getViewBridge() as IFormatViewBridge;
            bridge.OnImportYamlSubmit(yaml);
        }

        private void onFormatSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = lbFormat.SelectedItem as ListBoxItem;
            pageDetail.Visibility = null == item ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
            if (null == item)
                return;

            var bridge = facade.getViewBridge() as IFormatViewBridge;
            bridge.OnGetSubmit(item.Uid);
        }

        private void onPublicChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IFormatViewBridge;
            bridge.OnLocationChanged("public");
        }

        private void onPrivateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IFormatViewBridge;
            bridge.OnLocationChanged("private");
        }

        private void onLocalChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as IFormatViewBridge;
            bridge.OnLocationChanged("local");
        }

        private void onDeleteFormatClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ListBoxItem item = lbFormat.SelectedItem as ListBoxItem;
            if (null == item)
                return;

            var bridge = facade.getViewBridge() as IFormatViewBridge;
            bridge.OnDeleteSubmit(item.Uid);
        }

    }
}
