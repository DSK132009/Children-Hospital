using CMS.EmailEngine;
using CMS.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TreeNode = CMS.DocumentEngine.TreeNode;

namespace MVC.Models.Navigation
{
    public class NavigationLinkViewModel
    {
        public string DisplayText { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public string CssClass { get; set; }
        public string SpeedbumpText { get; set; }
        public bool UseSpeedBump { get; set; }
        public string AriaLabel { get; set; }
        public string LinkTitle { get; set; }
        public string IconUrl { get; set; }
        public string DocType { get; set; }
        public bool IsSpaced { get; set; }
        public bool IsMobileOnly { get; set; }
        public bool IsDesktopOnly { get; set; }
        public bool IsHighlighted { get; set; }
        public bool StickyIconOnMobile { get; set; }

        public List<NavigationLinkViewModel> Links { get; set; }

        public NavigationLinkViewModel()
        {
            Links = new List<NavigationLinkViewModel>();
        }
        public NavigationLinkViewModel(TreeNode Node)
        {
            Links = new List<NavigationLinkViewModel>();
            DisplayText = Node.DocumentName;
            Url = Node.GetStringValue("Url", "");
            CssClass = Node.GetStringValue("CssClass", "");
            SpeedbumpText = Node.GetStringValue("SpeedbumpText", "");
            UseSpeedBump = Node.GetBooleanValue("UseSpeedBump", false);
            AriaLabel = Node.GetStringValue("AriaLabel", "");
            Target = Node.GetStringValue("Target", "");
            LinkTitle = Node.GetStringValue("LinkTitle", "");
            IconUrl = Node.GetStringValue("IconUrl", "");
            DocType = Node.Parent.GetStringValue("NavigationType", "");
            IsSpaced = Node.GetBooleanValue("IsSpaced", false);
            IsMobileOnly = Node.GetBooleanValue("IsMobileOnly", false);
            IsDesktopOnly = Node.GetBooleanValue("IsDesktopOnly", false);
            IsHighlighted = Node.GetBooleanValue("IsHighlighted", false);
            StickyIconOnMobile = Node.GetBooleanValue("StickyIconOnMobile", false);
        }

        public void CheckSpeedbump(string defaultMessage)
        {
            if (UseSpeedBump && string.IsNullOrWhiteSpace(SpeedbumpText))
            {
                //use default speedbump
                SpeedbumpText = defaultMessage;
            }
        }
    }
}