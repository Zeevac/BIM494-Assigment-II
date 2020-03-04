using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Provider;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Widget;
using Newtonsoft.Json;
using System;

namespace BIM494_Assigment_I
{
    [Activity(Label = "AddContactActivity")]
    public class AddContactActivity : Activity
    {
        private ImageView imageView;
        private Button loadButton;
        private Button addButton;
        private EditText nameEditText, surnameEditText;
        private int index;
        private string filename;
        public static int SELECT_IMAGE = 1001;
        private Drawable drawable;
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
            index = MainActivity.messages.Count;
            Console.WriteLine(index);
            Person newPerson = new Person(index, nameEditText.Text + " " + surnameEditText.Text, drawable.GetHashCode());
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("person", JsonConvert.SerializeObject(newPerson));
            StartActivity(intent);
        }

        private void loadButtonClicked(object sender, EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted)
            {
                Intent = new Intent();
                Intent.SetType("image/*");
                Intent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), SELECT_IMAGE);
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 12);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == SELECT_IMAGE) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                Bitmap bitmap = MediaStore.Images.Media.GetBitmap(ContentResolver, uri);
                imageView.SetImageBitmap(bitmap);
            }
            else
            {
                Toast.MakeText(ApplicationContext, "You haven't picked an image", ToastLength.Short).Show();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == 12)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    Intent = new Intent();
                    Intent.SetType("image/*");
                    Intent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), SELECT_IMAGE);
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }


    }
}