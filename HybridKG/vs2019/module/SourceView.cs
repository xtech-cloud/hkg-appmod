
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class SourceView: View
    {
        public const string NAME = "Metatable.SourceView";

        private Facade facade = null;
        private SourceModel model = null;
        private ISourceUiBridge bridge = null;

        protected override void preSetup()
        {
            model = findModel(SourceModel.NAME) as SourceModel;
            var service = findService(SourceService.NAME) as SourceService;
            facade = findFacade("Metatable.SourceFacade");
            SourceViewBridge vb = new SourceViewBridge();
            vb.view = this;
            vb.service = service;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup SourceView");

           route("/hkg/metatable/Source/ImportYaml", this.handleSourceImportYaml);
    
           route("/hkg/metatable/Source/List", this.handleSourceList);
    
        }

        protected override void postSetup()
        {
            bridge = facade.getUiBridge() as ISourceUiBridge;
            object rootPanel = bridge.getRootPanel();
            // 通知主程序挂载界面
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["/HKG/Metatable/Source"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }

        private void handleSourceImportYaml(Model.Status _status, object _data)
        {
            var rsp = (Proto.BlankResponse)_data;
            if(rsp._status._code.AsInt() == 0)
                bridge.Alert("Success");
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", rsp._status._code.AsInt(), rsp._status._message.AsString()));
        }
    
        private void handleSourceList(Model.Status _status, object _data)
        {
            SourceModel.SourceStatus replayStatus = (SourceModel.SourceStatus)_data;
            if(replayStatus.getCode() == 0)
            {
                SourceModel.SourceStatus status = _status as SourceModel.SourceStatus;
                Dictionary<string, string> entity = new Dictionary<string, string>();
                foreach (var e in status.sources)
                {
                    entity[e._name.AsString()] = e._address.AsString();
                }
                bridge.RefreshSourceList(status.total, entity);
            }
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", replayStatus.getCode(), replayStatus.getMessage()));
        }
    
    }
}
