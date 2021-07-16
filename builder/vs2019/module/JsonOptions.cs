using System.Text.Encodings.Web;
using System.Text.Json;

namespace hkg.builder
{
    public class JsonOptions
    {
        public static JsonSerializerOptions DefaultSerializerOptions
        {
            get
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new AnyProtoConverter());
                options.WriteIndented = true;
                options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                return options;
            }
        }

    }
}
