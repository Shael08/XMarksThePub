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
using xMarksThePub.Model;
using XMarksThePub.Model;

namespace xMarksThePub
{


    [JsonObject("type")]
    public class Types
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }


}