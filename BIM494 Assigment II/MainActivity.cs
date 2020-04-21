using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;
using System.Collections.Generic;
using static Android.Widget.AdapterView;

namespace BIM494_Assigment_II
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static List<Person> persons = new List<Person>();
        public static Dictionary<Person, List<Message>> messages = new Dictionary<Person, List<Message>>();
        public PersonAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            StartService(new Intent(this, typeof(ShakeToLaunchService)));
            System.Diagnostics.Debug.WriteLine("MainActivity : OnCreate");
            if (persons.Count == 0)
            {
                Person melis = new Person(0, "Melisnaz", BitmapFactory.DecodeResource(Resources, Resource.Drawable.woman1));
                persons.Add(melis);
                Person orkun = new Person(1, "Orkun", BitmapFactory.DecodeResource(Resources, Resource.Drawable.man2));
                persons.Add(orkun);
                messages[orkun] = new List<Message>();
                messages[melis] = new List<Message>();
                messages[orkun].Add(new Message("Selam",null, orkun, false));
                messages[melis].Add(new Message("Naber", null,melis, false));
                messages[melis].Add(new Message("Nerdesin",null,melis, false));
            }

                ListView listView = (ListView)FindViewById(Resource.Id.listView);

                adapter = new PersonAdapter(this, persons);

                listView.Adapter = adapter;



                listView.ItemClick += (object sender, ItemClickEventArgs e) =>
                {
                    Person person = persons[e.Position];
                    ChatActivity.currentPerson = person;
                    Intent intent = new Intent(this, typeof(ChatActivity));
                    intent.PutExtra("name", person.Name);
                    intent.PutExtra("id", person.Id);
                    intent.PutExtra("person", JsonConvert.SerializeObject(person));
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
                    messages.Add(newPerson, new List<Message>());
                    adapter.NotifyDataSetChanged();
                }

            
        }

        protected override void OnResume()
        {
            base.OnResume();
            System.Diagnostics.Debug.WriteLine("MainActivity : OnResume");
            adapter.NotifyDataSetChanged();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            System.Diagnostics.Debug.WriteLine("MainActivity : OnDestroy");
            StopService(new Intent(this, typeof(ShakeToLaunchService)));
        }
    }
}