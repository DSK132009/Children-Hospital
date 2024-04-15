using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.CTA.IconCallout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterWidget("UMCHospital.Widgets.IconCallout", "Icon Callout", typeof(IconCalloutProperties), customViewName: "Widgets/CTAs/_IconCallout", IconClass = "icon-bubble-show", AllowCache = true)]

namespace MVC.Models.Widgets.CTA.IconCallout
{
    public class IconCalloutProperties : IWidgetProperties
    {
        //CTA PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "CTA Properties";

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 1, Label = "Link")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string Url { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 2, Label = "Open Link in New Tab")]
        public bool OpenNewTab { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "Aria Label")]
        public string AriaLabel { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 4, Label = "CTA Text")]
        public string Text { get; set; }

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 5, Label = "Icon")]
        public IEnumerable<MediaFilesSelectorItem> Icon { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 6, Label = "Icon Alternative Text")]
        public string IconAltText { get; set; }
    }
}