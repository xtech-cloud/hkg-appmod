
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class VocabularyView : View
    {
        public const string NAME = "hkg.metatable.VocabularyView";

        private Facade facade = null;
        private VocabularyModel model = null;
        private IVocabularyUiBridge bridge = null;
        private VocabularyService service = null;

        protected override void preSetup()
        {
            model = findModel(VocabularyModel.NAME) as VocabularyModel;
            service = findService(VocabularyService.NAME) as VocabularyService;
            facade = findFacade("hkg.metatable.VocabularyFacade");
            VocabularyViewBridge vb = new VocabularyViewBridge();
            vb.view = this;
            vb.service = service;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup VocabularyView");
            addRouter("/Application/Auth/Signin/Success", handleAuthSigninSuccess);
        }

        protected override void postSetup()
        {
            bridge = facade.getUiBridge() as IVocabularyUiBridge;
            object rootPanel = bridge.getRootPanel();
            // 通知主程序挂载界面
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["hkg.metatable.Vocabulary"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }
        public void Alert(string _message)
        {
            bridge.Alert(_message);
        }

        public void RefreshList(List<VocabularyModel.VocabularyStatus.Vocabulary> _vocabulary)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (var v in _vocabulary)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param["uuid"] = v.entity._uuid.AsString();
                param["name"] = v.entity._name.AsString();
                param["value"] = v.entity._value.AsString();
                list.Add(param);
            }
            bridge.RefreshList(list);
        }

        public void RefreshOne(VocabularyModel.VocabularyStatus.Vocabulary _vocabulary)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["uuid"] = _vocabulary.entity._uuid.AsString();
            param["name"] = _vocabulary.entity._name.AsString();
            param["label"] = _vocabulary.entity._label.AsString();
            param["value"] = _vocabulary.entity._value.AsString();
            bridge.RefreshOne(param);
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
