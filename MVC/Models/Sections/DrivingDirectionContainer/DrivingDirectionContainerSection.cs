using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Sections.DrivingDirectionContainer;

[assembly: RegisterSection("UMCHospital.Sections.DrivingDirectionContainerSection", "Driving Direction Container Section", typeof(DrivingDirectionContainerSectionProperties), customViewName: "Sections/_DrivingDirectionContainerSection", IconClass = "icon-earth")]

namespace MVC.Models.Sections.DrivingDirectionContainer
{
    public class DrivingDirectionContainerSectionProperties : ISectionProperties
    {
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Driving Direction Container Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Heading")]
        public string Heading { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 2, Label = "Link")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string Url { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 3, Label = "Open Link in New Tab")]
        public bool OpenNewTab { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 4, Label = "Aria Label")]
        public string AriaLabel { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 5, Label = "CTA Text")]
        public string Text { get; set; }
    }
}
