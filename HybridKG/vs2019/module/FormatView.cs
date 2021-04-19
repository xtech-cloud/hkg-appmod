
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class FormatView: View
    {
        public const string NAME = "Metatable.FormatView";

        private Facade facade = null;
        private FormatModel model = null;
        private IFormatUiBridge bridge = null;

        protected override void preSetup()
        {
            model = findModel(FormatModel.NAME) as FormatModel;
            var service = findService(FormatService.NAME) as FormatService;
            facade = findFacade("Metatable.FormatFacade");
            FormatViewBridge vb = new FormatViewBridge();
            vb.view = this;
            vb.service = service;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup FormatView");

           route("/hkg/metatable/Format/ImportYaml", this.handleFormatImportYaml);
    
           route("/hkg/metatable/Format/List", this.handleFormatList);
    
           route("/hkg/metatable/Format/Delete", this.handleFormatDelete);
    
        }

        protected override void postSetup()
        {
            bridge = facade.getUiBridge() as IFormatUiBridge;
            object rootPanel = bridge.getRootPanel();
            // 通知主程序挂载界面
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["/HKG/Metatable/Format"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }

        private void handleFormatImportYaml(Model.Status _status, object _data)
        {
            var rsp = (Proto.BlankResponse)_data;
            if(rsp._status._code.AsInt() == 0)
                bridge.Alert("Success");
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", rsp._status._code.AsInt(), rsp._status._message.AsString()));
        }

        private void handleFormatList(Model.Status _status, object _data)
        {
            FormatModel.FormatStatus replayStatus = (FormatModel.FormatStatus)_data;
            if (replayStatus.getCode() == 0)
            {
                FormatModel.FormatStatus status = _status as FormatModel.FormatStatus;
                Dictionary<string, Dictionary<string, string>> entity = new Dictionary<string, Dictionary<string, string>>();
                foreach (var e in status.formats)
                {
                    foreach(var m in e._pattern)
                    {
                        string path = string.Format("{0}/{1}", e._name.AsString(), m._to.AsString());
                        entity[path] = new Dictionary<string, string>();
                        entity[path]["from"] = m._from.AsString();
                    }
                }
                bridge.RefreshFormatList(status.total, entity);
            }
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", replayStatus.getCode(), replayStatus.getMessage()));
        }

        private void handleFormatDelete(Model.Status _status, object _data)
        {
            var rsp = (Proto.BlankResponse)_data;
            if(rsp._status._code.AsInt() == 0)
                bridge.Alert("Success");
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", rsp._status._code.AsInt(), rsp._status._message.AsString()));
        }
    
    }
}
