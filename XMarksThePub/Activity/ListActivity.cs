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
using xMarksThePub.Activity;
using Newtonsoft.Json;
using xMarksThePub.Model;
using xMarksThePub.Fragment;
using Android.Support.Design.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace XMarksThePub
{
    using AndroidUri = Uri;

    [Activity(Label = "ListActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class ListActivity : AppCompatActivity, IOnMapReadyCallback
    {
        BottomNavigationView bottomNavigation;
        static readonly LatLng JakabhegyiLatLng = new LatLng(46.0754064, 18.198169);
        static readonly LatLng kiskorsoLatLng = new LatLng(46.0771346, 18.2103851);
        GoogleMap googleMap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PubListLayout);


            var interestType = (InterestType)Intent.Extras.GetInt("InterestType");

            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);

            bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;

            switch (interestType)
            {
                case InterestType.Pub:
                    bottomNavigation.SelectedItemId = Resource.Id.menu_pub;
                    break;
                case InterestType.Tobbaco:
                    bottomNavigation.SelectedItemId = Resource.Id.menu_tobbaco;
                    break;
            }
        }

        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        void LoadFragment(int id)
        {
            FrameLayout Listlayout = (FrameLayout)FindViewById(Resource.Id.content_frame);
            FrameLayout Maplayout = (FrameLayout)FindViewById(Resource.Id.content_frame_map);

            if (id != Resource.Id.map)
            {
                Listlayout.Visibility = Android.Views.ViewStates.Visible;
                Maplayout.Visibility = Android.Views.ViewStates.Gone;

                Android.Support.V4.App.Fragment fragment = null;
                switch (id)
                {
                    case Resource.Id.menu_pub:
                        fragment = PubTobbacoFragment.Instantiate(this, Java.Lang.Class.FromType(typeof(PubTobbacoFragment)).Name);
                        break;
                    case Resource.Id.menu_tobbaco:
                        fragment = PubTobbacoFragment.Instantiate(this, Java.Lang.Class.FromType(typeof(PubTobbacoFragment)).Name);
                        break;
                }

                if (fragment == null)
                    return;

                SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.content_frame, fragment)
                    .Commit();
            }
            else
            {
                Listlayout.Visibility = Android.Views.ViewStates.Gone;
                Maplayout.Visibility = Android.Views.ViewStates.Visible;

                var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.content_frame_map);
                mapFragment.GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap map)
        {
            googleMap = map;

            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.MyLocationButtonEnabled = false;
            AddMarkersToMap();
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
    }
}