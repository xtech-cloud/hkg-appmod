
using XTC.oelMVCS;
namespace HKG.Module.Metatable
{
    public interface ISchemaUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);

        void RefreshSchemaList(long _total, object _entity);

    }
}
