
using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.collector
{
    public class DocumentService : Service
    {
        public const string NAME = "hkg.collector.DocumentService";
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

        public void PostScrape(Proto.DocumentScrapeRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["name"] = _request._name.AsAny();
            paramMap["keyword"] = _request._keyword.AsAny();
            paramMap["address"] = _request._address.AsAny();
            paramMap["attribute"] = _request._attribute.AsAny();

            post(string.Format("{0}/hkg/collector/Document/Scrape", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, options);
                Model.Status reply = Model.Status.New<Model.Status>(rsp._status._code.AsInt(), rsp._status._message.AsString());
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
            paramMap["offset"] = _request._offset.AsAny();
            paramMap["count"] = _request._count.AsAny();

            post(string.Format("{0}/hkg/collector/Document/List", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.DocumentListResponse>(_reply, options);
                Model.Status status = Model.Status.New<Model.Status>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                model.UpdateList(status, rsp._total.AsLong(), rsp._entity);
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
            paramMap["uuid"] = _request._uuid.AsAny();
            paramMap["host"] = _request._host.AsAny();
            paramMap["rule"] = Any.FromStringMap(_request._rule);

            post(string.Format("{0}/hkg/collector/Document/Tidy", domain), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, options);
                Model.Status reply = Model.Status.New<Model.Status>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                model.UpdateTidyFinish(reply, paramMap["uuid"].AsString());
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
                Model.Status reply = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateTidyFinish(reply, null); 
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
