using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.Models.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationDesc { get; set; }
        public string StreetAddress { get; set; }
        public Area Area { get; set; }
        public Coordinates Coordinates { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FilePath { get; set; }
        public string ImagePath { get; set; }
        public int MeterToNotify { get; set; }
    }

    public class Area
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public int AreaCode { get; set; }
    }

    public class Coordinates
    {
        public int Id { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
    }
}
