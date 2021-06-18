
using XTC.oelMVCS;
namespace hkg.metatable
{
    public class SourceViewBridge : ISourceViewBridge
    {
        public SourceView view{ get; set; }
        public SourceService service{ get; set; }


        public void OnImportYamlSubmit(string _content)
        {
            Proto.ImportYamlRequest req = new Proto.ImportYamlRequest();
            req._content = Proto.Field.FromString(_content);

            service.PostImportYaml(req);
        }
        

        public void OnListSubmit(long _offset, long _count)
        {
            Proto.ListRequest req = new Proto.ListRequest();
            req._offset = Proto.Field.FromLong(_offset);
            req._count = Proto.Field.FromLong(_count);

            service.PostList(req);
        }
        

        public void OnDeleteSubmit(string _uuid)
        {
            Proto.DeleteRequest req = new Proto.DeleteRequest();
            req._uuid = Proto.Field.FromString(_uuid);

            service.PostDelete(req);
        }

        public void OnLocationChanged(string _location)
        {
            service.useMock = _location.Equals("local");
            service.SwitchLocation(_location);

            Proto.ListRequest req = new Proto.ListRequest();
            req._offset = Proto.Field.FromLong(0);
            req._count = Proto.Field.FromLong(int.MaxValue);
            service.PostList(req);
        }

        public void OnGetSubmit(string _uuid)
        {
            view.OnGetSubmit(_uuid);
        }
    }
}
