using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Breadcrumbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Components.Widgets
{
    public class BreadcrumbViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ComponentViewModel widgetProperties)
        {
            BreadcrumbModel model = new BreadcrumbModel();

            var currentPage = widgetProperties.Page;

            model.BreadcrumbPages.Add(new BreadcrumbPageProperties
            {
                CurrentDocument = true,
                PageName = currentPage.DocumentName,
                PageAlias = currentPage.NodeAliasPath
            });

            var parentDocument = widgetProperties.Page.Parent;

            while (parentDocument != null)
            {
                if (parentDocument.ClassName != "custom.folder" && !string.IsNullOrWhiteSpace(parentDocument.DocumentName))
                {
                    model.BreadcrumbPages.Add(new BreadcrumbPageProperties
                    {
                        CurrentDocument = false,
                        PageName = parentDocument.DocumentName,
                        PageAlias = parentDocument.NodeAliasPath
                    });
                }

                parentDocument = parentDocument.Parent;
            }

            //add home page
            model.BreadcrumbPages.Add(new BreadcrumbPageProperties
            {
                CurrentDocument = false,
                PageName = "Home",
                PageAlias = "/"
            });

            model.BreadcrumbPages.Reverse();

            return View(model);
        }
    }
}
