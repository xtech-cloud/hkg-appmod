
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.builder
{
    public class DocumentController: Controller
    {
        public const string NAME = "hkg.builder.DocumentController";

        private DocumentView view {get;set;}

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

        public void Delete(Model.Status _reply, DocumentModel.DocumentStatus _status, List<string> _uuid)
        {
            if (_reply.getCode() != 0)
            {
                view.Alert(_reply.getMessage());
                return;
            }

            _status.document.RemoveAll((_item) =>
            {
                return _uuid.Contains(_item.entity._uuid.AsString());
            });
            view.RefreshRemovedDocument(_uuid);
        }

    }
}
