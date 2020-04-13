using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;
using Java.Lang;
using Org.Apache.Http.Client;
using Org.Apache.Http.Impl.Client;
using Org.Apache.Http.Client.Methods;
using Org.Apache.Http;
using Java.IO;
using Org.Json;

namespace BIM494_Assigment_II
{
	[Service]
	public class CurrencyService : Service
	{

		public override void OnCreate()
		{
			base.OnCreate();
            string s;
            string r = "";
            Task.Run(() =>
            {


                try{

                    s = GetJson("https://api.exchangeratesapi.io/latest?base=USD");
                    JSONObject jObj = new JSONObject(s);
                    r = jObj.GetJSONObject("rates").GetString("TRY");
                     
                    StopSelf();

            }
            catch (JSONException e)
            {

            }
            catch (IOException e)
            {

            }
            });
        }

		public override IBinder OnBind(Intent intent)
		{
            throw new NotImplementedException();
        }


        public string GetJson(string url){
            StringBuilder build = new StringBuilder();
            IHttpClient client = new DefaultHttpClient();
            HttpGet httpGet = new HttpGet(url);
            IHttpResponse response = client.Execute(httpGet);
            IHttpEntity entity = response.Entity;
            System.IO.Stream content = entity.Content;
            BufferedReader reader = new BufferedReader(new InputStreamReader(content));
            string con;
            while ((con = reader.ReadLine()) != null) {
                build.Append(con);
            }
            return build.ToString();
          
        }

        public override void OnDestroy(){

		}
	}
}