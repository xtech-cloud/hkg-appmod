
using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.builder.Proto
{

    public class DocumentMergeRequest
    {
        public DocumentMergeRequest()
        {
            _name = Any.FromString("");
            _label = Any.FromStringAry(new string[0]);
            _text = Any.FromStringAry(new string[0]);
            _format = Any.FromString("");

        }
        [JsonPropertyName("name")]
        public Any _name { get; set; }
        [JsonPropertyName("label")]
        public Any _label { get; set; }
        [JsonPropertyName("text")]
        public Any _text { get; set; }
        [JsonPropertyName("format")]
        public Any _format { get; set; }

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
            _label = Any.FromStringAry(new string[0]);
            _text = Any.FromString("");
            _updatedAt = Any.FromInt64(0);

        }
        [JsonPropertyName("uuid")]
        public Any _uuid { get; set; }
        [JsonPropertyName("name")]
        public Any _name { get; set; }
        [JsonPropertyName("label")]
        public Any _label { get; set; }
        [JsonPropertyName("text")]
        public Any _text { get; set; }
        [JsonPropertyName("updatedAt")]
        public Any _updatedAt { get; set; }

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

    public class DocumentBatchDeleteRequest
    {
        public DocumentBatchDeleteRequest()
        {
            _uuid = Any.FromStringAry(new string[0]);
        }
        [JsonPropertyName("uuid")]
        public Any _uuid { get; set; }

    }

    public class DocumentBatchDeleteResponse
    {
        public DocumentBatchDeleteResponse()
        {
            _status = new Status();
            _uuid = Any.FromStringAry(new string[0]);
        }
        [JsonPropertyName("status")]
        public Status _status { get; set; }
        [JsonPropertyName("uuid")]
        public Any _uuid { get; set; }

    }

}
