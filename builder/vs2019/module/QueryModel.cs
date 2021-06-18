using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using XTC.oelMVCS;

namespace hkg.builder
{
    public class ReplyStatus
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class CollectorDocumentListReply
    {
        public class Document
        {
            public string uuid { get; set; }
            public string name { get; set; }
            public string[] keyword { get; set; }
            public string address { get; set; }
            public string rawText { get; set; }
            public string tidyText { get; set; }
            public string crawledAt { get; set; }

            //编码，相同的name和keyword视为一个编码
            public string _code { get; set; }
        }

        public ReplyStatus status { get; set; }
        public Document[] entity { get; set; }
    }

    public class MetatableFormatListReply
    {
        public class Pattern
        {
            public string to { get; set; }
            public string[] from { get; set; }
        }
        public class Format
        {
            public string uuid { get; set; }
            public string name { get; set; }
            public Pattern[] pattern { get; set; }
        }
        public ReplyStatus status { get; set; }
        public Format[] entity { get; set; }
    }

    public class QueryModel : Model
    {
        public const string NAME = "hkg.builder.QueryModel";

        public class QueryStatus : Model.Status
        {
            public const string NAME = "hkg.builder.QueryStatus";
            public List<CollectorDocumentListReply.Document> collectorDocument = new List<CollectorDocumentListReply.Document>();
            public List<MetatableFormatListReply.Format> metatableFormat = new List<MetatableFormatListReply.Format>();
        }


        protected override void preSetup()
        {
            Error err;
            status_ = spawnStatus<QueryStatus>(QueryStatus.NAME, out err);
            if (0 != err.getCode())
            {
                getLogger().Error(err.getMessage());
            }
        }

        protected override void setup()
        {
            getLogger().Trace("setup hkg.builder.QueryModel");
        }

        protected override void preDismantle()
        {
            Error err;
            killStatus(QueryStatus.NAME, out err);
            if (0 != err.getCode())
            {
                getLogger().Error(err.getMessage());
            }
        }
        private QueryStatus status
        {
            get
            {
                return status_ as QueryStatus;
            }
        }

        public void SaveCollectorDocumentList(CollectorDocumentListReply.Document[] _list)
        {
            status.collectorDocument.Clear();
            status.collectorDocument.AddRange(_list);
        }

        public void SaveMetatableFormatList(MetatableFormatListReply.Format[] _list)
        {
            status.metatableFormat.Clear();
            status.metatableFormat.AddRange(_list);
        }

        public string BuildMergeParamFormat(string _formatName)
        {
            var found = status.metatableFormat.Find((_item) =>
            {
                return _item.name.Equals(_formatName);
            });
            if (null == found)
                return "";

            var options = new JsonSerializerOptions()
            {
                //WriteIndented = true
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            return JsonSerializer.Serialize(found.pattern, options);
        }

        public void BuildMergeParam(string _formatName, ref string _paramFormat, string _documentCode, ref string[] _paramLabel, ref string[] _paramText)
        {
            var options = new JsonSerializerOptions()
            {
                //WriteIndented = true
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            if (!string.IsNullOrEmpty(_formatName))
            {
                var found = status.metatableFormat.Find((_item) =>
                {
                    return _item.name.Equals(_formatName);
                });
                if (null != found)
                {
                    _paramFormat = JsonSerializer.Serialize(found.pattern, options);
                }
            }

            if (!string.IsNullOrEmpty(_documentCode))
            {
                var foundList = status.collectorDocument.FindAll((_item) =>
                {
                    return _item._code.Equals(_documentCode);
                });
                List<string> paramText = new List<string>();
                foreach(var found in foundList)
                {
                    _paramLabel = found.keyword;
                    paramText.Add(found.tidyText);
                }
                _paramText = paramText.ToArray();
            }
        }
    }
}
