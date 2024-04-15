using CMS.DocumentEngine;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TreeNode = CMS.DocumentEngine.TreeNode;

namespace MVC.Models.Navigation
{
    public class NavigationViewModelExtensions
    {
        private List<TreeNode> allNodes { get; set; }

        public NavigationViewModel CreateMenu(NavigationViewModel nav, string docType)
        {
            var columns = new string[] { "DocumentName", "Url", "Target", "CssClass", "NodeLevel", "NodeOrder", "NodeID", "AriaLabel", "NodeParentID", "IconUrl", "IsSpaced", "IsMobileOnly", "StickyIconOnMobile", "IsDesktopOnly", "IsHighlighted" };


            var navNode = DocumentHelper.GetDocuments("UMCHospital.navigation").WhereEquals("NavigationType", docType).PublishedVersion().NestingLevel(1).FirstOrDefault();

            if (navNode != null)
            {
                allNodes = DocumentHelper.GetDocuments("UMCHospital.navigationlink").Path(navNode.NodeAliasPath, PathTypeEnum.Children).Columns(columns).PublishedVersion().ToList();

                if (allNodes != null)
                {
                    foreach (var child in navNode.Children)
                    {
                        nav.Links.Add(CreateLink(child));
                    }

                }
            }
            return nav;
        }

        private NavigationLinkViewModel CreateLink(TreeNode Node)
        {
            var linkNode = allNodes.FirstOrDefault(x => x.NodeID == Node.NodeID);
            if (linkNode != null)
            {
                var childLink = new NavigationLinkViewModel(linkNode);
                foreach (var child in Node.Children)
                {
                    var gchild = CreateLink(child);
                    childLink.Links.Add(gchild);
                }

                return childLink;
            }
            return new NavigationLinkViewModel();
        }
    }

}