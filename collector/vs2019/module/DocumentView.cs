
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using XTC.oelMVCS;

namespace hkg.collector
{
    public class DocumentView : View
    {
        public const string NAME = "hkg.collector.DocumentView";

        private Facade facade = null;
        private DocumentModel model = null;
        private IDocumentUiBridge bridge = null;
        private DocumentService service = null;

        protected override void preSetup()
        {
            model = findModel(DocumentModel.NAME) as DocumentModel;
            service = findService(DocumentService.NAME) as DocumentService;
            facade = findFacade("hkg.collector.DocumentFacade");
            DocumentViewBridge vb = new DocumentViewBridge();
            vb.view = this;
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
            data["hkg.collector.Document"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }

        public void Alert(string _message)
        {
            bridge.Alert(_message);
        }

        public void RefreshList(long _total, List<DocumentModel.DocumentStatus.Document> _document)
        {
            List<Dictionary<string, Any>> list = new List<Dictionary<string, Any>>();
            foreach (var v in _document)
            {
                Dictionary<string, Any> param = new Dictionary<string, Any>();
                param["uuid"] = v.entity._uuid;
                param["name"] = v.entity._name;
                param["keyword"] = v.entity._keyword;
                param["address"] = v.entity._address;
                param["rawText"] = v.entity._rawText;
                param["tidyText"] = v.entity._tidyText;
                long timestamp = v.entity._crawledAt.AsInt64();
                DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                param["crawledAt"] = Any.FromString(dto.LocalDateTime.ToString());
                list.Add(param);
            }
            bridge.RefreshList(_total, list);
        }

        public void RefreshOne(DocumentModel.DocumentStatus.Document _document)
        {
            Dictionary<string, Any> param = new Dictionary<string, Any>();
            param["uuid"] = _document.entity._uuid;
            param["name"] = _document.entity._name;
            param["keyword"] = _document.entity._keyword;
            param["address"] = _document.entity._address;
            param["rawText"] = _document.entity._rawText;
            param["tidyText"] = _document.entity._tidyText;
            param["crawleAt"] = _document.entity._crawledAt;
            bridge.RefreshOne(param);
        }

        public void RefreshScrapeFinish(int _code, string _message, string _name, string _address)
        {
            bridge.RefreshScrapeFinish(_code, _message, _name, _address);
        }

        public void RefreshTidyFinish(int _code, string _message, string _uuid)
        {
            bridge.RefreshTidyFinish(_code, _message, _uuid);
        }

        public void RefreshRemovedDocument(List<string> _uuid)
        {
            bridge.RefreshRemovedDocument(_uuid);
        }


        public void OnGetSubmit(string _uuid)
        {
            model.UpdateGet(_uuid);
        }

        public void OnRefreshMetatableSourceSubmit(string _location)
        {
            var externalService = findService("hkg.metatable.SourceService");
            externalService.CallAlias(string.Format("/List@{0}", _location), (_parameter) =>
            {

            }, (_reply) =>
             {
                 var result = JsonSerializer.Deserialize<MetatableSourceListReply>(_reply, getOptions());
                 List<Dictionary<string, Any>> source = new List<Dictionary<string, Any>>();
                 if (0 == result.status.code)
                 {
                     foreach (var e in result.entity)
                     {
                         Dictionary<string, Any> p = new Dictionary<string, Any>();
                         p["address"] = e.address;
                         p["name"] = e.name;
                         p["expression"] = e.expression;
                         p["attribute"] = e.attribute;
                         source.Add(p);
                     }
                 }
                 bridge.RefreshQuerySourceList(source);
             }, (_err) =>
             {
                 bridge.Alert(_err.getMessage());
             }, null);
        }

        public void OnRefreshMetatableVocabularySubmit(string _location)
        {
            var externalService = findService("hkg.metatable.VocabularyService");
            externalService.CallAlias(string.Format("/List@{0}", _location), (_parameter) =>
            {
            }, (_reply) =>
             {
                 var result = JsonSerializer.Deserialize<MetatableVocabularyListReply>(_reply, getOptions());
                 List<Dictionary<string, Any>> list = new List<Dictionary<string, Any>>();
                 if (0 == result.status.code)
                 {
                     foreach (var e in result.entity)
                     {
                         Dictionary<string, Any> p = new Dictionary<string, Any>();
                         p["name"] = e.name;
                         p["label"] = e.label;
                         p["value"] = e.value;
                         list.Add(p);
                     }
                 }
                 bridge.RefreshQueryVocabularyList(list);

             }, (_err) =>
             {
                 bridge.Alert(_err.getMessage());
             }, null);

        }

        public void OnRefreshMetatableSchemaSubmit(string _location)
        {
            var externalService = findService("hkg.metatable.SchemaService");
            externalService.CallAlias(string.Format("/List@{0}", _location), (_parameter) =>
            {
            }, (_reply) =>
             {
                 var result = JsonSerializer.Deserialize<MetatableSchemaListReply>(_reply);
                 List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                 Dictionary<string, Dictionary<string, string>> expression = new Dictionary<string, Dictionary<string, string>>();
                 if (0 == result.status.code)
                 {
                     foreach (var e in result.entity)
                     {
                         Dictionary<string, string> p = new Dictionary<string, string>();
                         Dictionary<string, string> exp = new Dictionary<string, string>();
                         p["name"] = e.name;
                         p["rule.count"] = e.rule.Count.ToString();
                         for (int i = 0; i < e.rule.Count; ++i)
                         {
                             string rule = "";
                             p[string.Format("rule.{0}.name", i)] = e.rule[i].name;
                             p[string.Format("rule.{0}.field", i)] = e.rule[i].field;
                             p[string.Format("rule.{0}.type", i)] = e.rule[i].type;
                             p[string.Format("rule.{0}.element", i)] = e.rule[i].element;
                             if (!string.IsNullOrEmpty(e.rule[i].type))
                                 rule += string.Format("$t={0};", e.rule[i].type);
                             if (!string.IsNullOrEmpty(e.rule[i].element))
                                 rule += string.Format("$e={0};", e.rule[i].element);
                             string key = "";
                             string value = "";
                             if (null != e.rule[i].pair)
                             {
                                 if (!e.rule[i].pair.TryGetValue("key", out key))
                                     key = "";
                                 else
                                     rule += string.Format("$pk={0};", key);
                                 if (!e.rule[i].pair.TryGetValue("value", out value))
                                     value = "";
                                 else
                                     rule += string.Format("$pv={0};", value);
                             }
                             p[string.Format("rule.{0}.pair.key", i)] = key;
                             p[string.Format("rule.{0}.pair.value", i)] = value;

                             exp[rule] = e.rule[i].field;
                         }
                         list.Add(p);
                         expression[e.name] = exp;
                     }
                 }
                 bridge.RefreshQuerySchemaList(list);
                 bridge.RefreshQuerySchemaRuleExpression(expression);
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

        private JsonSerializerOptions getOptions()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Converters.Add(new AnyProtoConverter());
            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return options;
        }
    }
}
