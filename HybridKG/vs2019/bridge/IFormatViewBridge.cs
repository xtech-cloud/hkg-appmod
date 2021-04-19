
using XTC.oelMVCS;
namespace HKG.Module.Metatable
{
    public interface IFormatViewBridge : View.Facade.Bridge
    {
        void OnImportYamlSubmit(string _content);
        void OnListSubmit(long _offset, long _count);
        void OnDeleteSubmit(string _uuid);

    }
}
