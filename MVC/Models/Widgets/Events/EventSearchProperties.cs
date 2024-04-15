using CMS.Helpers;
using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Components.Widgets;
using MVC.Models.Widgets.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterWidget("UMCHospital.Widgets.EventSearch", typeof(EventSearchViewComponent), "Event Search", typeof(EventSearchProperties), IconClass = "icon-calendar")]

namespace MVC.Models.Widgets.Events
{
    public class EventSearchProperties : IWidgetProperties
    {
        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 0, Label = "Intro Text")]
        public string IntroText { get; set; }
        public string Category => QueryHelper.GetString("category", "");
        public string Location => QueryHelper.GetString("location", "");
        public string SearchText => QueryHelper.GetString("searchtext", "");
        public string DateFrom => QueryHelper.GetString("datefrom", "");
        public string DateTo => QueryHelper.GetString("dateto", "");
        public string Option => QueryHelper.GetString("option", "");
    }
}
