using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeaLog
{

    public class Rootobject
    {
        public Tea[] Tea { get; set; }
    }

    public class Tea
    {
        [JsonProperty(PropertyName = "tea_name")]
        public string TeaName { get; set; }

        [JsonProperty(PropertyName = "tea_type")]
        public string TeaType { get; set; }

        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "contains_caffeine")]
        public int ContainsCaffeine { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }

        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }
    }

}
