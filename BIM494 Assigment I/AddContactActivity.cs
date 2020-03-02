using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;

namespace BIM494_Assigment_I
{
    [Activity(Label = "AddContactActivity")]
    public class AddContactActivity : Activity
    {
        private ImageView imageView;
        private Button loadButton;
        private Button addButton;
        private EditText nameEditText, surnameEditText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_addContact);
            imageView = FindViewById<ImageView>(Resource.Id.load_image_imageView);
            loadButton = FindViewById<Button>(Resource.Id.load_image_button);
            addButton = FindViewById<Button>(Resource.Id.add_contact_button);
            nameEditText = FindViewById<EditText>(Resource.Id.name_editText);
            surnameEditText = FindViewById<EditText>(Resource.Id.surname_editText);
            loadButton.Click += loadButtonClicked;
            addButton.Click += addContactButtonClicked;
            
        }

        private void addContactButtonClicked(object sender, EventArgs e)
        {
            int index = MainActivity.messages.Count;
            Person newPerson = new Person(index, nameEditText.Text + " " + surnameEditText.Text, imageView.Id);
            Intent intent = new Intent(this,typeof(MainActivity));
            intent.PutExtra("person", JsonConvert.SerializeObject(newPerson));
            StartActivity(intent);
        }

        private void loadButtonClicked(object sender, EventArgs e)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), 1);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == 1) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                imageView.SetImageURI(uri);
            }
        }
    }
}