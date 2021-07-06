using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;

namespace hkg.metatable
{
    public partial class SchemaControl : UserControl
    {
        public class RuleInfo
        {
            public string Name { get; set; }
            public string Field { get; set; }
            public string Type { get; set; }
            public string Element { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public class SchemaUiBridge : ISchemaUiBridge
        {
            public SchemaControl control { get; set; }

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
                control.lbSchema.Items.Clear();
                foreach (var v in _list)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Uid = v["uuid"];
                    item.Content = v["name"];
                    control.lbSchema.Items.Add(item);
                }
            }

            public void RefreshOne(Dictionary<string, string> _element)
            {
                control.txtUUID.Text = _element["uuid"];

                control.RuleList.Clear();
                int count;
                int.TryParse(_element["rule.count"], out count);
                for (int i = 0; i < count; ++i)
                {
                    RuleInfo e = new RuleInfo();
                    e.Name = _element[string.Format("rule.{0}.name", i)];
                    e.Field = _element[string.Format("rule.{0}.field", i)];
                    e.Type= _element[string.Format("rule.{0}.type", i)];
                    e.Element= _element[string.Format("rule.{0}.element", i)];
                    e.Key= _element[string.Format("rule.{0}.key", i)];
                    e.Value= _element[string.Format("rule.{0}.value", i)];
                    control.RuleList.Add(e);
                }
            }
        }

        public SchemaFacade facade { get; set; }

        public ObservableCollection<RuleInfo> RuleList { get; set; }

        public SchemaControl()
        {
            RuleList = new ObservableCollection<RuleInfo>();
            InitializeComponent();
            spRenameSchema.Visibility = System.Windows.Visibility.Collapsed;
            pageDetail.Visibility = System.Windows.Visibility.Hidden;
        }

        private void onImportSchemaClick(object sender, System.Windows.RoutedEventArgs e)
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
            var bridge = facade.getViewBridge() as ISchemaViewBridge;
            bridge.OnImportYamlSubmit(yaml);
        }

        private void onSchemaSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = lbSchema.SelectedItem as ListBoxItem;
            pageDetail.Visibility = null == item ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
            if (null == item)
                return;

            var bridge = facade.getViewBridge() as ISchemaViewBridge;
            bridge.OnGetSubmit(item.Uid);
        }

        private void onPublicChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as ISchemaViewBridge;
            bridge.OnLocationChanged("public");
        }

        private void onPrivateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as ISchemaViewBridge;
            bridge.OnLocationChanged("private");
        }

        private void onLocalChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as ISchemaViewBridge;
            bridge.OnLocationChanged("local");
        }

        private void onDeleteSchemaClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ListBoxItem item = lbSchema.SelectedItem as ListBoxItem;
            if (null == item)
                return;

            var bridge = facade.getViewBridge() as ISchemaViewBridge;
            bridge.OnDeleteSubmit(item.Uid);
        }

    }
}
