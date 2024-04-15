using CMS.Core;
using CMS.EventLog;
using CMS.Helpers;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLRedirection
{
    public class RedirectionHelper
    {
        public RedirectionResponseModel GetRedirect(string url)
        {
            IEventLogService eventLog = Service.Resolve<IEventLogService>();

            try
            {
                var siteID = SiteContext.CurrentSiteID;

                url = url.Contains("?") ? url.Split('?')[0] : url;
                url = url.TrimEnd('/');
                url = url.ToLower();

                var redirectResult = CacheHelper.GetItem($"RedirectInfo_{url}_{siteID}") as RedirectionResponseModel;

                if (redirectResult == null)
                {
                    string dependencyCacheKey = "urlredirection.redirection|all";
                    var dependency = CacheHelper.GetCacheDependency(dependencyCacheKey);

                    var lstRedirectionObjects = CacheHelper.GetItem($"RedirectionObjectList_{siteID}") as List<RedirectionInfo>;

                    if (lstRedirectionObjects == null)
                    {
                        //Cache all redirect objects as a list to increase speed of future checks for non-cached results
                        lstRedirectionObjects = RedirectionInfoProvider.ProviderObject.Get().WhereEquals("RedirectionSiteID", siteID).ToList();
                        CacheHelper.Add($"RedirectionObjectList_{siteID}", lstRedirectionObjects, dependency, DateTime.Now.AddHours(24), CacheConstants.NoSlidingExpiration);
                    }

                    redirectResult = new RedirectionResponseModel();
                    var redirectInfo = lstRedirectionObjects.Where(x => x.RedirectionOriginalUrl.ToLower() == url || x.RedirectionOriginalUrl.ToLower() == $"{url}/").FirstOrDefault();
                    if (redirectInfo != null)
                    {
                        string finalUrl = redirectInfo.RedirectionTargetUrl.TrimStart('~');

                        if (finalUrl.TrimEnd('/').ToLower() == url)
                        {
                            eventLog.LogWarning("Redirection Loop Detected", "REDIRECT_IGNORED", "A redirection loop was detected and redirection processing ceased for the request. This usually occurs when a original and destination URL are the same");
                            //Cache and return an empty result instead of null so the cache check doesn't do the above logic and flood the event log for every additional looping redirect detected for this path
                            redirectResult = new RedirectionResponseModel();
                        }
                        else
                        {
                            redirectResult.FinalUrl = finalUrl;
                            redirectResult.PermanentRedirect = redirectInfo.RedirectionType == "301";
                        }
                    }
                    else
                    {
                        //Cache and return an empty result instead of null so the cache check doesn't do the above logic for every request that doesn't redirect
                        redirectResult = new RedirectionResponseModel();
                    }                    

                    CacheHelper.Add($"RedirectInfo_{url}_{siteID}", redirectResult, dependency, DateTime.Now.AddMinutes(10), CacheConstants.NoSlidingExpiration);
                }

                return redirectResult;
            }
            catch(Exception e)
            {                
                eventLog.LogException("RedirectionMethods.cs", "REDIRECT_FAILED", e, additionalMessage: "An exception occurred while processing a redirect. See exception for more details.");
            }

            return new RedirectionResponseModel();
        }
    }
}
