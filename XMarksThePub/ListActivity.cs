using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using XMarksThePub.Adapter;
using XMarksThePub.Model;
using XMarksThePub.Fragment;

using Uri = Android.Net.Uri;
using xMarksThePub;

namespace XMarksThePub
{
    using AndroidUri = Uri;

    [Activity(Label = "ListActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class ListActivity : AppCompatActivity
    {
        public static readonly int RC_INSTALL_GOOGLE_PLAY_SERVICES = 1000;
        public static readonly string TAG = "XMarksThePub";


        static readonly List<Pub> listItems = new List<Pub> {new Pub("Kocsma", "Opening Hours", typeof(LocationActivity)),
                                                             new Pub("Kiskorsó", "Opening Hours", typeof(MapWithMarkersActivity)),
                                                             new Pub("Kocsma", "Opening Hours", typeof(LocationActivity)),
                                                             new Pub("Csinos", "Opening Hours", typeof(MapWithMarkersActivity))
                                                            };



    bool isGooglePlayServicesInstalled;
        ListView pubListView;
        PubAdapter listAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PubListLayout);
            isGooglePlayServicesInstalled = TestIfGooglePlayServicesIsInstalled();

            var interestType = (InterestType)Intent.Extras.GetInt("InterestType");

            InitializeListView();

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (RC_INSTALL_GOOGLE_PLAY_SERVICES == requestCode && resultCode == Result.Ok)
            {
                isGooglePlayServicesInstalled = true;
            }
            else
            {
                Log.Warn(TAG, $"Don't know how to handle resultCode {resultCode} for request {requestCode}.");
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            pubListView.ItemClick += ItemSelected;
        }

        protected override void OnPause()
        {
            pubListView.ItemClick -= ItemSelected;
            base.OnPause();
        }

        private void InitializeListView()
        {
            pubListView = FindViewById<ListView>(Resource.Id.PubListView);

            if (isGooglePlayServicesInstalled)
            {
                listAdapter = new PubAdapter(this, listItems);
            }
            else
            {
                Log.Error(TAG, "Google Play Services is not installed");
                listAdapter = new PubAdapter(this, null);
            }

            pubListView.Adapter = listAdapter;
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
            sampleToStart.Start(this);
        }

        bool TestIfGooglePlayServicesIsInstalled()
        {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info(TAG, "Google Play Services is installed on this device.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error(TAG, "There is a problem with Google Play Services on this device: {0} - {1}", queryResult, errorString);
                var errorDialog = GoogleApiAvailability.Instance.GetErrorDialog(this, queryResult, RC_INSTALL_GOOGLE_PLAY_SERVICES);
                var dialogFrag = new Fragment.ErrorDialogFragment(errorDialog);

                dialogFrag.Show(FragmentManager, "GooglePlayServicesDialog");
            }

            return false;
        }
    }
}