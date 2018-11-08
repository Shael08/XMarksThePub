using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace xMarksThePub
{
    public class JsonHelper
    {
        private static readonly JsonHelper instance = new JsonHelper();

        private JsonHelper() { }

        public static JsonHelper Instance
        {
            get
            {
                return instance;
            }
        }


        public async Task<string> GetReleases(string url)
        {
            var client = new WebClient();

            Uri siteUri = new Uri(url);

            var response = await client.DownloadDataTaskAsync(siteUri);

            string result = System.Text.Encoding.UTF8.GetString(response);

            return result;
        }

        public string SerializeList<T>(List<T> items)
        {
            var jsondetails = JsonConvert.SerializeObject(items);

            return jsondetails;
        }

        public string Serialize<T>(T items)
        {
            var jsondetails = JsonConvert.SerializeObject(items);

            return jsondetails;
        }

        public List<T> DeserializeList<T>(string json)
        {
            List<T> result = default(List<T>);

            if (json != null || json != string.Empty)
            {
                result = JsonConvert.DeserializeObject<List<T>>(json);

            }

            return result;
        }

        public T Deserialize<T>(string json)
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