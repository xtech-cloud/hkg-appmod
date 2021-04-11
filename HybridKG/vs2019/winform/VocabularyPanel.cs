
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
                panel.btnList.Enabled = true;
                panel.lTotal.Text = _total.ToString();
                panel.listResult.DataSource = (Dictionary<string, string>)_entity;
            }
        }

        public VocabularyFacade facade { get; set; }

        private BindingSource listResult = new BindingSource();

        public VocabularyPanel()
        {
            InitializeComponent();

            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "rootPanel";
            this.TabIndex = 0;

            this.dgvPage.DataSource = new BindingSource(listResult, null);

            this.tbOffset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbOffset_KeyPress);
            this.tbCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbOffset_KeyPress);
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
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

        private void tbOffset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void tbCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            var bridge = facade.getViewBridge() as IVocabularyViewBridge;
            long offset = 0;
            long count = 50;
            if (!long.TryParse(tbOffset.Text, out offset))
                offset = 0;
            if (!long.TryParse(tbCount.Text, out count))
                count = 50;
            btnList.Enabled = false;
            bridge.OnListSubmit(offset, count);
        }
    }
}
