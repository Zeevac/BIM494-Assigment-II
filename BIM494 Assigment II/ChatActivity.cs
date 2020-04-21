using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace BIM494_Assigment_II
{
    [Activity(Label = "ChatActivity", Theme = "@style/MyTheme")]
    public class ChatActivity : AppCompatActivity
    {
        static int REQUEST_IMAGE_CAPTURE = 51521;
        static int REQUEST_LOCATION = 23231;
        private EditText ChatActivityMessageEditText;
        private ProgressBar pb;
        private Button ChatActivitySendButton, ChatActivityCameraButton;
        private int id;
        public static Person currentPerson;
        private LocationServiceConnection lsConnection;
        private CurrencyServiceConnection csConnection;
        private TextCheckerServiceConnection tsConnection;
        IDictionary<string, string> dict = new Dictionary<string, string>();
        NotificationManager notificationManager;
        private RecyclerViewAdapter adapter;
        private RecyclerView recylerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            dict.Add("asap", "as soon as possible");
            dict.Add("bbl", "be back like");
            dict.Add("omg", "oh my god");
            dict.Add("ttyl", "talk to you later");
            SetContentView(Resource.Layout.activity_chat);
            Android.Support.V7.Widget.Toolbar toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            string name = Intent.Extras.GetString("name");
            id = Intent.Extras.GetInt("id");
            Title = name;
            pb = FindViewById<ProgressBar>(Resource.Id.pb);
            pb.Max = 10;
            pb.Progress = 0;
            ImageView actionBarImageView = FindViewById<ImageView>(Resource.Id.conversation_contact_photo);
            TextView actionBarNameTW = FindViewById<TextView>(Resource.Id.action_bar_title_1);
            actionBarImageView.SetImageBitmap(MainActivity.persons[id].Image);
            actionBarNameTW.Text = MainActivity.persons[id].Name;
            ChatActivityMessageEditText = FindViewById<EditText>(Resource.Id.chat_activity_message_editText);
            //adapter = new MessageAdapter(ApplicationContext, MainActivity.messages[currentPerson]);
            //listView = FindViewById<ListView>(Resource.Id.listView2);
            //listView.Adapter = adapter;
            ChatActivitySendButton = FindViewById<Button>(Resource.Id.ChatActivitySendButton);
            ChatActivityCameraButton = FindViewById<Button>(Resource.Id.ChatActivityCameraButton);
            ChatActivitySendButton.Click += OnSendButtonClicked;
            ChatActivityCameraButton.Click += OnCameraButtonClicked;
            lsConnection = new LocationServiceConnection();
            csConnection = new CurrencyServiceConnection();
            tsConnection = new TextCheckerServiceConnection();
            lsConnection.ServiceConnectionChanged += ServiceConnectionChanged;
            csConnection.CurrencyServiceConnectionChanged += CurrencyServiceConnectionChanged;
            var intent = new Intent(this, typeof(TextCheckerService));
            BindService(intent, tsConnection, Bind.AutoCreate);
            notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            recylerView = FindViewById<RecyclerView>(Resource.Id.recylerview);
            adapter = new RecyclerViewAdapter(this, MainActivity.messages[currentPerson]);
            recylerView.SetAdapter(adapter);
            recylerView.SetLayoutManager(new LinearLayoutManager(this));
        }

        protected async void CurrencyServiceConnectionChanged(object sender, bool e)
        {
            string s = await csConnection.Service.DownloadCurrency();
            s = string.Format("{0:.##}", Convert.ToDecimal(s));
            MainActivity.messages[currentPerson].Add(new Message(("1$ = " + s + "₺"),null, currentPerson, true));
            //listView.SetSelection(adapter.Count - 1);
            adapter.NotifyDataSetChanged();
            recylerView.ScrollToPosition(MainActivity.messages[currentPerson].Count - 1);
            UnbindService(csConnection);
        }

        void ServiceConnectionChanged(object sender, bool isConnected)
        {
            if (lsConnection.Service == null)
                return;

            if (isConnected)
            {
                lsConnection.Service.LocationChanged += LocationChanged;
            }
            else
            {
                lsConnection.Service.LocationChanged -= LocationChanged;
            }
        }



        private void OnCameraButtonClicked(object sender, EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == (int)Permission.Granted)
            {
                Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);
                if (takePictureIntent.ResolveActivity(PackageManager) != null)
                {
                    StartActivityForResult(takePictureIntent, REQUEST_IMAGE_CAPTURE);
                }
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, REQUEST_IMAGE_CAPTURE);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == REQUEST_IMAGE_CAPTURE)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);
                    if (takePictureIntent.ResolveActivity(PackageManager) != null)
                    {
                        StartActivityForResult(takePictureIntent, REQUEST_IMAGE_CAPTURE);
                    }
                }
            }
            else if (requestCode == REQUEST_LOCATION)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    var intent = new Intent(this, typeof(LocationService));
                    BindService(intent, lsConnection, Bind.AutoCreate);

                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        public void OnSendButtonClicked(object sender, EventArgs e)
        {
            if (ChatActivityMessageEditText.Text != "")
            {
                Message message = new Message(ChatActivityMessageEditText.Text,null, currentPerson, true);
                foreach (KeyValuePair<string, string> item in dict)
                {
                    if (message.GetText().Contains(item.Key))
                    {
                        message.SetText(tsConnection.Service.Translate(message.GetText()));
                        break;
                    }
                }
                if (dict.ContainsKey(ChatActivityMessageEditText.Text))
                {


                }
                MainActivity.messages[currentPerson].Add(message);
                ChatActivityMessageEditText.Text = "";
                adapter.NotifyDataSetChanged();
                recylerView.ScrollToPosition(MainActivity.messages[currentPerson].Count - 1);
                notificationManager.Notify(0, GetNotification(message.GetText()));
                RunOnUiThread(() =>
                {
                    pb.IncrementProgressBy(1);
                    if (pb.Progress >= 10)
                    {
                        Toast.MakeText(this, "You've reached the message limit.", ToastLength.Long).Show();
                        ChatActivitySendButton.Clickable = false;
                    }
                });
            }
        }

        void LocationChanged(object sender, Android.Locations.LocationChangedEventArgs e)
        {
            var location = e.Location;
            string s = "Latitude: " + location.Latitude.ToString() + System.Environment.NewLine +
                "Longitude: " + location.Longitude.ToString() + System.Environment.NewLine +
                "Altitude: " + location.Altitude.ToString() + System.Environment.NewLine +
                "Speed: " + location.Speed.ToString();
            Message message = new Message(s,null ,currentPerson, true);
            MainActivity.messages[currentPerson].Add(message);
            //listView.SetSelection(adapter.Count - 1);
            adapter.NotifyDataSetChanged();
            recylerView.ScrollToPosition(MainActivity.messages[currentPerson].Count - 1);
            UnbindService(lsConnection);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.details_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.action_details)
            {
                Intent intent = new Intent(ApplicationContext, typeof(UserDetailsActivity));
                intent.PutExtra("id", id);
                StartActivity(intent);
            }
            else if (item.ItemId == Resource.Id.action_location)
            {
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Permission.Granted)
                {
                    var intent = new Intent(this, typeof(LocationService));
                    BindService(intent, lsConnection, Bind.AutoCreate);

                }
                else
                {
                    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessFineLocation }, REQUEST_LOCATION);
                }

            }
            else if (item.ItemId == Resource.Id.action_currency)
            {
                var intent = new Intent(this, typeof(CurrencyService));
                BindService(intent, csConnection, Bind.AutoCreate);
            }
            else if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
            }


            return base.OnOptionsItemSelected(item);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == REQUEST_IMAGE_CAPTURE && resultCode == Result.Ok)
            {
                Bundle extras = data.Extras;
                Bitmap imageBitmap = (Bitmap)extras.Get("data");
                Message message = new Message("",imageBitmap, currentPerson, true);
                MainActivity.messages[currentPerson].Add(message);
                //listView.SetSelection(adapter.Count - 1);
                adapter.NotifyDataSetChanged();
                recylerView.ScrollToPosition(MainActivity.messages[currentPerson].Count - 1);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnbindService(tsConnection);
        }

        Notification GetNotification(string content)
        {
            string tag = currentPerson.Name;
            Intent intent = new Intent(this, typeof(ChatActivity));
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            PendingIntentFlags flags = PendingIntentFlags.UpdateCurrent;
            intent.SetAction(Intent.ActionMain);
            intent.AddCategory(Intent.CategoryLauncher);
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, intent, flags);
            // Beginning with API level 26 (Oreo) all notifications must be assigned to a channel (aka: category), otherwise they won't be shown.
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                const string notificationChannelId = "MESSAGE_CHANNEL";
                
                var notificationChannel = new NotificationChannel(notificationChannelId, "Message", NotificationImportance.Low);
                notificationManager.CreateNotificationChannel(notificationChannel);

                return new NotificationCompat.Builder(this, notificationChannelId)
                .SetContentTitle(tag)
                .SetAutoCancel(true)
                .SetContentText(content)
                .SetSmallIcon(Resource.Drawable.whatsapp_16px)
                .SetContentIntent(pendingIntent)
                .Build();
            }
            else
            {
                // Running on a device older than Oreo.
                return new NotificationCompat.Builder(this)
                .SetContentTitle(tag)
                .SetAutoCancel(true)
                .SetContentText(content)
                .SetSmallIcon(Resource.Drawable.whatsapp_16px)
                .SetContentIntent(pendingIntent)
                .Build();
            }
        }
    }
}