using Core.Configuration;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XperienceAdapter.Models;
using XperienceAdapter.Repositories;

namespace MVC.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IPageRetriever _pageRetriever;
        private readonly IPageDataContextInitializer _pageDataContext;
        private IExceptionHandlerPathFeature ExceptionHandlerPathFeature => HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        public ErrorController(IPageRetriever pageRetriever, IPageDataContextInitializer pageDataContext)
        {
            _pageRetriever = pageRetriever;
            _pageDataContext = pageDataContext;
        }

        public IActionResult Index(int code)
        {
            if (code == 404)
            {
                var notFoundPage = _pageRetriever.Retrieve<CMS.DocumentEngine.Types.Custom.Page>().Where(x => x.NodeAliasPath == "/404").FirstOrDefault();

                if (notFoundPage != null)
                {
                    var model = Kentico.PageBuilder.Web.Mvc.ComponentViewModel.Create(notFoundPage);

                    _pageDataContext.Initialize(model.Page);

                    return View("PageTemplates/_StandardPageTemplate", model);
                }
            }

            return StatusCode(code);
        }
    }
}
