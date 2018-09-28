using System;
using System.Collections.Generic;
using System.Linq;

using Android;
using Android.App;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;

namespace XMarksThePub
{
    public static class GoogleMapHelpers
    {
        static readonly string TAG = "GoogleMapHelpers";

        static readonly string[] PERMISSIONS_LOCATION =
        {
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.AccessCoarseLocation
        };

        public static MapFragment AddMapFragmentToLayout(this AppCompatActivity activity, int resourceId, string tag = "map_frag",
                                                         IOnMapReadyCallback onMapReadyCallback = null)
        {
            var options = new GoogleMapOptions();
            options.InvokeMapType(GoogleMap.MapTypeHybrid)
                   .InvokeCompassEnabled(true);

            var mapFrag = MapFragment.NewInstance(options);

            activity.FragmentManager.BeginTransaction()
                    .Add(resourceId, mapFrag, tag)
                    .Commit();

            if (onMapReadyCallback == null)
            {
                if (activity is IOnMapReadyCallback callback)
                {
                    mapFrag.GetMapAsync(callback);
                }
                else
                {
                    throw new
                        ArgumentException("If the onMapReadyCallback is null, then the activity must implement the interface IOnMapReadyCallback.",
                                          nameof(activity));
                }
            }
            else
            {
                mapFrag.GetMapAsync(onMapReadyCallback);
            }

            return mapFrag;
        }

        public static bool PerformRuntimePermissionCheckForLocation(this AppCompatActivity activity, int requestCode)
        {
            if (activity.HasLocationPermissions())
            {
                return true;
            }

            if (activity.MustShowPermissionRationale())
            {
                var layoutForSnackbar = activity.FindViewById(Android.Resource.Id.Content);

                var requestPermissionAction = new Action<View>(delegate
                {
                    ActivityCompat.RequestPermissions(activity, PERMISSIONS_LOCATION, requestCode);
                });

                Snackbar.Make(layoutForSnackbar, Resource.String.location_permission_rationale, Snackbar.LengthIndefinite)
                        .SetAction(Resource.String.ok, requestPermissionAction);
            }
            else
            {
                ActivityCompat.RequestPermissions(activity, PERMISSIONS_LOCATION, requestCode);
            }

            return false;
        }

        public static bool MustShowPermissionRationale(this Activity activity)
        {
            var showShowRationale = false;

            foreach (var perm in PERMISSIONS_LOCATION)
            {
                if (ActivityCompat.ShouldShowRequestPermissionRationale(activity, perm))
                {
                    showShowRationale = true;
                    Log.Debug(TAG, $"Need to show permission rational for {perm}.");
                }
            }

            return showShowRationale;
        }

        public static bool HasLocationPermissions(this Activity activity)
        {
            var hasPermissions = true;
            foreach (var p in PERMISSIONS_LOCATION)
            {
                if (ContextCompat.CheckSelfPermission(activity, p) != (int)Permission.Granted)
                {
                    Log.Warn(TAG, $"App was not granted the {p} permission.");
                    hasPermissions = false;
                }
            }

            return hasPermissions;
        }

        public static bool AllPermissionsGranted(this IEnumerable<Permission> grantResults)
        {
            return grantResults.All(result => result != Permission.Denied);
        }
    }
}