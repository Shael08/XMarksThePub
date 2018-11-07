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
using SQLite;

namespace xMarksThePub.Model
{

    [JsonObject("openingHour"), Table("OpeningHours")]
    public class OpeningHours
    {
        [JsonProperty("closeing"), Column("closeing")]
        public string CloseString { get; set; }

        [JsonProperty("opening"), Column("opening")]
        public string OpenString { get; set; }

        [JsonProperty("weekday"), Column("weekday")]
        public int Day { get; set; }
    
    
    }
}