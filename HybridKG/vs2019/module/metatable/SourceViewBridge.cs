
using XTC.oelMVCS;
namespace HKG.Module.Metatable
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
        


    }
}
