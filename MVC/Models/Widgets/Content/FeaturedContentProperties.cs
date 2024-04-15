using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.Content.FeaturedContent;
using MVC.Models.Widgets.EditableText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterWidget("UMCHospital.Widgets.FeaturedContent", "Featured Content", typeof(FeaturedContentProperties), customViewName: "Widgets/Content/_FeaturedContent", IconClass = "icon-clipboard-list")]

namespace MVC.Models.Widgets.Content.FeaturedContent
{
    public class FeaturedContentProperties : IWidgetProperties
    {
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Featured Content Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Heading")]
        public string Heading { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 2, Label = "Left Content")]
        public string ContentLeft { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 3, Label = "Right Content")]
        public string ContentRight { get; set; }
    }
}
