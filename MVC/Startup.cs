using Autofac;
using CMS.SiteProvider;
using Core.Configuration;
using Kentico.Activities.Web.Mvc;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MVC.Configuration;
using MVC.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using URLRedirection;
using XperienceAdapter.Localization;

namespace MVC
{
    public class Startup
    {
        private const string AUTHENTICATION_COOKIE_NAME = "identity.authentication";
        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

#nullable enable
        public IConfigurationSection? Options { get; }
#nullable disable

        public AutoFacConfig AutoFacConfig => new AutoFacConfig();

        public Startup(IWebHostEnvironment environment,
                       IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
            Options = configuration.GetSection(nameof(XperienceOptions));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var options = new PageBuilderOptions()
            {
                DefaultSectionIdentifier = "UMCHospital.Sections.StandardSection",
                RegisterDefaultSection = false
            };

            // Enable desired Kentico Xperience features
            var kenticoServiceCollection = services.AddKentico(features =>
            {
                features.UsePageBuilder(options);
                features.UseActivityTracking();
                // features.UseABTesting();
                // features.UseWebAnalytics();
                // features.UseEmailTracking();
                // features.UseCampaignLogger();
                // features.UseScheduler();
                features.UsePageRouting(new PageRoutingOptions
                {
                    EnableAlternativeUrls = true,
                    EnableRouting = true
                });
            }).SetAdminCookiesSameSiteNone();

            if (Environment.IsDevelopment())
            {
                // By default, Xperience requires a secure connection (HTTPS) if administration and live site applications
                // are hosted on separate domains. This configuration simplifies the initial setup of the development
                // or evaluation environment without a the need for secure connection. The system ignores authentication
                // cookies and this information is taken from the URL.
                kenticoServiceCollection.DisableVirtualContextSecurityForLocalhost();
            }

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = typeof(SharedResource).GetTypeInfo().Assembly.GetName().Name;

                        return factory.Create("SharedResource", assemblyName);
                    };
                });

            services.Configure<XperienceOptions>(Options);

            if (!Environment.IsDevelopment())
            {
                //Force HTTPS Redirection in Kestrel/Edge deployments
                services.AddHttpsRedirection(options =>
                {
                    options.HttpsPort = 443;
                    options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
                });
            }
        }

        /// <summary>
        /// Registers a handler in case Xperience is not initialized yet.
        /// </summary>
        /// <param name="builder">Container builder.</param>
        private void RegisterInitializationHandler(ContainerBuilder builder) =>
            CMS.Base.ApplicationEvents.Initialized.Execute += (sender, eventArgs) => AutoFacConfig.ConfigureContainer(builder);

        public void ConfigureContainer(ContainerBuilder builder)
        {
            try
            {
                AutoFacConfig.ConfigureContainer(builder);
            }
            catch
            {
                RegisterInitializationHandler(builder);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Force HTTPS in non-local environment - NOTE: An SSL port must be configured in the appsettings.json file on the server for this to work
                app.UseHttpsRedirection();

                //500 Error Handling
                app.UseExceptionHandler("/Exception/ExceptionHandler");
            }

            app.Use(async (context, next) =>
            {
                //Redirection Module Handling and Lowercase URL Enforcement
                //Custom lowercase URL enforcement needed as these custom handling methods cause the Kentico lowercase enforcement to no longer function
                string url = context.Request.Path.Value;
                string handler = url.Split('/')[1].ToLower();
                string[] aliasesToExclude = { "cmspages", "cmsmodules", "cmsformcontrols", "cmsadmincontrols", "admin", "getattachment", "getfile", "getmedia", "getmetafile", "app_themes", "cmsapi", "socialmediaapi", "searchapi", "formsapi", "api", "cmsctx", "_content", "eventregistration" };

                if (!aliasesToExclude.Contains(handler) && !handler.StartsWith("kentico") && SiteContext.CurrentSite != null)
                {
                    //Originally was a static class but changed to non-static as it caused issues with caching
                    var redirectionHelper = new RedirectionHelper();

                    var response = redirectionHelper.GetRedirect(url);

                    //Redirection Check and Handler
                    if (!String.IsNullOrEmpty(response.FinalUrl))
                    {
                        context.Response.Redirect(response.FinalUrl, response.PermanentRedirect);
                        return;
                    }

                    //Lowercase URL Check and Enforcement
                    if (url != url.ToLower())
                    {
                        context.Response.Redirect(url.ToLower(), response.PermanentRedirect);
                        return;
                    }
                }

                await next();
            });

            //404 Handling
            app.UseCustomStatusCodePagesWithReExecute("/Error/{0}");

            //Add MIME type for webmanifest favicon file
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".webmanifest"] = "application/manifest+json";

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider,
                HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress,

                OnPrepareResponse = (context) =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();

                    headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(14)
                    };
                }
            });

            app.UseForwardedHeaders();

            app.UseResponseCaching();

            app.UseKentico();

            app.UseCookiePolicy();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Kentico().MapRoutes();
                //Add any custom routes needed here AFTER MapRoutes call

                //Teammember Detail Page Controller
                endpoints.MapControllerRoute(
                    name: "TeamMember",
                    pattern: "/TeamMember/{TeamMemberId}",
                    defaults: new { controller = "TeamMember", action = "Index" });

                //Event Detail Page Controller
                endpoints.MapControllerRoute(
                    name: "Events",
                    pattern: "/Events/{EventCodeName}",
                    defaults: new { controller = "Events", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "EventRegistration",
                    pattern: "EventRegistration",
                    defaults: new { controller = "EventRegistration", action = "Index" });

                //Sitemap.xml endpoint
                endpoints.MapControllerRoute(
                    name: "SiteMap",
                    pattern: "sitemap.xml",
                    defaults: new { controller = "Sitemap", action = "Index" });

                //Robots.txt endpoint
                endpoints.MapControllerRoute(
                    name: "RobotsTxt",
                    pattern: "robots.txt",
                    defaults: new { controller = "RobotsTxt", action = "Index" });

                //404 Error Handling
                endpoints.MapControllerRoute(
                    name: "error",
                    pattern: "/Error/{code}",
                    defaults: new { controller = "Error", action = "Index" });
            });

            FormBuilderStaticMarkupConfiguration.SetGlobalRenderingConfigurations();
        }
    }
}