using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace BIM494_Assigment_IV
{
    [Activity(Label = "RecyclerViewAdapter")]
    public class RecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<Message> messages;
        private Context context;
        private const int VIEW_TYPE_MESSAGE_SENT = 1;
        private const int VIEW_TYPE_IMAGE_SENT = 2;
        private const int VIEW_TYPE_MESSAGE_RECEIVED = 3;
        public RecyclerViewAdapter(Context context,List<Message> messages)
        {
            this.messages = messages;
            this.context = context;
        }

        public override int GetItemViewType(int position)
        {
            Message message = messages[position];
            if (!message.BelongsToCurrentUser)
            {
                return VIEW_TYPE_MESSAGE_RECEIVED;
            }
            else
            {
                if (message.Image == null)
                {
                    // If the current user is the sender of the message
                    return VIEW_TYPE_MESSAGE_SENT;
                }
                else
                {
                    // If some other user sent the message
                    return VIEW_TYPE_IMAGE_SENT;
                }
            }
            
        }

    
        public override int ItemCount => messages.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Message message = messages[position];

            switch (holder.ItemViewType)
            {
                case VIEW_TYPE_MESSAGE_SENT:
                    ((SentMessageHolder)holder).bind(message);
                    break;
                case VIEW_TYPE_IMAGE_SENT:
                    ((ImageMessageHolder)holder).bind(message);
                    break;
                case VIEW_TYPE_MESSAGE_RECEIVED:
                    ((ReceivedMessageHolder)holder).bind(message);
                    break;

            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view;

            if (viewType == VIEW_TYPE_MESSAGE_SENT)
            {
                view = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.my_message, parent, false);
                return new SentMessageHolder(view);
            }
            else if (viewType == VIEW_TYPE_IMAGE_SENT)
            {
                view = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.my_image_message, parent, false);
                return new ImageMessageHolder(view);
            }
            else
            {
                view = LayoutInflater.From(parent.Context)
                                .Inflate(Resource.Layout.their_message, parent, false);
                return new ReceivedMessageHolder(view);
            }

            return null;
        }

    }

    public class ImageMessageHolder : RecyclerView.ViewHolder
    {
        ImageView image;

        public ImageMessageHolder(View itemView) : base(itemView) {

            image = (ImageView)itemView.FindViewById(Resource.Id.imageView);
        }

        public void bind(Message message) {
            image.SetImageBitmap(BitmapFactory.DecodeByteArray(message.Image,0,message.Image.Length));
        }
    }


    public class SentMessageHolder : RecyclerView.ViewHolder
    {
        TextView messageTextView;

        public SentMessageHolder(View itemView) : base(itemView)
        {


            messageTextView = (TextView)itemView.FindViewById(Resource.Id.message_body);

        }

        public void bind(Message message)
        {
            messageTextView.Text = message.Text;
        }
    }

    public class ReceivedMessageHolder : RecyclerView.ViewHolder
    {
        TextView messageTextView;

        public ReceivedMessageHolder(View itemView) : base(itemView)
        {


            messageTextView = (TextView)itemView.FindViewById(Resource.Id.message_body);

        }

        public void bind(Message message)
        {
            messageTextView.Text = message.Text;
        }
    }
}