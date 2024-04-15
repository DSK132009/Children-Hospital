using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.CTA.DonorRegistration;
using System.Collections.Generic;

[assembly: RegisterWidget("UMCHospital.Widgets.DonorRegistration", "Donor Registration", typeof(DonorRegistrationProperties), customViewName: "Widgets/CTAs/_DonorRegistration", IconClass = "icon-heartshake", AllowCache = true)]

namespace MVC.Models.Widgets.CTA.DonorRegistration
{
    public class DonorRegistrationProperties : IWidgetProperties
    {
        //CTA PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "CTA Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Heading")]
        public string Heading { get; set; }

        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 2, Label = "")]
        public string DonorPropertiesTextOne { get; set; } = "Donor Organization One";

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 3, Label = "Image")]
        public IEnumerable<MediaFilesSelectorItem> ImageOne { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 4, Label = "Image Alternative Text")]
        public string ImageAltTextOne { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 5, Label = "Link")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string UrlOne { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 7, Label = "Open Link in New Tab")]
        public bool OpenNewTabOne { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 8, Label = "Aria Label")]
        public string AriaLabelOne { get; set; }

        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 9, Label = "")]
        public string DonorPropertiesTextTwo { get; set; } = "Donor Organization Two";

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 10, Label = "Image")]
        public IEnumerable<MediaFilesSelectorItem> ImageTwo { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 11, Label = "Image Alternative Text")]
        public string ImageAltTextTwo { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 12, Label = "Link")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string UrlTwo { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 13, Label = "Open Link in New Tab")]
        public bool OpenNewTabTwo { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 14, Label = "Aria Label")]
        public string AriaLabelTwo { get; set; }

        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 15, Label = "")]
        public string DonorPropertiesTextThree { get; set; } = "Donor Organization Three";

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 16, Label = "Image")]
        public IEnumerable<MediaFilesSelectorItem> ImageThree { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 17, Label = "Image Alternative Text")]
        public string ImageAltTextThree { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 18, Label = "Link")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string UrlThree { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 19, Label = "Open Link in New Tab")]
        public bool OpenNewTabThree { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 20, Label = "Aria Label")]
        public string AriaLabelThree { get; set; }

        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 21, Label = "")]
        public string DonorPropertiesTextFour { get; set; } = "Donor Organization Four";

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 22, Label = "Image")]
        public IEnumerable<MediaFilesSelectorItem> ImageFour { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 23, Label = "Image Alternative Text")]
        public string ImageAltTextFour { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 24, Label = "Link")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string UrlFour { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 25, Label = "Open Link in New Tab")]
        public bool OpenNewTabFour { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 26, Label = "Aria Label")]
        public string AriaLabelFour { get; set; }
    }
}