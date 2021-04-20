


using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Builder
{
    /// <summary>
    /// 用于将请求数据序列化为json
    /// </summary>
    class AnyConverter : JsonConverter<Any>
    {
        public override Any Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Any _value, JsonSerializerOptions options)
        {
            if(_value.IsString())
                writer.WriteStringValue(_value.AsString());
            else if (_value.IsInt())
                writer.WriteNumberValue(_value.AsInt());
            else if (_value.IsLong())
                writer.WriteNumberValue(_value.AsLong());
            else if (_value.IsFloat())
                writer.WriteNumberValue(_value.AsFloat());
            else if (_value.IsDouble())
                writer.WriteNumberValue(_value.AsDouble());
            else if (_value.IsBool())
                writer.WriteBooleanValue(_value.AsBool());
            else if(_value.IsStringAry())
            {
                writer.WriteStartArray();
                foreach(string v in _value.AsStringAry())
                {
                    writer.WriteStringValue(v);
                }
                writer.WriteEndArray();
            }
            else if (_value.IsInt32Ary())
            {
                writer.WriteStartArray();
                foreach (int v in _value.AsInt32Ary())
                {
                    writer.WriteNumberValue(v);
                }
                writer.WriteEndArray();
            }
            else if (_value.IsInt64Ary())
            {
                writer.WriteStartArray();
                foreach (long v in _value.AsInt64Ary())
                {
                    writer.WriteNumberValue(v);
                }
                writer.WriteEndArray();
            }
            else if (_value.IsFloat32Ary())
            {
                writer.WriteStartArray();
                foreach (float v in _value.AsFloat32Ary())
                {
                    writer.WriteNumberValue(v);
                }
                writer.WriteEndArray();
            }
            else if (_value.IsFloat64Ary())
            {
                writer.WriteStartArray();
                foreach (double v in _value.AsFloat64Ary())
                {
                    writer.WriteNumberValue(v);
                }
                writer.WriteEndArray();
            }
            else if (_value.IsBoolAry())
            {
                writer.WriteStartArray();
                foreach (bool v in _value.AsBoolAry())
                {
                    writer.WriteBooleanValue(v);
                }
                writer.WriteEndArray();
            }
            else if (_value.IsStringMap())
            {
                writer.WriteStartObject();
                foreach (var pair in _value.AsStringMap())
                {
                    writer.WriteString(pair.Key, pair.Value);
                }
                writer.WriteEndObject();
            }
            else if (_value.IsInt32Map())
            {
                writer.WriteStartObject();
                foreach (var pair in _value.AsInt32Map())
                {
                    writer.WriteNumber(pair.Key, pair.Value);
                }
                writer.WriteEndObject();
            }
            else if (_value.IsInt64Map())
            {
                writer.WriteStartObject();
                foreach (var pair in _value.AsInt64Map())
                {
                    writer.WriteNumber(pair.Key, pair.Value);
                }
                writer.WriteEndObject();
            }
            else if (_value.IsFloat32Map())
            {
                writer.WriteStartObject();
                foreach (var pair in _value.AsFloat32Map())
                {
                    writer.WriteNumber(pair.Key, pair.Value);
                }
                writer.WriteEndObject();
            }
            else if (_value.IsFloat64Map())
            {
                writer.WriteStartObject();
                foreach (var pair in _value.AsFloat64Map())
                {
                    writer.WriteNumber(pair.Key, pair.Value);
                }
                writer.WriteEndObject();
            }
            else if (_value.IsBoolMap())
            {
                writer.WriteStartObject();
                foreach (var pair in _value.AsBoolMap())
                {
                    writer.WriteBoolean(pair.Key, pair.Value);
                }
                writer.WriteEndObject();
            }
        }
    }//class

    /// <summary>
    /// 用于将json反序列化为回复数据
    /// </summary>
    class FieldConverter : JsonConverter<Proto.Field>
    {
        public override Proto.Field Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                List<string> ary = new List<string>();
                while (reader.Read())
                {
                   if(reader.TokenType == JsonTokenType.EndArray)
                    {
                        break;
                    }
                    string s = reader.GetString();
                    ary.Add(s);
                }
                return Proto.Field.FromStringAry(ary.ToArray());
            }
            return Proto.Field.FromString(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Proto.Field _value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }//class
}//namespace
