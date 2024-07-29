using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using TouristGuide.Models;
using TouristGuide.Services;
using Xamarin.Essentials;

namespace TouristGuide
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public Android.Locations.Location location;
        public List<LocationDto> locationsDto;
        public GoogleMap sightsSeeingGoogleMap;
        public static Context myMainActivity;

        public MainActivity()
        {
            Preferences.Set("LocationApi", "http://10.0.2.2:4445");
            //If Lang, Lng are not set here the app will use the device's current location
            Preferences.Set("PreferedStartLocationLang", "38.02808135979617");
            Preferences.Set("PreferedStartLocationLng", "23.76540184020999");
            Preferences.Set("MapZoom", 15);
            Preferences.Set("MapTilt", 65);
            Preferences.Set("MapBearing", 155);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            // always call the base implementation!
            base.OnSaveInstanceState(outState);
        }

        protected override void OnRestoreInstanceState(Bundle outState)
        {
            // always call the base implementation!
            base.OnSaveInstanceState(outState);
        }
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            savedInstanceState = new Bundle();
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            CheckAppPermissions();
            myMainActivity = this;

            var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };


            ActivityCompat.RequestPermissions((Activity)myMainActivity, permissions, 0);

            SetContentView(Resource.Layout.main_main);
            FindViewById<Button>(Resource.Id.button1).Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(MapsActivity));
                intent.PutExtra("name", new Bundle());
                StartActivity(intent);
            };

            FindViewById<Button>(Resource.Id.button2).Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(SightsList));
                intent.PutExtra("name", new Bundle());
                StartActivity(intent);
            };
        }

        private void CheckAppPermissions()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                return;
            }
            else
            {
                if (PackageManager.CheckPermission(Manifest.Permission.ReadExternalStorage, PackageName) != Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage, PackageName) != Permission.Granted)
                {
                    var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage, Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation};
                    RequestPermissions(permissions, 1);
                }
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }



        public bool IsGooglePlayServicesInstalled()
        {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info("MainActivity", "Google Play Services is installed on this device.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                // Check if there is a way the user can resolve the issue
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error("MainActivity", "There is a problem with Google Play Services on this device: {0} - {1}",
                          queryResult, errorString);

                // Alternately, display the error to the user.
            }

            return false;
        }

       protected override void OnStop()
        {

            base.OnStop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
