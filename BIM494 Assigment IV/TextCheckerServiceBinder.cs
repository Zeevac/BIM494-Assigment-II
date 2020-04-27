using Android.OS;


namespace BIM494_Assigment_IV
{
    public class TextCheckerServiceBinder : Binder
    {
        public TextCheckerService Service { get; private set; }

        public TextCheckerServiceBinder(TextCheckerService service)
        {
            this.Service = service;
        }
    }
}