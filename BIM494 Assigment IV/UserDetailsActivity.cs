
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace BIM494_Assigment_IV
{
    [Activity(Label = "UserDetailsActivity")]
    public class UserDetailsActivity : AppCompatActivity
    {
        private int UserID;
        private TextView nameTW;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_userDetails);
            if (Intent.Extras != null)
            {
                UserID = Intent.Extras.GetInt("id");
            }
            Title = MainActivity.persons[UserID].Name + "'s Details";
            nameTW = FindViewById<TextView>(Resource.Id.userDatils_activity_name_textView);
            nameTW.Text = MainActivity.persons[UserID].Name;
            ImageView imageViewUserDetails = FindViewById<ImageView>(Resource.Id.userDatils_activity_imageView);
            imageViewUserDetails.SetImageBitmap(BitmapFactory.DecodeByteArray(MainActivity.persons[UserID].Image,0, MainActivity.persons[UserID].Image.Length));
        }
    }
}