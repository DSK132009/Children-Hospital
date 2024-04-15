using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Components.Widgets;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.Navigation;
using MVC.Models.Widgets.TeamMembers;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: RegisterWidget("UMCHospital.Widgets.TeamMembers", "Team Members", typeof(TeamMemberProperties), customViewName: "Widgets/TeamMembers/_TeamMembers", IconClass = "icon-personalisation-variants", AllowCache = true)]

namespace MVC.Models.Widgets.TeamMembers
{
    public class TeamMemberProperties : IWidgetProperties 
    {
        //HERO PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string HeroPropertiesText { get; set; } = "Team Members Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Heading")]
        public string Heading { get; set; }

        [EditingComponent(ObjectSelector.IDENTIFIER, Order = 5, Label = "Selected Team Members")]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.ObjectType), "teammembers.teammember")]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.MaxItemsLimit), 0)]
        [EditingComponentProperty(nameof(ObjectSelectorProperties.IdentifyObjectByGuid), true)]
        public IEnumerable<ObjectSelectorItem> TeamMembers { get; set; } = Enumerable.Empty<ObjectSelectorItem>();
    }
}
