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

namespace XMarksThePub.Model
{
    public enum InterestType
    {
        pub = 0,
        tobbaco = 1
    }


    public class Pub
    {
        public Pub(string name, string description, Type activityToLaunch)
        {
            Name = name;
            Description = description;
            ActivityToLaunch = activityToLaunch;
        }

        public string Name { get; }
        public string Description { get;}
        public int? PhotoId { get;}
        public Type ActivityToLaunch { get; }

        public void Start(Activity context)
        {
            var i = new Intent(context, ActivityToLaunch);
            context.StartActivity(i);
        }
    }
}