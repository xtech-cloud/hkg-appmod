
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class FormatView : View
    {
        public const string NAME = "hkg.metatable.FormatView";

        private Facade facade = null;
        private FormatModel model = null;
        private IFormatUiBridge bridge = null;

        protected override void preSetup()
        {
            model = findModel(FormatModel.NAME) as FormatModel;
            var service = findService(FormatService.NAME) as FormatService;
            facade = findFacade("hkg.metatable.FormatFacade");
            FormatViewBridge vb = new FormatViewBridge();
            vb.view = this;
            vb.service = service;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup FormatView");
        }

        protected override void postSetup()
        {
            bridge = facade.getUiBridge() as IFormatUiBridge;
            object rootPanel = bridge.getRootPanel();
            // 通知主程序挂载界面
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["hkg.metatable.Format"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }

        public void Alert(string _message)
        {
            bridge.Alert(_message);
        }

        public void RefreshList(List<FormatModel.FormatStatus.Format> _format)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (var v in _format)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param["uuid"] = v.entity._uuid.AsString();
                param["name"] = v.entity._name.AsString();
                param["pattern.count"] = v.entity._pattern.Length.ToString();
                for (int i = 0; i < v.entity._pattern.Length; ++i)
                {
                    param[string.Format("pattern.{0}.to", i)] = v.entity._pattern[i]._to.AsString();
                    param[string.Format("pattern.{0}.from", i)] = v.entity._pattern[i]._from.AsString();
                }
                list.Add(param);
            }
            bridge.RefreshList(list);
        }

        public void RefreshOne(FormatModel.FormatStatus.Format _format)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["uuid"] = _format.entity._uuid.AsString();
            param["name"] = _format.entity._name.AsString();
            param["pattern.count"] = _format.entity._pattern.Length.ToString();
            for (int i = 0; i < _format.entity._pattern.Length; ++i)
            {
                param[string.Format("pattern.{0}.to", i)] = _format.entity._pattern[i]._to.AsString();
                param[string.Format("pattern.{0}.from", i)] = _format.entity._pattern[i]._from.AsString();
            }
            bridge.RefreshOne(param);
        }


        public void OnGetSubmit(string _uuid)
        {
            model.UpdateGet(_uuid);
        }

    }
}
