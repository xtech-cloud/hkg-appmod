
using System;
using System.Collections.Generic;
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
            route("/hkg/metatable/Query/Response/Source/List", handleMetatableQuerySourceList);
            route("/hkg/metatable/Query/Response/Vocabulary/List", handleMetatableQueryVocabularyList);
            route("/hkg/metatable/Query/Response/Schema/List", handleMetatableQuerySchemaList);
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
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (var v in _document)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param["uuid"] = v.entity._uuid.AsString();
                param["name"] = v.entity._name.AsString();
                param["keyword"] = v.entity._keyword.AsString();
                param["address"] = v.entity._address.AsString();
                param["rawText"] = v.entity._rawText.AsString();
                param["tidyText"] = v.entity._tidyText.AsString();
                long timestamp = v.entity._crawledAt.AsInt64();
                DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                param["crawledAt"] = dto.LocalDateTime.ToString();
                list.Add(param);
            }
            bridge.RefreshList(_total, list);
        }

        public void RefreshOne(DocumentModel.DocumentStatus.Document _document)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["uuid"] = _document.entity._uuid.AsString();
            param["name"] = _document.entity._name.AsString();
            param["keyword"] = _document.entity._keyword.AsString();
            param["address"] = _document.entity._address.AsString();
            param["rawText"] = _document.entity._rawText.AsString();
            param["tidyText"] = _document.entity._tidyText.AsString();
            param["crawleAt"] = _document.entity._crawledAt.AsString();
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

        public void QueryMetatableSource()
        {
            Dictionary<string, Any> param = new Dictionary<string, Any>();
            param["domain"] = Any.FromString(service.domain);
            param["offset"] = Any.FromInt64(0);
            param["count"] = Any.FromInt64(int.MaxValue);
            model.Broadcast("/hkg/metatable/Query/Request/Source/List", param);
        }

        public void QueryMetatableVocabulary()
        {
            Dictionary<string, Any> param = new Dictionary<string, Any>();
            param["domain"] = Any.FromString(service.domain);
            param["offset"] = Any.FromInt64(0);
            param["count"] = Any.FromInt64(int.MaxValue);
            model.Broadcast("/hkg/metatable/Query/Request/Vocabulary/List", param);
        }

        public void QueryMetatableSchema()
        {
            Dictionary<string, Any> param = new Dictionary<string, Any>();
            param["domain"] = Any.FromString(service.domain);
            param["offset"] = Any.FromInt64(0);
            param["count"] = Any.FromInt64(int.MaxValue);
            model.Broadcast("/hkg/metatable/Query/Request/Schema/List", param);
        }

        private void handleMetatableQuerySourceList(Model.Status _status, object _data)
        {
            string json = (string)_data;
            var result = JsonSerializer.Deserialize<MetatableSourceListReply>(json);
            List<Dictionary<string, string>> source = new List<Dictionary<string, string>>();
            if (0 == result.status.code)
            {
                foreach (var e in result.entity)
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p["address"] = e.address;
                    p["name"] = e.name;
                    p["expression"] = e.expression;
                    p["attribute"] = e.attribute;
                    source.Add(p);
                }
            }
            bridge.RefreshQuerySourceList(source);
        }

        private void handleMetatableQueryVocabularyList(Model.Status _status, object _data)
        {
            string json = (string)_data;
            var result = JsonSerializer.Deserialize<MetatableVocabularyListReply>(json);
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            if (0 == result.status.code)
            {
                foreach (var e in result.entity)
                {
                    Dictionary<string, string> p = new Dictionary<string, string>();
                    p["name"] = e.name;
                    p["label"] = Any.FromStringAry(e.label).AsString();
                    p["value"] = Any.FromStringAry(e.value).AsString();
                    list.Add(p);
                }
            }
            bridge.RefreshQueryVocabularyList(list);
        }

        private void handleMetatableQuerySchemaList(Model.Status _status, object _data)
        {
            string json = (string)_data;
            var result = JsonSerializer.Deserialize<MetatableSchemaListReply>(json);
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
        }
    }
}
