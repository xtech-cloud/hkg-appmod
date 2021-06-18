
using System.Collections.Generic;
using XTC.oelMVCS;
namespace hkg.builder
{
    public interface IDocumentUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);
        void RefreshList(long _total, List<Dictionary<string, string>> _document);

        void RefreshQueryCollectorDocumentList(List<Dictionary<string, string>> _list);
        void RefreshQueryMetatableFormatList(List<Dictionary<string, string>> _list);
    }
}
