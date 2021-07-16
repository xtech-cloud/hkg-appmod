
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using XTC.oelMVCS;

namespace hkg.builder
{
    namespace External
    {
        public class ReplyStatus
        {
            public int code { get; set; }
            public string message { get; set; }
        }

        namespace Metatable
        {

            public class PatternEntity
            {
                public PatternEntity()
                {
                    to = Any.FromString("");
                    from = Any.FromStringAry(new string[0]);

                }
                public Any to { get; set; }
                public Any from { get; set; }
            }

            public class FormatEntity
            {
                public FormatEntity()
                {
                    uuid = Any.FromString("");
                    name = Any.FromString("");
                    pattern = new PatternEntity[0];

                }
                public Any uuid { get; set; }
                public Any name { get; set; }
                public PatternEntity[] pattern { get; set; }

            }

            public class FormatListResponse
            {
                public FormatListResponse()
                {
                    status = new ReplyStatus();
                    total = Any.FromInt64(0);
                    entity = new FormatEntity[0];

                }
                public ReplyStatus status { get; set; }
                public Any total { get; set; }
                public FormatEntity[] entity { get; set; }

            }
        }

        namespace Collector
        {
            public class DocumentEntity
            {
                public DocumentEntity()
                {
                    uuid = Any.FromString("");
                    name = Any.FromString("");
                    keyword = Any.FromStringAry(new string[0]);
                    address = Any.FromString("");
                    rawText = Any.FromString("");
                    tidyText = Any.FromString("");
                    crawledAt = Any.FromInt64(0);
                    _code = "";
                }
                public Any uuid { get; set; }
                public Any name { get; set; }
                public Any keyword { get; set; }
                public Any address { get; set; }
                public Any rawText { get; set; }
                public Any tidyText { get; set; }
                public Any crawledAt { get; set; }
                public string _code = "";

            }

            public class DocumentListResponse
            {
                public DocumentListResponse()
                {
                    status = new ReplyStatus();
                    total = Any.FromInt64(0);
                    entity = new DocumentEntity[0];

                }
                public ReplyStatus status { get; set; }
                public Any total { get; set; }
                public DocumentEntity[] entity { get; set; }

            }
        }//namespace
    }//namespace
    public class ExternalModel : Model
    {
        public const string NAME = "hkg.builder.ExternalModel";


        public class ExternalStatus : Model.Status
        {
            public const string NAME = "hkg.collector.ExternalStatus";
            public List<External.Collector.DocumentEntity> collectorDocumentList = new List<External.Collector.DocumentEntity>();
            public List<External.Metatable.FormatEntity> metatableFormatList = new List<External.Metatable.FormatEntity>();
        }

        protected override void preSetup()
        {
            Error err;
            status_ = spawnStatus<ExternalStatus>(ExternalStatus.NAME, out err);
            if (0 != err.getCode())
            {
                getLogger().Error(err.getMessage());
            }
        }

        protected override void setup()
        {
            getLogger().Trace("setup hgk.builder.DocumentModel");
        }

        protected override void preDismantle()
        {
            Error err;
            killStatus(ExternalStatus.NAME, out err);
            if (0 != err.getCode())
            {
                getLogger().Error(err.getMessage());
            }
        }

        private ExternalStatus status
        {
            get
            {
                return status_ as ExternalStatus;
            }
        }

        public void SaveCollectorDocumentList(External.Collector.DocumentEntity[] _entity)
        {
            status.collectorDocumentList.Clear();
            status.collectorDocumentList.AddRange(_entity);
        }

        public void SaveMetatableFormatList(External.Metatable.FormatEntity[] _entity)
        {
            status.metatableFormatList.Clear();
            status.metatableFormatList.AddRange(_entity);
        }

        /*
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
        */

        public void BuildMergeParam(string _formatName, ref string _paramFormat, string _documentCode, ref string[] _paramLabel, ref string[] _paramText)
        {
            if (!string.IsNullOrEmpty(_formatName))
            {
                var found = status.metatableFormatList.Find((_item) =>
                {
                    return _item.name.AsString().Equals(_formatName);
                });
                if (null != found)
                {
                    _paramFormat = JsonSerializer.Serialize(found.pattern, JsonOptions.DefaultSerializerOptions);
                }
            }

            if (!string.IsNullOrEmpty(_documentCode))
            {
                var foundList = status.collectorDocumentList.FindAll((_item) =>
                {
                    return _item._code.Equals(_documentCode);
                });
                List<string> paramText = new List<string>();
                foreach (var found in foundList)
                {
                    _paramLabel = found.keyword.AsStringAry();
                    paramText.Add(found.tidyText.AsString());
                }
                _paramText = paramText.ToArray();
            }
        }

    }
}
