
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using XTC.oelMVCS;

namespace hkg.builder
{
    public class DocumentView : View
    {
        public const string NAME = "hkg.builder.DocumentView";

        private Facade facade = null;
        private DocumentModel model = null;
        private DocumentService service = null;
        private IDocumentUiBridge bridge = null;

        protected override void preSetup()
        {
            model = findModel(DocumentModel.NAME) as DocumentModel;
            service = findService(DocumentService.NAME) as DocumentService;
            facade = findFacade("hkg.builder.DocumentFacade");
            DocumentViewBridge vb = new DocumentViewBridge();
            vb.view = this;
            vb.model = model;
            vb.queryModel = findModel(QueryModel.NAME) as QueryModel;
            vb.service = service;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup DocumentView");
            route("/hkg/metatable/Query/Response/Format/List", handleMetatableQueryFormatList);
            route("/hkg/collector/Query/Response/Document/List", handleCollectorQueryDocumentList);
        }

        protected override void postSetup()
        {
            bridge = facade.getUiBridge() as IDocumentUiBridge;
            object rootPanel = bridge.getRootPanel();
            // 通知主程序挂载界面
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["hkg.builder.Document"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }

        public void Alert(string _message)
        {
            bridge.Alert(_message);
        }
        public void RefreshList(long _total, List<DocumentModel.DocumentStatus.Document> _document)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (var v in _document)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param["uuid"] = v.entity._uuid.AsString();
                param["name"] = v.entity._name.AsString();
                param["label"] = v.entity._label.AsString();
                param["text"] = v.entity._text.AsString();
                param["html"] = ViewUtility.DocumentToHtml(v.entity._text.AsString(), (ex) =>
                {
                    bridge.Alert(ex.Message);
                });
                long timestamp = v.entity._updatedAt.AsLong();
                DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                param["updatedAt"] = dto.LocalDateTime.ToString();
                list.Add(param);
            }
            bridge.RefreshList(_total, list);
        }

        public void QueryCollectorDocumentList()
        {
            Dictionary<string, Any> param = new Dictionary<string, Any>();
            param["domain"] = Any.FromString(service.domain);
            param["offset"] = Any.FromInt64(0);
            param["count"] = Any.FromInt64(int.MaxValue);
            model.Broadcast("/hkg/collector/Query/Request/Document/List", param);
        }

        public void QueryMetatableFormatList()
        {
            Dictionary<string, Any> param = new Dictionary<string, Any>();
            param["domain"] = Any.FromString(service.domain);
            param["offset"] = Any.FromInt64(0);
            param["count"] = Any.FromInt64(int.MaxValue);
            model.Broadcast("/hkg/metatable/Query/Request/Format/List", param);
        }

        private void handleCollectorQueryDocumentList(Model.Status _status, object _data)
        {
            string json = (string)_data;
            var result = JsonSerializer.Deserialize<CollectorDocumentListReply>(json);
            var modelQuery = findModel(QueryModel.NAME) as QueryModel;
            modelQuery.SaveCollectorDocumentList(result.entity);

            Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>();
            if (0 == result.status.code)
            {
                foreach (var e in result.entity)
                {
                    // 相同的名字和标签视为同一个文档
                    string code = e.name;
                    foreach (var k in e.keyword)
                    {
                        code += k;
                    }
                    if (!dict.ContainsKey(code))
                    {
                        dict[code] = new Dictionary<string, string>();
                        dict[code]["name"] = e.name;
                        dict[code]["code"] = code;
                        dict[code]["label"] = Proto.Field.FromStringAry(e.keyword).AsString();
                    }
                    e._code = code;
                }
            }
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach(var d in dict.Values)
            {
                list.Add(d);
            }
            bridge.RefreshQueryCollectorDocumentList(list);
        }

        private void handleMetatableQueryFormatList(Model.Status _status, object _data)
        {
           
            string json = (string)_data;
            var result = JsonSerializer.Deserialize<MetatableFormatListReply>(json);
            var modelQuery = findModel(QueryModel.NAME) as QueryModel;
            modelQuery.SaveMetatableFormatList(result.entity);

            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            if (0 == result.status.code)
            {
                foreach (var e in result.entity)
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p["name"] = e.name;
                    list.Add(p);
                }
            }
            bridge.RefreshQueryMetatableFormatList(list);
        }
    }
}
