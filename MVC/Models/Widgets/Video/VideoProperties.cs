using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.Video;
using System.Collections.Generic;

[assembly: RegisterWidget("UMCHospital.Widgets.Video", "Video", typeof(VideoProperties), customViewName: "Widgets/Video/_Video", IconClass = "icon-clapperboard", AllowCache = true)]

namespace MVC.Models.Widgets.Video
{ 
    public class VideoProperties : IWidgetProperties
    {
        //CTA PROPERTIES
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Video Widget Properties";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Embeddable Youtube Link", ExplanationText = "eg: https://www.youtube.com/embed/6qGiXY1SB68")]
        public string YoutubeUrl { get; set; }

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 3, Label = "Image")]
        public IEnumerable<MediaFilesSelectorItem> Image { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 4, Label = "Image Alternate Text")]
        public string ImageAltText { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 5, Label = "Video Title")]
        public string VideoTitle { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 6, Label = "Video Description")]
        public string VideoDescription { get; set; }
    }
}