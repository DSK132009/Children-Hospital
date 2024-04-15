using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.Widgets.Locations;
using MVC.ViewComponents;

[assembly: RegisterWidget("UMCHospital.Widgets.Locations", typeof(LocationSearchViewComponent), "Location Search", typeof(LocationSearchProperties), IconClass = "icon-map")]

namespace MVC.Models.Widgets.Locations
{
    public class LocationSearchProperties : IWidgetProperties
    {
        [EditingComponent(DropDownComponent.IDENTIFIER, Order = 0, Label = "Default Filter Option")]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), "quickprimarycare;Quick & PrimaryCare\r\nquickcare;Quick Care\r\nprimarycare;Primary Care")]
        public string DefaultFilterOption { get; set; }
    }
}
