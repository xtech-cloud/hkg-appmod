using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.collector
{
    public class QueryView : View
    {
        public const string NAME = "hkg.collector.QueryView";

        private BlankModel model = null;
        private QueryService service = null;

        protected override void preSetup()
        {
            model = findModel(BlankModel.NAME) as BlankModel;
            service = findService(QueryService.NAME) as QueryService;
        }

        protected override void setup()
        {
            getLogger().Trace("setup hkg.collector.QueryView");

            route(Actions.Query_Request_Document_List, this.handleQueryDocumentList);
        }

        private void handleQueryDocumentList(Model.Status _status, object _data)
        {
            Dictionary<string, Any> param = (Dictionary<string, Any>)_data;
            Any domain;
            if (!param.TryGetValue("domain", out domain))
                domain = Any.FromString("domain");
            Any offset;
            if (!param.TryGetValue("offset", out offset))
                offset = Any.FromInt64(0);
            Any count;
            if (!param.TryGetValue("count", out count))
                count = Any.FromInt64(int.MaxValue);

            Proto.ListRequest req = new Proto.ListRequest();
            req._offset = Any.FromInt64(offset.AsInt64());
            req._count = Any.FromInt64(count.AsInt64());

            service.PostDocumentList(domain.AsString(), req, (_reply)=>
            {
                model.Broadcast(Actions.Query_Response_Document_List, _reply);
            });
        }
    }
}
