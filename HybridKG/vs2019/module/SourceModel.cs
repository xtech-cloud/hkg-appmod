
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class SourceModel : Model
    {
        public const string NAME = "Metatable.SourceModel";

        public class SourceStatus : Model.Status
        {
            public const string NAME = "Metatable.SourceStatus";

            public List<Proto.SourceEntity> sources = new List<Proto.SourceEntity>();
            public long total = 0;
        }

        protected override void preSetup()
        {
            Error err;
            status_ = spawnStatus<SourceStatus>(SourceStatus.NAME, out err);
        }

        protected override void setup()
        {
            getLogger().Trace("setup SourceModel");
        }

        protected override void preDismantle()
        {
            Error err;
            killStatus(SourceStatus.NAME, out err);
        }

        private SourceStatus status
        {
            get
            {
                return status_ as SourceStatus;
            }
        }

        public void SaveSources(SourceStatus _status)
        {
            status.sources = _status.sources;
            status.total = _status.total;
        }
    }
}
