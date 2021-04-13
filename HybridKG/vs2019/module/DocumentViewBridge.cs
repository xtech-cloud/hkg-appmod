
using XTC.oelMVCS;
namespace HKG.Module.Collector
{
    public class DocumentViewBridge : IDocumentViewBridge
    {
        public DocumentView view{ get; set; }
        public DocumentService service{ get; set; }
        public DocumentModel model { get; set; }



        public void OnScrapeSubmit(string _name, string[] _keyword, string _address, string _attribute)
        {
            Proto.DocumentScrapeRequest req = new Proto.DocumentScrapeRequest();
            req._name = Proto.Field.FromString(_name);
            //TODO 未实现的字段赋值 string[] keyword
            req._address = Proto.Field.FromString(_address);
            req._attribute = Proto.Field.FromString(_attribute);

            service.PostScrape(req);
        }
        

        public void OnListSubmit(long _offset, long _count)
        {
            Proto.ListRequest req = new Proto.ListRequest();
            req._offset = Proto.Field.FromLong(_offset);
            req._count = Proto.Field.FromLong(_count);

            service.PostList(req);
        }

        public void OnDocumentSelected(string _uuid)
        {
            model.Broadcast("/hkg/collector/document/selected", _uuid);
        }
    }
}
