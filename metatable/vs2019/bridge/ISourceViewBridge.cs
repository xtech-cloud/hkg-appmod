
using XTC.oelMVCS;
namespace HKG.Module.Metatable
{
    public interface ISourceViewBridge : View.Facade.Bridge
    {
        void OnImportYamlSubmit(string _content);
        void OnListSubmit(long _offset, long _count);

    }
}
