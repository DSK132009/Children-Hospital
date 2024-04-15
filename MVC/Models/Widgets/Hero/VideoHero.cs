using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.Hero;
using System.Collections.Generic;

//Commented out as this is not ready for UMC Children's
//[assembly: RegisterWidget("UMCHospital.Widgets.VideoHero", "Video Hero", typeof(VideoHero), customViewName: "Widgets/Hero/_VideoHero", IconClass = "icon-triangle-right", AllowCache = true)]

namespace MVC.Models.Widgets.Hero
{
    public class VideoHero : IWidgetProperties
    {
        //HERO PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string HeroPropertiesText { get; set; } = "Hero Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Heading")]
        public string Heading { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "Sub Heading")]
        public string SubHeading { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "Vimeo ID")]
        [EditingComponentProperty(nameof(TextInputProperties.ExplanationText), "Enter the Vimeo ID for the video. The ID is at the end of the URL. Ex: https://vimeo.com/XXXX where XXXX is the Video ID")]
        public string VimeoID { get; set; }

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 4, Label = "Mobile Image")]
        public IEnumerable<MediaFilesSelectorItem> MobileImage { get; set; }

        //QUICK LINK ONE PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 10, Label = "")]
        public string QuickLinkOnePropertiesText { get; set; } = "Main Quick Link One Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 11, Label = "Link Text")]
        public string QuickLinkOneHeading { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 12, Label = "Link Icon Name")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Media)]
        public string QuickLinkOneIcon { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 13, Label = "Image Alt Text")]
        public string QuickLinkOneAltText { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 14, Label = "Open Link in New Tab")]
        public bool QuickLinkOneOpenNewTab { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 15, Label = "Link URL")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string QuickLinkOneLink { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 16, Label = "Link Title")]
        public string QuickLinkOneTitle { get; set; }


        //QUICK LINK TWO PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 17, Label = "")]
        public string QuickLinkTwoPropertiesText { get; set; } = "Main Quick Link Two Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 18, Label = "Link Text")]
        public string QuickLinkTwoHeading { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 19, Label = "Link Icon Name")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Media)]
        public string QuickLinkTwoIcon { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 20, Label = "Image Alt Text")]
        public string QuickLinkTwoAltText { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 21, Label = "Open Link in New Tab")]
        public bool QuickLinkTwoOpenNewTab { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 22, Label = "Link URL")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string QuickLinkTwoLink { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 23, Label = "Link Title")]
        public string QuickLinkTwoTitle { get; set; }


        //QUICK LINK THREE PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 24, Label = "")]
        public string QuickLinkThreePropertiesText { get; set; } = "Main Quick Link Three Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 25, Label = "Link Text")]
        public string QuickLinkThreeHeading { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 26, Label = "Link Icon Name")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Media)]
        public string QuickLinkThreeIcon { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 27, Label = "Image Alt Text")]
        public string QuickLinkThreeAltText { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 28, Label = "Open Link in New Tab")]
        public bool QuickLinkThreeOpenNewTab { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 29, Label = "Link URL")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string QuickLinkThreeLink { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 30, Label = "Link Title")]
        public string QuickLinkThreeTitle { get; set; }


        //QUICK LINK FOUR PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 31, Label = "")]
        public string QuickLinkPropertiesText { get; set; } = "Main Quick Link Four Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 32, Label = "Link Text")]
        public string QuickLinkFourHeading { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 33, Label = "Link Icon Name")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Media)]
        public string QuickLinkFourIcon { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 34, Label = "Image Alt Text")]
        public string QuickLinkFourAltText { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 35, Label = "Open Link in New Tab")]
        public bool QuickLinkFourOpenNewTab { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 36, Label = "Link URL")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string QuickLinkFourLink { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 37, Label = "Link Title")]
        public string QuickLinkFourTitle { get; set; }

        //FOOTER LINK ONE PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 38, Label = "")]
        public string FooterQuickLinkOnePropertiesText { get; set; } = "Footer Quick Link One Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 39, Label = "Link Text")]
        public string FooterQuickLinkOneHeading { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 40, Label = "Link Icon Name")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Media)]
        public string FooterQuickLinkOneIcon { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 41, Label = "Image Alt Text")]
        public string FooterQuickLinkOneAltText { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 42, Label = "Open Link in New Tab")]
        public bool FooterQuickLinkOneOpenNewTab { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 43, Label = "Link URL")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string FooterQuickLinkOneLink { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 44, Label = "Link Title")]
        public string FooterQuickLinkOneTitle { get; set; }


        //QUICK LINK TWO PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 45, Label = "")]
        public string FooterQuickLinkTwoPropertiesText { get; set; } = "Footer Quick Link Two Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 46, Label = "Link Text")]
        public string FooterQuickLinkTwoHeading { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 47, Label = "Link Icon Name")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Media)]
        public string FooterQuickLinkTwoIcon { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 48, Label = "Image Alt Text")]
        public string FooterQuickLinkTwoAltText { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 49, Label = "Open Link in New Tab")]
        public bool FooterQuickLinkTwoOpenNewTab { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 50, Label = "Link URL")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string FooterQuickLinkTwoLink { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 51, Label = "Link Title")]
        public string FooterQuickLinkTwoTitle { get; set; }


        //FOOTER QUICK LINK THREE PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 52, Label = "")]
        public string FooterQuickLinkThreePropertiesText { get; set; } = "Footer Quick Link Three Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 53, Label = "Link Text")]
        public string FooterQuickLinkThreeHeading { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 54, Label = "Link Icon Name")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Media)]
        public string FooterQuickLinkThreeIcon { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 55, Label = "Image Alt Text")]
        public string FooterQuickLinkThreeAltText { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 56, Label = "Open Link in New Tab")]
        public bool FooterQuickLinkThreeOpenNewTab { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 57, Label = "Link URL")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string FooterQuickLinkThreeLink { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 58, Label = "Link Title")]
        public string FooterQuickLinkThreeTitle { get; set; }


        //FOOTER QUICK LINK FOUR PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 59, Label = "")]
        public string FooterQuickLinkPropertiesText { get; set; } = "Footer Quick Link Four Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 60, Label = "Link Text")]
        public string FooterQuickLinkFourHeading { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 61, Label = "Link Icon Name")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Media)]
        public string FooterQuickLinkFourIcon { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 62, Label = "Image Alt Text")]
        public string FooterQuickLinkFourAltText { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 63, Label = "Open Link in New Tab")]
        public bool FooterQuickLinkFourOpenNewTab { get; set; }

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 64, Label = "Link URL")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string FooterQuickLinkFourLink { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 65, Label = "Link Title")]
        public string FooterQuickLinkFourTitle { get; set; }
    }
}
