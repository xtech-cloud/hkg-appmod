
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class SourceModel : Model
    {
        public const string NAME = "hkg.metatable.SourceModel";

        public class SourceStatus : Model.Status
        {
            public class Source
            {
                public Proto.SourceEntity entity { get; set; }
            }
            public const string NAME = "hkg.metatable.SourceStatus";
            public List<Source> source = new List<Source>();
        }

        private SourceController controll { get; set; }

        protected override void preSetup()
        {
            controll = findController(SourceController.NAME) as SourceController;
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

        public void UpdateList(Model.Status _reply, Proto.SourceEntity[] _source)
        {
            controll.List(_reply, status, _source);
        }

        public void UpdateGet(string _uuid)
        {
            controll.Get(status, _uuid);
        }
    }
}
