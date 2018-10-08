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
    public class PubAdapter : BaseAdapter<Pub>
    {
        readonly List<Pub> pubList;
        readonly Activity context;
        public PubAdapter(Activity context, List<Pub> items) : base()
        {
            this.context = context;
            this.pubList = items ?? new List<Pub>(0);
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override Pub this[int position]
        {
            get { return pubList[position]; }
        }

        public override int Count
        {
            get { return pubList.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView as FeatureRowHolder ?? new FeatureRowHolder(context);
            var sample = pubList[position];

            row.UpdateFrom(sample);
            return row;
        }
    }
}