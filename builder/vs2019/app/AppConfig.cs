using System.Text.Json;
using XTC.oelMVCS;

namespace app
{
    class ConfigSchema
    {
        public string domain_public {get;set;}
        public string domain_private{get;set;}
    }
    class AppConfig: Config
    {
        public override void Merge(string _content)
        {
            ConfigSchema schema = JsonSerializer.Deserialize<ConfigSchema>(_content);
            fields_["_.domain.public"] = Any.FromString(schema.domain_public);
            fields_["_.domain.private"] = Any.FromString(schema.domain_private);
            fields_["_.domain.local"] = Any.FromString("");
        }
    }//class
}//namespace
