
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class VocabularyModel : Model
    {
        public const string NAME = "hkg.metatable.VocabularyModel";

        private VocabularyController controller { get; set; }

        public class VocabularyStatus : Model.Status
        {
            public class Vocabulary
            {
                public Proto.VocabularyEntity entity { get; set; }
            }

            public const string NAME = "hkg.metatable.VocabularyStatus";
            public List<Vocabulary> vocabulary = new List<Vocabulary>();
        }


        protected override void preSetup()
        {
            controller = findController(VocabularyController.NAME) as VocabularyController;
            Error err;
            status_ = spawnStatus<VocabularyStatus>(VocabularyStatus.NAME, out err);
        }

        protected override void setup()
        {
            getLogger().Trace("setup VocabularyModel");
        }

        protected override void preDismantle()
        {
            Error err;
            killStatus(VocabularyStatus.NAME, out err);
        }

        private VocabularyStatus status
        {
            get
            {
                return status_ as VocabularyStatus;
            }
        }

        public void UpdateList(Model.Status _reply, Proto.VocabularyEntity[] _list)
        {
            controller.List(_reply, status, _list);
        }

        public void UpdateGet(string _uuid)
        {
            controller.Get(status, _uuid);
        }
    }
}
