
using XTC.oelMVCS;
namespace hkg.metatable
{
    public class VocabularyViewBridge : IVocabularyViewBridge
    {
        public VocabularyView view{ get; set; }
        public VocabularyService service{ get; set; }


        public void OnImportYamlSubmit(string _content)
        {
            Proto.ImportYamlRequest req = new Proto.ImportYamlRequest();
            req._content = Any.FromString(_content);

            service.PostImportYaml(req);
        }
        

        public void OnListSubmit(long _offset, long _count)
        {
            Proto.ListRequest req = new Proto.ListRequest();
            req._offset = Any.FromInt64(_offset);
            req._count = Any.FromInt64(_count);

            service.PostList(req);
        }
        

        public void OnFindSubmit(string _name)
        {
            Proto.FindRequest req = new Proto.FindRequest();
            req._name = Any.FromString(_name);

            service.PostFind(req);
        }
        

        public void OnDeleteSubmit(string _uuid)
        {
            Proto.DeleteRequest req = new Proto.DeleteRequest();
            req._uuid = Any.FromString(_uuid);

            service.PostDelete(req);
        }

        public void OnLocationChanged(string _location)
        {
            service.useMock = _location.Equals("local");
            service.SwitchLocation(_location);

            Proto.ListRequest req = new Proto.ListRequest();
            req._offset = Any.FromInt64(0);
            req._count = Any.FromInt64(int.MaxValue);
            service.PostList(req);
        }

        public void OnGetSubmit(string _uuid)
        {
            view.OnGetSubmit(_uuid);
        }

    }
}
