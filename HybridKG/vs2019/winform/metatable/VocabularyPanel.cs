
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public partial class VocabularyPanel : UserControl
    {
        public class VocabularyUiBridge : IVocabularyUiBridge
        {
            public VocabularyPanel panel { get; set; }

            public object getRootPanel()
            {
                return panel;
            }

            public void Alert(string _message)
            {
                MessageBox.Show(_message);
            }

            public void RefreshVocabularyList(long _total, object _entity)
            {
                panel.btnRefresh.Enabled = true;
                panel.vocabularies = (Dictionary<string, List<string>>)_entity;
                foreach (string path in panel.vocabularies.Keys)
                {
                    string[] sections = path.Split("/");
                    var nodes = panel.tvVocabulary.Nodes;
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
                //panel.tvVocabulary.ExpandAll();
            }
        }

        public VocabularyFacade facade { get; set; }
        private Dictionary<string, List<string>> vocabularies = new Dictionary<string, List<string>>();


        public VocabularyPanel()
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
            var bridge = facade.getViewBridge() as IVocabularyViewBridge;
            bridge.OnImportYamlSubmit(yaml);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var bridge = facade.getViewBridge() as IVocabularyViewBridge;
            long offset = 0;
            long count = int.MaxValue;
            btnRefresh.Enabled = false;
            bridge.OnListSubmit(offset, count);
        }

        private void tvVocabulary_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count > 0)
                return;
            string fullpath = node.FullPath;
            List<string> labels;
            if (!this.vocabularies.TryGetValue(fullpath, out labels))
                return;

            lbLabel.Items.Clear();
            foreach (string label in labels)
            {
                lbLabel.Items.Add(label);
            }
        }
    }
}
