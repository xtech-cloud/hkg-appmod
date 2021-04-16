
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public partial class SchemaPanel : UserControl
    {
        public class SchemaUiBridge : ISchemaUiBridge
        {
            public SchemaPanel panel { get; set; }

            public object getRootPanel()
            {
                return panel;
            }

            public void Alert(string _message)
            {
                MessageBox.Show(_message);
            }

            public void RefreshSchemaList(long _total, object _entity)
            {
                panel.btnRefresh.Enabled = true;
                panel.rules = (Dictionary<string, Dictionary<string,string>>)_entity;
                foreach(string path in panel.rules.Keys)
                {
                    string[] sections = path.Split("/");
                    var nodes = panel.tvSchema.Nodes;
                    foreach (string section in sections)
                    {
                        if (string.IsNullOrEmpty(section))
                            continue;
                        var found = nodes.Find(section, false);
                        if (found.Length == 0)
                        {
                            TreeNode newNode = new TreeNode();
                            newNode.Name = section;
                            newNode.Text = section;
                            nodes.Add(newNode);
                            found = new TreeNode[] { newNode };
                        }
                        nodes = found[0].Nodes;
                    }
                }
                panel.tvSchema.ExpandAll();
            }
        }

        public SchemaFacade facade { get; set; }
        private Dictionary<string, Dictionary<string, string>> rules = new Dictionary<string, Dictionary<string, string>>();

        public SchemaPanel()
        {
            InitializeComponent();

            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "rootPanel";
            this.TabIndex = 0;

            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnImportYaml.Click += new System.EventHandler(this.btnImportYaml_Click);
        }
        private void btnImportYaml_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Import Yaml";
            dialog.Filter = "Yaml File(*.yml)|*.yml";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            string file = dialog.FileName;
            if (string.IsNullOrEmpty(file))
                return;
            string yaml = File.ReadAllText(file);
            var bridge = facade.getViewBridge() as ISchemaViewBridge;
            bridge.OnImportYamlSubmit(yaml);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var bridge = facade.getViewBridge() as ISchemaViewBridge;
            long offset = 0;
            long count = int.MaxValue;
            bridge.OnListSubmit(offset, count);
        }

        private void tvSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count > 0)
                return;
            string fullpath = node.FullPath;
            Dictionary<string, string> rule;
            if (!this.rules.TryGetValue(fullpath, out rule))
                return;
            tbField.Text = rule["field"];
            tbText.Text = rule["text"];
            tbElement.Text = rule["element"];
        }
    }
}
