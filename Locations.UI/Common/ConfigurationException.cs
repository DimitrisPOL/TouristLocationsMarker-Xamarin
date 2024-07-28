using System;
using Locations.UI.Configuration;

namespace Locations.UI.Common
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException() : base($"No Configuration found for {nameof(ApplicationConfiguration)}") { }
    }
}
