
using XTC.oelMVCS;
namespace hkg.collector
{
    public class DocumentViewBridge : IDocumentViewBridge
    {
        public DocumentView view { get; set; }
        public DocumentService service { get; set; }


        public void OnScrapeSubmit(string _name, string[] _keyword, string _address, string _attribute)
        {
            Proto.DocumentScrapeRequest req = new Proto.DocumentScrapeRequest();
            req._name = Proto.Field.FromString(_name);
            req._keyword = Proto.Field.FromStringAry(_keyword);
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


        public void OnTidySubmit(string _uuid, string _host, System.Collections.Generic.Dictionary<string, string> _rule)
        {
            Proto.DocumentTidyRequest req = new Proto.DocumentTidyRequest();
            req._uuid = Proto.Field.FromString(_uuid);
            req._host = Proto.Field.FromString(_host);
            req._rule = _rule;

            service.PostTidy(req);
        }

        public void OnLocationChanged(string _location)
        {
            service.useMock = _location.Equals("local");
            service.SwitchLocation(_location);

            view.QueryMetatableSource();
            view.QueryMetatableVocabulary();
            view.QueryMetatableSchema();

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
