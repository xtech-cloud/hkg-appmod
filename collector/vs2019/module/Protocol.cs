
using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.collector.Proto
{

    public class DocumentScrapeRequest
    {
        public DocumentScrapeRequest()
        {
            _name = Any.FromString("");
            _keyword = Any.FromStringAry(new string[0]);
            _address = Any.FromString("");
            _attribute = Any.FromString("");

        }
        [JsonPropertyName("name")]
        public Any _name { get; set; }
        [JsonPropertyName("keyword")]
        public Any _keyword { get; set; }
        [JsonPropertyName("address")]
        public Any _address { get; set; }
        [JsonPropertyName("attribute")]
        public Any _attribute { get; set; }

    }

    public class DocumentTidyRequest
    {
        public DocumentTidyRequest()
        {
            _uuid = Any.FromString("");
            _host = Any.FromString("");
            _rule = Any.FromStringMap(new Dictionary<string, string>());

        }
        [JsonPropertyName("uuid")]
        public Any _uuid { get; set; }
        [JsonPropertyName("host")]
        public Any _host { get; set; }
        [JsonPropertyName("rule")]
        public Any _rule { get; set; }

    }

    public class DocumentListResponse
    {
        public DocumentListResponse()
        {
            _status = new Status();
            _total = Any.FromInt64(0);
            _entity = new DocumentEntity[0];

        }
        [JsonPropertyName("status")]
        public Status _status { get; set; }
        [JsonPropertyName("total")]
        public Any _total { get; set; }
        [JsonPropertyName("entity")]
        public DocumentEntity[] _entity { get; set; }

    }

    public class Status
    {
        public Status()
        {
            _code = Any.FromInt32(0);
            _message = Any.FromString("");

        }
        [JsonPropertyName("code")]
        public Any _code { get; set; }
        [JsonPropertyName("message")]
        public Any _message { get; set; }

    }

    public class BlankResponse
    {
        public BlankResponse()
        {
            _status = new Status();

        }
        [JsonPropertyName("status")]
        public Status _status { get; set; }

    }

    public class DocumentEntity
    {
        public DocumentEntity()
        {
            _uuid = Any.FromString("");
            _name = Any.FromString("");
            _keyword = Any.FromStringAry(new string[0]);
            _address = Any.FromString("");
            _rawText = Any.FromString("");
            _tidyText = Any.FromString("");
            _crawledAt = Any.FromInt64(0);

        }
        [JsonPropertyName("uuid")]
        public Any _uuid { get; set; }
        [JsonPropertyName("name")]
        public Any _name { get; set; }
        [JsonPropertyName("keyword")]
        public Any _keyword { get; set; }
        [JsonPropertyName("address")]
        public Any _address { get; set; }
        [JsonPropertyName("rawText")]
        public Any _rawText { get; set; }
        [JsonPropertyName("tidyText")]
        public Any _tidyText { get; set; }
        [JsonPropertyName("crawledAt")]
        public Any _crawledAt { get; set; }

    }

    public class ListRequest
    {
        public ListRequest()
        {
            _offset = Any.FromInt64(0);
            _count = Any.FromInt64(0);

        }
        [JsonPropertyName("offset")]
        public Any _offset { get; set; }
        [JsonPropertyName("count")]
        public Any _count { get; set; }

    }

    public class DocumentDeleteRequest
    {
        public DocumentDeleteRequest()
        {
            _uuid = Any.FromString("");
        }
        [JsonPropertyName("uuid")]
        public Any _uuid { get; set; }
    }

    public class DocumentBatchDeleteRequest
    {
        public DocumentBatchDeleteRequest()
        {
            _uuid = Any.FromStringAry(new string[0]);
        }
        [JsonPropertyName("uuid")]
        public Any _uuid { get; set; }
    }

    public class DocumentDeleteResponse
    {
        public DocumentDeleteResponse()
        {
            _uuid = Any.FromString("");
            _status = new Status();
        }
        [JsonPropertyName("uuid")]
        public Any _uuid { get; set; }
        [JsonPropertyName("status")]
        public Status _status { get; set; }
    }

    public class DocumentBatchDeleteResponse
    {
        public DocumentBatchDeleteResponse()
        {
            _uuid = Any.FromStringAry(new string[0]);
            _status = new Status();
        }
        [JsonPropertyName("uuid")]
        public Any _uuid { get; set; }
        [JsonPropertyName("status")]
        public Status _status { get; set; }
    }



}
