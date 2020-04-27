
using Android.App;
using Android.Content;
using System.Threading.Tasks;
using Java.Lang;
using Org.Apache.Http.Client;
using Org.Apache.Http.Impl.Client;
using Org.Apache.Http.Client.Methods;
using Org.Apache.Http;
using Java.IO;
using Org.Json;
using Android.OS;

namespace BIM494_Assigment_IV
{
	[Service]
	public class CurrencyService : Service
	{

		public override void OnCreate()
		{
			base.OnCreate();
            
            
        }

        public async Task<string> DownloadCurrency()
        {
            string s;
            string r = "";
            await Task.Factory.StartNew(() =>
            {
                try
                {

                    s = GetJson("https://api.exchangeratesapi.io/latest?base=USD");
                    JSONObject jObj = new JSONObject(s);
                    r = jObj.GetJSONObject("rates").GetString("TRY");
                    System.Console.WriteLine("burada");
          

                }
                catch (JSONException e)
                {

                }
                catch (IOException e)
                {

                }
            });
            return r;
        }

		public override IBinder OnBind(Intent intent)
		{
            return new CurrencyServiceBinder(this);
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