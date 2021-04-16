
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class SchemaModel : Model
    {
        public const string NAME = "Metatable.SchemaModel";

        public class SchemaStatus : Model.Status
        {
            public const string NAME = "Metatable.SchemaStatus";

            public List<Proto.SchemaEntity> schemas = new List<Proto.SchemaEntity>();
            public long total = 0;
        }

        protected override void preSetup()
        {
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

        public void SaveSchemas(SchemaStatus _status)
        {
            status.schemas = _status.schemas;
            status.total = _status.total;
        }
    }
}
