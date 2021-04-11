
using XTC.oelMVCS;
namespace HKG.Module
{
    public class CrossViewBridge : ICrossViewBridge
    {
        public CrossView view { get; set; }
        public CrossService service { get; set; }

        public void OnScrapeFromMetatableSubmit()
        {
            service.ScrapeFromMetatable();
        }
    }
}
