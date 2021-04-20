
using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class SchemaService: Service
    {
        public const string NAME = "Metatable.SchemaService";
        private SchemaModel model = null;

        protected override void preSetup()
        {
            model = findModel(SchemaModel.NAME) as SchemaModel;
        }

        protected override void setup()
        {
            getLogger().Trace("setup SchemaService");
        }

        protected override void postSetup()
        {
            Proto.ListRequest req = new Proto.ListRequest();
            req._offset = Proto.Field.FromLong(0);
            req._count = Proto.Field.FromLong(int.MaxValue);
            PostList(req);
        }

        public void PostImportYaml(Proto.ImportYamlRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["content"] = _request._content.AsAny();

            post(string.Format("{0}/hkg/metatable/Schema/ImportYaml", getConfig()["domain"].AsString()), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, options);
                SchemaModel.SchemaStatus status = Model.Status.New<SchemaModel.SchemaStatus>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                model.Broadcast("/hkg/metatable/Schema/ImportYaml", rsp);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
            }, null);
        }
        

        public void PostList(Proto.ListRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["offset"] = _request._offset.AsAny();
            paramMap["count"] = _request._count.AsAny();

            post(string.Format("{0}/hkg/metatable/Schema/List", getConfig()["domain"].AsString()), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.SchemaListResponse>(_reply, options);
                SchemaModel.SchemaStatus status = Model.Status.New<SchemaModel.SchemaStatus>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                status.schemas = new List<Proto.SchemaEntity>(rsp._entity);
                status.total = rsp._total.AsLong();
                model.SaveSchemas(status);
                model.Broadcast("/hkg/metatable/Schema/List", status);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
            }, null);
        }
        

        public void PostDelete(Proto.DeleteRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["uuid"] = _request._uuid.AsAny();

            post(string.Format("{0}/hkg/metatable/Schema/Delete", getConfig()["domain"].AsString()), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, options);
                SchemaModel.SchemaStatus status = Model.Status.New<SchemaModel.SchemaStatus>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                model.Broadcast("/hkg/metatable/Schema/Delete", rsp);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
            }, null);
        }
        


        protected override void asyncRequest(string _url, string _method, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(_url); 
            req.Method = _method;
            req.ContentType =
            "application/json;charset=utf-8";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new AnyConverter());
            string json = System.Text.Json.JsonSerializer.Serialize(_params, options);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
            }
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            if(rsp == null)
            {
                _onError(Error.NewNullErr("HttpWebResponse is null"));
                return;
            }
            if(rsp.StatusCode != HttpStatusCode.OK)
            {
                rsp.Close();
                _onError(new Error(rsp.StatusCode.GetHashCode(), "HttpStatusCode != 200"));
                return;
            }
            string reply = "";
            StreamReader sr;
            using (sr = new StreamReader(rsp.GetResponseStream()))
            {
                reply = sr.ReadToEnd();
            }
            sr.Close();
            _onReply(reply);
        }
    }
}
