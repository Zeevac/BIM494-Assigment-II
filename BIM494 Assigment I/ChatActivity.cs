using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace BIM494_Assigment_I
{
    [Activity(Label = "ChatActivity")]
    public class ChatActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var name = Intent.Extras.GetString("name");
            this.Title = name;
            // Create your application here
        }

    }
}