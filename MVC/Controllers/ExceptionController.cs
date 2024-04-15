using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace MVC.Controllers
{
    [Route("Exception")]
    public class ExceptionController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public ExceptionController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [Route("ExceptionHandler")]
        public ActionResult ExceptionHandler()
        {
            if (_env != null)
            {
                var filePath = _env.ContentRootPath + "\\wwwroot\\500Error.html";

                if (System.IO.File.Exists(filePath))
                {
                    var htmlData = System.IO.File.ReadAllText(filePath);

                    return Content(htmlData, "text/html", Encoding.UTF8);
                }
            }

            return Content("500 - An error occurred while processing your request");
        }
    }
}
