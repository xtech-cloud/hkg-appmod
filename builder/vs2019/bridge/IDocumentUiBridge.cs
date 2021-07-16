
using System.Collections.Generic;
using XTC.oelMVCS;
namespace hkg.builder
{
    public interface IDocumentUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);
        void RefreshList(long _total, List<Dictionary<string, string>> _document);
        void RefreshRemovedDocument(List<string> _uuid);

        void RefreshExternalCollectorDocumentList(List<Dictionary<string, string>> _list);
        void RefreshExternalMetatableFormatList(List<Dictionary<string, string>> _list);
        void RefreshActivateLocation(string _location);
    }
}
