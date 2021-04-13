
using XTC.oelMVCS;
namespace HKG.Module.Collector
{
    public interface IDocumentUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);

        void RefreshList(long _total, object _result);

        void RefreshScrapeProgress(float _value);
        void RefreshScrapeFinish();

        void RefreshDocument(string _rawText);
    }
}
