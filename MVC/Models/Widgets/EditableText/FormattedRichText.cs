using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.Widgets.EditableText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterWidget("UMCHospital.Widgets.FormattedRichText", "Formatted Rich Text", typeof(FormattedRichText), customViewName: "Widgets/EditableText/_FormattedRichText", IconClass = "icon-l-menu-text-col-bottom")]

namespace MVC.Models.Widgets.EditableText
{
    public class FormattedRichText : IWidgetProperties
    {
        [EditingComponent(RichTextComponent.IDENTIFIER)]
        public string HtmlContent { get; set; }
    }
}
