
using System;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class SourceController: Controller
    {
        public const string NAME = "Metatable.SourceController";

        protected override void setup()
        {
            getLogger().Trace("setup SourceController");
        }
    }
}
