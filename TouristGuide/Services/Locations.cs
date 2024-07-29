using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.Gms.Location;
using Android.Gms.Maps.Model;
using Android.Media;
using Android.Util;
using Android.Widget;
using TouristGuide.Models;
using Xamarin.Essentials;
using TouristGuide.Services;
using System.IO;
using System.Net;
using System.ComponentModel;
using Android;
using Android.Content.Res;
using System.Threading.Tasks;
using Java.Interop;

namespace TouristGuide
{
    public class FusedLocationProviderCallback : LocationCallback
    {
        readonly MapsActivity activity;
        protected MediaPlayer player;
        public static List<LocationDto> Locations = new List<LocationDto>();
        public static Android.Locations.Location LocationNew = default;



        public FusedLocationProviderCallback(MapsActivity activity)
        {
            this.activity = activity;
            this.player = new MediaPlayer();
        }

        public override void OnLocationAvailability(LocationAvailability locationAvailability)
        {
            Log.Debug("FusedLocationProviderSample", "IsLocationAvailable: {0}", locationAvailability.IsLocationAvailable);
        }

        public override async void OnLocationResult(LocationResult result)
        {
            var lo = result.LastLocation;

            if (LocationNew != null && lo.Latitude == LocationNew.Latitude && lo.Longitude == LocationNew.Longitude)
                return;
            else
                LocationNew = lo;

            if (result.Locations.Any())
            {

                if (activity._sightsSeeingGoogleMap == null)
                    return;


                if (activity._currentLocation == null)
                    return;


                if (activity._locationsDto != null)
                {

                    foreach (LocationDto loc in activity._locationsDto)
                    {
                        double.TryParse(loc.Coordinates.Lat, out var lat);
                        double.TryParse(loc.Coordinates.Lng, out var lng);

                        if (lat == 0 || lng == 0)
                        {
                            continue;

                        }

                        var marketLoc = new Location(lat, lng);

                        Location here = new Location(activity._currentLocation.Latitude, activity._currentLocation.Longitude);
                        double kms = Location.CalculateDistance(here, marketLoc, DistanceUnits.Kilometers);
                        if (kms * 1000 < loc.MeterToNotify && loc.FilePath != null)
                        {
                            try
                            {
                                activity.FindViewById<TextView>(Resource.Id.button2).Text = loc.LocationName;

                                if (loc.FilePath != null)
                                    activity.PlaySong(loc.FilePath);

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("ERROR:" + ex.Message);
                            }

                        }
                    }
                }
            }
            else
            {
                // No locations to work with.
            }
        }
    }
}