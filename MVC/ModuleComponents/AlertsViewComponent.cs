using Alerts;
using CMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ModuleComponents
{
    public class AlertsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<AlertInfo> alerts = CacheHelper.GetItem("Alerts_AlertList") as List<AlertInfo>;

            if (alerts == null)
            {
                alerts = AlertInfoProvider.ProviderObject.Get()
                    .Columns("AlertName", "AlertText", "AlertEnabled", "AlertPublishFrom", "AlertPublishTo", "AlertOrder")
                    .WhereTrue("AlertEnabled")
                    .Or(
                        (AlertInfoProvider.ProviderObject.Get().WhereLessOrEquals("AlertPublishFrom", DateTime.Now).Or().WhereEqualsOrNull("AlertPublishFrom", DateTime.MinValue))
                        .And(AlertInfoProvider.ProviderObject.Get().WhereGreaterOrEquals("AlertPublishTo", DateTime.Now).Or().WhereEqualsOrNull("AlertPublishTo", DateTime.MinValue))
                    )
                    .OrderBy("AlertOrder")
                    .ToList();

                string dependencyCacheKey = "alerts.alert|all";
                var dependency = CacheHelper.GetCacheDependency(dependencyCacheKey);

                CacheHelper.Add("Alerts_AlertList", alerts, dependency, DateTime.Now.AddMinutes(5), CacheConstants.NoSlidingExpiration);
            }

            return View(alerts);
        }
    }
}
