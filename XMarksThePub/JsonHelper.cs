using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace xMarksThePub
{
    public class JsonHelper
    {
        private string SerializeList<T>(List<T> items)
        {
            var jsondetails = JsonConvert.SerializeObject(items);

            return jsondetails;
        }

        private string Serialize<T>(T items)
        {
            var jsondetails = JsonConvert.SerializeObject(items);

            return jsondetails;
        }

        private List<T> DeserializeList<T>(string json)
        {
            List<T> result = default(List<T>);

            if (json != null || json != string.Empty)
            {
                result = JsonConvert.DeserializeObject<List<T>>(json);
            }

            return result;
        }

        private T Deserialize<T>(string json)
        {
            T result = default(T);

            if (json != null || json != string.Empty)
            {
                result = JsonConvert.DeserializeObject<T>(json);
            }

            return result;
        }
    }
}