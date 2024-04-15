using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Components.ServiceListing;
using MVC.Models.Widgets.ServiceListing;

[assembly: RegisterWidget("UMCHospital.Widgets.ServiceListing", typeof(ServiceListingViewComponent), "ServiceListing", typeof(ServiceListingProperties), IconClass = "icon-l-list-img-article")]
namespace MVC.Models.Widgets.ServiceListing
{

    public class ServiceListingProperties : IWidgetProperties
    {
    }
}
