
using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.builder
{
    public class DocumentService : Service
    {
        public const string NAME = "hkg.builder.DocumentService";
        private DocumentModel model = null;
        public string domain { get; private set; }

        protected override void preSetup()
        {
            model = findModel(DocumentModel.NAME) as DocumentModel;
        }

        protected override void setup()
        {
            getLogger().Trace("setup DocumentService");
        }

        public void SwitchLocation(string _location)
        {
            domain = getConfig()[string.Format("domain.{0}", _location)].AsString();
        }

        public void PostMerge(Proto.DocumentMergeRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["name"] = _request._name;
            paramMap["label"] = _request._label;
            paramMap["text"] = _request._text;
            paramMap["format"] = _request._format;

            post(string.Format("{0}/hkg/builder/Document/Merge", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new AnyProtoConverter());
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, options);
                Model.Status reply = Model.Status.New<Model.Status>(rsp._status._code.AsInt32(), rsp._status._message.AsString());
                model.Broadcast("/hkg/builder/Document/Merge", reply);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
            }, null);
        }


        public void PostList(Proto.ListRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["offset"] = _request._offset;
            paramMap["count"] = _request._count;

            post(string.Format("{0}/hkg/builder/Document/List", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new AnyProtoConverter());
                var rsp = JsonSerializer.Deserialize<Proto.DocumentListResponse>(_reply, options);
                Model.Status reply = Model.Status.New<Model.Status>(rsp._status._code.AsInt32(), rsp._status._message.AsString());
                model.UpdateList(reply, rsp._total.AsInt64(), rsp._entity);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
                Model.Status reply = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateList(reply, 0, null);
            }, null);
        }

        public void PostBatchDelete(Proto.DocumentBatchDeleteRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["uuid"] = _request._uuid;

            post(string.Format("{0}/hkg/builder/Document/BatchDelete", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new AnyProtoConverter());
                var rsp = JsonSerializer.Deserialize<Proto.DocumentBatchDeleteResponse>(_reply, options);
                Model.Status reply = Model.Status.New<Model.Status>(rsp._status._code.AsInt32(), rsp._status._message.AsString());
                List<string> uuid = new List<string>();
                uuid.AddRange(rsp._uuid.AsStringAry());
                model.UpdateDelete(reply, uuid);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
                Model.Status reply = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateDelete(reply, null);
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
                options.Converters.Add(new AnyProtoConverter());
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
