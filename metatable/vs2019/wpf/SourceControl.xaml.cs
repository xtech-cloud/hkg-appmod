
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace hkg.metatable
{
    public partial class SourceControl: UserControl
    {
        public class SourceUiBridge : ISourceUiBridge
        {
            public SourceControl control { get; set; }

            public object getRootPanel()
            {
                return control;
            }

            public void Alert(string _message)
            {
                HandyControl.Controls.Growl.Warning(_message, "StatusGrowl");
            }

            public void RefreshSourceList(List<Dictionary<string, string>> _source)
            {
                control.lbSource.Items.Clear();
                foreach(var source in _source)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Uid = source["uuid"];
                    item.Content = source["name"];
                    control.lbSource.Items.Add(item);
                }
            }

            public void RefreshSource(Dictionary<string, string> _source)
            {
                control.txtUUID.Text = _source["uuid"];
                control.txtAddress.Text = _source["address"];
                control.txtAttribute.Text = _source["attribute"];
                control.txtExpression.Text = _source["expression"];
            }
            public void RefreshActivateLocation(string _location)
            {
                if (_location.Equals("public"))
                    control.rbPublic.IsEnabled = true;
                else if (_location.Equals("private"))
                    control.rbPrivate.IsEnabled = true;
            }
        }

        public SourceFacade facade { get; set; }


        public SourceControl()
        {
            InitializeComponent();
            dpMainPage.Visibility = System.Windows.Visibility.Hidden;
            rbPrivate.IsEnabled = false;
            rbPublic.IsEnabled = false;
            spRenameSource.Visibility = System.Windows.Visibility.Collapsed;
            pageDetail.Visibility = System.Windows.Visibility.Hidden;
        }

        private void onImportSourceClick(object sender, System.Windows.RoutedEventArgs e)
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
            var bridge = facade.getViewBridge() as ISourceViewBridge;
            bridge.OnImportYamlSubmit(yaml);
        }

        private void onSourceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem item = lbSource.SelectedItem as ListBoxItem;
            pageDetail.Visibility = null == item ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
            if (null == item)
                return;

            var bridge = facade.getViewBridge() as ISourceViewBridge;
            bridge.OnGetSubmit(item.Uid);
        }

        private void onPublicChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as ISourceViewBridge;
            bridge.OnLocationChanged("public");
            dpMainPage.Visibility = System.Windows.Visibility.Visible;
        }

        private void onPrivateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as ISourceViewBridge;
            bridge.OnLocationChanged("private");
            dpMainPage.Visibility = System.Windows.Visibility.Visible;
        }

        private void onLocalChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            var bridge = facade.getViewBridge() as ISourceViewBridge;
            bridge.OnLocationChanged("local");
            dpMainPage.Visibility = System.Windows.Visibility.Visible;
        }

        private void onDeleteSourceClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ListBoxItem item = lbSource.SelectedItem as ListBoxItem;
            if (null == item)
                return;

            var bridge = facade.getViewBridge() as ISourceViewBridge;
            bridge.OnDeleteSubmit(item.Uid);
        }

    }
}
