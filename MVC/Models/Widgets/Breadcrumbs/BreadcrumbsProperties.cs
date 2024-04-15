using Kentico.PageBuilder.Web.Mvc;
using MVC.Components.Widgets;
using MVC.Models.Widgets.Breadcrumbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterWidget("UMCHospital.Widgets.Breadcrumbs", typeof(BreadcrumbViewComponent), "Breadcrumbs", typeof(BreadcrumbsProperties), IconClass = "icon-breadcrumb")]

namespace MVC.Models.Widgets.Breadcrumbs
{
    public class BreadcrumbsProperties : IWidgetProperties
    {

    }
}