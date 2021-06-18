
using System;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class SourceController : Controller
    {
        public const string NAME = "hkg.metatable.SourceController";
        private SourceView view { get; set; }

        protected override void preSetup()
        {
            view = findView(SourceView.NAME) as SourceView;
        }

        protected override void setup()
        {
            getLogger().Trace("setup SourceController");
        }

        public void List(Model.Status _reply, SourceModel.SourceStatus _status, Proto.SourceEntity[] _source)
        {
            _status.source.Clear();
            if (_reply.getCode() != 0)
            {
                view.Alert(_reply.getMessage());
            }
            else
            {

                foreach (var s in _source)
                {
                    var source = new SourceModel.SourceStatus.Source();
                    source.entity = s;
                    _status.source.Add(source);
                }
            }
            view.RefreshSourceList(_status.source);
        }

        public void Get(SourceModel.SourceStatus _status, string _uuid)
        {
            var source = _status.source.Find((_item) =>
            {
                return _item.entity._uuid.AsString().Equals(_uuid);
            });
            if (null == source)
            {
                view.Alert("source not found");
                return;
            }
            view.RefreshSource(source);
        }
    }
}
