using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Maps.Model;
using Android.Locations;
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

    public class PubTobbacoFragment : Android.Support.V4.App.Fragment, ILocationListener
    {
        private List<Store> listItems = new List<Store>();
        ListView pubListView;
        PubAdapter listAdapter;
        string interestType;
        Location CurrentLocation;
        LocationManager _locationManager;
        string _locationProvider;

        public PubTobbacoFragment() : base() {}

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitializeLocationManager();

            Bundle args = this.Arguments;
            int type = args.GetInt("type");

            interestType = Enum.GetNames(typeof(InterestType))[type];

            await PopulateList();
        }

        public override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }

        public override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)Activity.GetSystemService(Context.LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };


            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }

            CurrentLocation = _locationManager.GetLastKnownLocation(_locationProvider);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.PubTobbacoFragment, container, false);

            pubListView = view.FindViewById<ListView>(Resource.Id.PubTobbacoListView);

            listAdapter = new PubAdapter(Activity);

            pubListView.Adapter = listAdapter;
            pubListView.ItemClick += ItemSelected;

            return view;
        }

        private async Task PopulateList()
        {
            var result = await JsonHelper.Instance.GetReleases("http://shael.pythonanywhere.com/api/store/");

            var list = JsonHelper.Instance.Deserialize<AllStore>(result);

            listItems = list.Stores.FindAll(x => x.Types.Name == interestType);

            listAdapter.PubList = listItems;

            listAdapter.NotifyDataSetChanged();
        }

        private async void ItemSelected(object sender, AdapterView.ItemClickEventArgs e)
        {
            var position = e.Position;
            var sampleToStart = listItems[position];

            if (CurrentLocation != null)
            {

                string url = GetDirectionsUrl(new LatLng(CurrentLocation.Latitude, CurrentLocation.Longitude), new LatLng(sampleToStart.Latitude, sampleToStart.Longitude));
                var result = await JsonHelper.Instance.GetReleases(url);

                sampleToStart.Start(Activity, result);
            }
            else
            {
                Toast.MakeText(this.Context, "Can not get locataion data", ToastLength.Short);
            }
        }

        private string GetDirectionsUrl(LatLng origin, LatLng dest)
        {
            string str_origin = "origin=" + origin.Latitude + "," + origin.Longitude;
            string str_dest = "destination=" + dest.Latitude + "," + dest.Longitude;
            string mode = "mode = walking";
            string key = "key=AIzaSyCI6y2cUkZFZ35YUC03a_0fDPl-ntc2Ros";

            string parameters = str_origin + "&" + str_dest + "&" + mode + "&" + key;

            string output = "json";

            string url = "https://maps.googleapis.com/maps/api/directions/" + output + "?" + parameters;

            return url;
        }

        public void OnLocationChanged(Location location)
        {
            CurrentLocation = location;
        }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras) { }
    }
}