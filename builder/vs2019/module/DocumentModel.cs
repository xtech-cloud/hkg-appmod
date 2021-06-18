
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.builder
{
    public class DocumentModel : Model
    {
        public const string NAME = "hkg.builder.DocumentModel";

        public class DocumentStatus : Model.Status
        {
            public class Document
            {
                public Proto.DocumentEntity entity { get; set; }
            }
            public const string NAME = "hkg.builder.DocumentStatus";
            public List<Document> document = new List<Document>();
        }

        private DocumentController controller {get;set;}

        protected override void preSetup()
        {
            controller = findController(DocumentController.NAME) as DocumentController;
            Error err;
            status_ = spawnStatus<DocumentStatus>(DocumentStatus.NAME, out err);
            if(0 != err.getCode())
            {
                getLogger().Error(err.getMessage());
            }
        }

        protected override void setup()
        {
            getLogger().Trace("setup DocumentModel");
        }

        protected override void preDismantle()
        {
            Error err;
            killStatus(DocumentStatus.NAME, out err);
            if(0 != err.getCode())
            {
                getLogger().Error(err.getMessage());
            }
        }

        private DocumentStatus status
        {
            get
            {
                return status_ as DocumentStatus;
            }
        }

        public void UpdateList(Model.Status _reply, long _total, Proto.DocumentEntity[] _list)
        {
            controller.List(_reply, status, _total, _list);
        }
    }
}
