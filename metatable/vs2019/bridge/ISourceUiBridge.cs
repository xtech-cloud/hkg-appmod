
using System.Collections.Generic;
using XTC.oelMVCS;
namespace hkg.metatable
{
    public interface ISourceUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);

        void RefreshSourceList(List<Dictionary<string, string>> _source);
        void RefreshSource(Dictionary<string, string> _source);
        void RefreshActivateLocation(string _location);
    }
}
