using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.Sections.Newsletter;

[assembly: RegisterSection("UMCHospital.Sections.NewsletterSection", "Newsletter Section", typeof(NewsletterSection), customViewName: "Sections/_NewsletterSection", IconClass = "icon-newspaper")]

namespace MVC.Models.Sections.Newsletter
{
    public class NewsletterSection : ISectionProperties
    {
        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 0, Label = "Newsletter Heading")]
        public string Heading { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 1, Label = "Newsletter Description")]
        public string Description { get; set; }
    }
}