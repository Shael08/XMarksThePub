using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using xMarksThePub.Activity;
using xMarksThePub.Model;
using XMarksThePub;
using XMarksThePub.Adapter;
using XMarksThePub.Model;
using static Android.Widget.AdapterView;

namespace xMarksThePub.Fragment
{
    using AndroidUri = Uri;

    public class PubTobbacoFragment : Android.Support.V4.App.Fragment
    {
        private static List<Store> listItems = new List<Store>();
        ListView pubListView;
        PubAdapter listAdapter;
        string interestType;

        public PubTobbacoFragment() : base() {}

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Bundle args = this.Arguments;
            int type = args.GetInt("type");

            interestType = Enum.GetNames(typeof(InterestType))[type];

            await PopulateList();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.PubTobbacoFragment, container, false);

            pubListView = view.FindViewById<ListView>(Resource.Id.PubTobbacoListView);

            listAdapter = new PubAdapter(Activity, listItems);

            pubListView.Adapter = listAdapter;
            pubListView.ItemClick += ItemSelected;

            return view;
        }

        private async Task PopulateList()
        {
            var result = await JsonHelper.Instance.GetReleases("http://shael.pythonanywhere.com/api/store/");

            var list = JsonHelper.Instance.Deserialize<AllStore>(result);

            listAdapter.PubList = list.Stores.FindAll(x => x.Types.Name == interestType);

            listAdapter.NotifyDataSetChanged();
        }

        void ItemSelected(object sender, AdapterView.ItemClickEventArgs e)
        {
            var position = e.Position;
            var sampleToStart = listItems[position];
            sampleToStart.Start(Activity);
        }


    }
}