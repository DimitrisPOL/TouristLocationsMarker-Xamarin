using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Common.Util;
using Android.Gms.Location;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using TouristGuide.Models;
using TouristGuide.Services;
using Xamarin.Essentials;
using static Android.App.ActionBar;
using static Android.Gms.Maps.GoogleMap;

namespace TouristGuide
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MapsActivity : Activity, IOnMapReadyCallback, IOnMarkerClickListener
    {
        private static string _locationsUri;
        public string _filePath;
        public int _mapZoom;
        public int _mapTilt;
        public int _mapBearing;
        FusedLocationProviderCallback _callback;
        public Android.Locations.Location _currentLocation;
        public LatLng _preferedStartLocation;
        public List<LocationDto> _locationsDto;
        public GoogleMap _sightsSeeingGoogleMap;
        FusedLocationProviderClient _fusedLocationProviderClient;
        HttpClientWrapper _wrapper = new HttpClientWrapper();
        WebClient webClient = new WebClient();
        public DateTime _lastMarkersRequested;

        public MapsActivity()
        {
            _locationsUri =  Preferences.Get("LocationApi", "http://10.0.2.2:4445");
            var Lang = Preferences.Get("PreferedStartLocationLang", "0");
            var Lng = Preferences.Get("PreferedStartLocationLng", "0");
            _mapZoom = Preferences.Get("MapZoom", 15);
            _mapTilt = Preferences.Get("MapTilt", 65);
            _mapBearing = Preferences.Get("MapBearing", 155);
           _preferedStartLocation = new LatLng(double.Parse( Lang), double.Parse( Lng));
        }



        protected  async override void OnCreate(Bundle savedInstanceState)
        {
            savedInstanceState = Intent.Extras;
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.maps_main);

            FindViewById<Button>(Resource.Id.button1).Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("name", savedInstanceState);
                StartActivity(intent);
            };

      
           var btnClose = FindViewById<Button>(Resource.Id.closeCard);
             btnClose.Click += BtnStop_Click;   
            
            var btnPlay = FindViewById<Button>(Resource.Id.playSong);
            btnPlay.Click += BtnPlay_Click;

            _lastMarkersRequested = DateTime.Now;


            var LocationsJson =  savedInstanceState.GetString("locations");

            if(LocationsJson != null)
                _locationsDto = JsonSerializer.Deserialize<List<LocationDto>>(LocationsJson);

            if (_locationsDto == null)
            {                
                try
                {
                    _locationsDto = (List<LocationDto>)await _wrapper.getLoctions();
                }
                catch(Exception ex) 
                {
                    Log.Debug(GetType().FullName, ex.Message);
                }
            }

            _fusedLocationProviderClient = new FusedLocationProviderClient(this);

           _currentLocation = await _fusedLocationProviderClient.GetLastLocationAsync();

            if(StateManager.locationRequest == null)
            {
                _callback = new FusedLocationProviderCallback(this);

                var isInstalled = IsGooglePlayServicesInstalled();

                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Permission.Granted)
                {
                    LocationRequest locationRequest = new LocationRequest()
                                          .SetPriority(LocationRequest.PriorityHighAccuracy)
                                          .SetInterval(60 * 1000 * 1)
                                          .SetFastestInterval(60 * 1000 * 1);


                    _fusedLocationProviderClient = new FusedLocationProviderClient(this);

                    await _fusedLocationProviderClient.RequestLocationUpdatesAsync(locationRequest, _callback);
                   
                    StateManager.locationRequest = locationRequest;

                }
                else
                {
                    // The app does not have permission ACCESS_FINE_LOCATION 
                }
            }

            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

     
        }

        private void BtnStop_Click(object sender, System.EventArgs e)
        {
            var cardview = FindViewById<CardView>(Resource.Id.cardView1);
            var btnClose = FindViewById<Button>(Resource.Id.closeCard);
            var btnPlay = FindViewById<Button>(Resource.Id.playSong);
            cardview.Visibility = ViewStates.Gone;
            btnClose.Visibility = ViewStates.Gone;
            btnPlay.Visibility = ViewStates.Gone;
        }

        private void BtnPlay_Click(object sender, System.EventArgs e)
        {
            if(_filePath != null)
                 PlaySong(_filePath);
        }

        //Save state before sleeping
        protected async override void OnSaveInstanceState(Bundle outState)
        {
            var LocationsJson = JsonSerializer.Serialize<List<LocationDto>>(_locationsDto);
            outState.PutString("locations", LocationsJson);
            Log.Debug(GetType().FullName, "Activity A - Saving instance state");

            // always call the base implementation!
            base.OnSaveInstanceState(outState);
        }
        
        //Restore state on resume
        protected override void OnRestoreInstanceState(Bundle outState)
        {
       
            var LocationsJson =  outState.GetString("locations");

            _locationsDto = JsonSerializer.Deserialize<List<LocationDto>>(LocationsJson);
            // always call the base implementation!
            base.OnRestoreInstanceState(outState);
        }

        protected override async void OnDestroy()
        {
            base.OnDestroy();
            try
            {
                await _fusedLocationProviderClient.RemoveLocationUpdatesAsync(_callback);
            }

            catch (Exception ex)
            {
                Log.Debug(GetType().FullName, ex.Message);
            }

            StateManager.locationRequest = null;
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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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

        public void OnMapReady(GoogleMap googleMap)
        {
            _sightsSeeingGoogleMap = googleMap;

            if (_currentLocation == null)
                return;

            ConfigureGoogleMap(googleMap);
        }

        public void ConfigureGoogleMap(GoogleMap googleMap)
        {            
            if(_preferedStartLocation.Latitude == default || _preferedStartLocation.Longitude == default) {
                _preferedStartLocation = new LatLng(_currentLocation.Latitude, _currentLocation.Longitude);
            }

            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(_preferedStartLocation);
            builder.Zoom(_mapZoom);
            builder.Bearing(_mapBearing);
            builder.Tilt(_mapTilt);

            CameraPosition cameraPosition = builder.Build();

            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            googleMap.MoveCamera(cameraUpdate);

            googleMap.SetOnMarkerClickListener(this);
            MarkerOptions currentLocationMarker = new MarkerOptions();
            currentLocationMarker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.geen_figure));
            currentLocationMarker.SetPosition(_preferedStartLocation);
            currentLocationMarker.SetTitle("starting point");

            googleMap.AddMarker(currentLocationMarker);

            if (_locationsDto != null)
            {
                foreach (LocationDto loc in _locationsDto)
                {
                    double.TryParse(loc.Coordinates.Lat.Replace(',', '.'),out var lat);
                    double.TryParse(loc.Coordinates.Lng.Replace(',', '.'), out var lng);

                    if(lat!= 0 && lng!= 0)
                    {
                        MarkerOptions locationMarker = new MarkerOptions();
                        locationMarker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.greek_icon));
                        locationMarker.SetPosition(new LatLng(lat, lng));
                        locationMarker.SetTitle(loc.LocationName);
                        locationMarker.SetSnippet(loc.LocationName);

                        googleMap.AddMarker(locationMarker);
                    }
                }
            }
        }

        public bool OnMarkerClick(Marker marker)
        {
            LocationDto loc = null;
            if(_currentLocation != null && marker != null)
                loc = _locationsDto.Where(location => location.LocationName == marker.Title).FirstOrDefault();

            var textview = FindViewById<TextView>(Resource.Id.textView1);
            var imageView = FindViewById<ImageView>(Resource.Id.imageView1);

            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                try
                {
                    if(loc != null)
                    {
                        var imageBytes = webClient.DownloadData(System.IO.Path.Combine(_locationsUri, loc.ImagePath));
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Debug(GetType().FullName, ex.Message);
                }

            }

            imageView.SetImageBitmap(imageBitmap);


            var cardview = FindViewById<CardView>(Resource.Id.cardView1);
            var btnClose  = FindViewById<Button>(Resource.Id.closeCard);
             var btnPlay  = FindViewById<Button>(Resource.Id.playSong);


            cardview.Visibility = ViewStates.Visible;
            btnClose.Visibility = ViewStates.Visible;
            btnPlay.Visibility = ViewStates.Visible;

            textview.Text = loc?.LocationDesc;
            _filePath = loc?.FilePath;
            //PlaySong(loc);
            return true;
        }

        public async Task PlaySong(string filePath)
        {
            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filePath.Replace("\\", ""));
            var exists = File.Exists(backingFile);
            Context context = Android.App.Application.Context;
            if (!exists)
            {            
                var dataArray = await webClient.DownloadDataTaskAsync(new Uri(System.IO.Path.Combine(_locationsUri, filePath)));
                backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic), filePath);

                using (var writer = File.Create(backingFile))
                {
                    writer.Write(dataArray);
                }
            }

            using (FileStream fs = File.Open(backingFile, FileMode.Open))
            {
                var uri = Android.Net.Uri.Parse(backingFile);
                var assets = context.Assets;
                var player = new MediaPlayer();
                player.SetDataSource(backingFile);
                player.Prepare();
                player.Start();
            }

        }

        protected override void OnStop()
        {
            base.OnStop();
        }

    }

}
