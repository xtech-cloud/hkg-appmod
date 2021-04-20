
using System;
using XTC.oelMVCS;

namespace HKG.Module.Builder
{
    public class DocumentController: Controller
    {
        public const string NAME = "Builder.DocumentController";

        protected override void setup()
        {
            getLogger().Trace("setup DocumentController");
        }
    }
}
