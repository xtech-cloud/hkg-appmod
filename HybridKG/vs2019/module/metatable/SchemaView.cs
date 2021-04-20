
using System;
using System.Collections.Generic;
using System.Text.Json;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class SchemaView : View
    {
        public const string NAME = "Metatable.SchemaView";

        private Facade facade = null;
        private SchemaModel model = null;
        private ISchemaUiBridge bridge = null;

        protected override void preSetup()
        {
            model = findModel(SchemaModel.NAME) as SchemaModel;
            var service = findService(SchemaService.NAME) as SchemaService;
            facade = findFacade("Metatable.SchemaFacade");
            SchemaViewBridge vb = new SchemaViewBridge();
            vb.view = this;
            vb.service = service;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup SchemaView");

            route("/hkg/metatable/Schema/ImportYaml", this.handleSchemaImportYaml);

            route("/hkg/metatable/Schema/List", this.handleSchemaList);

            route("/hkg/metatable/Schema/Delete", this.handleSchemaDelete);

        }

        protected override void postSetup()
        {
            bridge = facade.getUiBridge() as ISchemaUiBridge;
            object rootPanel = bridge.getRootPanel();
            // 通知主程序挂载界面
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["/HKG/Metatable/Schema"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }

        private void handleSchemaImportYaml(Model.Status _status, object _data)
        {
            var rsp = (Proto.BlankResponse)_data;
            if (rsp._status._code.AsInt() == 0)
                bridge.Alert("Success");
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", rsp._status._code.AsInt(), rsp._status._message.AsString()));
        }

        private void handleSchemaList(Model.Status _status, object _data)
        {
            SchemaModel.SchemaStatus replayStatus = (SchemaModel.SchemaStatus)_data;
            if (replayStatus.getCode() == 0)
            {
                SchemaModel.SchemaStatus status = _status as SchemaModel.SchemaStatus;
                Dictionary<string, Dictionary<string, string>> entity = new Dictionary<string, Dictionary<string, string>>();
                foreach (var e in status.schemas)
                {
                    foreach (var r in e._rule)
                    {
                        string path = string.Format("{0}/{1}", e._name.AsString(), r._name.AsString());
                        entity[path] = new Dictionary<string, string>();
                        entity[path]["name"] = r._name.AsString();
                        entity[path]["field"] = r._field.AsString();
                        entity[path]["type"] = r._type.AsString();
                        entity[path]["element"] = r._element.AsString();
                        entity[path]["pairKey"] = r._pair._key.AsString();
                        entity[path]["pairValue"] = r._pair._value.AsString();
                    }
                }
                bridge.RefreshSchemaList(status.total, entity);
            }
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", replayStatus.getCode(), replayStatus.getMessage()));
        }

        private void handleSchemaDelete(Model.Status _status, object _data)
        {
            var rsp = (Proto.BlankResponse)_data;
            if (rsp._status._code.AsInt() == 0)
                bridge.Alert("Success");
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", rsp._status._code.AsInt(), rsp._status._message.AsString()));

        }

    }
}
