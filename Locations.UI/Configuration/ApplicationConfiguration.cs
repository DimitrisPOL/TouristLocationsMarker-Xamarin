namespace Locations.UI.Configuration
{
    public static class ApplicationConfiguration
    {

        public static MapCenter MapCenter;
    }

    public class MapCenter
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
    }
}
