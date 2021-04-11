
using System;
using XTC.oelMVCS;

namespace HKG.Module.Collector
{
    public class DocumentController: Controller
    {
        public const string NAME = "Collector.DocumentController";

        protected override void setup()
        {
            getLogger().Trace("setup DocumentController");
        }
    }
}
