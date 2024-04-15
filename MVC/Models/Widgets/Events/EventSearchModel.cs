using Events;
using Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.Widgets.Events
{
    public class EventSearchModel
    {
        public string IntroText { get; set; }
        public List<CategoryInfo> Categories { get; set; }
        public List<LocationInfo> Locations { get; set; }
        public List<EventSessionInfo> Events { get; set; }
    }
}
