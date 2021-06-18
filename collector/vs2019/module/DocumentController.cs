
using System;
using XTC.oelMVCS;

namespace hkg.collector
{
    public class DocumentController : Controller
    {
        public const string NAME = "hkg.collector.DocumentController";

        private DocumentView view { get; set; }

        protected override void preSetup()
        {
            view = findView(DocumentView.NAME) as DocumentView;
        }

        protected override void setup()
        {
            getLogger().Trace("setup DocumentController");
        }

        public void List(Model.Status _reply, DocumentModel.DocumentStatus _status, long _total, Proto.DocumentEntity[] _list)
        {
            _status.document.Clear();
            if (_reply.getCode() != 0)
            {
                view.Alert(_reply.getMessage());
            }
            else
            {
                foreach (var v in _list)
                {
                    var source = new DocumentModel.DocumentStatus.Document();
                    source.entity = v;
                    _status.document.Add(source);
                }
            }
            view.RefreshList(_total, _status.document);
        }

        public void Get(DocumentModel.DocumentStatus _status, string _uuid)
        {
            var doc = _status.document.Find((_item) =>
            {
                return _item.entity._uuid.AsString().Equals(_uuid);
            });
            if (null == doc)
            {
                view.Alert("document not found");
                return;
            }
            view.RefreshOne(doc);
        }
        public void ScrapeFinish(Model.Status _reply, string _name, string _address)
        {
            view.RefreshScrapeFinish(_reply.getCode(), _reply.getMessage(), _name, _address);
        }

        public void TidyFinish(Model.Status _reply, string _uuid)
        {
            view.RefreshTidyFinish(_reply.getCode(), _reply.getMessage(), _uuid);
        }
    }
}
