
using XTC.oelMVCS;
namespace HKG.Module.Metatable
{
    public interface IVocabularyUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);

        void RefreshVocabularyList(long _total, object _entity);
    }
}
