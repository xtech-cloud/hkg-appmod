
using System;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class SchemaController : Controller
    {
        public const string NAME = "hkg.metatable.SchemaController";

        private SchemaView view { get; set; }

        protected override void preSetup()
        {
            view = findView(SchemaView.NAME) as SchemaView;
        }

        protected override void setup()
        {
            getLogger().Trace("setup SchemaController");
        }

        public void List(Model.Status _reply, SchemaModel.SchemaStatus _status, Proto.SchemaEntity[] _list)
        {
            _status.schema.Clear();
            if (_reply.getCode() != 0)
            {
                view.Alert(_reply.getMessage());
            }
            else
            {
                foreach (var v in _list)
                {
                    var source = new SchemaModel.SchemaStatus.Schema();
                    source.entity = v;
                    _status.schema.Add(source);
                }
            }
            view.RefreshList(_status.schema);
        }

        public void Get(SchemaModel.SchemaStatus _status, string _uuid)
        {
            var format = _status.schema.Find((_item) =>
            {
                return _item.entity._uuid.AsString().Equals(_uuid);
            });
            if (null == format)
            {
                view.Alert("schema not found");
                return;
            }
            view.RefreshOne(format);
        }

    }
}
