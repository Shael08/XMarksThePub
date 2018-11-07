using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using xMarksThePub;
using xMarksThePub.Model;
using XMarksThePub.Model;

namespace XMarksThePub
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        InterestType interestType;
        Button start;
        RadioButton pub;
        RadioButton tobbaco;
        static readonly Android.Gms.Maps.Model.LatLng JakabhegyiLatLng = new Android.Gms.Maps.Model.LatLng(46.0754064, 18.198169);
        static readonly Android.Gms.Maps.Model.LatLng kiskorsoLatLng = new Android.Gms.Maps.Model.LatLng(46.0771346, 18.2103851);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            InitViews();

        }

        protected override void OnResume()
        {
            base.OnResume();
            AddEventHandlers();
        }

        protected override void OnPause()
        {
            base.OnPause();
            RemoveEventHandlers();
        }

        private void InitViews()
        {
            start = FindViewById<Button>(Resource.Id.startButton);
            pub = FindViewById<RadioButton>(Resource.Id.kocsmaRadioButton);
            tobbaco = FindViewById<RadioButton>(Resource.Id.DohanyboltRadioButton);
        }

        private void AddEventHandlers()
        {

            start.Click += StartClick;
            pub.Click += Kocsma_Click;
            tobbaco.Click += Dohanybolt_Click;
        }

        private void RemoveEventHandlers()
        {
            start.Click -= StartClick;
            pub.Click -= Kocsma_Click;
            tobbaco.Click -= Dohanybolt_Click;
        }

        private void Kocsma_Click(object sender, EventArgs e)
        {
            interestType = InterestType.Pub;
        }

        private void Dohanybolt_Click(object sender, EventArgs e)
        {
            interestType = InterestType.Tobacco;
        }

        private void StartClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ListActivity));
            intent.PutExtra("InterestType", (int)interestType);

            StartActivity(intent);
        }


    }
}

