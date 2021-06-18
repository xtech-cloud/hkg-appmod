using XTC.oelMVCS;

namespace hkg.collector
{
    public class BlankModel : Model
    {
        public const string NAME = "hkg.collector.BlankModel";


        protected override void setup()
        {
            getLogger().Trace("setup hkg.collector.BlankModel");
        }
    }
}
