
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class SchemaModel : Model
    {
        public const string NAME = "hkg.metatable.SchemaModel";

        public class SchemaStatus : Model.Status
        {
            public class Schema
            {
                public Proto.SchemaEntity entity { get; set; }
            }
            public const string NAME = "hkg.metatable.SchemaStatus";
            public List<Schema> schema = new List<Schema>();
        }

        private SchemaController controller { get; set; }

        protected override void preSetup()
        {
            controller = findController(SchemaController.NAME) as SchemaController;
            Error err;
            status_ = spawnStatus<SchemaStatus>(SchemaStatus.NAME, out err);
        }

        protected override void setup()
        {
            getLogger().Trace("setup SchemaModel");
        }

        protected override void preDismantle()
        {
            Error err;
            killStatus(SchemaStatus.NAME, out err);
        }

        private SchemaStatus status
        {
            get
            {
                return status_ as SchemaStatus;
            }
        }
        public void UpdateList(Model.Status _reply, Proto.SchemaEntity[] _list)
        {
            controller.List(_reply, status, _list);
        }

        public void UpdateGet(string _uuid)
        {
            controller.Get(status, _uuid);
        }
    }
}
