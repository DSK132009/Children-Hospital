using Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.Locations
{
    public class LocationSearchModel
    {
        public LocationSearchModel()
        {
            Locations = new List<LocationInfo>();
            LocationsHourSets = new List<LocationsHourSetInfo>();
            LocationHours = new List<HourInfo>();
        }

        public List<LocationInfo> Locations { get; set; }
        public List<LocationsHourSetInfo> LocationsHourSets { get; set; }
        public List<HourInfo> LocationHours { get; set; }

        public string DefaultFilterOption { get; set; }
    }
}
