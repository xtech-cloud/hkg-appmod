
using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using XTC.oelMVCS;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace HKG.Module.Metatable
{
    public class SourceService: Service
    {
        public const string NAME = "Metatable.SourceService";
        private SourceModel model = null;

        protected override void preSetup()
        {
            model = findModel(SourceModel.NAME) as SourceModel;
        }

        protected override void setup()
        {
            getLogger().Trace("setup SourceService");
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

            post(string.Format("{0}/hkg/metatable/Source/ImportYaml", getConfig()["domain"].AsString()), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                };
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, options);
                SourceModel.SourceStatus status = Model.Status.New<SourceModel.SourceStatus>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                model.Broadcast("/hkg/metatable/Source/ImportYaml", rsp);
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

            post(string.Format("{0}/hkg/metatable/Source/List", getConfig()["domain"].AsString()), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                };
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.SourceListResponse>(_reply, options);
                SourceModel.SourceStatus status = Model.Status.New<SourceModel.SourceStatus>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                status.sources = new List<Proto.SourceEntity>(rsp._entity);
                status.total = rsp._total.AsLong();
                model.SaveSources(status);
                model.Broadcast("/hkg/metatable/Source/List", status);
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
            var options = new JsonSerializerOptions()
            {
                //WriteIndented = true
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            options.Converters.Add(new AnyConverter());
            byte[] data = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(_params, options);
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
