
using XTC.oelMVCS;
namespace HKG.Module.Metatable
{
    public interface IVocabularyViewBridge : View.Facade.Bridge
    {
        void OnImportYamlSubmit(string _content);
        void OnListSubmit(long _offset, long _count);
        void OnFindSubmit(string _name);

    }
}
