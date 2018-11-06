using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Newtonsoft.Json;
using SQLite;
using xMarksThePub;
using xMarksThePub.Model;

namespace XMarksThePub.Model
{
    public enum InterestType
    {
        Pub = 0,
        Tobacco = 1
    }


    [JsonObject("objects"), Table("Stores")]
    public class Store
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("id"), PrimaryKey]
        public int Id { get; set; }

        [JsonProperty("latitude"), Column("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude"), Column("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("name"), Column("name")]
        public string Name { get; set; }

        [JsonProperty("openingHours")]
        public List<OpeningHours> OpeningHours { get; set; }

        [JsonProperty("types"), Column("InterestType")]
        public Types Types { get; set; }

        [Ignore, JsonIgnore]
        public string Open
        {
            get
            {
                var result = OpeningHours.Find(x => x.Day == (int)DateTime.Now.DayOfWeek);

                if (result != null)
                {
                    return result.OpenString.Substring(0, result.OpenString.Length - 3);
                }
                else
                {
                    return default(string);
                }
            }
        }

        [Ignore, JsonIgnore]
        public string Close
        {
            get
            {
                var result = OpeningHours.Find(x => x.Day == (int)DateTime.Now.DayOfWeek);

                if (result != null)
                {

                    return result.CloseString.Substring(0, result.CloseString.Length - 3);
                }
                else
                {
                    return default(string);
                }
            }
        }

        [JsonIgnore]
        public static Type ActivityToLaunch = typeof(MapWithMarkersActivity);

        public void Start(Activity context)
        {
            var i = new Intent(context, ActivityToLaunch);
            context.StartActivity(i);
        }
    }

}