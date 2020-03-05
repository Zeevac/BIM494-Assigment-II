using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using System.Collections.Generic;
using static Android.Widget.AdapterView;

namespace BIM494_Assigment_I
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static List<Person> persons = new List<Person>();
        public static Dictionary<int, List<string>> messages = new Dictionary<int, List<string>>();
        private PersonAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            if (persons.Count == 0)
            {
                persons.Add(new Person(0, "Safa", BitmapFactory.DecodeResource(Resources, Resource.Drawable.person)));
                persons.Add(new Person(1, "Melis", BitmapFactory.DecodeResource(Resources, Resource.Drawable.person)));
                persons.Add(new Person(2, "Orkun", BitmapFactory.DecodeResource(Resources, Resource.Drawable.person)));
                messages[0] = new List<string>();
                messages[1] = new List<string>();
                messages[2] = new List<string>();
                messages[0].Add("Naber?");
                messages[0].Add("Nasılsın?");
                messages[1].Add("Nerdesin?");
                messages[1].Add("Saat Kaç?");
                messages[2].Add("Buluşalım mı?");
                messages[2].Add("Kaçta?");
            }

            ListView listView = (ListView)FindViewById(Resource.Id.listView);

            adapter = new PersonAdapter(this, persons);

            listView.Adapter = adapter;



            listView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                Person person = persons[e.Position];
                Intent intent = new Intent(this, typeof(ChatActivity));
                intent.PutExtra("name", person.Name);
                intent.PutExtra("id", person.Id);
                StartActivity(intent);
            };



            FloatingActionButton fab = (FloatingActionButton)FindViewById(Resource.Id.fab);

            fab.Click += delegate
            {
                Intent intent = new Intent(this, typeof(AddContactActivity));
                StartActivity(intent);
            };

            if (Intent.GetStringExtra("personName") != null)
            {
                int personID = Intent.GetIntExtra("personID", 0);
                string personName = Intent.GetStringExtra("personName");
                string personSurname = Intent.GetStringExtra("personSurname");
                string personImagePath = Intent.GetStringExtra("personImagePath");
                Bitmap personImage = BitmapFactory.DecodeFile(personImagePath);
                Person newPerson = new Person(personID, personName + " " + personSurname, personImage);
                persons.Add(newPerson);
                messages.Add(newPerson.Id, new List<string>());
                adapter.NotifyDataSetChanged();
            }

        }

        protected override void OnResume()
        {
            base.OnResume();
            adapter.NotifyDataSetChanged();
        }
    }
}