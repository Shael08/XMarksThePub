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
    /// <summary>
    /// Ez nem szorul különösebb magyarázatra, egyéb infó: lásd MainActivity!
    /// </summary>
    public enum InterestType
    {
        Pub = 0,
        Tobbaco = 1
    }

    /// <summary>
    /// Ez az alap
    /// </summary>
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