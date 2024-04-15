using CMS.Helpers;
using Kentico.Forms.Web.Mvc;
using Kentico.Forms.Web.Mvc.Widgets;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Configuration
{
    public static class FormBuilderStaticMarkupConfiguration
    {
        public static void SetGlobalRenderingConfigurations()
        {
            FormFieldRenderingConfiguration.GetConfiguration.Execute += InjectMarkupIntoKenticoComponents;

            //FormFieldRenderingConfiguration.Widget.RootConfiguration =
            //    new ElementRenderingConfiguration
            //    {
            //        ElementName = "div",
            //        HtmlAttributes = { { "class", "row" } }
            //    };

            //Add or remove colon after labels
            FormFieldRenderingConfiguration.Widget.ColonAfterLabel = true;

            //Remove Kentico default wrappers on form elements and add any custom ones needed
            FormFieldRenderingConfiguration.Widget.EditorWrapperConfiguration.CustomHtmlString = ElementRenderingConfiguration.CONTENT_PLACEHOLDER;
            FormFieldRenderingConfiguration.Widget.LabelWrapperConfiguration.CustomHtmlString = ElementRenderingConfiguration.CONTENT_PLACEHOLDER;
            FormFieldRenderingConfiguration.Widget.ComponentWrapperConfiguration.CustomHtmlString = ElementRenderingConfiguration.CONTENT_PLACEHOLDER;

            //FormFieldRenderingConfiguration.Widget.EditorWrapperConfiguration.CustomHtml = MvcHtmlString.Create($"<div>{ElementRenderingConfiguration.CONTENT_PLACEHOLDER}</div>");

            //Add custom CSS class to form labels
            //FormFieldRenderingConfiguration.Widget.LabelHtmlAttributes["class"] = "input-group-text";

            FormWidgetRenderingConfiguration.Default = new FormWidgetRenderingConfiguration
            {

                // Submit button HTML attributes
                SubmitButtonHtmlAttributes = { { "class", "btn btn-primary" } },
                //Form HTML attributes
                FormHtmlAttributes = { { "class", "needs-validation newsletter-cta-form" } },
            };
        }

        private static void InjectMarkupIntoKenticoComponents(object sender, GetFormFieldRenderingConfigurationEventArgs e)
        {
            //Other methods can be assigned here for other functionality as noted in https://docs.xperience.io/developing-websites/form-builder-development/customizing-the-form-widget

            // Assigns additional attributes to 'TextArea' fields and handles Validation checks
            AddFieldSpecificMarkup(e);
        }

        private static void AddFieldSpecificMarkup(GetFormFieldRenderingConfigurationEventArgs e)
        {
            // Sets a custom CSS class for the wrapping element of 'TextAreaComponent' type form fields
            if (e.FormComponent is TextAreaComponent)
            {
                e.Configuration.LabelWrapperConfiguration.CustomHtmlString = $"{ElementRenderingConfiguration.CONTENT_PLACEHOLDER}";

                e.Configuration.RootConfiguration =
                new ElementRenderingConfiguration
                {
                    ElementName = "div",
                    HtmlAttributes = { { "class", "fixed-placeholder fixed-placeholder-textarea form-group col-12" } }
                };
                e.Configuration.LabelHtmlAttributes["class"] = "sr-only";
                e.Configuration.EditorHtmlAttributes["class"] = "";
            }

            if (e.FormComponent is DropDownComponent)
            {
                e.Configuration.LabelWrapperConfiguration.CustomHtmlString = $"{ElementRenderingConfiguration.CONTENT_PLACEHOLDER}";

                e.Configuration.LabelHtmlAttributes["class"] = "sr-only";
            }

            //Validation Checking and Markup Edits
            bool valid = true;

            //We only want to do this during a post-back. If a post-back occurs, the URL will start with Kentico.Components
            if (RequestContext.CurrentURL.StartsWith("/Kentico.Components/"))
            {
                if (e.FormComponent.BaseProperties.Required)
                {
                    if (e.FormComponent.GetObjectValue() == null)
                    {
                        valid = false;
                    }
                    else
                    {
                        valid = !String.IsNullOrEmpty(e.FormComponent.GetObjectValue().ToString());
                    }
                }

                if (valid)
                {
                    foreach (var rule in e.FormComponent.BaseProperties.ValidationRuleConfigurations)
                    {
                        var fieldValue = e.FormComponent.GetObjectValue();

                        if (fieldValue != null)
                        {
                            valid = rule.ValidationRule.IsValueValid(fieldValue);

                            if (!valid)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            //If a field is invalid, add the CSS class "error" to change the styling of the field
            if (!valid)
            {
                e.Configuration.RootConfiguration.HtmlAttributes["class"] += " error";
            }
        }
    }
}
