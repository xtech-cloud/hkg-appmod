
using System;
using System.Collections.Generic;
using System.Text.Json;
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
            vb.externalModel = findModel(ExternalModel.NAME) as ExternalModel;
            vb.service = service;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup DocumentView");

            addRouter("/Application/Auth/Signin/Success", handleAuthSigninSuccess);
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
                long timestamp = v.entity._updatedAt.AsInt64();
                DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                param["updatedAt"] = dto.LocalDateTime.ToString();
                list.Add(param);
            }
            bridge.RefreshList(_total, list);
        }
        public void RefreshRemovedDocument(List<string> _uuid)
        {
            bridge.RefreshRemovedDocument(_uuid);
        }

        public void OnRefreshMetatableFormatSubmit(string _location)
        {
            var externalService = findService("hkg.metatable.FormatService");
            externalService.CallAlias(string.Format("/List@{0}", _location), (_parameter) =>
            {
            }, (_reply) =>
             {
                 var reply = JsonSerializer.Deserialize<External.Metatable.FormatListResponse>(_reply, JsonOptions.DefaultSerializerOptions);

                 List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                 if (0 == reply.status.code)
                 {
                     var modelExternal = findModel(ExternalModel.NAME) as ExternalModel;
                     modelExternal.SaveMetatableFormatList(reply.entity);
                     foreach (var e in reply.entity)
                     {
                         Dictionary<string, string> pattern = new Dictionary<string, string>();
                         pattern["name"] = e.name.AsString();
                         list.Add(pattern);
                     }
                 }
                 bridge.RefreshExternalMetatableFormatList(list);
             }, (_err) =>
             {
                 bridge.Alert(_err.getMessage());
             }, null);
        }

        public void OnRefreshCollectorDocumentSubmit(string _location)
        {
            var externalService = findService("hkg.collector.DocumentService");
            externalService.CallAlias(string.Format("/List@{0}", _location), (_parameter) =>
            {
            }, (_reply) =>
             {
                 var reply = JsonSerializer.Deserialize<External.Collector.DocumentListResponse>(_reply, JsonOptions.DefaultSerializerOptions);

                 Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>();
                 if (0 == reply.status.code)
                 {
                     var modelExternal = findModel(ExternalModel.NAME) as ExternalModel;
                     modelExternal.SaveCollectorDocumentList(reply.entity);
                     foreach (var e in reply.entity)
                     {
                         // 相同的名字和标签视为同一个文档
                         string code = e.name.AsString();
                         foreach (var k in e.keyword.AsStringAry())
                         {
                             code += k;
                         }
                         if (!dict.ContainsKey(code))
                         {
                             dict[code] = new Dictionary<string, string>();
                             dict[code]["name"] = e.name.AsString();
                             dict[code]["code"] = code;
                             dict[code]["label"] = e.keyword.AsString();
                         }
                         e._code = code;
                     }
                 }
                 List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                 foreach (var d in dict.Values)
                 {
                     list.Add(d);
                 }
                 bridge.RefreshExternalCollectorDocumentList(list);
             }, (_err) =>
             {
                 bridge.Alert(_err.getMessage());
             }, null);

        }

        private void handleAuthSigninSuccess(Model.Status _status, object _data)
        {
            Dictionary<string, Any> data = (Dictionary<string, Any>)_data;
            if (data["location"].AsString().Equals("public"))
            {
                service.domainPublic = data["host"].AsString();
            }
            if (data["location"].AsString().Equals("private"))
            {
                service.domainPrivate = data["host"].AsString();
            }
            service.accessToken = data["accessToken"].AsString();
            service.uuid = data["uuid"].AsString();
            bridge.RefreshActivateLocation(data["location"].AsString());
        }
    }
}
