using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Text.Json;
using TouristGuide.Models;
using TouristGuide.Services.Requests;
using Android.Util;
using Xamarin.Essentials;

namespace TouristGuide.Services
{
    public class HttpClientWrapper
    {
        HttpClient client;
        public static List<LocationDto> locationDtos = new List<LocationDto>();
        private static string _locationsUri;

        public HttpClientWrapper()
        {

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            _locationsUri = Preferences.Get("LocationApi", "http://10.0.2.2:4445");
            client = new HttpClient(httpClientHandler);
        }


        public async Task<IEnumerable<LocationDto>>  getLoctions()
        {
            Uri uri = new Uri($"{_locationsUri}/api/Locations/getLocations");

            if (locationDtos.Count() > 0)
                return locationDtos;
            try
            {
                HttpResponseMessage response;
                response = await client.PostAsync(uri, null, new System.Threading.CancellationToken());


                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    locationDtos = JsonSerializer.Deserialize<List<LocationDto>>(content);
                    return locationDtos;
                }
            }
            catch (Exception ex)
            {
                Log.Debug(GetType().FullName, ex.Message);
            }

            return null;
        }     
        
        public async Task<IEnumerable<LocationDto>>  getLoctionsByDate(DateTime date)
        {
            Uri uri = new Uri($"{_locationsUri}/api/Locations/getLocationsByDate");
            var req = new LocationsDateRequest(date.Ticks);

            var json = JsonSerializer.Serialize(req, req.GetType());

            try
            {
                HttpResponseMessage response;
                response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"), new System.Threading.CancellationToken());

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    locationDtos = JsonSerializer.Deserialize<List<LocationDto>>(content);
                    return locationDtos;
                }

            }
            catch (Exception ex)
            {
                Log.Debug(GetType().FullName, ex.Message);
            }

            return null;
        }
    }


}