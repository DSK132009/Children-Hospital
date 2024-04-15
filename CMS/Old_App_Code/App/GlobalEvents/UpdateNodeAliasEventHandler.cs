using CMS;
using CMS.Core;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Routing.Internal;
using CMSApp.Old_App_Code.App.GlobalEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

[assembly: RegisterModule(typeof(UpdateNodeAliasEventHandler))]

namespace CMSApp.Old_App_Code.App.GlobalEvents
{
    //With how K13 works, Node Alias and the actual page URL can be out of sync
    //This handler ensures that when a page's URL is updated, it's Node Alias is updated as well

    public class UpdateNodeAliasEventHandler : Module
    {
        public UpdateNodeAliasEventHandler() : base("UpdateNodeAliasEventHandler")
        {
        }

        protected override void OnInit()
        {
            base.OnInit();

            DocumentEvents.Insert.After += DocumentUpdateNodeAlias;
            DocumentEvents.Update.After += DocumentUpdateNodeAlias;
        }

        private void DocumentUpdateNodeAlias(object sender, DocumentEventArgs e)
        {
            //Added to keep NodeAlias and URL Slug in sync
            var editedNode = e.Node;

            if (editedNode != null && editedNode.HasUrl())
            {
                var urlInfo = PageUrlPathInfo.Provider.Get().WhereEquals("PageUrlPathNodeID", editedNode.NodeID).FirstOrDefault();
                //var formerUrlInfo = PageFormerUrlPathInfo.Provider.Get().WhereEquals("PageFormerUrlPathID", editedNode.NodeID).FirstOrDefault();

                if (urlInfo != null && urlInfo.PageUrlPathUrlPath.ToLower() != editedNode.NodeAliasPath.ToLower().TrimStart('/'))
                {
                    string newSlug;
                    //URL Slug is longer than 50 characters; Truncate to 50;
                    if (urlInfo.PageUrlPathUrlPath.Split('/').Last().Length > 50)
                    {
                        newSlug = urlInfo.PageUrlPathUrlPath.Split('/').Last().Substring(0, 49).TrimEnd('-'); //Technically 50 would work, but making it 49 to account for special cases

                        if (new PageUrlPathSlugUpdater(editedNode).TryUpdate(newSlug.ToLower(), out var collisions))
                        {
                            editedNode.NodeAlias = newSlug;
                            editedNode.Update();
                        }
                        else
                        {
                            Service.Resolve<IEventLogService>().LogWarning("UpdateNodeAliasEventHandler", "WARNING", $"A collision was detected when synchronizing the Alias and URL Slug for the page '{editedNode.DocumentName}'. Node alias was not updated. Manual review may be required.");
                        }
                    }
                    else
                    {
                        //Update Node Alias to match URL Slug
                        editedNode.NodeAlias = urlInfo.PageUrlPathUrlPath.Split('/').Last();
                        editedNode.Update();
                    }
                }
            }
        }
    }
}