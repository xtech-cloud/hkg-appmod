
using XTC.oelMVCS;
namespace HKG.Module.Builder
{
    public interface IDocumentViewBridge : View.Facade.Bridge
    {
        void OnMergeSubmit(string _name, string[] _label, string[] _text, string _format);
        void OnListSubmit(long _offset, long _count);
        void OnDocumentSelected(string _uuid);

    }
}
