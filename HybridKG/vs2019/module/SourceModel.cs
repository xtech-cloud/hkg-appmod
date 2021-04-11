
using System;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class SourceModel : Model
    {
        public const string NAME = "Metatable.SourceModel";

        public class SourceStatus : Model.Status
        {
            public const string NAME = "Metatable.SourceStatus";
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
    }
}
