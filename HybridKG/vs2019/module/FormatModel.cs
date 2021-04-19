
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class FormatModel : Model
    {
        public const string NAME = "Metatable.FormatModel";

        public class FormatStatus : Model.Status
        {
            public const string NAME = "Metatable.FormatModel";

            public List<Proto.FormatEntity> formats = new List<Proto.FormatEntity>();
            public long total = 0;
        }

        protected override void preSetup()
        {
            Error err;
            status_ = spawnStatus<FormatStatus>(FormatStatus.NAME, out err);
        }

        protected override void setup()
        {
            getLogger().Trace("setup FormatModel");
        }

        protected override void preDismantle()
        {
            Error err;
            killStatus(FormatStatus.NAME, out err);
        }

        private FormatStatus status
        {
            get
            {
                return status_ as FormatStatus;
            }
        }

        public void SaveFormats(FormatStatus _status)
        {
            status.formats = _status.formats;
            status.total = _status.total;
        }
    }
}
