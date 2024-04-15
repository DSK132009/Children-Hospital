using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class SitemapController : Controller
    {
        public ActionResult Index()
        {
            var nodes = CacheHelper.GetItem("Custom_XmlSiteMapNodes") as List<SitemapNode>;

            if (nodes == null)
            {
                var siteName = SiteContext.CurrentSiteName;
                nodes = getSiteMapModels().ToList();

                string dependencyCacheKey = $"node|{siteName}|/|childnodes";
                var dependency = CacheHelper.GetCacheDependency(dependencyCacheKey);

                CacheHelper.Add("Custom_XmlSiteMapNodes", nodes, dependency, DateTime.Now.AddHours(24), CacheConstants.NoSlidingExpiration);
            }

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }

        private IEnumerable<SitemapNode> getSiteMapModels()
        {
            var docs = DocumentHelper.GetDocuments().WhereNull("NodeLinkedNodeID").Types("custom.page").OrderBy("NodeAliasPath");
            var homePageAlias = SettingsKeyInfoProvider.GetValue("CMSDefaultUrlPathPrefix").ToLower();

            foreach (var doc in docs)
            {
                if (doc.DocumentSearchExcluded)
                {
                    continue;
                }
                yield return new SitemapNode(doc.NodeAliasPath.ToLower() == homePageAlias ? "/" : doc.NodeAliasPath.ToLower())
                {
                    LastModificationDate = doc.DocumentModifiedWhen
                };
            }
        }
    }
}
