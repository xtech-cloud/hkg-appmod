using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using XTC.oelMVCS;
using System.Text.Encodings.Web;

namespace hkg.metatable
{
    public class QueryService : Service
    {
        public const string NAME = "hkg.metatable.QueryService";

        protected override void setup()
        {
            getLogger().Trace("setup QueryService");
        }

        public void PostSourceList(string _domain, Proto.ListRequest _request, System.Action<string> _onReply)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["offset"] = _request._offset.AsAny();
            paramMap["count"] = _request._count.AsAny();

            post(string.Format("{0}/hkg/metatable/Source/List", _domain), paramMap, (_reply) =>
            {
                _onReply(_reply);
            }, (_err) =>
            {
                Proto.BlankResponse rsp = new Proto.BlankResponse();
                rsp._status._code = Proto.Field.FromInt(_err.getCode());
                rsp._status._message = Proto.Field.FromString(_err.getMessage());
                string jsonRsp = responseToJson(rsp);
                _onReply(jsonRsp);
            }, null);
        }

        public void PostSchemaList(string _domain, Proto.ListRequest _request, System.Action<string> _onReply)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["offset"] = _request._offset.AsAny();
            paramMap["count"] = _request._count.AsAny();

            post(string.Format("{0}/hkg/metatable/Schema/List", _domain), paramMap, (_reply) =>
            {
                _onReply(_reply);
            }, (_err) =>
            {
                Proto.BlankResponse rsp = new Proto.BlankResponse();
                rsp._status._code = Proto.Field.FromInt(_err.getCode());
                rsp._status._message = Proto.Field.FromString(_err.getMessage());
                string jsonRsp = responseToJson(rsp);
                _onReply(jsonRsp);
            }, null);
        }

        public void PostFormatList(string _domain, Proto.ListRequest _request, System.Action<string> _onReply)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["offset"] = _request._offset.AsAny();
            paramMap["count"] = _request._count.AsAny();

            post(string.Format("{0}/hkg/metatable/Format/List", _domain), paramMap, (_reply) =>
            {
                _onReply(_reply);
            }, (_err) =>
            {
                Proto.BlankResponse rsp = new Proto.BlankResponse();
                rsp._status._code = Proto.Field.FromInt(_err.getCode());
                rsp._status._message = Proto.Field.FromString(_err.getMessage());
                string jsonRsp = responseToJson(rsp);
                _onReply(jsonRsp);
            }, null);
        }

        public void PostVocabularyList(string _domain, Proto.ListRequest _request, System.Action<string> _onReply)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["offset"] = _request._offset.AsAny();
            paramMap["count"] = _request._count.AsAny();

            post(string.Format("{0}/hkg/metatable/Vocabulary/List", _domain), paramMap, (_reply) =>
            {
                _onReply(_reply);
            }, (_err) =>
            {
                Proto.BlankResponse rsp = new Proto.BlankResponse();
                rsp._status._code = Proto.Field.FromInt(_err.getCode());
                rsp._status._message = Proto.Field.FromString(_err.getMessage());
                string jsonRsp = responseToJson(rsp);
                _onReply(jsonRsp);
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
        private string responseToJson(object _rsp)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new FieldConverter());
            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return JsonSerializer.Serialize(_rsp, options);
        }
    }
}
