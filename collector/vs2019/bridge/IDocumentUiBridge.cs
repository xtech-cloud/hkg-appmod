
using System.Collections.Generic;
using XTC.oelMVCS;
namespace hkg.collector
{
    public interface IDocumentUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);

        void RefreshList(long _total, List<Dictionary<string, Any>> _document);
        void RefreshOne(Dictionary<string, Any> _document);
        void RefreshQuerySourceList(List<Dictionary<string, Any>> _list);
        void RefreshQueryVocabularyList(List<Dictionary<string, Any>> _list);
        void RefreshQuerySchemaList(List<Dictionary<string, Any>> _list);
        void RefreshQuerySchemaRuleExpression(Dictionary<string, Dictionary<string, string>> _list);

        void RefreshScrapeFinish(int _code, string _message, string _name, string _address);
        void RefreshTidyFinish(int _code, string _message, string _uuid);
        void RefreshRemovedDocument(List<string> _uuid);

        void RefreshActivateLocation(string _location);
    }
}
