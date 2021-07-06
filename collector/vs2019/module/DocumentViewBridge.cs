
using XTC.oelMVCS;
using System.Collections.Generic;
namespace hkg.collector
{
    public class DocumentViewBridge : IDocumentViewBridge
    {
        public DocumentView view { get; set; }
        public DocumentService service { get; set; }


        public void OnScrapeSubmit(string _name, string[] _keyword, string _address, string _attribute)
        {
            Proto.DocumentScrapeRequest req = new Proto.DocumentScrapeRequest();
            req._name = Any.FromString(_name);
            req._keyword = Any.FromStringAry(_keyword);
            req._address = Any.FromString(_address);
            req._attribute = Any.FromString(_attribute);

            service.PostScrape(req);
        }


        public void OnListSubmit(long _offset, long _count, Dictionary<string, string> _filter)
        {
            Proto.ListRequest req = new Proto.ListRequest();
            req._offset = Any.FromInt64(_offset);
            req._count = Any.FromInt64(_count);

            service.PostList(req);
        }


        public void OnTidySubmit(string _uuid, string _host, System.Collections.Generic.Dictionary<string, string> _rule)
        {
            Proto.DocumentTidyRequest req = new Proto.DocumentTidyRequest();
            req._uuid = Any.FromString(_uuid);
            req._host = Any.FromString(_host);
            req._rule = Any.FromStringMap(_rule);

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
            req._offset = Any.FromInt64(0);
            req._count = Any.FromInt64(int.MaxValue);
            service.PostList(req);
        }

        public void OnGetSubmit(string _uuid)
        {
            view.OnGetSubmit(_uuid);
        }

        public void OnDeleteSubmit(string _uuid)
        {
            Proto.DocumentDeleteRequest req = new Proto.DocumentDeleteRequest();
            req._uuid = Any.FromString(_uuid);
            service.PostDelete(req);
        }

        public void OnBatchDeleteSubmit(string[] _uuid)
        {
            Proto.DocumentBatchDeleteRequest req = new Proto.DocumentBatchDeleteRequest();
            req._uuid = Any.FromStringAry(_uuid);
            service.PostBatchDelete(req);
        }
    }
}
