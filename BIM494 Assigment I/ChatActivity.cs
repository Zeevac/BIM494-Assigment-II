using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace BIM494_Assigment_I
{
    [Activity(Label = "ChatActivity")]
    public class ChatActivity : AppCompatActivity
    {
        RecyclerView recyclerView;
        RecyclerViewAdapter adapter;
        EditText ChatActivityMessageEditText;
        int id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_chat);
            var name = Intent.Extras.GetString("name");
            id = Intent.Extras.GetInt("id");
            this.Title = name;
            // Create your application here
            
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);
            ChatActivityMessageEditText = FindViewById<EditText>(Resource.Id.chat_activity_message_editText);
            adapter = new RecyclerViewAdapter(MainActivity.messages[id]);
            RecyclerView.LayoutManager layoutManager = new LinearLayoutManager(this.ApplicationContext);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetItemAnimator(new DefaultItemAnimator());
            recyclerView.SetAdapter(adapter);
            
            Button ChatActivitySendButton = FindViewById<Button>(Resource.Id.ChatActivitySendButton);
            ChatActivitySendButton.Click += OnSendButtonClicked;
        }

        private void OnSendButtonClicked(object sender, EventArgs e)
        {

            MainActivity.messages[id].Add(ChatActivityMessageEditText.Text);
            ChatActivityMessageEditText.Text = "";
            adapter.NotifyDataSetChanged();
        }
    }
}