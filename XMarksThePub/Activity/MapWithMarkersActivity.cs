using System;
using System.Collections.Generic;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using xMarksThePub;
using xMarksThePub.Model;
using Android.Locations;
using Android.Runtime;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading;
using XMarksThePub.Model;

namespace XMarksThePub
{
    [Activity(Label = "MapWithMarkersActivity")]
    public class MapWithMarkersActivity : AppCompatActivity, IOnMapReadyCallback, ILocationListener
    {
        static LatLng Origin;
        static LatLng Destination;
        GoogleMap googleMap;
        Location CurrentLocation;
        LocationManager _locationManager;
        string _locationProvider;
        SemaphoreSlim semaphore = new SemaphoreSlim(0, 1);
        Marker originMarker;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapLayout);

            var result = Intent.Extras.GetString("direction");

            var destination = JsonHelper.Instance.Deserialize<Store>(result);

            Destination = new LatLng(destination.Latitude, destination.Longitude);

            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

            InitializeLocationManager();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        public async void OnMapReady(GoogleMap map)
        {
            googleMap = map;

            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.MyLocationButtonEnabled = false;

            await semaphore.WaitAsync();

            Origin = new LatLng(CurrentLocation.Latitude, CurrentLocation.Longitude);

            var url = GetDirectionsUrl(Origin, Destination);

            var result = await JsonHelper.Instance.GetReleases(url);

            var direction = JsonHelper.Instance.Deserialize<DirectionRootObject>(result);

            List<List<LatLng>> polyline = new List<List<LatLng>>();

            foreach (var route in direction.routes)
            {
                polyline.Add(route.overview_polyline.GetDecodedPolyLines);
            }

            AddMarkersToMap();
            AddDirection(polyline);
        }

        private void AddDirection(List<List<LatLng>> result)
        {
            PolylineOptions lineOptions = new PolylineOptions();

            foreach (var pathList in result)
            {
                foreach (var path in pathList)
                {
                    lineOptions.Add(path);
                }
            }
            lineOptions.InvokeColor(0x66FF0000);
            googleMap.AddPolyline(lineOptions);
        }

        void AddMarkersToMap()
        {
            var destMarker = new MarkerOptions();
            destMarker.SetPosition(Destination)
                      .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan));
            googleMap.AddMarker(destMarker);

            var originMarkeroptions = new MarkerOptions();
            originMarkeroptions.SetPosition(Origin);   
            originMarker = googleMap.AddMarker(originMarkeroptions);

            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(Origin, 15);
            googleMap.MoveCamera(cameraUpdate);
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

        #region ILocationListener

        public void OnLocationChanged(Location location)
        {
            CurrentLocation = location;
            if (semaphore.CurrentCount == 0)
            {
                semaphore.Release();
            }
            else
            {

                if (googleMap != null)
                {
                    LatLng mylocation = new LatLng(location.Latitude, location.Longitude);
                    if (originMarker != null)
                    {
                        originMarker.Remove();
                        originMarker = null;
                    }
                    originMarker = googleMap.AddMarker(new MarkerOptions().SetPosition(mylocation));
                }
            }
        }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras) { }

        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
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

        #endregion
    }
}