
using XTC.oelMVCS;
namespace hkg.metatable
{
    public interface IVocabularyViewBridge : View.Facade.Bridge
    {
        void OnImportYamlSubmit(string _content);
        void OnListSubmit(long _offset, long _count);
        void OnFindSubmit(string _name);
        void OnDeleteSubmit(string _uuid);


        void OnLocationChanged(string _location);
        void OnGetSubmit(string _uuid);
    }
}
