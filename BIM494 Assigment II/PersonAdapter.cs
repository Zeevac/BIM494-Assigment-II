using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;
using System.Linq;

namespace BIM494_Assigment_II
{
    public class PersonAdapter : BaseAdapter
    {
        private LayoutInflater mInflater;
        private List<Person> personArrayList;

        public PersonAdapter(Activity activity, List<Person> personArrayList)
        {

            mInflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
            this.personArrayList = personArrayList;
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
            
            personMessage.Text = "";
            personName.Text = person.Name;
            if (MainActivity.messages[person].Count != 0){
                personMessage.Text = MainActivity.messages[person][MainActivity.messages[person].Count-1].GetText();
            }
            
            personImage.SetImageBitmap(person.Image);
            return convertView;
        }
    }
}