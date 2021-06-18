
using XTC.oelMVCS;
namespace hkg.builder
{
    public class DocumentViewBridge : IDocumentViewBridge
    {
        public DocumentView view{ get; set; }
        public DocumentService service{ get; set; }
        public DocumentModel model { get; set; }
        public QueryModel queryModel { get; set; }

        public void OnMergeSubmit(string _name, string[] _label, string[] _text, string _format)
        {
            Proto.DocumentMergeRequest req = new Proto.DocumentMergeRequest();
            req._name = Proto.Field.FromString(_name);
            req._label = Proto.Field.FromStringAry(_label);
            req._text = Proto.Field.FromStringAry(_text);
            req._format = Proto.Field.FromString(_format);

            service.PostMerge(req);
        }
        

        public void OnListSubmit(long _offset, long _count)
        {
            Proto.ListRequest req = new Proto.ListRequest();
            req._offset = Proto.Field.FromLong(_offset);
            req._count = Proto.Field.FromLong(_count);

            service.PostList(req);
        }

        public void OnLocationChanged(string _location)
        {
            service.useMock = _location.Equals("local");
            service.SwitchLocation(_location);

            view.QueryMetatableFormatList();
            view.QueryCollectorDocumentList();

            Proto.ListRequest req = new Proto.ListRequest();
            req._offset = Proto.Field.FromLong(0);
            req._count = Proto.Field.FromLong(int.MaxValue);
            service.PostList(req);
        }

        public void OnGetSubmit(string _uuid)
        {
            throw new System.NotImplementedException();
        }

        public void BuildMergeParam(string _formatName, ref string _paramFormat, string _documentCode, ref string[] _paramLabel, ref string[] _paramText)
        {
            queryModel.BuildMergeParam(_formatName, ref _paramFormat, _documentCode, ref _paramLabel, ref _paramText);
        }
    }
}
