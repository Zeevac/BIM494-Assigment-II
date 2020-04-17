using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BIM494_Assigment_II
{
    [Service]
    public class TextCheckerService : Service
    {
        IDictionary<string, string> dict = new Dictionary<string, string>();

        public override void OnCreate()
        {
            base.OnCreate();
            System.Diagnostics.Debug.WriteLine("TextCheckerService: OnCreate");
            dict.Add("asap", "as soon as possible");
            dict.Add("bbl", "be back like");
            dict.Add("omg", "oh my god");
            dict.Add("ttyl", "talk to you later");

        }

        public string Translate(string data)
        {
            System.Diagnostics.Debug.WriteLine("TextCheckerService: Translate");
            string message = data;
            foreach (KeyValuePair<string, string> item in dict)
            {
                message = message.Replace(item.Key, item.Value);
            }
            System.Diagnostics.Debug.WriteLine(message);
            return message;
        }

        public override IBinder OnBind(Intent intent)
        {
            System.Diagnostics.Debug.WriteLine("TextCheckerService: OnBind");
            return new TextCheckerServiceBinder(this);
        }
    }
}