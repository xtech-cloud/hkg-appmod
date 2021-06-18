using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class QueryView : View
    {
        public const string NAME = "hkg.metatable.QueryView";

        private BlankModel model = null;
        private QueryService service = null;

        protected override void preSetup()
        {
            model = findModel(BlankModel.NAME) as BlankModel;
            service = findService(QueryService.NAME) as QueryService;
        }

        protected override void setup()
        {
            getLogger().Trace("setup QueryView");

            route(Actions.Query_Request_Source_List, this.handleQuerySourceList);
            route(Actions.Query_Request_Format_List, this.handleQueryFormatList);
            route(Actions.Query_Request_Schema_List, this.handleQuerySchemaList);
            route(Actions.Query_Request_Vocabulary_List, this.handleQueryVocabularyList);
        }

        private void handleQuerySourceList(Model.Status _status, object _data)
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
            req._offset = Proto.Field.FromLong(offset.AsInt64());
            req._count = Proto.Field.FromLong(count.AsInt64());

            service.PostSourceList(domain.AsString(), req, (_reply)=>
            {
                model.Broadcast(Actions.Query_Response_Source_List, _reply);
            });
        }

        private void handleQueryFormatList(Model.Status _status, object _data)
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
            req._offset = Proto.Field.FromLong(offset.AsInt64());
            req._count = Proto.Field.FromLong(count.AsInt64());

            service.PostFormatList(domain.AsString(), req, (_reply) =>
            {
                model.Broadcast(Actions.Query_Response_Format_List, _reply);
            });
        }
        private void handleQuerySchemaList(Model.Status _status, object _data)
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
            req._offset = Proto.Field.FromLong(offset.AsInt64());
            req._count = Proto.Field.FromLong(count.AsInt64());

            service.PostSchemaList(domain.AsString(), req, (_reply) =>
            {
                model.Broadcast(Actions.Query_Response_Schema_List, _reply);
            });
        }

        private void handleQueryVocabularyList(Model.Status _status, object _data)
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
            req._offset = Proto.Field.FromLong(offset.AsInt64());
            req._count = Proto.Field.FromLong(count.AsInt64());

            service.PostVocabularyList(domain.AsString(), req, (_reply) =>
            {
                model.Broadcast(Actions.Query_Response_Vocabulary_List, _reply);
            });
        }

    }
}
