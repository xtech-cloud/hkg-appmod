
using XTC.oelMVCS;
namespace hkg.builder
{
    public interface IDocumentViewBridge : View.Facade.Bridge
    {
        void OnMergeSubmit(string _name, string[] _label, string[] _text, string _format);
        void OnListSubmit(long _offset, long _count);

        void OnLocationChanged(string _location);
        void OnGetSubmit(string _uuid);

        void BuildMergeParam(string _formatName, ref string _paramFormat, string _documentCode, ref string[] _paramLabel, ref string[] _paramText);
    }
}
