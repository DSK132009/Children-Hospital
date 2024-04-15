using CMS.Helpers;
using CMS.Membership;
using CMS.Search;
using Kentico.PageBuilder.Web.Mvc;
using Locations;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Locations;
using MVC.Models.Search;
using MVC.Models.Widgets.Locations;
using MVC.Models.Widgets.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewComponents
{
    public class LocationSearchViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ComponentViewModel<LocationSearchProperties> widgetProperties)
        {
            // Displays the search page without any search results if the query is empty
            var allLocations = LocationInfoProvider.ProviderObject.Get().ToList();
            var allLocationHourSets = LocationsHourSetInfoProvider.ProviderObject.Get().ToList();
            var allHours = HourInfoProvider.ProviderObject.Get().ToList();

            LocationSearchModel locationSearchModel = new LocationSearchModel();
            locationSearchModel.LocationsHourSets = allLocationHourSets;
            locationSearchModel.Locations = allLocations;
            locationSearchModel.LocationHours = allHours;
            locationSearchModel.DefaultFilterOption = widgetProperties.Properties.DefaultFilterOption;

            return View(locationSearchModel);
        }
    }
}
