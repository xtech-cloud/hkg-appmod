
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Collector
{
    public class DocumentModel : Model
    {
        public const string NAME = "Collector.DocumentModel";

        public class DocumentStatus : Model.Status
        {
            public const string NAME = "Collector.DocumentStatus";

            public List<Proto.DocumentEntity> documents = new List<Proto.DocumentEntity>();
            public long total = 0;
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

        public void SaveDocuments(DocumentStatus _status)
        {
            status.documents = _status.documents;
        }
    }
}
