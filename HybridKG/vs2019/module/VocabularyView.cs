
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class VocabularyView: View
    {
        public const string NAME = "Metatable.VocabularyView";

        private Facade facade = null;
        private VocabularyModel model = null;
        private IVocabularyUiBridge bridge = null;

        protected override void preSetup()
        {
            model = findModel(VocabularyModel.NAME) as VocabularyModel;
            var service = findService(VocabularyService.NAME) as VocabularyService;
            facade = findFacade("Metatable.VocabularyFacade");
            VocabularyViewBridge vb = new VocabularyViewBridge();
            vb.view = this;
            vb.service = service;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup VocabularyView");

           route("/hkg/metatable/Vocabulary/ImportYaml", this.handleVocabularyImportYaml);
    
           route("/hkg/metatable/Vocabulary/List", this.handleVocabularyList);
    
           route("/hkg/metatable/Vocabulary/Find", this.handleVocabularyFind);
    
        }

        protected override void postSetup()
        {
            bridge = facade.getUiBridge() as IVocabularyUiBridge;
            object rootPanel = bridge.getRootPanel();
            // 通知主程序挂载界面
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["/HKG/Metatable/Vocabulary"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }

        private void handleVocabularyImportYaml(Model.Status _status, object _data)
        {
            var rsp = (Proto.BlankResponse)_data;
            if(rsp._status._code.AsInt() == 0)
                bridge.Alert("Success");
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", rsp._status._code.AsInt(), rsp._status._message.AsString()));
        }
    
        private void handleVocabularyList(Model.Status _status, object _data)
        {
            var rsp = (Proto.VocabularyListResponse)_data;
            if (rsp._status._code.AsInt() == 0)
            {
                Dictionary<string, string> entity = new Dictionary<string, string>();
                foreach(var e in rsp._entity)
                {
                    string labels = "";
                    foreach (string label in e._label.AsStringAry())
                        labels += string.Format("{0}, ", label);
                    if (!string.IsNullOrEmpty(labels))
                        labels = labels.Remove(labels.Length - 2, 2);
                    entity[e._name.AsString()] = labels;
                }
                bridge.RefreshVocabularyList(rsp._total.AsLong(), entity);
            }
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", rsp._status._code.AsInt(), rsp._status._message.AsString()));
        }
    
        private void handleVocabularyFind(Model.Status _status, object _data)
        {
            var rsp = (Proto.VocabularyFindResponse)_data;
            if(rsp._status._code.AsInt() == 0)
                bridge.Alert("Success");
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", rsp._status._code.AsInt(), rsp._status._message.AsString()));
        }
    
    }
}
