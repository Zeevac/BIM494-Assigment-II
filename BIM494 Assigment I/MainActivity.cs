using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.Design.Widget;
using Android.Content;
using static Android.Widget.AdapterView;
using System;
using Newtonsoft.Json;

namespace BIM494_Assigment_I
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static List<Person> persons = new List<Person>();
        public static Dictionary<int, List<string>> messages = new Dictionary<int, List<string>>();
        PersonAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            persons.Add(new Person(0,"Safa", Resource.Drawable.image));
            persons.Add(new Person(1,"Melis", Resource.Drawable.image));
            persons.Add(new Person(2,"Orkun", Resource.Drawable.image));
            messages.Add(0, new List<string>());
            messages.Add(1, new List<string>());
            messages.Add(2, new List<string>());
            messages[0].Add("Naber?");
            messages[0].Add("Nasılsın?");
            messages[1].Add("Nerdesin?");
            messages[1].Add("Saat Kaç?");
            messages[2].Add("Buluşalım mı?");
            messages[2].Add("Kaçta?");
            ListView listView = (ListView)FindViewById(Resource.Id.listView);
     
            adapter = new PersonAdapter(this,persons);
            
            listView.Adapter = adapter;
            


            listView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                Person person = persons[e.Position];
                var intent = new Intent(this, typeof(ChatActivity));
                intent.PutExtra("name", person.Name);
                intent.PutExtra("id", person.Id);
                this.StartActivity(intent);
            };



            FloatingActionButton fab = (FloatingActionButton)FindViewById(Resource.Id.fab);
         
            fab.Click += delegate
            {
                var intent = new Intent(this, typeof(AddContactActivity));
                this.StartActivity(intent);
            };

            if(Intent.GetStringExtra("person") != null)
            {
                Person newPerson = JsonConvert.DeserializeObject<Person>(Intent.GetStringExtra("person"));
                persons.Add(newPerson);
                messages.Add(newPerson.Id, new List<string>());
                adapter.NotifyDataSetChanged();
            }
            
        }

    }
}