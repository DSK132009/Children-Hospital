using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.Sections.GeneralCardContainer;
using MVC.Models.Sections.Newsletter;

[assembly: RegisterSection("UMCHospital.Sections.GeneralCardContainerSection", "General Card Container Section", typeof(GeneralCardContainerSectionProperties), customViewName: "Sections/_GeneralCardContainerSection", IconClass = "icon-lines-rectangle-o")]

namespace MVC.Models.Sections.GeneralCardContainer
{
    public class GeneralCardContainerSectionProperties : ISectionProperties
    {
    }
}
