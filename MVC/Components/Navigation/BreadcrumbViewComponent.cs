using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Breadcrumbs;

namespace MVC.ViewComponents
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
                if (parentDocument.ClassName != "custom.folder")
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

            model.BreadcrumbPages.Reverse();

            return View(model);
        }
    }
}
