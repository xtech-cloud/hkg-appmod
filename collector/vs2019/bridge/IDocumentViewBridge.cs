
using XTC.oelMVCS;
using System.Collections.Generic;

namespace hkg.collector
{
    public interface IDocumentViewBridge : View.Facade.Bridge
    {
        void OnScrapeSubmit(string _name, string[] _keyword, string _address, string _attribute);
        void OnListSubmit(long _offset, long _count, Dictionary<string, string> _filter);
        void OnTidySubmit(string _uuid, string _host, Dictionary<string, string> _rule);
        void OnDeleteSubmit(string _uuid);
        void OnBatchDeleteSubmit(string[] _uuid);

        void OnLocationChanged(string _location);
        void OnGetSubmit(string _uuid);
    }
}
