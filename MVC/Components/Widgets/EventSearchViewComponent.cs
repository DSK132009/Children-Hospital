using CMS.Helpers;
using Events;
using Kentico.PageBuilder.Web.Mvc;
using Locations;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Widgets.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Components.Widgets
{
    public class EventSearchViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ComponentViewModel<EventSearchProperties> widgetProperties)
        {
            var model = new EventSearchModel
            {
                IntroText = widgetProperties.Properties.IntroText,
                Categories = CategoryInfo.Provider.Get().OrderBy("CategoryName").ToList(),
                Locations = LocationInfo.Provider.Get().OrderBy("LocationName").ToList()                
            };

            var events = EventSessionInfo.Provider.Get();

            DateTime dateFrom = DateTime.Now.AddDays(-1);
            DateTime dateTo = DateTime.MaxValue;

            if (!String.IsNullOrEmpty(widgetProperties.Properties.Category) && widgetProperties.Properties.Category != "all")
            {
                events = events.And().WhereEquals("EventCategory", widgetProperties.Properties.Category);
            }

            if(!String.IsNullOrEmpty(widgetProperties.Properties.Location) && widgetProperties.Properties.Location != "all")
            {
                events = events.And().WhereEquals("LocationGuid", widgetProperties.Properties.Location);
            }

            if(!String.IsNullOrEmpty(widgetProperties.Properties.DateFrom))
            {
                dateFrom = ValidationHelper.GetDateTime(widgetProperties.Properties.DateFrom, DateTime.MinValue, System.Globalization.CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(widgetProperties.Properties.DateTo))
            {
                dateTo = ValidationHelper.GetDateTime(widgetProperties.Properties.DateTo, DateTime.MaxValue, System.Globalization.CultureInfo.InvariantCulture);
            }

            if(!String.IsNullOrEmpty(widgetProperties.Properties.Option))
            {
                if (!widgetProperties.Properties.Option.Contains("all"))
                {
                    var options = widgetProperties.Properties.Option.Split('|');
                    
                    if(options.Count() > 1)
                    {
                        events = events.And(EventSessionInfo.Provider.Get().WhereEquals("EventType", options.First()).Or().WhereEquals("EventType", options.Last()));
                    }
                    else
                    {
                        events = events.WhereEquals("EventType", options.First());
                    }
                }
            }

            if (!String.IsNullOrEmpty(widgetProperties.Properties.SearchText))
            {
                events = events.And(EventSessionInfo.Provider.Get().WhereContains("EventName", widgetProperties.Properties.SearchText).Or().WhereContains("Description", widgetProperties.Properties.SearchText));
            }

            if (String.IsNullOrEmpty(widgetProperties.Properties.DateFrom) && String.IsNullOrEmpty(widgetProperties.Properties.DateTo))
            {
                events = events.WhereGreaterOrEquals("EventEnd", dateFrom);
            }
            else
            {
                events = events.WhereGreaterOrEquals("EventStart", dateFrom);
                events = events.WhereLessOrEquals("EventStart", dateTo);
            }

            model.Events = events.OrderBy("EventStart").ToList();

            return View(model);
        }
    }
}
