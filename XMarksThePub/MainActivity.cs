using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using XMarksThePub.Model;

namespace XMarksThePub
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        InterestType interestType;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);


            Button start = FindViewById<Button>(Resource.Id.startButton);
            start.Click += StartClick;

            RadioButton kocsma = FindViewById<RadioButton>(Resource.Id.kocsmaRadioButton);
            kocsma.Click += Kocsma_Click;

            RadioButton dohanybolt = FindViewById<RadioButton>(Resource.Id.DohanyboltRadioButton);
            dohanybolt.Click += Dohanybolt_Click;


        }

        private void Kocsma_Click(object sender, EventArgs e)
        {
            interestType = InterestType.pub;

            //RunOnUiThread(() =>
            //    Toast.MakeText(this, "Pub selected", ToastLength.Long).Show()
            //);

        }

        private void Dohanybolt_Click(object sender, EventArgs e)
        {

            interestType = InterestType.tobbaco;
        }


        private void StartClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ListActivity));
            intent.PutExtra("InterestType", (int)interestType);

            StartActivity(intent);
        }
        
	}
}