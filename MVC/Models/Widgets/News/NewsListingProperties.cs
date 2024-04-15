using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.News.LatestNews;

[assembly: RegisterWidget("UMCHospital.Widgets.NewsListingProperties", "News Listing", typeof(NewsListingProperties), customViewName: "Widgets/News/_NewsListing", IconClass = "icon-newspaper", AllowCache = true)]

namespace MVC.Models.Widgets.News.LatestNews
{ 
    public class NewsListingProperties : IWidgetProperties
    {
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 0, Label = "Header Text")]
        public string HeaderText { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "\"Load More\" Button Text")]
        public string LoadMoreText { get; set; } = "Load More";
    }
}