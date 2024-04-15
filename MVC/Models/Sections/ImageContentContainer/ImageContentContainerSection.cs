using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Sections.ImageContentContainer;

[assembly: RegisterSection("UMCHospital.Sections.ImageContentContainerSection", "Image Content Container Section", typeof(ImageContentContainerSectionProperties), customViewName: "Sections/_ImageContentContainerSection", IconClass = "icon-pictures")]

namespace MVC.Models.Sections.ImageContentContainer
{
    public class ImageContentContainerSectionProperties : ISectionProperties
    {
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Image Content Container Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Heading")]
        public string Heading { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 2, Label = "Description")]
        public string Description { get; set; }
    }
}
