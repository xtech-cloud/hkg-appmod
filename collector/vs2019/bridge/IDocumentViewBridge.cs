
using XTC.oelMVCS;
using System.Collections.Generic;

namespace hkg.collector
{
    public interface IDocumentViewBridge : View.Facade.Bridge
    {
        void OnScrapeSubmit(string _name, string[] _keyword, string _address, string _attribute);
        void OnListSubmit(long _offset, long _count);
        void OnTidySubmit(string _uuid, string _host, Dictionary<string, string> _rule);

        void OnLocationChanged(string _location);
        void OnGetSubmit(string _uuid);
    }
}
