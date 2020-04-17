using Android.OS;


namespace BIM494_Assigment_II
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