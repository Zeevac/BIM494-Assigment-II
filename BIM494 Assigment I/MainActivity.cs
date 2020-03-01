using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.Design.Widget;
using Android.Content;
using static Android.Widget.AdapterView;
using System;

namespace BIM494_Assigment_I
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        List<Person> persons = new List<Person>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));
            persons.Add(new Person("Random Name", "Merhaba", Resource.Drawable.image));

            ListView listView = (ListView)FindViewById(Resource.Id.listView);

            PersonAdapter adapter = new PersonAdapter(this,persons);

            listView.Adapter = adapter;



            listView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                Person person = persons[e.Position];
                var intent = new Intent(this, typeof(ChatActivity));
                intent.PutExtra("name", person.Name);
                this.StartActivity(intent);
            };



            FloatingActionButton fab = (FloatingActionButton)FindViewById(Resource.Id.fab);
         
            fab.Click += delegate
            {
                var intent = new Intent(this, typeof(AddContactActivity));
                this.StartActivity(intent);
            };

           
        }
       
    }
}