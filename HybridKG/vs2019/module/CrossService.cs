
using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using XTC.oelMVCS;
using System;

namespace HKG.Module
{
    public class CrossService : Service
    {
        public const string NAME = "CrossService";

        private Collector.DocumentModel modelDocument = null;
        private Metatable.SchemaModel modelSchema = null;
        private Metatable.SourceModel modelSource = null;
        private Metatable.VocabularyModel modelVocabulary = null;


        protected override void preSetup()
        {
            modelDocument = findModel(Collector.DocumentModel.NAME) as Collector.DocumentModel;
            modelSchema = findModel(Metatable.SchemaModel.NAME) as Metatable.SchemaModel;
            modelSource = findModel(Metatable.SourceModel.NAME) as Metatable.SourceModel;
            modelVocabulary = findModel(Metatable.VocabularyModel.NAME) as Metatable.VocabularyModel;
        }

        protected override void setup()
        {
            getLogger().Trace("setup CrossService");
        }



        public void ScrapeFromMetatable()
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["offset"] = Any.FromInt64(0);
            paramMap["count"] = Any.FromInt64(long.MaxValue);
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

                    List<Dictionary<string, Any>> docParamMaps = new List<Dictionary<string, Any>>();
                    foreach (var eVocabulary in rspVocabulary._entity)
                    {
                        foreach (var v in eVocabulary._value.AsStringAry())
                        {
                            foreach (var eSource in rspSource._entity)
                            {
                                Dictionary<string, Any> docParamMap = new Dictionary<string, Any>();
                                docParamMap["name"] = Any.FromString(v);
                                docParamMap["keyword"] = Any.FromStringAry(eVocabulary._label.AsStringAry());
                                docParamMap["address"] = Any.FromString(eSource._expression.AsString().Replace("{?}", v));
                                docParamMap["attribute"] = Any.FromString(eSource._attribute.AsString());
                                docParamMaps.Add(docParamMap);
                            }
                        }
                    }

                    int index = 0;
                    int total = docParamMaps.Count;
                    foreach (var docParamMap in docParamMaps)
                    {
                        post(string.Format("{0}/hkg/collector/Document/Scrape", getConfig()["domain"].AsString()), docParamMap, (_reply) =>
                        {
                            index += 1;
                            if (index == total)
                                modelDocument.Broadcast("/hkg/collector/document/scrape/finish", null);
                            else
                                modelDocument.Broadcast("/hkg/collector/document/scrape/progress", ((float)index) / ((float)total));
                            var rspScrape = JsonSerializer.Deserialize<Collector.Proto.BlankResponse>(_reply, options);
                            if (0 != rspScrape._status._code.AsInt())
                            {
                                getLogger().Error(rspScrape._status._message.AsString());
                                return;
                            }
                        }, (_err) =>
                        {
                            index += 1;
                            if (index == total)
                                modelDocument.Broadcast("/hkg/collector/document/scrape/finish", null);
                            else
                                modelDocument.Broadcast("/hkg/collector/document/scrape/progress", ((float)index) / ((float)total));
                            getLogger().Error(_err.getMessage());
                        }, null);
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

        public void TidyFromMetatable()
        {
            Dictionary<string, Any> listParamMap = new Dictionary<string, Any>();
            listParamMap["offset"] = Any.FromInt64(0);
            listParamMap["count"] = Any.FromInt64(long.MaxValue);
            post(string.Format("{0}/hkg/collector/Document/List", getConfig()["domain"].AsString()), listParamMap, (_reply) =>
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new Collector.FieldConverter());
                var rspDocument = JsonSerializer.Deserialize<Collector.Proto.DocumentListResponse>(_reply, options);
                if (0 != rspDocument._status._code.AsInt())
                {
                    getLogger().Error(rspDocument._status._message.AsString());
                    return;
                }

                List<Dictionary<string, Any>> tidyParamMaps = new List<Dictionary<string, Any>>();
                foreach (var eDocument in rspDocument._entity)
                {
                    Dictionary<string, Any> tidyParamMap = new Dictionary<string, Any>();
                    tidyParamMap["uuid"] = Any.FromString(eDocument._uuid.AsString());
                    tidyParamMaps.Add(tidyParamMap);
                    // 获取来源表名
                    var uri = new Uri(eDocument._address.AsString());
                    string host = uri.Host;
                    string schema = uri.Scheme;
                    string sourceName = modelSource.HostToName(host);
                    var rules = modelSchema.GetAllRule(sourceName);
                    tidyParamMap["rule"] = Any.FromStringMap(rules);
                    tidyParamMap["host"] = Any.FromString(string.Format("{0}://{1}", schema, host));
                }

                int index = 0;
                int total = tidyParamMaps.Count;
                foreach (var tidyParamMap in tidyParamMaps)
                {
                    post(string.Format("{0}/hkg/collector/Document/Tidy", getConfig()["domain"].AsString()), tidyParamMap, (_reply) =>
                    {
                        index += 1;
                        if (index == total)
                            modelDocument.Broadcast("/hkg/collector/document/tidy/finish", null);
                        else
                            modelDocument.Broadcast("/hkg/collector/document/tidy/progress", ((float)index) / ((float)total));
                        var options = new JsonSerializerOptions();
                        options.Converters.Add(new Metatable.FieldConverter());
                        var rspScrape = JsonSerializer.Deserialize<Collector.Proto.BlankResponse>(_reply, options);
                        if (0 != rspScrape._status._code.AsInt())
                        {
                            getLogger().Error(rspScrape._status._message.AsString());
                            return;
                        }
                    }, (_err) =>
                    {
                        index += 1;
                        if (index == total)
                            modelDocument.Broadcast("/hkg/collector/document/tidy/finish", null);
                        else
                            modelDocument.Broadcast("/hkg/collector/document/tidy/progress", ((float)index) / ((float)total));
                        getLogger().Error(_err.getMessage());
                    }, null);
                }
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
