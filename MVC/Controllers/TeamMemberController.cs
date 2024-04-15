using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Custom;
using CMS.Helpers;
using CMS.SiteProvider;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.TeamMember;
using SimpleMvcSitemap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMembers;

namespace MVC.Controllers
{
    public class TeamMemberController : Controller
    {
        private readonly IPageRetriever _pageRetriever;
        private readonly IPageDataContextInitializer _pageDataContextInitializer;
        private readonly ITeamMemberInfoProvider _teamMemberInfoProvider;

        public TeamMemberController(IPageRetriever pageRetriever, IPageDataContextInitializer pageDataContextInitializer, ITeamMemberInfoProvider teamMemberInfoProvider)
        {
            //Dependency injection for required features of the controller
            _pageRetriever = pageRetriever;
            _pageDataContextInitializer = pageDataContextInitializer;
            _teamMemberInfoProvider = teamMemberInfoProvider;
        }

        public ActionResult Index(int TeamMemberId)
        {
            var teamMember = _teamMemberInfoProvider.Get(TeamMemberId);

            if(teamMember == null)
            {
                //Teammember not found; return 404
                return NotFound();
            }

            if(!String.IsNullOrEmpty(teamMember.ExternalLink))
            {
                //Team member links to another URL; Redirect user to this URL
                return Redirect(teamMember.ExternalLink);
            }

            if(String.IsNullOrEmpty(teamMember.Bio))
            {
                //Team member has no bio or external link filled out; Return 404
                return NotFound();
            }

            var page = _pageRetriever.Retrieve<TreeNode>(query => query.Path("/")).FirstOrDefault();            

            if(page == null)
            {
                //Dummy page not found; return 404
                //Dummy page in this case is the root; if that's not found we have bigger problems
                return NotFound();
            }

            //Using the site root as a dummy page so we are updating the name to the Team Member name before
            //initializing the context so the layout knows what to use for the page title
            //Note that this is not actually saved to the page, so the page name in the database stays the same,
            //this is just to trick the context into thinking that the page is named this for the metadata attributes
            page.DocumentName = teamMember.Name;

            //Initialize page context so Layout has the context of the current page            
            _pageDataContextInitializer.Initialize(page);

            var model = new TeamMemberDetailModel
            {
                TeamMember = teamMember,
                Page = page
            };
            
            return View("_TeamMemberDetail", model);
        }
    }
}
