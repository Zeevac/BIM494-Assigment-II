using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using static Android.Widget.AdapterView;

namespace BIM494_Assigment_IV
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static List<Person> persons = new List<Person>();
        //public static Dictionary<Person, List<Message>> messages = new Dictionary<Person, List<Message>>();
        public PersonAdapter adapter;
        private SQLiteConnection conn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            conn = MyConnectionFactory.Instance;
            StartService(new Intent(this, typeof(ShakeToLaunchService)));
            if(persons.Count == 0)
            {
                foreach (var person in conn.Table<Person>())
                {
                    persons.Add(person);
                    //messages[person] = new List<Message>();
                }
            }


            //Person melis = new Person(0, "Melisnaz", BitmapConverter.GetBytesFromBitmap(BitmapFactory.DecodeResource(Resources,Resource.Drawable.woman1)));
            //persons.Add(melis);
            //conn.Insert(melis);
            //Person orkun = new Person(1, "Orkun", BitmapConverter.GetBytesFromBitmap(BitmapFactory.DecodeResource(Resources, Resource.Drawable.man2)));
            //persons.Add(orkun);
            //conn.Insert(orkun);
            //messages[orkun] = new List<Message>();
            //messages[melis] = new List<Message>();
            //messages[orkun].Add(new Message("Selam",null, orkun.Id,  false));
            //messages[melis].Add(new Message("Naber", null,melis.Id, false));
            //messages[melis].Add(new Message("Nerdesin",null,melis.Id, false));
            //conn.Insert(new Message("How are you?", null, Person.GetPerson(persons,"Orkun").Id, false));

            ListView listView = (ListView)FindViewById(Resource.Id.listView);

            adapter = new PersonAdapter(this, persons);

            listView.Adapter = adapter;

            adapter.NotifyDataSetChanged();

            listView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                    Person person = persons[e.Position];
                    ChatActivity.currentPerson = person;
                    Intent intent = new Intent(this, typeof(ChatActivity));
                    intent.PutExtra("person", JsonConvert.SerializeObject(person));
                    StartActivity(intent);
            };



            FloatingActionButton fab = (FloatingActionButton)FindViewById(Resource.Id.fab);

            fab.Click += delegate
            {
                Intent intent = new Intent(this, typeof(AddContactActivity));
                StartActivity(intent);
            };

            /*if (Intent.GetStringExtra("personName") != null)
            {
                int personID = Intent.GetIntExtra("personID", 0);
                string personName = Intent.GetStringExtra("personName");
                string personSurname = Intent.GetStringExtra("personSurname");
                string personImagePath = Intent.GetStringExtra("personImagePath");
                Bitmap personImage = BitmapFactory.DecodeFile(personImagePath);
                Person newPerson = new Person(personID, personName + " " + personSurname, BitmapConverter.GetBytesFromBitmap(personImage));
                persons.Add(newPerson);
                conn.Insert(newPerson);
                //messages.Add(newPerson, new List<Message>());
                adapter.NotifyDataSetChanged();
            }
            */
    
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