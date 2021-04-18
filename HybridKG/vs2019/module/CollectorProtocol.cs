
using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Collections.Generic;
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
            BoolValue = 6,
            StringAryValue = 11,
            IntAryValue = 12,
            LongAryValue = 13,
            FloatAryValue = 14,
            DoubleAryValue = 15,
            BoolAryValue = 16,
            StringMapValue = 21,
            IntMapValue = 22,
            LongMapValue = 23,
            FloatMapValue = 24,
            DoubleMapValue = 25,
            BoolMapValue = 26
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
            any.value_ = _value.ToString();
            return any;
        }

        public static Field FromBool(bool _value)
        {
            Field any = new Field();
            any.tag_ = Tag.BoolValue;
            any.value_ = _value.ToString();
            return any;
        }

        public static Field FromInt(int _value)
        {
            Field any = new Field();
            any.tag_ = Tag.IntValue;
            any.value_ = _value.ToString();
            return any;
        }

        public static Field FromLong(long _value)
        {
            Field any = new Field();
            any.tag_ = Tag.LongValue;
            any.value_ = _value.ToString();
            return any;
        }
        public static Field FromStringAry(string[] _value)
        {
            Field any = new Field();
            any.tag_ = Tag.StringAryValue;
            string ary = "";
            foreach(string v in _value)
            {
                ary += string.Format("\"{0}\",", v);
            }
            if(!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("[{0}]", ary);
            return any;
        }

        public static Field FromFloatAry(float[] _value)
        {
            Field any = new Field();
            any.tag_ = Tag.FloatAryValue;
            string ary = "";
            foreach (float v in _value)
            {
                ary += string.Format("{0},", v);
            }
            if (!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("[{0}]", ary);
            return any;
        }

        public static Field FromDoubleAry(double[] _value)
        {
            Field any = new Field();
            any.tag_ = Tag.DoubleAryValue;
            string ary = "";
            foreach (double v in _value)
            {
                ary += string.Format("{0},", v);
            }
            if (!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("[{0}]", ary);
            return any;
        }

        public static Field FromBoolAry(bool[] _value)
        {
            Field any = new Field();
            any.tag_ = Tag.BoolAryValue;
            string ary = "";
            foreach (bool v in _value)
            {
                ary += string.Format("{0},", v);
            }
            if (!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("[{0}]", ary);
            return any;
        }

        public static Field FromIntAry(int[] _value)
        {
            Field any = new Field();
            any.tag_ = Tag.IntAryValue;
            string ary = "";
            foreach (int v in _value)
            {
                ary += string.Format("{0},", v);
            }
            if (!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("[{0}]", ary);
            return any;
        }

        public static Field FromLongAry(long[] _value)
        {
            Field any = new Field();
            any.tag_ = Tag.LongAryValue;
            string ary = "";
            foreach (long v in _value)
            {
                ary += string.Format("{0},", v);
            }
            if (!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("[{0}]", ary);
            return any;
        }

        public static Field FromStringMap(Dictionary<string, string> _value)
        {
            Field any = new Field();
            any.tag_ = Tag.StringAryValue;
            string ary = "";
            foreach (var pair in _value)
            {
                ary += string.Format("\"{0}\": \"{1}\",", pair.Key, pair.Value);
            }
            if (!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("{{0}}", ary);
            return any;
        }

        public static Field FromFloatMap(Dictionary<string, float> _value)
        {
            Field any = new Field();
            any.tag_ = Tag.FloatAryValue;
            string ary = "";
            foreach (var pair in _value)
            {
                ary += string.Format("\"{0}\": {1},", pair.Key, pair.Value);
            }
            if (!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("{{0}}", ary);
            return any;
        }

        public static Field FromDoubleMap(Dictionary<string, double> _value)
        {
            Field any = new Field();
            any.tag_ = Tag.DoubleAryValue;
            string ary = "";
            foreach (var pair in _value)
            {
                ary += string.Format("\"{0}\": {1},", pair.Key, pair.Value);
            }
            if (!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("{{0}}", ary);
            return any;
        }

        public static Field FromBoolMap(Dictionary<string, int> _value)
        {
            Field any = new Field();
            any.tag_ = Tag.BoolAryValue;
            string ary = "";
            foreach (var pair in _value)
            {
                ary += string.Format("\"{0}\": {1},", pair.Key, pair.Value);
            }
            if (!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("{{0}}", ary);
            return any;
        }

        public static Field FromIntMap(Dictionary<string, int> _value)
        {
            Field any = new Field();
            any.tag_ = Tag.IntAryValue;
            string ary = "";
            foreach (var pair in _value)
            {
                ary += string.Format("\"{0}\": {1},", pair.Key, pair.Value);
            }
            if (!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("{{0}}", ary);
            return any;
        }

        public static Field FromLongMap(Dictionary<string, long> _value)
        {
            Field any = new Field();
            any.tag_ = Tag.LongMapValue;
            string ary = "";
            foreach (var pair in _value)
            {
                ary += string.Format("\"{0}\": {1},", pair.Key, pair.Value);
            }
            if (!string.IsNullOrEmpty(ary))
            {
                ary = ary.Remove(ary.Length - 1, 1);
            }
            any.value_ = string.Format("{{0}}", ary);
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

        public bool IsStringAry()
        {
            return tag_ == Tag.StringAryValue;
        }

        public bool IsIntAry()
        {
            return tag_ == Tag.IntAryValue;
        }

        public bool IsLongAry()
        {
            return tag_ == Tag.LongAryValue;
        }

        public bool IsFloatAry()
        {
            return tag_ == Tag.FloatAryValue;
        }

        public bool IsDoubleAry()
        {
            return tag_ == Tag.DoubleAryValue;
        }

        public bool IsBoolAry()
        {
            return tag_ == Tag.BoolAryValue;
        }

        public bool IsStringMap()
        {
            return tag_ == Tag.StringMapValue;
        }

        public bool IsIntMap()
        {
            return tag_ == Tag.IntMapValue;
        }

        public bool IsLongMap()
        {
            return tag_ == Tag.LongMapValue;
        }

        public bool IsFloatMap()
        {
            return tag_ == Tag.FloatMapValue;
        }

        public bool IsDoubleMap()
        {
            return tag_ == Tag.DoubleMapValue;
        }

        public bool IsBoolMap()
        {
            return tag_ == Tag.BoolMapValue;
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

        public string[] AsStringAry()
        {
            List<string> v = new List<string>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    string value = e.Trim();
                    value = value.Remove(0, 1);
                    value = value.Remove(value.Length - 1, 1);
                    v.Add(value);
                }
            }
            return v.ToArray();
        }

        public int[] AsIntAry()
        {
            List<int> v = new List<int>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    int value;
                    if(int.TryParse(e.Trim(), out value))
                        v.Add(value);
                    else
                        v.Add(0);
                }
            }
            return v.ToArray();
        }

        public long[] AsLongAry()
        {
            List<long> v = new List<long>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    long value;
                    if (long.TryParse(e.Trim(), out value))
                        v.Add(value);
                    else
                        v.Add(0);
                }
            }
            return v.ToArray();
        }

        public float[] AsFloatAry()
        {
            List<float> v = new List<float>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    float value;
                    if (float.TryParse(e.Trim(), out value))
                        v.Add(value);
                    else
                        v.Add(0);
                }
            }
            return v.ToArray();
        }

        public double[] AsDoubleAry()
        {
            List<double> v = new List<double>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    double value;
                    if (double.TryParse(e.Trim(), out value))
                        v.Add(value);
                    else
                        v.Add(0);
                }
            }
            return v.ToArray();
        }

        public bool[] AsBoolAry()
        {
            List<bool> v = new List<bool>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    bool value;
                    if (bool.TryParse(e.Trim(), out value))
                        v.Add(value);
                    else
                        v.Add(false);
                }
            }
            return v.ToArray();
        }

        public Dictionary<string, string> AsStringMap()
        {
            Dictionary<string, string> v = new Dictionary<string, string>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    string[] pair = e.Trim().Split(":");
                    string key = pair[0].Trim();
                    key = key.Remove(0, 1);
                    key = key.Remove(key.Length - 1, 1);
                    string value = pair[0].Trim();
                    value = value.Remove(0, 1);
                    value = value.Remove(value.Length - 1, 1);
                    v[key] = value;
                }
            }
            return v;
        }

        public Dictionary<string, int> AsIntMap()
        {
            Dictionary<string, int> v = new Dictionary<string, int>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    string[] pair = e.Trim().Split(":");
                    string key = pair[0].Trim();
                    key = key.Remove(0, 1);
                    key = key.Remove(key.Length - 1, 1);
                    int value = 0;
                    int.Parse(pair[1].Trim());
                    v[key] = value;
                }
            }
            return v;
        }

        public Dictionary<string, long> AsLongMap()
        {
            Dictionary<string, long> v = new Dictionary<string, long>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    string[] pair = e.Trim().Split(":");
                    string key = pair[0].Trim();
                    key = key.Remove(0, 1);
                    key = key.Remove(key.Length - 1, 1);
                    long value = 0;
                    long.Parse(pair[1].Trim());
                    v[key] = value;
                }
            }
            return v;
        }

        public Dictionary<string, float> AsFloatMap()
        {
            Dictionary<string, float> v = new Dictionary<string, float>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    string[] pair = e.Trim().Split(":");
                    string key = pair[0].Trim();
                    key = key.Remove(0, 1);
                    key = key.Remove(key.Length - 1, 1);
                    float value = 0;
                    float.Parse(pair[1].Trim());
                    v[key] = value;
                }
            }
            return v;
        }

        public Dictionary<string, double> AsDoubleMap()
        {
            Dictionary<string, double> v = new Dictionary<string, double>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    string[] pair = e.Trim().Split(":");
                    string key = pair[0].Trim();
                    key = key.Remove(0, 1);
                    key = key.Remove(key.Length - 1, 1);
                    double value = 0;
                    double.Parse(pair[1].Trim());
                    v[key] = value;
                }
            }
            return v;
        }

        public Dictionary<string, bool> AsBoolMap()
        {
            Dictionary<string, bool> v = new Dictionary<string, bool>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(","))
                {
                    string[] pair = e.Trim().Split(":");
                    string key = pair[0].Trim();
                    key = key.Remove(0, 1);
                    key = key.Remove(key.Length - 1, 1);
                    bool value = false;
                    bool.Parse(pair[1].Trim());
                    v[key] = value;
                }
            }
            return v;
        }

        public Any AsAny()
        {
            if(IsString())
                return Any.FromString(AsString());
            if(IsInt())
                return Any.FromInt32(AsInt());
            if(IsLong())
                return Any.FromInt64(AsLong());
            if(IsFloat())
                return Any.FromFloat32(AsFloat());
            if(IsDouble())
                return Any.FromFloat64(AsDouble());
            if(IsBool())
                return Any.FromBool(AsBool());
            if (IsStringAry())
                return Any.FromStringAry(AsStringAry());
            if (IsIntAry())
                return Any.FromInt32Ary(AsIntAry());
            if (IsLongAry())
                return Any.FromInt64Ary(AsLongAry());
            if (IsFloatAry())
                return Any.FromFloat32Ary(AsFloatAry());
            if (IsDoubleAry())
                return Any.FromFloat64Ary(AsDoubleAry());
            if (IsBoolAry())
                return Any.FromBoolAry(AsBoolAry());
            if (IsStringAry())
                return Any.FromStringAry(AsStringAry());
            if (IsIntMap())
                return Any.FromInt32Map(AsIntMap());
            if (IsLongMap())
                return Any.FromInt64Map(AsLongMap());
            if (IsFloatMap())
                return Any.FromFloat32Map(AsFloatMap());
            if (IsDoubleMap())
                return Any.FromFloat64Map(AsDoubleMap());
            if (IsBoolMap())
                return Any.FromBoolMap(AsBoolMap());
            return new Any();
        }
    }//class

        public class DocumentScrapeRequest
        {
            public DocumentScrapeRequest()
            {
                _name = new Field();
                _keyword = new Field();
                _address = new Field();
                _attribute = new Field();

            }
            [JsonPropertyName("name")]
            public Field _name {get;set;}
            [JsonPropertyName("keyword")]
            public Field _keyword {get;set;}
            [JsonPropertyName("address")]
            public Field _address {get;set;}
            [JsonPropertyName("attribute")]
            public Field _attribute {get;set;}

        }
    
        public class DocumentTidyRequest
        {
            public DocumentTidyRequest()
            {
                _uuid = new Field();
                _host = new Field();
                _rule = new System.Collections.Generic.Dictionary<string, string>();

            }
            [JsonPropertyName("uuid")]
            public Field _uuid {get;set;}
            [JsonPropertyName("host")]
            public Field _host {get;set;}
            [JsonPropertyName("rule")]
            public System.Collections.Generic.Dictionary<string, string> _rule {get;set;}

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
                _uuid = new Field();
                _name = new Field();
                _keyword = new Field();
                _address = new Field();
                _rawText = new Field();
                _tidyText = new Field();
                _crawledAt = new Field();

            }
            [JsonPropertyName("uuid")]
            public Field _uuid {get;set;}
            [JsonPropertyName("name")]
            public Field _name {get;set;}
            [JsonPropertyName("keyword")]
            public Field _keyword {get;set;}
            [JsonPropertyName("address")]
            public Field _address {get;set;}
            [JsonPropertyName("rawText")]
            public Field _rawText {get;set;}
            [JsonPropertyName("tidyText")]
            public Field _tidyText {get;set;}
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
