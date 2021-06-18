
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class FormatModel : Model
    {
        public const string NAME = "hkg.metatable.FormatModel";

        public class FormatStatus : Model.Status
        {
            public class Format
            {
                public Proto.FormatEntity entity { get; set; }
            }
            public const string NAME = "hkg.metatable.FormatStatus";
            public List<Format> format = new List<Format>();
        }

        private FormatController controller { get; set; }
        protected override void preSetup()
        {
            controller = findController(FormatController.NAME) as FormatController;
            Error err;
            status_ = spawnStatus<FormatStatus>(FormatStatus.NAME, out err);
            if (err.getCode() != 0)
                getLogger().Error(err.getMessage());
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
        public void UpdateList(Model.Status _reply, Proto.FormatEntity[] _list)
        {
            controller.List(_reply, status, _list);
        }

        public void UpdateGet(string _uuid)
        {
            controller.Get(status, _uuid);
        }
    }
}
