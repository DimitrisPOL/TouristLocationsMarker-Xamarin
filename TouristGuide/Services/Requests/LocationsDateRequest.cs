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

namespace TouristGuide.Services.Requests
{
    public class LocationsDateRequest
    {
        public LocationsDateRequest(long date)
        {
            this.Ticks = date;
        }

        public long Ticks { get; set; }
    }
}