using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Custom;
using CMS.DocumentEngine.Types.UMCHospital;
using CMS.SiteProvider;
using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Widgets.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Components.Widgets
{
    public class InteriorNavigationViewComponent : ViewComponent
    {
        private IPageRetriever _pageRetriever;

        public InteriorNavigationViewComponent(IPageRetriever pageRetriever)
        {
            _pageRetriever = pageRetriever;
        }

        public IViewComponentResult Invoke(ComponentViewModel<InteriorNavigationProperties> widgetProperties)
        {
            var page = widgetProperties.Page;

            if (page != null)
            {
                List<TreeNode> sideNavItems = null;
                
                if (page.Children.Count == 0 && page.NodeLevel > 2)
                {
                    widgetProperties.Properties.Pages = _pageRetriever.Retrieve<Page>(documentQuery => documentQuery.WhereEquals("NodeParentID", page.Parent.Parent.NodeID).WhereEquals("HideFromInteriorNavigation", false).Published().OrderBy("NodeOrder")).ToList<TreeNode>();
                    sideNavItems = _pageRetriever.Retrieve<SideNavigationItem>(documentQuery => documentQuery.WhereEquals("NodeParentID", page.Parent.Parent.NodeID).Published().OrderBy("NodeOrder")).ToList<TreeNode>();
                }
                else
                {
                    widgetProperties.Properties.Pages = _pageRetriever.Retrieve<Page>(documentQuery => documentQuery.WhereEquals("NodeParentID", page.Parent.NodeID).WhereEquals("HideFromInteriorNavigation", false).Published().OrderBy("NodeOrder")).ToList<TreeNode>();
                    sideNavItems = _pageRetriever.Retrieve<SideNavigationItem>(documentQuery => documentQuery.WhereEquals("NodeParentID", page.Parent.NodeID).Published().OrderBy("NodeOrder")).ToList<TreeNode>();
                }
                
                if (sideNavItems != null && sideNavItems.Count > 0)
                {
                    widgetProperties.Properties.Pages.AddRange(sideNavItems);
                    widgetProperties.Properties.Pages = widgetProperties.Properties.Pages.OrderBy(p => p.NodeOrder).ToList();
                }
            }

            return View(widgetProperties);
        }
    }
}
