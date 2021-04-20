
using System;
using XTC.oelMVCS;

namespace HKG.Module.Builder
{
    public class DocumentModel : Model
    {
        public const string NAME = "Builder.DocumentModel";

        public class DocumentStatus : Model.Status
        {
            public const string NAME = "Builder.DocumentStatus";
        }

        protected override void preSetup()
        {
            Error err;
            status_ = spawnStatus<DocumentStatus>(DocumentStatus.NAME, out err);
        }

        protected override void setup()
        {
            getLogger().Trace("setup DocumentModel");
        }

        protected override void preDismantle()
        {
            Error err;
            killStatus(DocumentStatus.NAME, out err);
        }

        private DocumentStatus status
        {
            get
            {
                return status_ as DocumentStatus;
            }
        }
    }
}
