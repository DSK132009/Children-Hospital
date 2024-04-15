using Microsoft.AspNetCore.Mvc;
using MVC.Models.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewComponents
{
    //The different navigation types that can be set come from a hidden site setting (hidden to avoid client accidentally editing this).
    //If you need to change these, expose the site setting, edit it, then hide it again

    public class MainNavigation : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var navVM = new NavigationViewModel();
            var mainNav = navVM.New("MainNavigation");

            return View(mainNav);
        }
    }

    public class MainNavigationLogin : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var navVM = new NavigationViewModel();
            var mainNav = navVM.New("MainNavigationLogin");

            return View(mainNav);
        }
    }

    public class FooterNavigation : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var navVM = new NavigationViewModel();
            var utilNav = navVM.New("FooterNavigation");

            return View(utilNav);
        }
    }

    public class FooterNavigationSocial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var navVM = new NavigationViewModel();
            var utilNav = navVM.New("FooterNavigationSocial");

            return View(utilNav);
        }
    }

    public class FooterNavigationIcons : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var navVM = new NavigationViewModel();
            var utilNav = navVM.New("FooterNavigationIcons");

            return View(utilNav);
        }
    }

    public class FooterNavigationIconsDesktop : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var navVM = new NavigationViewModel();
            var utilNav = navVM.New("FooterNavigationIconsDesktop");

            return View(utilNav);
        }
    }

    public class FooterNavigationIconsMobileSticky : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var navVM = new NavigationViewModel();
            var utilNav = navVM.New("FooterNavigationIcons");

            return View(utilNav);
        }
    }
}
