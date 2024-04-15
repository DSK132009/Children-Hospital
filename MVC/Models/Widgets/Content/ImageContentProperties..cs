using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.Widgets.Content.ImageContent;
using System.Collections.Generic;

[assembly: RegisterWidget("UMCHospital.Widgets.ImageContent", "Image Content", typeof(ImageContentProperties), customViewName: "Widgets/Content/_ImageContent", IconClass = "icon-picture")]

namespace MVC.Models.Widgets.Content.ImageContent
{
    public class ImageContentProperties : IWidgetProperties
    {
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Driving Direction Properties";

        [EditingComponent(MediaFilesSelector.IDENTIFIER, Order = 1, Label = "Image")]
        public IEnumerable<MediaFilesSelectorItem> Image { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "Image Alternate Text")]
        public string ImageAltText { get; set; }

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 3, Label = "Heading")]
        public string Heading { get; set; }

        [EditingComponent(RichTextComponent.IDENTIFIER, Order = 4, Label = "Content")]
        public string Content { get; set; }

    }
}
