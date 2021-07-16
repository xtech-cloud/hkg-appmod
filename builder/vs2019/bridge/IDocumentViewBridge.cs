
using XTC.oelMVCS;
namespace hkg.builder
{
    public interface IDocumentViewBridge : View.Facade.Bridge
    {
        void OnMergeSubmit(string _name, string[] _label, string[] _text, string _format);
        void OnListSubmit(long _offset, long _count);
        void OnBatchDeleteSubmit(string[] _uuid);

        void OnLocationChanged(string _location);
        void OnGetSubmit(string _uuid);

        void BuildMergeParam(string _formatName, ref string _paramFormat, string _documentCode, ref string[] _paramLabel, ref string[] _paramText);

        void OnRefreshMetatableFormatSubmit(string _location);
        void OnRefreshCollectorDocumentSubmit(string _location);
    }
}
