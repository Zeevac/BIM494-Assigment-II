using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace BIM494_Assigment_II
{
    [Activity(Label = "RecyclerViewAdapter")]
    public class RecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<string> messages;
        private string name;
        public RecyclerViewAdapter(List<string> messages, string name)
        {
            this.messages = messages;
            this.name = name;
        }


        public class MyViewHolder : RecyclerView.ViewHolder
        {
            public TextView messageTextView;
            public MyViewHolder(View itemView) : base(itemView)
            {

                messageTextView = itemView.FindViewById<TextView>(Resource.Id.recyclerview_list_row_message);

            }
        }
        public override int ItemCount => messages.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            string chatMessage = messages[position];
            MyViewHolder myViewHolder = holder as MyViewHolder;
            myViewHolder.messageTextView.Text = chatMessage;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.recyclerview_list_row, parent, false);
            return new MyViewHolder(itemView);
        }

    }
}