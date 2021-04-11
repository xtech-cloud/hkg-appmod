
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using XTC.oelMVCS;
namespace HKG.Module.Metatable
{
    class AnyConverter : JsonConverter<Any>
    {
        public override Any Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Any();
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
        }
    }//class

    class FieldConverter : JsonConverter<Proto.Field>
    {
        public override Proto.Field Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Proto.Field.FromString(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Proto.Field _value, JsonSerializerOptions options)
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
        }
    }//class
}//namespace
