
using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module
{
    public class CrossService : Service
    {
        public const string NAME = "CrossService";

        protected override void preSetup()
        {
        }

        protected override void setup()
        {
            getLogger().Trace("setup CrossService");
        }



        public void ScrapeFromMetatable ()
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["offset"] = Any.FromLong(0);
            paramMap["count"] = Any.FromLong(long.MaxValue);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new Metatable.FieldConverter());

            post(string.Format("{0}/hkg/metatable/Vocabulary/List", getConfig()["domain"].AsString()), paramMap, (_reply) =>
            {
                var rspVocabulary = JsonSerializer.Deserialize<Metatable.Proto.VocabularyListResponse>(_reply, options);
                if (0 != rspVocabulary._status._code.AsInt())
                {
                    getLogger().Error(rspVocabulary._status._message.AsString());
                    return;
                }

                post(string.Format("{0}/hkg/metatable/Source/List", getConfig()["domain"].AsString()), paramMap, (_reply) =>
                {
                    var rspSource = JsonSerializer.Deserialize<Metatable.Proto.SourceListResponse>(_reply, options);
                    if (0 != rspSource._status._code.AsInt())
                    {
                        getLogger().Error(rspSource._status._message.AsString());
                        return;
                    }

                    foreach (var eVocabulary in rspVocabulary._entity)
                    {
                        foreach (var eSource in rspSource._entity)
                        {
                            Dictionary<string, Any> docParamMap = new Dictionary<string, Any>();
                            docParamMap["name"] = Any.FromString(eVocabulary._name.AsString());
                            //TODO 未实现的字段 string[] keyword
                            docParamMap["address"] = Any.FromString(eSource._expression.AsString().Replace("{?}", eVocabulary._name.AsString()));
                            docParamMap["attribute"] = Any.FromString(eSource._attribute.AsString());

                            post(string.Format("{0}/hkg/collector/Document/Scrape", getConfig()["domain"].AsString()), docParamMap, (_reply) =>
                            {
                                var rspScrape = JsonSerializer.Deserialize<Collector.Proto.BlankResponse>(_reply, options);
                                if (0 != rspScrape._status._code.AsInt())
                                {
                                    getLogger().Error(rspScrape._status._message.AsString());
                                    return;
                                }
                            }, (_err) =>
                            {
                                getLogger().Error(_err.getMessage());
                            }, null);
                        }
                    }

                }, (_err) =>
                {
                    getLogger().Error(_err.getMessage());
                }, null);

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
            options.Converters.Add(new Metatable.AnyConverter());
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
