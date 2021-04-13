
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Collector
{
    public class DocumentView: View
    {
        public const string NAME = "Collector.DocumentView";

        private Facade facade = null;
        private DocumentModel model = null;
        private IDocumentUiBridge bridge = null;

        protected override void preSetup()
        {
            model = findModel(DocumentModel.NAME) as DocumentModel;
            var service = findService(DocumentService.NAME) as DocumentService;
            facade = findFacade("Collector.DocumentFacade");
            DocumentViewBridge vb = new DocumentViewBridge();
            vb.view = this;
            vb.service = service;
            vb.model = model;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup DocumentView");

           route("/hkg/collector/Document/Scrape", this.handleDocumentScrape);
    
           route("/hkg/collector/Document/List", this.handleDocumentList);

            route("/hkg/collector/document/scrape/progress", this.handleScrapeProgress);
            route("/hkg/collector/document/scrape/finish", this.handleScrapeFinish);
            route("/hkg/collector/document/selected", this.handleDocumentSelected);
            
        }

        protected override void postSetup()
        {
            bridge = facade.getUiBridge() as IDocumentUiBridge;
            object rootPanel = bridge.getRootPanel();
            // 通知主程序挂载界面
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["/HKG/Collector/Document"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }

        private void handleDocumentScrape(Model.Status _status, object _data)
        {
            var rsp = (Proto.BlankResponse)_data;
            if(rsp._status._code.AsInt() == 0)
                bridge.Alert("Success");
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", rsp._status._code.AsInt(), rsp._status._message.AsString()));
        }
    
        private void handleDocumentList(Model.Status _status, object _data)
        {
            var replayStatus = (Model.Status)_data;
            if(replayStatus.getCode() == 0)
            {
                DocumentModel.DocumentStatus status = _status as DocumentModel.DocumentStatus;
                Metatable.SourceModel.SourceStatus statusSource = _status.Access(Metatable.SourceModel.SourceStatus.NAME) as Metatable.SourceModel.SourceStatus;

                Dictionary<string, string> entity = new Dictionary<string, string>();
                foreach (var e in status.documents)
                {
                    string keyword = "";
                    foreach(var k in e._keyword.AsStringAry())
                    {
                        keyword += string.Format("{0}, ", k);
                    }

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        keyword = keyword.Remove(keyword.Length - 2, 2);
                    }
                    string key = string.Format("{0} ({1})", e._name.AsString(), keyword);

                    var source = statusSource.sources.Find((_item) =>
                    {
                        return new Uri(_item._address.AsString()).Host.Equals(new Uri(e._address.AsString()).Host);
                    });
                    if(null != source)
                    {
                        key += string.Format(" # {0}", source._name.AsString());
                    }
                    entity[key] = e._uuid.AsString();
                }
                bridge.RefreshList(status.total, entity);
            }
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", replayStatus.getCode(), replayStatus.getMessage()));
        }

        private void handleScrapeProgress(Model.Status _status, object _data)
        {
            float progress = (float)_data;
            bridge.RefreshScrapeProgress(progress);
        }

        private void handleScrapeFinish(Model.Status _status, object _data)
        {
            bridge.RefreshScrapeFinish();
        }

        private void handleDocumentSelected(Model.Status _status, object _data)
        {
            string uuid = (string)_data;
            DocumentModel.DocumentStatus status = _status.Access(DocumentModel.DocumentStatus.NAME) as DocumentModel.DocumentStatus;
            Proto.DocumentEntity doc = status.documents.Find((_item) =>
           {
               return _item._uuid.AsString().Equals(uuid);
           });
            if (null == doc)
                return;

            bridge.RefreshDocument(doc._rawText.AsString());
        }


    }
}
