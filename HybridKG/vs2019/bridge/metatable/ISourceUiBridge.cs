
using XTC.oelMVCS;
namespace HKG.Module.Metatable
{
    public interface ISourceUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);

        void RefreshSourceList(long _total, object _entity);
    }
}
