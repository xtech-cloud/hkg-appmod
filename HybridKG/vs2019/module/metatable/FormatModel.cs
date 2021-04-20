
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class FormatModel : Model
    {
        public class Pattern
        {
            public string[] from { get; set; }
            public string to { get; set; }
        }

        public const string NAME = "Metatable.FormatModel";

        public class FormatStatus : Model.Status
        {
            public const string NAME = "Metatable.FormatModel";

            public List<Proto.FormatEntity> formats = new List<Proto.FormatEntity>();
            public long total = 0;
        }

        protected override void preSetup()
        {
            Error err;
            status_ = spawnStatus<FormatStatus>(FormatStatus.NAME, out err);
        }

        protected override void setup()
        {
            getLogger().Trace("setup FormatModel");
        }

        protected override void preDismantle()
        {
            Error err;
            killStatus(FormatStatus.NAME, out err);
        }

        private FormatStatus status
        {
            get
            {
                return status_ as FormatStatus;
            }
        }

        public void SaveFormats(FormatStatus _status)
        {
            status.formats = _status.formats;
            status.total = _status.total;
        }

        public string GetPatternsJson(string _name)
        {
            List<Pattern> patterns = new List<Pattern>();

            Proto.FormatEntity found = status.formats.Find((_item) =>
            {
                return _item._name.AsString().Equals(_name);
            });
            if(null != found)
            {
                foreach(var pattern in found._pattern)
                {
                    Pattern p = new Pattern();
                    p.to = pattern._to.AsString();
                    List<string> froms = new List<string>() ;
                    foreach(var from in pattern._from.AsStringAry())
                    {
                        froms.Add(from);
                    }
                    p.from = froms.ToArray();
                    patterns.Add(p);
                }
            }
            var options = new JsonSerializerOptions()
            {
                //WriteIndented = true
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            return System.Text.Json.JsonSerializer.Serialize(patterns, options);
        }
    }
}
