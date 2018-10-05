
using System;
using System.IO;
using Android.Graphics;
using Android.OS;
using Android.Util;

namespace xMarksThePub
{
    public sealed class ImageLoader
    {
        private static readonly ImageLoader instance = new ImageLoader();
        
        //Singleton model, igy a konstruktor privat, hogy ne keletkezhessen tobb peldany
        //Hasznalata: Imageloader.Instance.fuggvenyNeve(parameterei)
        private ImageLoader(){}

        public static ImageLoader Instance
        {
            get
            {
                return instance;
            }
        }

        public void SaveImage(Bitmap image, int imageId)
        {
            byte[] bitmapData = null;
            using (var stream = new MemoryStream())
            {
                image.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }

            if(bitmapData != null)
            {
                WriteAllBytes(bitmapData, imageId);
            }
            else
            {
                Log.Error("ERROR", "Failed to convert image to bitmap");
            }
        }

        public Bitmap LoadImage(int imageId)
        {
            byte[] bitmapData = ReadAllBytes(imageId);

            Bitmap bitmap = BitmapFactory.DecodeByteArray(bitmapData, 0, bitmapData.Length);

            return bitmap;
        }

        private bool Exists(int imageId)
        {
            return File.Exists(PathToFile(imageId));
        }

        private byte[] ReadAllBytes(int imageId)
        {
            if (Exists(imageId))
            {
                try
                {
                    return File.ReadAllBytes(PathToFile(imageId));
                }
                catch
                {
                    Log.Error("ERROR", "Failed to load image");
                }
            }
            return null;
        }

        private void WriteAllBytes(byte[] bytes, int imageId)
        {
            try
            {
                File.WriteAllBytes(PathToFile(imageId), bytes);
            }
            catch
            {
                Log.Error("ERROR", "Failed to save image");
            }
        }

        private string PathToFile(int imageId)
        {
            var path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), imageId.ToString() + ".jpg" );
            return path;
        }

    }
}