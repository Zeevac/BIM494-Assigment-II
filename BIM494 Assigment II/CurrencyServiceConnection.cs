using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BIM494_Assigment_II
{
    public class CurrencyServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public event EventHandler<bool> CurrencyServiceConnectionChanged;
        public CurrencyService Service { get; private set; }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            CurrencyServiceBinder csBinder = service as CurrencyServiceBinder;

            if (csBinder == null)
                return;

            Service = csBinder.Service;

            CurrencyServiceConnectionChanged?.Invoke(this, true);
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            CurrencyServiceConnectionChanged?.Invoke(this, false);
            Service = null;
        }
    }
}