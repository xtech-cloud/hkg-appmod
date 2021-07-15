
using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using XTC.oelMVCS;
using System.Threading.Tasks;

namespace hkg.collector
{
    public class DocumentService : Service
    {
        public const string NAME = "hkg.collector.DocumentService";
        public string domainPublic = "";
        public string domainPrivate = "";
        public string accessToken = "";
        public string uuid = "";
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
            if (_location.Equals("public"))
                domain = domainPublic;
            else if (_location.Equals("private"))
                domain = domainPrivate;

            //开发时使用
            if (string.IsNullOrEmpty(domain))
                domain = getConfig()[string.Format("_.domain.{0}", _location)].AsString();
        }

        public void PostScrape(Proto.DocumentScrapeRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["name"] = _request._name;
            paramMap["keyword"] = _request._keyword;
            paramMap["address"] = _request._address;
            paramMap["attribute"] = _request._attribute;

            post(string.Format("{0}/hkg/collector/Document/Scrape", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new AnyProtoConverter());
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, options);
                Model.Status reply = Model.Status.New<Model.Status>(rsp._status._code.AsInt32(), rsp._status._message.AsString());
                model.UpdateScrapeFinish(reply, paramMap["name"].AsString(), paramMap["address"].AsString());
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
                Model.Status reply = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateScrapeFinish(reply, null, null);
            }, null);
        }


        public void PostList(Proto.ListRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["offset"] = _request._offset;
            paramMap["count"] = _request._count;

            post(string.Format("{0}/hkg/collector/Document/List", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new AnyProtoConverter());
                var rsp = JsonSerializer.Deserialize<Proto.DocumentListResponse>(_reply, options);
                Model.Status status = Model.Status.New<Model.Status>(rsp._status._code.AsInt32(), rsp._status._message.AsString());
                model.UpdateList(status, rsp._total.AsInt64(), rsp._entity);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
                Model.Status status = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateList(status, 0, null);
            }, null);
        }


        public void PostTidy(Proto.DocumentTidyRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["uuid"] = _request._uuid;
            paramMap["host"] = _request._host;
            paramMap["rule"] = _request._rule;

            post(string.Format("{0}/hkg/collector/Document/Tidy", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new AnyProtoConverter());
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, options);
                Model.Status reply = Model.Status.New<Model.Status>(rsp._status._code.AsInt32(), rsp._status._message.AsString());
                model.UpdateTidyFinish(reply, paramMap["uuid"].AsString());
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
                Model.Status reply = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateTidyFinish(reply, null);
            }, null);
        }

        public void PostDelete(Proto.DocumentDeleteRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["uuid"] = _request._uuid;

            post(string.Format("{0}/hkg/collector/Document/Delete", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new AnyProtoConverter());
                var rsp = JsonSerializer.Deserialize<Proto.DocumentDeleteResponse>(_reply, options);
                Model.Status status = Model.Status.New<Model.Status>(rsp._status._code.AsInt32(), rsp._status._message.AsString());
                List<string> uuid = new List<string>();
                uuid.Add(rsp._uuid.AsString());
                model.UpdateDelete(status, uuid);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
                Model.Status status = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateDelete(status, null);
            }, null);
        }

        public void PostBatchDelete(Proto.DocumentBatchDeleteRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["uuid"] = _request._uuid;

            post(string.Format("{0}/hkg/collector/Document/BatchDelete", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new AnyProtoConverter());
                var rsp = JsonSerializer.Deserialize<Proto.DocumentBatchDeleteResponse>(_reply, options);
                Model.Status status = Model.Status.New<Model.Status>(rsp._status._code.AsInt32(), rsp._status._message.AsString());
                List<string> uuid = new List<string>();
                uuid.AddRange(rsp._uuid.AsStringAry());
                model.UpdateDelete(status, uuid);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
                Model.Status status = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateDelete(status, null);
            }, null);
        }




        protected override void asyncRequest(string _url, string _method, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
        {
            doAsyncRequest(_url, _method, _params, _onReply, _onError, _options);    
        }

        protected async void doAsyncRequest(string _url, string _method, Dictionary<string, Any> _params, OnReplyCallback _onReply, OnErrorCallback _onError, Options _options)
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
                byte[] data = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(_params, options);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                }
                HttpWebResponse rsp = await req.GetResponseAsync() as HttpWebResponse;
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
