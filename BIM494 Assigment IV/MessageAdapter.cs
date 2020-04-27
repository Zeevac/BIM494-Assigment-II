using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace BIM494_Assigment_IV
{
    public class MessageAdapter : BaseAdapter
    {
        private LayoutInflater mInflater;
        private List<Message> messages;
        public MessageAdapter(Context context, List<Message> messages)
        {
            mInflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            this.messages = messages;

        }

        public override int Count => messages.Count;

        public override Object GetItem(int position)
        {
            return (Object)messages.ElementAt(position);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            
            MessageViewHolder holder = new MessageViewHolder();
                Message message = messages[position];
                if (message.BelongsToCurrentUser)
                { // this message was sent by us 
                    convertView = mInflater.Inflate(Resource.Layout.my_message, null);
                    holder.messageTextView = (TextView)convertView.FindViewById(Resource.Id.message_body);
                    holder.messageTextView.Text = message.Text;

                }
                else
                { // this message was sent by someone else
                    convertView = mInflater.Inflate(Resource.Layout.their_message, null);
                    holder.messageTextView = (TextView)convertView.FindViewById(Resource.Id.message_body);
                    holder.messageTextView.Text = message.Text;
                }

            return convertView;
        }
    }

    class MessageViewHolder
    {
        public TextView messageTextView;
    }
}