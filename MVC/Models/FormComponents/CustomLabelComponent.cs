using Kentico.Forms.Web.Mvc;
using MVC.Models.FormComponents.CustomLabel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: RegisterFormComponent(CustomLabelComponent.IDENTIFIER, typeof(CustomLabelComponent), "Custom Label", IconClass = "icon-menu")]

namespace MVC.Models.FormComponents.CustomLabel
{
    public class CustomLabelComponent : FormComponent<CustomLabelComponentProperties, string>
    {
        public const string IDENTIFIER = "CustomLabelComponent";

        [BindableProperty]
        public string LabelText { get; set; }

        public override string GetValue()
        {
            return LabelText;
        }

        public override void SetValue(string value)
        {
            LabelText = value;
        }
    }
}