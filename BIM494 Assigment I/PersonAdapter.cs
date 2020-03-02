using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace BIM494_Assigment_I { 
public class PersonAdapter : BaseAdapter
{
    private LayoutInflater mInflater;
    private List<Person> personArrayList;

    public PersonAdapter(Activity activity, List<Person> personArrayList)
    {

        this.mInflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
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
        personName.Text = person.Name;
        personMessage.Text = MainActivity.messages[person.Id][MainActivity.messages[person.Id].Count - 1];
        personImage.SetImageResource(person.ImageId);
        return convertView;
    }
}
}