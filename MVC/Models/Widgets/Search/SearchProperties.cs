using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Components.Search;
using MVC.Components.Widgets;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.Search;

[assembly: RegisterWidget("UMCHospital.Widgets.Search", "Search", typeof(SearchProperties), customViewName: "Widgets/SiteSearch/_SearchResults", IconClass = "icon-magnifier")]

namespace MVC.Models.Widgets.Search
{
    public class SearchProperties : IWidgetProperties
    {
        //CTA PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Search Page Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Popular Searches", ExplanationText = "Comma separated list of words or phrases for popular search terms")]
        public string PopularSearches { get; set; }
    }
}