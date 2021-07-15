
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class SourceView : View
    {
        public const string NAME = "hkg.metatable.SourceView";

        private Facade facade = null;
        private SourceModel model = null;
        private ISourceUiBridge bridge = null;
        private SourceService service = null;

        protected override void preSetup()
        {
            model = findModel(SourceModel.NAME) as SourceModel;
            service = findService(SourceService.NAME) as SourceService;
            facade = findFacade("hkg.metatable.SourceFacade");
            SourceViewBridge vb = new SourceViewBridge();
            vb.view = this;
            vb.service = service;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup SourceView");
            addRouter("/Application/Auth/Signin/Success", handleAuthSigninSuccess);
        }

        protected override void postSetup()
        {
            bridge = facade.getUiBridge() as ISourceUiBridge;
            object rootPanel = bridge.getRootPanel();
            // 通知主程序挂载界面
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["hkg.metatable.Source"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }

        public void Alert(string _message)
        {
            bridge.Alert(_message);
        }

        public void RefreshSourceList(List<SourceModel.SourceStatus.Source> _source)
        {
            List<Dictionary<string, string>> source = new List<Dictionary<string, string>>();
            foreach (var s in _source)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param["uuid"] = s.entity._uuid.AsString();
                param["name"] = s.entity._name.AsString();
                param["address"] = s.entity._address.AsString();
                param["attribute"] = s.entity._attribute.AsString();
                param["expression"] = s.entity._expression.AsString();
                source.Add(param);
            }
            bridge.RefreshSourceList(source);
        }

        public void RefreshSource(SourceModel.SourceStatus.Source _source)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["uuid"] = _source.entity._uuid.AsString();
            param["name"] = _source.entity._name.AsString();
            param["address"] = _source.entity._address.AsString();
            param["attribute"] = _source.entity._attribute.AsString();
            param["expression"] = _source.entity._expression.AsString();
            bridge.RefreshSource(param);
        }


        public void OnGetSubmit(string _uuid)
        {
            model.UpdateGet(_uuid);
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
            service.CreateAlias();
        }

    }
}
