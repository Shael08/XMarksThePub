using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using xMarksThePub;
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

        private void InitViews()  //konkrétan innen indul az alkalmazás
        {
            start = FindViewById<Button>(Resource.Id.startButton); //a legelején a sart gomb...
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
            interestType = InterestType.Tobbaco;
        }

        private void StartClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ListActivity));
            intent.PutExtra("InterestType", (int)interestType);

            StartActivity(intent);
        }
    }
}

