using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.Sections.VideoContainer;

[assembly: RegisterSection("UMCHospital.Sections.VideoContainerSection", "Video Container Section", typeof(VideoContainerSectionProperties), customViewName: "Sections/_VideoContainerSection", IconClass = "icon-clapperboard")]

namespace MVC.Models.Sections.VideoContainer
{
    public class VideoContainerSectionProperties : ISectionProperties
    {
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 0, Label = "Video Container Heading")]
        public string Heading { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 1, Label = "Video Container Description")]
        public string Description { get; set; }
    }
}
