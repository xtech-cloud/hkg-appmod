
using XTC.oelMVCS;
namespace HKG.Module.Metatable
{
    public interface IFormatUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);

        void RefreshFormatList(long _total, object _entity);
    }
}
