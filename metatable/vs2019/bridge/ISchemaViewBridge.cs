
using XTC.oelMVCS;
namespace hkg.metatable
{
    public interface ISchemaViewBridge : View.Facade.Bridge
    {
        void OnImportYamlSubmit(string _content);
        void OnListSubmit(long _offset, long _count);
        void OnDeleteSubmit(string _uuid);

        void OnLocationChanged(string _location);
        void OnGetSubmit(string _uuid);
    }
}
