using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Custom;
using CMS.DocumentEngine.Types.UMCHospital;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.Widgets.Navigation
{
    public class InteriorNavigationViewModel
    {
        public string LinkTitle { get; set; }
        public string LinkUrl { get; set; }
        public string LinkTarget { get; set; }
        public bool HideFromInteriorNavigation { get; set; }
        public Guid NodeGUID { get; set; }

        public InteriorNavigationViewModel(TreeNode node)
        {
            if (node.ClassName == Page.CLASS_NAME)
            {
                HideFromInteriorNavigation = node.GetBooleanValue("HideFromInteriorNavigation", false);
                LinkTitle = node.GetStringValue("PageName", "");
                LinkUrl = node.NodeAliasPath;
                LinkTarget = "";

            }
            else if (node.ClassName == SideNavigationItem.CLASS_NAME)
            {
                HideFromInteriorNavigation = false;
                LinkTitle = node.GetStringValue("DisplayName", "");
                LinkUrl = node.GetStringValue("Link", "");
                LinkTarget = node.GetBooleanValue("OpenInNewTab", false) ? "_blank" : "";
            }

            NodeGUID = node.NodeGUID;
        }
    }
}
