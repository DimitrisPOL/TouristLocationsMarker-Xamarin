using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TouristGuide.Models
{
    [Serializable]
    public class LocationDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("locationName")]
        public string LocationName { get; set; }
        [JsonPropertyName("locationDesc")]
        public string LocationDesc{ get; set; }
        [JsonPropertyName("area")]
        public Area Area { get; set; }
        [JsonPropertyName("coordinates")]
        public Coordinates Coordinates { get; set; }
        [JsonPropertyName("filePath")]
        public string FilePath { get; set; }  
        [JsonPropertyName("imagePath")]
        public string ImagePath { get; set; }  
        [JsonPropertyName("meterToNotify")]
        public int MeterToNotify { get; set; }
        public LocationDto()
        {

        }
    }

    [Serializable]
    public class Area
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("areaName")]
        public string AreaName { get; set; }
        [JsonPropertyName("areaCode")]
        public int AreaCode { get; set; }
    }

    [Serializable]
    public class Coordinates
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("lat")]
        public string Lat { get; set; }
        [JsonPropertyName("lng")]
        public string Lng { get; set; }
    }
}