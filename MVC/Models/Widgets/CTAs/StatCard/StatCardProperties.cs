using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.CTA.StatCard;

[assembly: RegisterWidget("UMCHospital.Widgets.StatCard", "Stat Card", typeof(StatCardProperties), customViewName: "Widgets/CTAs/_StatCard", IconClass = "icon-book-opened", AllowCache = true)]

namespace MVC.Models.Widgets.CTA.StatCard
{
    public class StatCardProperties : IWidgetProperties
    {
        //CTA PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Stat Card Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Stat Card Title")]
        public string Title { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 2, Label = "Stat Card Text")]
        public string Text { get; set; }
    }
}