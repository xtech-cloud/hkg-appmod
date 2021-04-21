
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Builder
{
    public class DocumentView : View
    {
        public const string NAME = "Builder.DocumentView";

        private Facade facade = null;
        private DocumentModel model = null;
        private IDocumentUiBridge bridge = null;

        protected override void preSetup()
        {
            model = findModel(DocumentModel.NAME) as DocumentModel;
            var service = findService(DocumentService.NAME) as DocumentService;
            facade = findFacade("Builder.DocumentFacade");
            DocumentViewBridge vb = new DocumentViewBridge();
            vb.view = this;
            vb.service = service;
            vb.model = model;
            facade.setViewBridge(vb);
        }

        protected override void setup()
        {
            getLogger().Trace("setup DocumentView");

            route("/hkg/builder/Document/Merge", this.handleDocumentMerge);

            route("/hkg/builder/Document/List", this.handleDocumentList);
            route("/hkg/metatable/Format/List", this.handleFormatList);
            route("/hkg/builder/document/merge/progress", this.handleMergeProgress);
            route("/hkg/builder/document/merge/finish", this.handleMergeFinish);
            route("/hkg/builder/document/selected", this.handleDocumentSelected);
        }

        protected override void postSetup()
        {
            bridge = facade.getUiBridge() as IDocumentUiBridge;
            object rootPanel = bridge.getRootPanel();
            // 通知主程序挂载界面
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["/HKG/Builder/Document"] = rootPanel;
            model.Broadcast("/module/view/attach", data);
        }

        private void handleDocumentMerge(Model.Status _status, object _data)
        {
            var rsp = (Proto.BlankResponse)_data;
            if (rsp._status._code.AsInt() == 0)
                bridge.Alert("Success");
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", rsp._status._code.AsInt(), rsp._status._message.AsString()));
        }

        private void handleDocumentList(Model.Status _status, object _data)
        {
            var replayStatus = (Model.Status)_data;
            if (replayStatus.getCode() == 0)
            {
                DocumentModel.DocumentStatus status = _status as DocumentModel.DocumentStatus;
                Metatable.SourceModel.SourceStatus statusSource = _status.Access(Metatable.SourceModel.SourceStatus.NAME) as Metatable.SourceModel.SourceStatus;

                Dictionary<string, string> entity = new Dictionary<string, string>();
                foreach (var e in status.documents)
                {
                    string label = "";
                    foreach (var k in e._label.AsStringAry())
                    {
                        label += string.Format("{0}, ", k);
                    }

                    if (!string.IsNullOrEmpty(label))
                    {
                        label = label.Remove(label.Length - 2, 2);
                    }
                    string key = string.Format("{0} ({1})", e._name.AsString(), label);

                    entity[key] = e._uuid.AsString();
                }
                bridge.RefreshList(status.total, entity);
            }
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", replayStatus.getCode(), replayStatus.getMessage()));
        }

        private void handleFormatList(Model.Status _status, object _data)
        {
            Metatable.FormatModel.FormatStatus replayStatus = (Metatable.FormatModel.FormatStatus)_data;
            if (replayStatus.getCode() == 0)
            {
                Metatable.FormatModel.FormatStatus status = _status as Metatable.FormatModel.FormatStatus;
                List<string> formats = new List<string>();
                foreach (var e in status.formats)
                {
                    formats.Add(e._name.AsString());
                }
                bridge.RefreshFormatList(formats);
            }
            else
                bridge.Alert(string.Format("Failure：\n\nCode: {0}\nMessage:\n{1}", replayStatus.getCode(), replayStatus.getMessage()));
        }

        private void handleMergeProgress(Model.Status _status, object _data)
        {
            float progress = (float)_data;
            bridge.RefreshProgress(progress);
        }

        private void handleMergeFinish(Model.Status _status, object _data)
        {
            bridge.RefreshFinish();
        }

        private void handleDocumentSelected(Model.Status _status, object _data)
        {
            string uuid = (string)_data;
            DocumentModel.DocumentStatus status = _status.Access(DocumentModel.DocumentStatus.NAME) as DocumentModel.DocumentStatus;
            Proto.DocumentEntity doc = status.documents.Find((_item) =>
            {
                return _item._uuid.AsString().Equals(uuid);
            });
            if (null == doc)
                return;

            long timestamp = doc._updatedAt.AsLong();
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["text"] = doc._text.AsString();
            data["updatedAt"] = dto.LocalDateTime.ToString();
            data["label"] = doc._label.AsString();
            bridge.RefreshDocument(data);
        }


    }
}
