
using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable
{
    public class VocabularyService : Service
    {
        public const string NAME = "hkg.metatable.VocabularyService";
        public string domainPublic = "";
        public string domainPrivate = "";
        public string accessToken = "";
        public string uuid = "";
        private VocabularyModel model = null;
        private string domain { get; set; }

        protected override void preSetup()
        {
            model = findModel(VocabularyModel.NAME) as VocabularyModel;
        }

        protected override void setup()
        {
            getLogger().Trace("setup VocabularyService");
        }

        public void CreatePublicAlias()
        {
            createAlias("/List@public", string.Format("{0}/hkg/metatable/Vocabulary/List", domainPublic), "POST", false, new Dictionary<string, Any>
            {
                {"offset", Any.FromInt64(0) },
                {"count", Any.FromInt64(int.MaxValue) },
            });
        }

        public void CreatePrivateAlias()
        {
            createAlias("/List@private", string.Format("{0}/hkg/metatable/Vocabulary/List", domainPrivate), "POST", false, new Dictionary<string, Any>
            {
                {"offset", Any.FromInt64(0) },
                {"count", Any.FromInt64(int.MaxValue) },
            });
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

        public void PostImportYaml(Proto.ImportYamlRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["content"] = _request._content;

            post(string.Format("{0}/hkg/metatable/Vocabulary/ImportYaml", domain), paramMap, (_reply) =>
            {
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, JsonOptions.DefaultSerializerOptions);
                //TODO
                Proto.ListRequest req = new Proto.ListRequest();
                req._offset = Any.FromInt64(0);
                req._count = Any.FromInt64(int.MaxValue);
                PostList(req);
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

            post(string.Format("{0}/hkg/metatable/Vocabulary/List", domain), paramMap, (_reply) =>
            {
                var rsp = JsonSerializer.Deserialize<Proto.VocabularyListResponse>(_reply, JsonOptions.DefaultSerializerOptions);
                Model.Status status = Model.Status.New<Model.Status>(rsp._status._code.AsInt32(), rsp._status._message.AsString());
                model.UpdateList(status, rsp._entity);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
                Model.Status status = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateList(status, null);
            }, null);
        }


        public void PostFind(Proto.FindRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["name"] = _request._name;

            post(string.Format("{0}/hkg/metatable/Vocabulary/Find", domain), paramMap, (_reply) =>
            {
                var rsp = JsonSerializer.Deserialize<Proto.VocabularyFindResponse>(_reply, JsonOptions.DefaultSerializerOptions);
                VocabularyModel.VocabularyStatus status = Model.Status.New<VocabularyModel.VocabularyStatus>(rsp._status._code.AsInt32(), rsp._status._message.AsString());
                model.Broadcast("/hkg/metatable/Vocabulary/Find", rsp);
            }, (_err) =>
            {
                getLogger().Error(_err.getMessage());
            }, null);
        }


        public void PostDelete(Proto.DeleteRequest _request)
        {
            Dictionary<string, Any> paramMap = new Dictionary<string, Any>();
            paramMap["uuid"] = _request._uuid;

            post(string.Format("{0}/hkg/metatable/Vocabulary/Delete", domain), paramMap, (_reply) =>
            {
                var rsp = JsonSerializer.Deserialize<Proto.BlankResponse>(_reply, JsonOptions.DefaultSerializerOptions);
                //TODO
                Proto.ListRequest req = new Proto.ListRequest();
                req._offset = Any.FromInt64(0);
                req._count = Any.FromInt64(int.MaxValue);
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
                byte[] data = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(_params, JsonOptions.DefaultSerializerOptions);
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
