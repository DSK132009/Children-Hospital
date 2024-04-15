using Kentico.PageBuilder.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterSection("UMCHospital.Sections.StatCardContainerSection", "Stat Card Container Section", customViewName: "Sections/_StatCardContainerSection", IconClass = "icon-book-opened")]

namespace MVC.Models.Sections.StatCardContainer
{
    public class StatCardContainerSection : ISectionProperties
    {
    }
}
