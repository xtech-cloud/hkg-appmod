
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Collector
{
    public class DocumentService: Service
    {
        public const string NAME = "Collector.DocumentService";
        private DocumentModel model = null;

        protected override void preSetup()
        {
            model = findModel(DocumentModel.NAME) as DocumentModel;
        }

        protected override void setup()
        {
            getLogger().Trace("setup DocumentService");
        }

        protected override void postSetup()
        {
        }

        public void PostScrape(Proto.DocumentScrapeRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["name"] = _request._name.AsAny();
            paramMap["keywork"] = _request._keyword.AsAny();
            paramMap["address"] = _request._address.AsAny();
            paramMap["attribute"] = _request._attribute.AsAny();

            post(string.Format("{0}/hkg/collector/Document/Scrape", getConfig()["domain"].AsString()), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                };
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, options);
                DocumentModel.DocumentStatus status = Model.Status.New<DocumentModel.DocumentStatus>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                model.Broadcast("/hkg/collector/Document/Scrape", rsp);
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

            post(string.Format("{0}/hkg/collector/Document/List", getConfig()["domain"].AsString()), paramMap, (_reply) =>
            {
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                };
                options.Converters.Add(new FieldConverter());
                var rsp = JsonSerializer.Deserialize<Proto.DocumentListResponse>(_reply, options);
                DocumentModel.DocumentStatus status = Model.Status.New<DocumentModel.DocumentStatus>(rsp._status._code.AsInt(), rsp._status._message.AsString());
                status.documents = new List<Proto.DocumentEntity>(rsp._entity);
                status.total = rsp._total.AsLong();
                model.SaveDocuments(status);
                model.Broadcast("/hkg/collector/Document/List", status);
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
