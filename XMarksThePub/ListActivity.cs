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
using XMarksThePub.Adapter;
using XMarksThePub.Model;

namespace XMarksThePub
{
    [Activity(Label = "ListActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class ListActivity : MainActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PubListLayout);

            ListView pubListView = FindViewById<ListView>(Resource.Id.PubListView);

            List<Pub> listItems = new List<Pub>();

            Pub pubitem = new Pub
            {
                Id = 123,
                Name = "kocsma",
                PhotoId = 456
            };

            listItems.Add(pubitem);
            listItems.Add(pubitem);
            listItems.Add(pubitem);

            var ListAdapter = new PubAdapter(this, listItems);
            pubListView.Adapter = ListAdapter;

        }
    }
}