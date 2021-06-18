
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class SchemaView : View
    {
        public const string NAME = "hkg.metatable.SchemaView";

        private Facade facade = null;
        private SchemaModel model = null;
        private ISchemaUiBridge bridge = null;

        protected override void preSetup()
        {
            model = findModel(SchemaModel.NAME) as SchemaModel;
            var service = findService(SchemaService.NAME) as SchemaService;
            facade = findFacade("hkg.metatable.SchemaFacade");
            SchemaViewBridge vb = new SchemaViewBridge();
            vb.view = this;
            vb.service = service;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup SchemaView");
        }

        protected override void postSetup()
        {
            bridge = facade.getUiBridge() as ISchemaUiBridge;
            object rootPanel = bridge.getRootPanel();
            // 通知主程序挂载界面
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["hkg.metatable.Schema"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }
        public void Alert(string _message)
        {
            bridge.Alert(_message);
        }

        public void RefreshList(List<SchemaModel.SchemaStatus.Schema> _format)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (var v in _format)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param["uuid"] = v.entity._uuid.AsString();
                param["name"] = v.entity._name.AsString();
                param["rule.count"] = v.entity._rule.Length.ToString();
                for (int i = 0; i < v.entity._rule.Length; ++i)
                {
                    param[string.Format("rule.{0}.name", i)] = v.entity._rule[i]._name.AsString();
                    param[string.Format("rule.{0}.field", i)] = v.entity._rule[i]._field.AsString();
                    param[string.Format("rule.{0}.type", i)] = v.entity._rule[i]._type.AsString();
                    param[string.Format("rule.{0}.element", i)] = v.entity._rule[i]._element.AsString();
                    param[string.Format("rule.{0}.key", i)] = v.entity._rule[i]._pair._key.AsString();
                    param[string.Format("rule.{0}.value", i)] = v.entity._rule[i]._pair._value.AsString();
                }
                list.Add(param);
            }
            bridge.RefreshList(list);
        }

        public void RefreshOne(SchemaModel.SchemaStatus.Schema _schema)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["uuid"] = _schema.entity._uuid.AsString();
            param["name"] = _schema.entity._name.AsString();
            param["rule.count"] = _schema.entity._rule.Length.ToString();
            for (int i = 0; i < _schema.entity._rule.Length; ++i)
            {
                param[string.Format("rule.{0}.name", i)] = _schema.entity._rule[i]._name.AsString();
                param[string.Format("rule.{0}.field", i)] = _schema.entity._rule[i]._field.AsString();
                param[string.Format("rule.{0}.type", i)] = _schema.entity._rule[i]._type.AsString();
                param[string.Format("rule.{0}.element", i)] = _schema.entity._rule[i]._element.AsString();
                param[string.Format("rule.{0}.key", i)] = _schema.entity._rule[i]._pair._key.AsString();
                param[string.Format("rule.{0}.value", i)] = _schema.entity._rule[i]._pair._value.AsString();
            }
            bridge.RefreshOne(param);
        }


        public void OnGetSubmit(string _uuid)
        {
            model.UpdateGet(_uuid);
        }
    }
}
