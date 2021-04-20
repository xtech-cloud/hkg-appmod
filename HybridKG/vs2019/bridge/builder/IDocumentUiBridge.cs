
using XTC.oelMVCS;
namespace HKG.Module.Builder
{
    public interface IDocumentUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);
        void RefreshList(long _total, object _result);

        void RefreshProgress(float _value);
        void RefreshFinish();

        void RefreshDocument(object _doc);
        void RefreshFormatList(object _formats);
    }
}
