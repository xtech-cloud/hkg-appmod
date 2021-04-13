
using XTC.oelMVCS;
namespace HKG.Module.Collector
{
    public interface IDocumentViewBridge : View.Facade.Bridge
    {
        void OnScrapeSubmit(string _name, string[] _keyword, string _address, string _attribute);
        void OnListSubmit(long _offset, long _count);
        void OnDocumentSelected(string _uuid);

    }
}
