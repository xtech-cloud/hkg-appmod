
using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class SchemaModel : Model
    {
        public const string NAME = "Metatable.SchemaModel";

        public class SchemaStatus : Model.Status
        {
            public const string NAME = "Metatable.SchemaStatus";

            public List<Proto.SchemaEntity> schemas = new List<Proto.SchemaEntity>();
            public long total = 0;
        }

        protected override void preSetup()
        {
            Error err;
            status_ = spawnStatus<SchemaStatus>(SchemaStatus.NAME, out err);
        }

        protected override void setup()
        {
            getLogger().Trace("setup SchemaModel");
        }

        protected override void preDismantle()
        {
            Error err;
            killStatus(SchemaStatus.NAME, out err);
        }

        private SchemaStatus status
        {
            get
            {
                return status_ as SchemaStatus;
            }
        }

        public void SaveSchemas(SchemaStatus _status)
        {
            status.schemas = _status.schemas;
            status.total = _status.total;
        }

        public Dictionary<string, string> GetAllRule(string _schemaName)
        {
            Dictionary<string, string> rules = new Dictionary<string, string>();
            foreach(Proto.SchemaEntity schema in status.schemas)
            {
                if (!schema._name.AsString().Equals(_schemaName))
                    continue;
                foreach (Proto.RuleEntity r in schema._rule)
                {
                    string rule = "";
                    if (!string.IsNullOrEmpty(r._type.AsString()))
                        rule += string.Format("$t={0};", r._type.AsString());
                    if (!string.IsNullOrEmpty(r._element.AsString()))
                        rule += string.Format("$e={0};", r._element.AsString());
                    if (!string.IsNullOrEmpty(r._pair._key.AsString()))
                        rule += string.Format("$pk={0};", r._pair._key.AsString());
                    if (!string.IsNullOrEmpty(r._pair._value.AsString()))
                        rule += string.Format("$pv={0};", r._pair._value.AsString());
                    rules[rule] = r._field.AsString();
                }
            }
            return rules;
        }
    }
}
