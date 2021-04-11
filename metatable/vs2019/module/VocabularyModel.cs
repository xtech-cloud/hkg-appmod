
using System;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class VocabularyModel : Model
    {
        public const string NAME = "Metatable.VocabularyModel";

        public class VocabularyStatus : Model.Status
        {
            public const string NAME = "Metatable.VocabularyStatus";
        }

        protected override void preSetup()
        {
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
    }
}
