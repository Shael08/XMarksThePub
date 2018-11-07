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

namespace XMarksThePub
{
    [Activity(Label = "MapWithMarkersActivity")]
    public class MapWithMarkersActivity : AppCompatActivity, IOnMapReadyCallback
    {
        static LatLng Origin;
        static LatLng Destination;
        GoogleMap googleMap;
        List<List<LatLng>> polyline = new List<List<LatLng>>();

        public void OnMapReady(GoogleMap map)
        {
            googleMap = map;

            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.MyLocationButtonEnabled = false;

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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapLayout);

            var result = Intent.Extras.GetString("direction");

            var direction = JsonHelper.Instance.Deserialize<DirectionRootObject>(result);

            Origin = new LatLng(direction.routes.First().legs.First().start_location.lat, direction.routes.First().legs.First().start_location.lng);
            Destination = new LatLng(direction.routes.Last().legs.Last().end_location.lat, direction.routes.Last().legs.Last().end_location.lng);
            foreach (var route in direction.routes)
            {
                polyline.Add(route.overview_polyline.GetDecodedPolyLines);
            }

            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

        }

        void AddMarkersToMap()
        {
            var destMarker = new MarkerOptions();
            destMarker.SetPosition(Destination)
                      .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan));
            googleMap.AddMarker(destMarker);

            var originMarker = new MarkerOptions();
            originMarker.SetPosition(Origin);
            googleMap.AddMarker(originMarker);

            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(Origin, 15);
            googleMap.MoveCamera(cameraUpdate);
        }



    }
}