using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using xMarksThePub.Activity;
using xMarksThePub.Model;
using XMarksThePub;
using XMarksThePub.Adapter;
using XMarksThePub.Model;
using static Android.Widget.AdapterView;

namespace xMarksThePub.Fragment
{
    using AndroidUri = Uri;

    public class PubTobbacoFragment : Android.Support.V4.App.Fragment
    {
        private static List<Pub> listItems = new List<Pub>();
        ListView pubListView;
        PubAdapter listAdapter;

        public PubTobbacoFragment() : base() { }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.PubTobbacoFragment, container, false);

            PopulateList();
            pubListView = view.FindViewById<ListView>(Resource.Id.PubTobbacoListView);

            listAdapter = new PubAdapter(Activity, listItems);

            pubListView.Adapter = listAdapter;
            pubListView.ItemClick += ItemSelected;

            return view;
        }

        private void PopulateList()
        {
            Dictionary<string, OpeningHours> openingHours = new Dictionary<string, OpeningHours>();

            for (int i = 0; i < 7; i++)
            {
                openingHours.Add(Enum.GetName(typeof(DayOfWeek), i), new OpeningHours(new TimeSpan(i, 0, 0), new TimeSpan(i + 12, 0, 0)));
            }

            listItems = new List<Pub> {new Pub("Kocsma", openingHours, typeof(LocationActivity)),
                                       new Pub("Kiskorsó", openingHours, typeof(MapWithMarkersActivity)),
                                       new Pub("Kocsma", openingHours, typeof(LocationActivity)),
                                       new Pub("Csinos", openingHours, typeof(MapWithMarkersActivity)),
                                       new Pub("ImageTestWithDolan", openingHours, typeof(ImageLoaderTestActivity))
                                       };

        }

        void ItemSelected(object sender, AdapterView.ItemClickEventArgs e)
        {
            var position = e.Position;
            //if (position == 0)
            //{
            //    var geoUri = AndroidUri.Parse("geo:46.0754064, 18.198169");
            //    var mapIntent = new Intent(Intent.ActionView, geoUri);
            //    StartActivity(mapIntent);
            //    return;
            //}

            var sampleToStart = listItems[position];
            sampleToStart.Start(Activity);
        }


    }
}