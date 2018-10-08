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

// Minden komment vagy az adott blokk felett, vagy adott blokk sorai végén foglal helyet!

namespace XMarksThePub
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)] //ez határozza meg, hogy melyik "Activity" indul először, amikor megnyitom az alkalmazást, vagyis meghatározza a kezdőpontot
    public class MainActivity : AppCompatActivity
    {
        InterestType interestType;
        Button start;
        RadioButton pub;
        RadioButton tobbaco;
        /// <summary>
        /// Az OnCreate indul legelőször. Amikor itt vagyunk, még nem látszik a képernyőn semmi
        /// Itt érdemes minden olyan adatot becsatolni, ami szükséges "OnStart" részhez
        /// </summary>
        
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
        /// <summary>
        /// Itt adjuk meg a kapcsolatokat az xml-ben lévő elemek és a változók között.
        /// Itt még a nyomógombokat nem kezeljük, csak létrehozzuk őket.
        /// </summary>
        private void InitViews()  //konkrétan innen indul az alkalmazás
        {
            start = FindViewById<Button>(Resource.Id.startButton); //a legelején a sart gomb...
            pub = FindViewById<RadioButton>(Resource.Id.kocsmaRadioButton);
            tobbaco = FindViewById<RadioButton>(Resource.Id.DohanyboltRadioButton);
        }
        /// <summary>
        /// Ezeket eseménykezelőnek hívjuk. Most adjuk őket hozzá a gombokhoz.
        /// </summary>
        private void AddEventHandlers()
        {

            start.Click += StartClick;
            pub.Click += Kocsma_Click;
            tobbaco.Click += Dohanybolt_Click;
        }
        /// <summary>
        /// Ez a blokk lényegében az eggyel fentebbinek a fordítottja. :D
        /// Ez a blokk a gomboktól eltüntetni az előbb hozzájuk rendelt funkció(ka)t
        /// </summary>
        private void RemoveEventHandlers()
        {
            start.Click -= StartClick;
            pub.Click -= Kocsma_Click;
            tobbaco.Click -= Dohanybolt_Click;
        }
        /// <summary>
        /// Ez a rész akkor hajtódik végre, ha a felhasználó kipipálta a start gomb lenyomása előtt a kocsmákat.
        /// Ez lényegében csak egy értékadás (Kocsma = 0; Dohánybolt = 1, lásd "Pub.cs" 15. sorától)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kocsma_Click(object sender, EventArgs e)
        {
            interestType = InterestType.Pub;
        }
        /// <summary>
        /// Ugyanaz, mint egy blokkal feljebb...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dohanybolt_Click(object sender, EventArgs e)
        {
            interestType = InterestType.Tobbaco;
        }
        /// <summary>
        /// Ez a rész olyan, mint egy "postás".
        /// Két aktivitás között ez biztosítja a kommunikációt.
        /// Az "intent" mondja meg, hogy a MainActivityből a ListActivitybe megy át a kép...
        /// A "PutExtra"-val tudom megmondani az "InterestType" helyén,
        /// hogy kocsmát, vagy dohányboltot szeretnék
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ListActivity));
            intent.PutExtra("InterestType", (int)interestType);

            StartActivity(intent);
        }
    }
}

