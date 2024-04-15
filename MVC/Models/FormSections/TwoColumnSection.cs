using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using MVC.Models.FormSections.TwoColumnSection;

[assembly: RegisterFormSection("UMCHospital.FormSections.TwoColumnSection", "Two Column Section",
    customViewName: "FormSections/_TwoColumnSection", IconClass = "icon-square", PropertiesType = typeof(TwoColumnSectionProperties))]

namespace MVC.Models.FormSections.TwoColumnSection
{
    public class TwoColumnSectionProperties : IFormSectionProperties
    {
        [EditingComponent(CustomLabelComponent.IDENTIFIER, Order = 0, Label = "")]
        public string CtaPropertiesText { get; set; } = "Please note total length should add to 12";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 1, Label = "Column 1 Length (1-11)")]
        public string Columnl1Length { get; set; } = "6";

        [EditingComponent(TextInputComponent.IDENTIFIER, Order = 2, Label = "Column 1 Length (1-11)")]
        public string Column2Length { get; set; } = "6";
    }
}
