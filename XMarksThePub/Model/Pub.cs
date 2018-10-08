using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Newtonsoft.Json;
using SQLite;
using xMarksThePub.Model;

namespace XMarksThePub.Model
{
    public enum InterestType
    {
        Pub = 0,
        Tobbaco = 1
    }

    [Table("Stores")]
    public class Pub
    {
        public Pub(string name, Dictionary<string, OpeningHours> openingHours,  Type activityToLaunch)
        {
            Name = name;
            OpeningHours = openingHours;
            ActivityToLaunch = activityToLaunch;
        }

        [PrimaryKey, JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("name"), Column("name")]
        public string Name { get; }

        [JsonProperty("photoId"), Column("photoId")]
        public int? PhotoId { get; }

        [JsonProperty("latitude"), Column("latitude")]
        public int Latitude { get; }

        [JsonProperty("longitude"), Column("longitude")]
        public int Longitude { get; }

        [Ignore, JsonProperty("OpeningHours")]
        private Dictionary<string, OpeningHours> OpeningHours { get; set; }

        [Ignore, JsonIgnore]
        public string Open
        {
            get
            {
                OpeningHours open;
                OpeningHours.TryGetValue(Enum.GetNames(typeof(DayOfWeek))[(int)DateTime.Now.DayOfWeek], out open);
                return open.OpenString;
            }
        }

        [Ignore, JsonIgnore]
        public string Close
        {
            get
            {
                OpeningHours close;
                OpeningHours.TryGetValue(Enum.GetNames(typeof(DayOfWeek))[(int)DateTime.Now.DayOfWeek], out close);
                return close.CloseString;
            }
        }

        [Ignore, JsonIgnore]
        public InterestType Interest
        {
            get
            {
                return (InterestType)InterestInt;
            }
            set
            {
                InterestInt = (int)(Interest);
            }
        }

        [JsonIgnore, Column("InterestType")]
        private int InterestInt { get; set; }

        [Ignore, JsonIgnore]
        public Type ActivityToLaunch { get; }

        public void Start(Activity context)
        {
            var i = new Intent(context, ActivityToLaunch);
            context.StartActivity(i);
        }

    }
}