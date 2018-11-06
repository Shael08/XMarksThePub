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
using XMarksThePub.Model;

namespace XMarksThePub.Adapter
{
    public class PubAdapter : BaseAdapter<Store>
    {
        public List<Store> PubList { get; set; }
        readonly Activity context;
        public PubAdapter(Activity context, List<Store> items) : base()
        {
            this.context = context;
            this.PubList = items ?? new List<Store>(0);
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override Store this[int position]
        {
            get { return PubList[position]; }
        }

        public override int Count
        {
            get { return PubList.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView as FeatureRowHolder ?? new FeatureRowHolder(context);
            var sample = PubList[position];

            row.UpdateFrom(sample);
            return row;
        }
    }
}