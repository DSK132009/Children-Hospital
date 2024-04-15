using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.CTA.GeneralCard;
using System.Collections.Generic;

[assembly: RegisterWidget("UMCHospital.Widgets.GeneralCard", "General Card", typeof(GeneralCardProperties), customViewName: "Widgets/CTAs/_GeneralCard", IconClass = "icon-bubble-show", AllowCache = true)]

namespace MVC.Models.Widgets.CTA.GeneralCard
{
    public class GeneralCardProperties : IWidgetProperties
    {
        //CTA PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "General Card Properties";

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 1, Label = "Image")]
        public IEnumerable<MediaFilesSelectorItem> Image { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "Image Alternative Text")]
        public string ImageAltText { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "Heading")]
        public string Heading { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 4, Label = "Content")]
        public string Content { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 5, Label = "Link Url")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string LinkUrl { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 6, Label = "Open Link in New Tab")]
        public bool LinkOpenNewTab { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 7, Label = "Link Aria Label")]
        public string LinkAriaLabel { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 8, Label = "Link Text")]
        public string LinkText { get; set; }
    }
}