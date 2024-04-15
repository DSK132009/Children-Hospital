using CMS.DataEngine;
using CMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class RobotsTxtController : Controller
    {
        public ContentResult Index()
        {
            var robotsContent = CacheHelper.GetItem("GlobalCache_RobotsTxtContent") as string;

            if (String.IsNullOrEmpty(robotsContent))
            {
                robotsContent = SettingsKeyInfoProvider.GetValue("Global_RobotsTxtValue");

                var dependencyKey = "cms.settingskey|byname|robotstxtvalue";
                var dependency = CacheHelper.GetCacheDependency(dependencyKey);

                CacheHelper.Add("GlobalCache_RobotsTxtContent", robotsContent, dependency, DateTime.Now.AddHours(12), CacheConstants.NoSlidingExpiration);
            }


            return Content(robotsContent, "text/plain", Encoding.UTF8);
        }
    }
}
