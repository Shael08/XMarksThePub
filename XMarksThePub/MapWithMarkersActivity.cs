using System;

using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using xMarksThePub;

namespace XMarksThePub
{
    [Activity(Label = "MapWithMarkersActivity")]
    public class MapWithMarkersActivity : AppCompatActivity, IOnMapReadyCallback
    {
        static readonly LatLng JakabhegyiLatLng = new LatLng(46.0754064, 18.198169);
        static readonly LatLng kiskorsoLatLng = new LatLng(46.0771346, 18.2103851);
        Button animateToLocationButton;
        GoogleMap googleMap;


        public void OnMapReady(GoogleMap map)
        {
            googleMap = map;

            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.MyLocationButtonEnabled = false;
            AddMarkersToMap();
            animateToLocationButton.Click += AnimateToJakabhegyi;
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapLayout);

            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

            animateToLocationButton = FindViewById<Button>(Resource.Id.animateButton);
            animateToLocationButton.Click += AnimateToJakabhegyi;

            SetupZoomInButton();
            SetupZoomOutButton();
        }


        void AnimateToJakabhegyi(object sender, EventArgs e)
        {
            var builder = CameraPosition.InvokeBuilder();
            builder.Target(JakabhegyiLatLng);
            builder.Zoom(18);
            builder.Bearing(155);
            builder.Tilt(65);
            var cameraPosition = builder.Build();

            googleMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
        }

        void AddMarkersToMap()
        {
            var kiskorsoMarker = new MarkerOptions();
            kiskorsoMarker.SetPosition(kiskorsoLatLng)
                      .SetTitle("Kiskorsó")
                      .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan));
            googleMap.AddMarker(kiskorsoMarker);


            var jakabhegyiMarker = new MarkerOptions();
            jakabhegyiMarker.SetPosition(JakabhegyiLatLng)
                               .SetTitle("Jakabhegyi Kollégium");
            googleMap.AddMarker(jakabhegyiMarker);

            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(kiskorsoLatLng, 15);
            googleMap.MoveCamera(cameraUpdate);
        }

        void SetupZoomInButton()
        {
            var zoomInButton = FindViewById<Button>(Resource.Id.zoomInButton);
            zoomInButton.Click += (sender, e) => { googleMap.AnimateCamera(CameraUpdateFactory.ZoomIn()); };
        }

        void SetupZoomOutButton()
        {
            var zoomOutButton = FindViewById<Button>(Resource.Id.zoomOutButton);
            zoomOutButton.Click += (sender, e) => { googleMap.AnimateCamera(CameraUpdateFactory.ZoomOut()); };
        }
    }
}