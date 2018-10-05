using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace xMarksThePub.Activity
{
    [Activity(Label = "ImageLoaderTestActivity")]
    public class ImageLoaderTestActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TestLayout);

            Button button = FindViewById<Button>(Resource.Id.change);

            button.Click += delegate {
                var imageView = FindViewById<ImageView>(Resource.Id.dolanImageView);

                Bitmap dolan = BitmapFactory.DecodeResource(Resources, Resource.Drawable.Dolan);
                ImageLoader.Instance.SaveImage(dolan, 1234);
                Bitmap test = ImageLoader.Instance.LoadImage(1234);

                imageView.SetImageBitmap(test);
            };
        }
    }
}