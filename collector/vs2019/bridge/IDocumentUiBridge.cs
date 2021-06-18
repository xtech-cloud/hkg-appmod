
using System.Collections.Generic;
using XTC.oelMVCS;
namespace hkg.collector
{
    public interface IDocumentUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);

        void RefreshList(long _total, List<Dictionary<string, string>> _document);
        void RefreshOne(Dictionary<string, string> _document);
        void RefreshQuerySourceList(List<Dictionary<string, string>> _list);
        void RefreshQueryVocabularyList(List<Dictionary<string, string>> _list);
        void RefreshQuerySchemaList(List<Dictionary<string, string>> _list);
        void RefreshQuerySchemaRuleExpression(Dictionary<string, Dictionary<string, string>> _list);

        void RefreshScrapeFinish(int _code, string _message, string _name, string _address);
        void RefreshTidyFinish(int _code, string _message, string _uuid);
    }
}
