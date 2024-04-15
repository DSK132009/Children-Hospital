using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.Breadcrumbs
{
    public class BreadcrumbModel
    {
        public List<BreadcrumbPageProperties> BreadcrumbPages { get; set; }

        public BreadcrumbModel()
        {
            BreadcrumbPages = new List<BreadcrumbPageProperties>();
        }
    }

    public class BreadcrumbPageProperties
    {
        public string PageName { get; set; }
        public string PageAlias { get; set; }
        public bool CurrentDocument { get; set; }
    }
}
