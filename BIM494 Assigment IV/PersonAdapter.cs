using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace BIM494_Assigment_IV
{
    public class PersonAdapter : BaseAdapter
    {
        private LayoutInflater mInflater;
        private List<Person> personArrayList;
        private SQLiteConnection conn;
        
        public PersonAdapter(Activity activity, List<Person> personArrayList)
        {

            mInflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
            this.personArrayList = personArrayList;
            conn = MyConnectionFactory.Instance;
        }

        public override int Count => personArrayList.Count;

        public override Object GetItem(int position)
        {
            return (Object)personArrayList.ElementAt(position);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            convertView = mInflater.Inflate(Resource.Layout.List_Item, null);
            TextView personName = (TextView)convertView.FindViewById(Resource.Id.name);
            TextView personMessage = (TextView)convertView.FindViewById(Resource.Id.message);
            ImageView personImage = (ImageView)convertView.FindViewById(Resource.Id.imageView);
            Person person = personArrayList.ElementAt(position);
            List<Message> currentPersonMessages = new List<Message>();
            personMessage.Text = "";
            personName.Text = person.Name;
            var list = conn.Query<Message>("SELECT * FROM Messages WHERE SenderID = ?", person.Id);
            foreach (var message in list)
            {
                currentPersonMessages.Add(message);
            }
            if (currentPersonMessages.Count != 0){
                if(currentPersonMessages[currentPersonMessages.Count - 1].Image != null)
                {
                    personMessage.Text = "Image";
                }
                else
                {
                    personMessage.Text = currentPersonMessages[currentPersonMessages.Count - 1].Text;
                }
                
            }
            
            personImage.SetImageBitmap(BitmapFactory.DecodeByteArray(person.Image, 0, person.Image.Length));
            return convertView;
        }
    }
}