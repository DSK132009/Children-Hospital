using Kentico.PageBuilder.Web.Mvc;

[assembly: RegisterSection("UMCHospital.Sections.IconCalloutContainerSection", "Icon Callout Container Section", customViewName: "Sections/_IconCalloutContainerSection", IconClass = "icon-lines-rectangle-o")]

namespace MVC.Models.Sections.IconCalloutContainer
{
    public class IconCalloutContainerSection : ISectionProperties
    {
    }
}
