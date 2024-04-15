using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.Content.DrivingDirection;

[assembly: RegisterWidget("UMCHospital.Widgets.DrivingDirection", "Driving Direction", typeof(DrivingDirectionProperties), customViewName: "Widgets/Content/_DrivingDirectionContent", IconClass = "icon-earth")]

namespace MVC.Models.Widgets.Content.DrivingDirection
{
    public class DrivingDirectionProperties : IWidgetProperties
    {
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Driving Direction Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Heading")]
        public string Heading { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 2, Label = "Content")]
        public string Content { get; set; }

    }
}
