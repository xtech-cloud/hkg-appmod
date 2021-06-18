
using System;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class FormatController : Controller
    {
        public const string NAME = "hkg.metatable.FormatController";

        private FormatView view { get; set; }

        protected override void preSetup()
        {
            view = findView(FormatView.NAME) as FormatView;
        }

        protected override void setup()
        {
            getLogger().Trace("setup FormatController");
        }

        public void List(Model.Status _reply, FormatModel.FormatStatus _status, Proto.FormatEntity[] _list)
        {
            _status.format.Clear();

            if (_reply.getCode() != 0)
            {
                view.Alert(_reply.getMessage());
            }
            else
            {
                foreach (var v in _list)
                {
                    var source = new FormatModel.FormatStatus.Format();
                    source.entity = v;
                    _status.format.Add(source);
                }
            }

            view.RefreshList(_status.format);
        }

        public void Get(FormatModel.FormatStatus _status, string _uuid)
        {
            var format = _status.format.Find((_item) =>
            {
                return _item.entity._uuid.AsString().Equals(_uuid);
            });
            if (null == format)
            {
                view.Alert("format not found");
                return;
            }
            view.RefreshOne(format);
        }
    }
}
