using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using XMarksThePub.Model;

namespace xMarksThePub.Model
{
    [JsonObject("meta")]
    public class Meta
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

    }

    class AllStore
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("objects")]
        public List<Store> Stores { get; set; }
    }
}