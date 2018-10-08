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
        bool isGooglePlayServicesInstalled;
        public static readonly int RC_INSTALL_GOOGLE_PLAY_SERVICES = 1000;
        public static readonly string TAG = "XMarksThePub";


        public PubTobbacoFragment() : base() { }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            isGooglePlayServicesInstalled = TestIfGooglePlayServicesIsInstalled();

            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.PubTobbacoFragment, container, false);

            PopulateList();
            pubListView = view.FindViewById<ListView>(Resource.Id.PubTobbacoListView);

            if (isGooglePlayServicesInstalled)
            {
                listAdapter = new PubAdapter(Activity, listItems);
            }
            else
            {
                Log.Error(TAG, "Google Play Services is not installed");
                listAdapter = new PubAdapter(Activity, null);
            }

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

        //protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    if (RC_INSTALL_GOOGLE_PLAY_SERVICES == requestCode && resultCode == Result.Ok)
        //    {
        //        isGooglePlayServicesInstalled = true;
        //    }
        //    else
        //    {
        //        Log.Warn(TAG, $"Don't know how to handle resultCode {resultCode} for request {requestCode}.");
        //    }
        //}

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

        bool TestIfGooglePlayServicesIsInstalled()
        {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(Activity);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info(TAG, "Google Play Services is installed on this device.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error(TAG, "There is a problem with Google Play Services on this device: {0} - {1}", queryResult, errorString);
                //var errorDialog = GoogleApiAvailability.Instance.GetErrorDialog(this, queryResult, RC_INSTALL_GOOGLE_PLAY_SERVICES);
                //var dialogFrag = new Fragment.ErrorDialogFragment(errorDialog);

                //dialogFrag.Show(FragmentManager, "GooglePlayServicesDialog");
            }

            return false;
        }
    }
}