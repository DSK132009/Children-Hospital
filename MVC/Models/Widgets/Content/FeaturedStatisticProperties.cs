using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.Content.FeaturedStatistics;
using MVC.Models.Widgets.EditableText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterWidget("UMCHospital.Widgets.FeaturedStatistics", "Featured Statistics", typeof(FeaturedStatisticsProperties), customViewName: "Widgets/Content/_FeaturedStatistics", IconClass = "icon-database")]

namespace MVC.Models.Widgets.Content.FeaturedStatistics
{
    public class FeaturedStatisticsProperties : IWidgetProperties
    {
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Featured Statistic Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Statistic One Heading")]
        public string HeadingOne { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "Statistic One SubTitle")]
        public string SubTitleOne { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "Statistic Two Heading")]
        public string HeadingTwo { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 4, Label = "Statistic Two SubTitle")]
        public string SubTitleTwo { get; set; }

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 5, Label = "Image")]
        public IEnumerable<MediaFilesSelectorItem> Image { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 6, Label = "Image Alternative Text")]
        public string ImageAltText { get; set; }
    }
}
