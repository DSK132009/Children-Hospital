using CMS.Base.Web.UI;
using CMS.PortalEngine.Web.UI;
using System;
using CMS;
using System.Linq;

using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.EventLog;
using CMS.DataEngine;
using Locations.Locations.Extenders;
using CMS.Core;

[assembly: RegisterCustomClass("LocationsOnlyFormExtender", typeof(LocationsFormExtender))]
namespace Locations.Locations.Extenders
{
    public class LocationsFormExtender : ControlExtender<UIForm>
    {
        public override void OnInit()
        {
            Control.OnBeforeSave += Control_OnBeforeSave;
        }

        private void Control_OnBeforeSave(object sender, EventArgs e)
        {
            var location = Control.EditedObject as LocationInfo;
            if (location != null && location.DataChanged("LocationName, ThumbnailImage, IsOpen, PhoneNumber "))
            {
                var success = GeocodeLocation(location);
                if (!success)
                {
                    var message = "The location was saved successfully," +
                        " but we were unable to retrieve the lattitude and longitude for the location." +
                        " This may result in the location not showing up properly on the google maps widget." +
                        " Please confirm that the address for this location is accurate and try again.";

                    Control.ShowWarning(message);
                }

                location.GoogleMapsLink = $"https://www.google.com/maps/place/{location.StreetAddress} {location.StreetAddress2},+{location.City},+{location.State}+{location.Zip}/";

            }

        }

        public bool GeocodeLocation(LocationInfo location)
        {

            var apiKey = SettingsKeyInfoProvider.GetValue("GeocodioApiKey");
            var requestUriString = SettingsKeyInfoProvider.GetValue("GeocodioRequestUri");

            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(requestUriString))
            {
                return false;
            }

            var address = string.Format("{0}, {1} {2} {3}", location.StreetAddress, location.City, location.State, location.Zip);


            var requestUri = string.Format(requestUriString, Uri.EscapeDataString(address), apiKey);

            try
            {
                var request = WebRequest.Create(requestUri);
                var response = request.GetResponse();
                using (var sReader = new StreamReader(response.GetResponseStream()))
                {
                    var parsedObject = JObject.Parse(sReader.ReadToEnd());
                    if (parsedObject == null)
                    {
                        return false;
                    }

                    JToken result;
                    if (parsedObject.TryGetValue("results", out result))
                    {
                        if (result != null && result.Any())
                        {
                            var geoLocation = result[0]["location"];
                            var lat = ValidationHelper.GetDecimal(geoLocation["lat"], 0);
                            var lng = ValidationHelper.GetDecimal(geoLocation["lng"], 0);
                            location.Latitude = lat;
                            location.Longitude = lng;
                            return true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {                
                Service.Resolve<IEventLogService>().LogException("LocationsFormExtender.GeocodeLocation", "GEOCODE_FAILED", ex, SiteContext.CurrentSiteID, string.Format("Attempting to geocode location: {0} failed.", location.LocationName));
                return false;
            }
            return false;
        }

    }
}