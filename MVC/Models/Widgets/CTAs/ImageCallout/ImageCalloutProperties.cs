using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.CTA.IconCallout;
using MVC.Models.Widgets.CTA.ImageCallout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterWidget("UMCHospital.Widgets.ImageCallout", "Image Callout", typeof(ImageCalloutProperties), customViewName: "Widgets/CTAs/_ImageCallout", IconClass = "icon-brush", AllowCache = true)]

namespace MVC.Models.Widgets.CTA.ImageCallout
{
    public class ImageCalloutProperties : IWidgetProperties
    {
        //CTA PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Image Callout Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "CTA Heading")]
        public string Heading { get; set; }

        [EditingComponent(DropDownComponent.IDENTIFIER, Order = 2, Label = "Header Color")]
        [EditingComponentProperty(nameof(DropDownProperties.DataSource), ";(Default)\r\ncolor: #00509f;Blue\r\ncolor: #0da14b;Green\r\ncolor: #dc312b;Red\r\ncolor: #f99f1b;Yellow\r\ncolor: #8b3a9e;Purple")]
        public string HeadingColor { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 3, Label = "CTA Text")]
        public string Text { get; set; }

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 4, Label = "Image")]
        public IEnumerable<MediaFilesSelectorItem> Image { get; set; }


        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 5, Label = "Image Alternative Text")]
        public string ImageAltText { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 6, Label = "Link Text")]
        public string ButtonText { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 7, Label = "Link URL")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string ButtonUrl { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 8, Label = "Aria Label")]
        public string ButtonAriaLabel { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 9, Label = "Open Link in New Tab?")]
        public bool OpenNewTab { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 10, Label = "Left Align Image?")]
        public bool AlignLeft { get; set; }

    }
}