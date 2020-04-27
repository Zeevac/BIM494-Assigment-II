using System;
using Android.OS;
using Android.Locations;
using Android.Runtime;

namespace BIM494_Assigment_IV
{
    public class LocationServiceBinder : Binder
    {
        public LocationService Service { get; private set; }

        public LocationServiceBinder(LocationService service)
        {
            this.Service = service;
        }
    }
}