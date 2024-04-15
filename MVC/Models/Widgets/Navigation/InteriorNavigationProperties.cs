using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Custom;
using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Components.Widgets;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.Navigation;
using System.Collections.Generic;

[assembly: RegisterWidget("UMCHospital.Widgets.InteriorNavigation", typeof(InteriorNavigationViewComponent), "Interior Navigation", typeof(InteriorNavigationProperties), IconClass = "icon-parent-children-scheme-3")]

namespace MVC.Models.Widgets.Navigation
{
    public class InteriorNavigationProperties : IWidgetProperties 
    {
        //HERO PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string HeroPropertiesText { get; set; } = "Interior Navigation Properties";

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 1, Label = "Right Panel Content")]
        public string Content { get; set; }

        public List<TreeNode> Pages { get; set; }
    }
}
