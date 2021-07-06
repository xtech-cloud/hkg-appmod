
using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.metatable.Proto
{

        public class PatternEntity
        {
            public PatternEntity()
            {
                _to = Any.FromString("");
                _from = Any.FromStringAry(new string[0]);

            }
            [JsonPropertyName("to")]
            public Any _to {get;set;}
            [JsonPropertyName("from")]
            public Any _from {get;set;}

        }
    
        public class FormatEntity
        {
            public FormatEntity()
            {
                _uuid = Any.FromString("");
                _name = Any.FromString("");
                _pattern = new PatternEntity[0];

            }
            [JsonPropertyName("uuid")]
            public Any _uuid {get;set;}
            [JsonPropertyName("name")]
            public Any _name {get;set;}
            [JsonPropertyName("pattern")]
            public PatternEntity[] _pattern {get;set;}

        }
    
        public class FormatListResponse
        {
            public FormatListResponse()
            {
                _status = new Status();
                _total = Any.FromInt64(0);
                _entity = new FormatEntity[0];

            }
            [JsonPropertyName("status")]
            public Status _status {get;set;}
            [JsonPropertyName("total")]
            public Any _total {get;set;}
            [JsonPropertyName("entity")]
            public FormatEntity[] _entity {get;set;}

        }
    
        public class RuleEntity
        {
            public RuleEntity()
            {
                _name = Any.FromString("");
                _field = Any.FromString("");
                _type = Any.FromString("");
                _element = Any.FromString("");
                _pair = new PairEntity();

            }
            [JsonPropertyName("name")]
            public Any _name {get;set;}
            [JsonPropertyName("field")]
            public Any _field {get;set;}
            [JsonPropertyName("type")]
            public Any _type {get;set;}
            [JsonPropertyName("element")]
            public Any _element {get;set;}
            [JsonPropertyName("pair")]
            public PairEntity _pair {get;set;}

        }
    
        public class SchemaEntity
        {
            public SchemaEntity()
            {
                _uuid = Any.FromString("");
                _name = Any.FromString("");
                _rule = new RuleEntity[0];

            }
            [JsonPropertyName("uuid")]
            public Any _uuid {get;set;}
            [JsonPropertyName("name")]
            public Any _name {get;set;}
            [JsonPropertyName("rule")]
            public RuleEntity[] _rule {get;set;}

        }
    
        public class SchemaListResponse
        {
            public SchemaListResponse()
            {
                _status = new Status();
                _total = Any.FromInt64(0);
                _entity = new SchemaEntity[0];

            }
            [JsonPropertyName("status")]
            public Status _status {get;set;}
            [JsonPropertyName("total")]
            public Any _total {get;set;}
            [JsonPropertyName("entity")]
            public SchemaEntity[] _entity {get;set;}

        }
    
        public class Status
        {
            public Status()
            {
                _code = Any.FromInt32(0);
                _message = Any.FromString("");

            }
            [JsonPropertyName("code")]
            public Any _code {get;set;}
            [JsonPropertyName("message")]
            public Any _message {get;set;}

        }
    
        public class PairEntity
        {
            public PairEntity()
            {
                _key = Any.FromString("");
                _value = Any.FromString("");

            }
            [JsonPropertyName("key")]
            public Any _key {get;set;}
            [JsonPropertyName("value")]
            public Any _value {get;set;}

        }
    
        public class BlankResponse
        {
            public BlankResponse()
            {
                _status = new Status();

            }
            [JsonPropertyName("status")]
            public Status _status {get;set;}

        }
    
        public class ImportYamlRequest
        {
            public ImportYamlRequest()
            {
                _content = Any.FromString("");

            }
            [JsonPropertyName("content")]
            public Any _content {get;set;}

        }
    
        public class ListRequest
        {
            public ListRequest()
            {
                _offset = Any.FromInt64(0);
                _count = Any.FromInt64(0);

            }
            [JsonPropertyName("offset")]
            public Any _offset {get;set;}
            [JsonPropertyName("count")]
            public Any _count {get;set;}

        }
    
        public class FindRequest
        {
            public FindRequest()
            {
                _name = Any.FromString("");

            }
            [JsonPropertyName("name")]
            public Any _name {get;set;}

        }
    
        public class DeleteRequest
        {
            public DeleteRequest()
            {
                _uuid = Any.FromString("");

            }
            [JsonPropertyName("uuid")]
            public Any _uuid {get;set;}

        }
    
        public class SourceEntity
        {
            public SourceEntity()
            {
                _uuid = Any.FromString("");
                _name = Any.FromString("");
                _address = Any.FromString("");
                _expression = Any.FromString("");
                _attribute = Any.FromString("");

            }
            [JsonPropertyName("uuid")]
            public Any _uuid {get;set;}
            [JsonPropertyName("name")]
            public Any _name {get;set;}
            [JsonPropertyName("address")]
            public Any _address {get;set;}
            [JsonPropertyName("expression")]
            public Any _expression {get;set;}
            [JsonPropertyName("attribute")]
            public Any _attribute {get;set;}

        }
    
        public class SourceListResponse
        {
            public SourceListResponse()
            {
                _status = new Status();
                _total = Any.FromInt64(0);
                _entity = new SourceEntity[0];

            }
            [JsonPropertyName("status")]
            public Status _status {get;set;}
            [JsonPropertyName("total")]
            public Any _total {get;set;}
            [JsonPropertyName("entity")]
            public SourceEntity[] _entity {get;set;}

        }
    
        public class VocabularyEntity
        {
            public VocabularyEntity()
            {
                _uuid = Any.FromString("");
                _name = Any.FromString("");
                _label = Any.FromStringAry(new string[0]);
                _value = Any.FromStringAry(new string[0]);

            }
            [JsonPropertyName("uuid")]
            public Any _uuid {get;set;}
            [JsonPropertyName("name")]
            public Any _name {get;set;}
            [JsonPropertyName("label")]
            public Any _label {get;set;}
            [JsonPropertyName("value")]
            public Any _value {get;set;}

        }
    
        public class VocabularyListResponse
        {
            public VocabularyListResponse()
            {
                _status = new Status();
                _total = Any.FromInt64(0);
                _entity = new VocabularyEntity[0];

            }
            [JsonPropertyName("status")]
            public Status _status {get;set;}
            [JsonPropertyName("total")]
            public Any _total {get;set;}
            [JsonPropertyName("entity")]
            public VocabularyEntity[] _entity {get;set;}

        }
    
        public class VocabularyFindResponse
        {
            public VocabularyFindResponse()
            {
                _status = new Status();
                _entity = new VocabularyEntity[0];

            }
            [JsonPropertyName("status")]
            public Status _status {get;set;}
            [JsonPropertyName("entity")]
            public VocabularyEntity[] _entity {get;set;}

        }
    
}
