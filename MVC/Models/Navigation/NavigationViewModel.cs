using CMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using CMS.Localization;
using CMS.SiteProvider;

namespace MVC.Models.Navigation
{
    public class NavigationViewModel : IEnumerable<NavigationLinkViewModel>
    {
        public List<NavigationLinkViewModel> Links { get; set; }
        public NavigationViewModel()
        {
            Links = new List<NavigationLinkViewModel>();
        }

        public NavigationViewModel New(string navigationType)
        {
            var menuKey = String.Format("UMCHospital_Menu|{0}", navigationType);

            var cachedMainNav = CacheHelper.Cache(settings => CreateMenu(navigationType, settings), new CacheSettings(4320, menuKey));

            return cachedMainNav;
        }

        private NavigationViewModel CreateMenu(string docType, CacheSettings settings)
        {
            var nav = new NavigationViewModel();
            var navExtensions = new NavigationViewModelExtensions();
            var menu = navExtensions.CreateMenu(nav, docType);

            if (settings.Cached)
            {
                settings.CacheDependency = CacheHelper.GetCacheDependency(new[] { string.Format("nodes|{0}|UMCHospital.navigation|all", SiteContext.CurrentSiteName), string.Format("nodes|{0}|UMCHospital.navigationlink|all", SiteContext.CurrentSiteName) });
            }

            return menu;
        }

        public IEnumerator<NavigationLinkViewModel> GetEnumerator()
        {
            return Links.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Links.GetEnumerator();
        }
    }
}