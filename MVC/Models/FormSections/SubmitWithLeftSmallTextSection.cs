using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.FormSections.SubmitWithLeftSmallTextSection;
using MVC.Models.FormSections.TwoColumnSection;

[assembly: RegisterFormSection("UMCHospital.FormSections.SubmitWithLeftSmallTextSection", "Submit With Left Small Text Section",
    customViewName: "FormSections/_SubmitWithLeftSmallTextSection", IconClass = "icon-square", PropertiesType = typeof(SubmitWithLeftSmallTextSectionProperties))]

namespace MVC.Models.FormSections.SubmitWithLeftSmallTextSection
{
    public class SubmitWithLeftSmallTextSectionProperties : IFormSectionProperties
    {
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Text for Submit Button";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Text")]
        public string LeftText { get; set; } = "Enter text in section properties...";
    }
}
