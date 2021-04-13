
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module
{
    public class CrossView : View
    {
        public const string NAME = "CrossView";

        private Facade facade = null;

        protected override void preSetup()
        {
            facade = findFacade("CrossFacade");
            var service = findService(CrossService.NAME) as CrossService;
            CrossViewBridge vb = new CrossViewBridge();
            vb.view = this;
            vb.service = service;
            facade.setViewBridge(vb);

        }

        protected override void setup()
        {
            getLogger().Trace("setup CrossView");
        }

        protected override void postSetup()
        {
            
        }
    }
}
