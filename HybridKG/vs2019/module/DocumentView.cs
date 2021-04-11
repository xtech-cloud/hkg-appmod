
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
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup DocumentView");

           route("/hkg/collector/Document/Scrape", this.handleDocumentScrape);
    
           route("/hkg/collector/Document/List", this.handleDocumentList);
    
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
            var rsp = (Proto.DocumentListResponse)_data;
            if(rsp._status._code.AsInt() == 0)
            {
                Dictionary<string, string> entity = new Dictionary<string, string>();
                foreach (var e in rsp._entity)
                {
                    string keyword = "";
                    foreach(var k in e._keyword)
                    {
                        keyword += string.Format("{0}, ", k);
                    }

                    if(!string.IsNullOrEmpty(keyword))
                    {
                        keyword = keyword.Remove(keyword.Length - 2, 2);
                    }
                    string key = string.Format("{0} ({1})", e._name.AsString(), keyword);
                    entity[key] = e._rawText.AsString();
                }
                bridge.RefreshList(rsp._total.AsLong(), entity);
            }
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", rsp._status._code.AsInt(), rsp._status._message.AsString()));
        }
    
    }
}
