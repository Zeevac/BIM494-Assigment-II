using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace BIM494_Assigment_I
{
    [Activity(Label = "RecyclerViewAdapter")]
    public class RecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<string> messages;
        public RecyclerViewAdapter(List<string> messages)
        {
            this.messages = messages;

        }


        public class MyViewHolder : RecyclerView.ViewHolder
        {
            public TextView messageTextView;
            public MyViewHolder(View itemView) : base(itemView)
            {
               
                messageTextView = itemView.FindViewById<TextView>(Resource.Id.recyclerview_list_row_textView);
                
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
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.recyclerview_list_row,parent,false);
            return new MyViewHolder(itemView);
        }
        
    }
}