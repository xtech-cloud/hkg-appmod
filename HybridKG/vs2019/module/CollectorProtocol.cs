
using System;
using System.Text;
using System.Text.Json.Serialization;
using XTC.oelMVCS;

namespace HKG.Module.Collector.Proto
{
    public class Field
    {
        public enum Tag
        {
            NULL = 0,
            StringValue = 1,
            IntValue = 2,
            LongValue = 3,
            FloatValue = 4,
            DoubleValue = 5,
            BoolValue = 6
        }

        private string value_ = "";
        private Tag tag_ = Tag.NULL;

        public Field()
        {
        }

        public static Field FromString(string _value)
        {
            Field any = new Field();
            any.tag_ = Tag.StringValue;
            any.value_ = _value;
            return any;
        }

        public static Field FromFloat(float _value)
        {
            Field any = new Field();
            any.tag_ = Tag.FloatValue;
            any.value_ = _value.ToString();
            return any;
        }

        public static Field FromDouble(double _value)
        {
            Field any = new Field();
            any.tag_ = Tag.DoubleValue;
            any.value_ = _value.ToString(); ;
            return any;
        }

        public static Field FromBool(bool _value)
        {
            Field any = new Field();
            any.tag_ = Tag.BoolValue;
            any.value_ = _value.ToString(); ;
            return any;
        }

        public static Field FromInt(int _value)
        {
            Field any = new Field();
            any.tag_ = Tag.IntValue;
            any.value_ = _value.ToString(); ;
            return any;
        }

        public static Field FromLong(long _value)
        {
            Field any = new Field();
            any.tag_ = Tag.LongValue;
            any.value_ = _value.ToString(); ;
            return any;
        }

        public bool IsNull()
        {
            return tag_ == Tag.NULL;
        }

        public bool IsString()
        {
            return tag_ == Tag.StringValue;
        }

        public bool IsInt()
        {
            return tag_ == Tag.IntValue;
        }

        public bool IsLong()
        {
            return tag_ == Tag.LongValue;
        }

        public bool IsFloat()
        {
            return tag_ == Tag.FloatValue;
        }

        public bool IsDouble()
        {
            return tag_ == Tag.DoubleValue;
        }

        public bool IsBool()
        {
            return tag_ == Tag.BoolValue;
        }

        public string AsString()
        {
            return value_;
        }


        public int AsInt()
        {
            int value = 0;
            int.TryParse(value_, out value);
            return value;
        }

        public long AsLong()
        {
            long value = 0;
            long.TryParse(value_, out value);
            return value;
        }

        public float AsFloat()
        {
            float value = 0;
            float.TryParse(value_, out value);
            return value;
        }

        public double AsDouble()
        {
            double value = 0;
            double.TryParse(value_, out value);
            return value;
        }

        public bool AsBool()
        {
            bool value = false;
            bool.TryParse(value_, out value);
            return value;
        }

        public Any AsAny()
        {
            if(IsString())
                return Any.FromString(AsString());
            if(IsInt())
                return Any.FromInt(AsInt());
            if(IsLong())
                return Any.FromLong(AsLong());
            if(IsFloat())
                return Any.FromFloat(AsFloat());
            if(IsDouble())
                return Any.FromDouble(AsDouble());
            if(IsBool())
                return Any.FromBool(AsBool());
            return new Any();
        }

    }//class

        public class DocumentScrapeRequest
        {
            public DocumentScrapeRequest()
            {
                _name = new Field();
                _keyword = new string[0];
                _address = new Field();
                _attribute = new Field();

            }
            [JsonPropertyName("name")]
            public Field _name {get;set;}
            [JsonPropertyName("keyword")]
            public string[] _keyword {get;set;}
            [JsonPropertyName("address")]
            public Field _address {get;set;}
            [JsonPropertyName("attribute")]
            public Field _attribute {get;set;}

        }
    
        public class DocumentListResponse
        {
            public DocumentListResponse()
            {
                _status = new Status();
                _total = new Field();
                _entity = new DocumentEntity[0];

            }
            [JsonPropertyName("status")]
            public Status _status {get;set;}
            [JsonPropertyName("total")]
            public Field _total {get;set;}
            [JsonPropertyName("entity")]
            public DocumentEntity[] _entity {get;set;}

        }
    
        public class Status
        {
            public Status()
            {
                _code = new Field();
                _message = new Field();

            }
            [JsonPropertyName("code")]
            public Field _code {get;set;}
            [JsonPropertyName("message")]
            public Field _message {get;set;}

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
    
        public class DocumentEntity
        {
            public DocumentEntity()
            {
                _name = new Field();
                _keyword = new string[0];
                _address = new Field();
                _rawText = new Field();
                _crawledAt = new Field();

            }
            [JsonPropertyName("name")]
            public Field _name {get;set;}
            [JsonPropertyName("keyword")]
            public string[] _keyword {get;set;}
            [JsonPropertyName("address")]
            public Field _address {get;set;}
            [JsonPropertyName("rawText")]
            public Field _rawText {get;set;}
            [JsonPropertyName("crawledAt")]
            public Field _crawledAt {get;set;}

        }
    
        public class ListRequest
        {
            public ListRequest()
            {
                _offset = new Field();
                _count = new Field();

            }
            [JsonPropertyName("offset")]
            public Field _offset {get;set;}
            [JsonPropertyName("count")]
            public Field _count {get;set;}

        }
    
}
