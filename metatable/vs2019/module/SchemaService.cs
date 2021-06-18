
using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class SchemaService : Service
    {
        public const string NAME = "hkg.metatable.SchemaService";
        private string domain { get; set; }
        private SchemaModel model = null;

        protected override void preSetup()
        {
            model = findModel(SchemaModel.NAME) as SchemaModel;
        }

        protected override void setup()
        {
            getLogger().Trace("setup SchemaService");
        }

        public void SwitchLocation(string _location)
        {
            domain = getConfig()[string.Format("domain.{0}", _location)].AsString();
        }

        public void PostImportYaml(Proto.ImportYamlRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["content"] = _request._content.AsAny();

            post(string.Format("{0}/hkg/metatable/Schema/ImportYaml", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, options);
                Model.Status status = Model.Status.New<Model.Status>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                //TODO
                Proto.ListRequest req = new Proto.ListRequest();
                req._offset = Proto.Field.FromLong(0);
                req._count = Proto.Field.FromLong(int.MaxValue);
                PostList(req);
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

            post(string.Format("{0}/hkg/metatable/Schema/List", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.SchemaListResponse>(_reply, options);
                Model.Status status = Model.Status.New<Model.Status>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                model.UpdateList(status, rsp._entity);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
                Model.Status status = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateList(status, null);
            }, null);
        }


        public void PostDelete(Proto.DeleteRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["uuid"] = _request._uuid.AsAny();

            post(string.Format("{0}/hkg/metatable/Schema/Delete", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, options);
                Model.Status status = Model.Status.New<Model.Status>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                //TODO
                Proto.ListRequest req = new Proto.ListRequest();
                req._offset = Proto.Field.FromLong(0);
                req._count = Proto.Field.FromLong(int.MaxValue);
                PostList(req);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
            }, null);
        }



        protected override void asyncRequest(string _url, string _method, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            string reply = "";
            try
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
                if (rsp == null)
                {
                    _onError(Error.NewNullErr("HttpWebResponse is null"));
                    return;
                }
                if (rsp.StatusCode != HttpStatusCode.OK)
                {
                    rsp.Close();
                    _onError(new Error(rsp.StatusCode.GetHashCode(), "HttpStatusCode != 200"));
                    return;
                }
                StreamReader sr;
                using (sr = new StreamReader(rsp.GetResponseStream()))
                {
                    reply = sr.ReadToEnd();
                }
                sr.Close();
            }
            catch (System.Exception ex)
            {
                _onError(Error.NewException(ex));
                return;
            }
            _onReply(reply);
        }
    }
}
