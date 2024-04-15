using CMS.Core;
using CMS.DocumentEngine.Types.UMCHospital;
using CMS.EventLog;
using CMS.SiteProvider;
using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Components.ServiceListing
{
    public class ServiceListingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ComponentViewModel widgetProperties)
        {
            try
            {
                var lstService = ServiceListingProvider.GetServiceListings().ToList();
                foreach (var service in lstService)
                {
                    //Remove leading and ending whitespace characters
                    var previewHeaderTextTrim = service.PreviewHeaderText.Trim();
                    TextInfo text = new CultureInfo(CultureInfo.CurrentCulture.Name, false).TextInfo;
                    //transform preview header text to title case
                    string previewHeaderText = text.ToTitleCase(previewHeaderTextTrim);
                    //save the modifcations in prep for the next step
                    service.SetValue("PreviewHeaderText", previewHeaderText);
                }
                var orderList = lstService.OrderBy(x => x.PreviewHeaderText);

                //get the first element in list and assign the anchor value for use with section jumping on the page
                var service_a_f = orderList.Where(x => x.PreviewHeaderText[0] >= 'A' && x.PreviewHeaderText[0] <= 'F').FirstOrDefault();
                if (service_a_f != null)
                {
                    service_a_f.SetValue("AnchorID", "a-f");
                }

                //get the first Element where the Preview Header Text is in the range of G to L and then assign the anchor Id value for use page section jumping
                var service_g_l = orderList.Where(x => x.PreviewHeaderText[0] >= 'G' && x.PreviewHeaderText[0] <= 'L').FirstOrDefault();
                if (service_g_l != null)
                {
                    service_g_l.SetValue("AnchorID", "g-l");
                }

                var service_m_p = orderList.Where(x => x.PreviewHeaderText[0] >= 'M' && x.PreviewHeaderText[0] <= 'P').FirstOrDefault();
                if (service_m_p != null)
                {
                    service_m_p.SetValue("AnchorID", "m-p");
                }

                var service_q_z = orderList.Where(x => x.PreviewHeaderText[0] >= 'Q' && x.PreviewHeaderText[0] <= 'Z').FirstOrDefault();
                if (service_q_z != null)
                {
                    service_q_z.SetValue("AnchorID", "q-z");
                }

                return View(orderList);
            }
            catch (Exception ex)
            {
                //get current siteID
                int siteId = SiteContext.CurrentSiteID;
                var eventLog = Service.Resolve<IEventLogService>();
                // Logs an exception event into the event log
                eventLog.LogException("ServiceListingViewComponent.cs", "Exception", ex, siteId, "Service is null", LoggingPolicy.DEFAULT);
                return View();

            }

        }
    }
}
