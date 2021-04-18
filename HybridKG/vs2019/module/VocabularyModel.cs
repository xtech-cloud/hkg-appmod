
using System.Collections.Generic;
using XTC.oelMVCS;

namespace HKG.Module.Metatable
{
    public class VocabularyModel : Model
    {
        public const string NAME = "Metatable.VocabularyModel";

        public class VocabularyStatus : Model.Status
        {
            public const string NAME = "Metatable.VocabularyStatus";
            public List<Proto.VocabularyEntity> vocabularies = new List<Proto.VocabularyEntity>();
            public long total = 0;
        }

        protected override void preSetup()
        {
            Error err;
            status_ = spawnStatus<VocabularyStatus>(VocabularyStatus.NAME, out err);
        }

        protected override void setup()
        {
            getLogger().Trace("setup VocabularyModel");
        }

        protected override void preDismantle()
        {
            Error err;
            killStatus(VocabularyStatus.NAME, out err);
        }

        private VocabularyStatus status
        {
            get
            {
                return status_ as VocabularyStatus;
            }
        }

        public void SaveDocuments(VocabularyStatus _status)
        {
            status.vocabularies = _status.vocabularies;
        }

        public string GetSchema(string _value, string[] _keyword)
        {
            var found = status.vocabularies.Find((_item) =>
            {
                bool hasValue = false;
                foreach(string v in _item._value.AsStringAry())
                {
                    if (_value.Equals(v))
                        hasValue = true;
                }
                int hasKeyword = 0;
                foreach (string v in _item._label.AsStringAry())
                {
                    foreach (string k in _keyword)
                    {
                        if (v.Equals(k))
                            hasKeyword += 1;
                    }
                }
                return hasValue && (hasKeyword == _keyword.Length);
            });
            if (null == found)
                return "";
            return found._schema.AsString();
        }
    }
}
