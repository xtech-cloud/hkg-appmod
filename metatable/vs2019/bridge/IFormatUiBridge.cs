
using System.Collections.Generic;
using XTC.oelMVCS;
namespace hkg.metatable
{
    public interface IFormatUiBridge : View.Facade.Bridge
    {
        object getRootPanel();
        void Alert(string _message);
        void RefreshList(List<Dictionary<string, string>> _source);
        void RefreshOne(Dictionary<string, string> _source);

        void RefreshActivateLocation(string _location);
    }
}
