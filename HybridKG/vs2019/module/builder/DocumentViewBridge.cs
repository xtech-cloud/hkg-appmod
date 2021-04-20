
using XTC.oelMVCS;
namespace HKG.Module.Builder
{
    public class DocumentViewBridge : IDocumentViewBridge
    {
        public DocumentView view{ get; set; }
        public DocumentService service{ get; set; }
        public DocumentModel model { get; set; }


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

        public void OnDocumentSelected(string _uuid)
        {
            model.Broadcast("/hkg/builder/document/selected", _uuid);
        }
    }
}
