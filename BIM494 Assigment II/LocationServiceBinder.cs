using System;
using Android.OS;
using Android.Locations;
using Android.Runtime;

namespace BIM494_Assigment_II
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