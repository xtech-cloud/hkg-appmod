
using XTC.oelMVCS;
namespace HKG.Module
{
    public class CrossViewBridge : ICrossViewBridge
    {
        public CrossView view { get; set; }
        public CrossService service { get; set; }

        public void OnMergeFromMetatableSubmit(string _format)
        {
            service.MergeFromMetatable(_format);
        }

        public void OnScrapeFromMetatableSubmit()
        {
            service.ScrapeFromMetatable();
        }

        public void OnTidyFromMetatableSubmit()
        {
            service.TidyFromMetatable();
        }
    }
}
