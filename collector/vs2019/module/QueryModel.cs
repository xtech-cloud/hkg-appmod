using System;
using System.Collections.Generic;
using XTC.oelMVCS;

namespace hkg.collector
{
    public class QueryStatus
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class MetatableSourceListReply
    {
        public class Source
        {
            public string address { get; set; }
            public string name { get; set; }
            public string attribute { get; set; }
            public string expression { get; set; }
        }
        public QueryStatus status { get; set; }
        public Source[] entity{ get; set; }
    }

    public class MetatableVocabularyListReply
    {
        public class Vocabulary
        {
            public string name { get; set; }
            public string[] label { get; set; }
            public string[] value { get; set; }
        }
        public QueryStatus status { get; set; }
        public Vocabulary[] entity{ get; set; }
    }

    public class MetatableSchemaListReply
    {
        public class Rule
        {
            public string name { get; set; }
            public string field { get; set; }
            public string type { get; set; }
            public string element { get; set; }
            public Dictionary<string, string> pair{ get; set; }
        }

        public class Schema 
        {
            public string name { get; set; }
            public List<Rule> rule{ get; set; }
        }
        public QueryStatus status { get; set; }
        public Schema[] entity{ get; set; }
    }



    public class QueryModel : Model
    {
        public const string NAME = "hkg.collector.QueryModel";

        protected override void setup()
        {
            getLogger().Trace("setup hkg.collector.QueryModel");
        }
    }
}
