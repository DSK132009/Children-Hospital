using CMS.DataEngine;
using Kentico.Forms.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models.FormComponents.CustomLabel
{
    public class CustomLabelComponentProperties : FormComponentProperties<string>
    {
        public override string DefaultValue { get; set; }

        public CustomLabelComponentProperties() : base(FieldDataType.Text)
        {

        }
    }
}