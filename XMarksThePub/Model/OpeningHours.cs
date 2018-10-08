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
    [Table("OpeningHours")]
    public class OpeningHours
    {

        public OpeningHours(TimeSpan open, TimeSpan close)
        {
            Open = open;
            Close = close;
        }

        [JsonProperty("Open")]
        public TimeSpan Open { get; set; }

        [JsonProperty("Close")]
        public TimeSpan Close { get; set; }

        [JsonIgnore, Column("open")]
        public string OpenString
        {
            get
            {
                return Open.ToString(@"hh\:mm");
            }
        }

        [JsonIgnore, Column("close")]
        public string CloseString
        {
            get
            {
                return Close.ToString(@"hh\:mm");
            }
        }

    }
}