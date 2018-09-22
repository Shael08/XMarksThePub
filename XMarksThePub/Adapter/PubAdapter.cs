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
        List<Pub> pubList;
        Activity context;
        public PubAdapter(Activity context, List<Pub> items) : base()
        {
            this.context = context;
            this.pubList = items;
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
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.PubListItem, null);
            view.FindViewById<TextView>(Resource.Id.PubName).Text = pubList[position].Name;
            return view;
        }
    }
}