using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.News.LatestNews;

[assembly: RegisterWidget("UMCHospital.Widgets.LatestNews", "Latest News", typeof(LatestNewsProperties), customViewName: "Widgets/News/_LatestNews", IconClass = "icon-newspaper", AllowCache = true)]

namespace MVC.Models.Widgets.News.LatestNews
{ 
    public class LatestNewsProperties : IWidgetProperties
    {
        //CTA PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Latest News Widget Properties";

        [EditingComponent(UrlSelector.IDENTIFIER, Order = 1, Label = "Link")]
        [EditingComponentProperty(nameof(UrlSelectorProperties.Tabs), ContentSelectorTabs.Page | ContentSelectorTabs.Media)]
        public string Url { get; set; }

        [EditingComponent(CheckBoxComponent.IDENTIFIER, Order = 2, Label = "Open Link in New Tab")]
        public bool OpenNewTab { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "Aria Label")]
        public string AriaLabel { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 4, Label = "CTA Text")]
        public string Text { get; set; }
    }
}