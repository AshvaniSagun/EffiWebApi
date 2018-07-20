using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EfficiencyClassWebAPI.Models
{
    public class TestResponseVo
    {
        [JsonProperty(PropertyName = "value")]
        public decimal? Value { get; set; }
        [JsonProperty(PropertyName = "calculatedEfficiencyClass")]
        public string CalculatedEfficiencyClass { get; set; }
        [JsonProperty(PropertyName = "responseCode")]
        public string ResponseCode { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }
}