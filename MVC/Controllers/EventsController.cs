using CMS.DocumentEngine;
using Events;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class EventsController : Controller
    {
        private readonly IPageRetriever _pageRetriever;
        private readonly IPageDataContextInitializer _pageDataContextInitializer;
        private readonly IEventSessionInfoProvider _eventSessionInfoProvider;

        public EventsController(IPageRetriever pageRetriever, IPageDataContextInitializer pageDataContextInitializer, IEventSessionInfoProvider eventSessionInfoProvider)
        {
            //Dependency injection for required features of the controller
            _pageRetriever = pageRetriever;
            _pageDataContextInitializer = pageDataContextInitializer;
            _eventSessionInfoProvider = eventSessionInfoProvider;
        }

        public ActionResult Index(string EventCodeName)
        {
            var eventSession = _eventSessionInfoProvider.Get(EventCodeName);

            if (eventSession == null)
            {
                //Event session not found; return 404
                return NotFound();
            }

            var page = _pageRetriever.Retrieve<TreeNode>(query => query.Path("/")).FirstOrDefault();

            if (page == null)
            {
                //Dummy page not found; return 404
                //Dummy page in this case is the root, if that's not found we have bigger problems
                return NotFound();
            }

            //Using the site root as a dummy page so we are updating the name to the Event name before
            //initializing the context so the layout knows what to use for the page title
            //Note that this is not actually saved to the page, so the page name in the database stays the same,
            //this is just to trick the context into thinking that the page is named this for the metadata attributes
            page.DocumentName = eventSession.EventName;

            //Initialize page context so Layout has the context of the current page            
            _pageDataContextInitializer.Initialize(page);

            var model = new EventDetailModel
            {
                Event = eventSession,
                Page = page
            };

            return View("_EventDetail", model);
        }
    }
}
