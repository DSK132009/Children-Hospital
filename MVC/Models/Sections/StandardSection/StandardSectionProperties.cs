using Kentico.PageBuilder.Web.Mvc;
using MVC.Models.Sections.StandardSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: RegisterSection("UMCHospital.Sections.StandardSection", " Standard Section", typeof(StandardSectionProperties), customViewName: "Sections/_StandardSection", IconClass = "icon-square-dashed-line")]

namespace MVC.Models.Sections.StandardSection
{
    public class StandardSectionProperties : ISectionProperties
    {
    }
}
