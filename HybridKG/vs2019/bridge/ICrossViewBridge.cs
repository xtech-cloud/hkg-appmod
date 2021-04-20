
using XTC.oelMVCS;
namespace HKG.Module
{
    public interface ICrossViewBridge : View.Facade.Bridge
    {
        void OnScrapeFromMetatableSubmit();
        void OnTidyFromMetatableSubmit();
        void OnMergeFromMetatableSubmit(string _format);
    }
}
