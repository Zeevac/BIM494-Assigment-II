using Android.Content;
using Android.OS;

namespace BIM494_Assigment_II
{
    public class TextCheckerServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public TextCheckerService Service { get; private set; }
        public bool IsConnected { get; private set; }
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            System.Diagnostics.Debug.WriteLine("TextCheckerServiceConnection: OnServiceConnected");
            TextCheckerServiceBinder tsBinder = service as TextCheckerServiceBinder;
            IsConnected = tsBinder != null;
            System.Diagnostics.Debug.WriteLine("TextCheckerServiceConnection: TextCheckerServiceBinder is null");
            if (tsBinder == null)
                return;

            Service = tsBinder.Service;
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            System.Diagnostics.Debug.WriteLine("TextCheckerServiceConnection: OnServiceDisconnected");
            IsConnected = false;    
            Service = null;
        }
    }
}