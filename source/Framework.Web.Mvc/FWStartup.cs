using Framework.Web.Mvc.Binders;
using Framework.Web.Mvc.Metadata;
using Framework.Web.Mvc.Razor;
using Framework.Web.Mvc.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Framework.Web.Mvc
{
    /// <summary>
    /// Represents the framework Startup class.
    /// </summary>
    public abstract class FWStartup
    {
        /// <summary>
        /// Creates a new instance of the Framework.Web.Mvc.FWStartup class.
        /// </summary>
        /// <param name="env">The hosting enviroment.</param>
        public FWStartup(IHostingEnvironment env)
        {
            Configuration = ApplicationConfigurationBuilder(env).Build();

            // TODO: allow the user to inform a custom namespace.
            var assembly = Assembly.GetEntryAssembly();
            FWApp.DefaultNamespace = System.IO.Path.GetFileNameWithoutExtension(assembly.ManifestModule.Name);
            FWApp.Cultures = GetApplicationCultures();
            FWApp.WebRootPath = env.WebRootPath;
        }

        /// <summary>
        /// Configures the application services. This method gets called by the runtime.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Transient objects are always different; a new instance is provided to every controller and every service.
            // Scoped objects are the same within a request, but different across different requests
            // Singleton objects are the same for every object and every request(regardless of whether an instance is provided in ConfigureServices)
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Adds the configuration interface as a singleton to allow reading the appsettings file.
            services.AddSingleton<IConfiguration>(Configuration);

            // Adds the custom view engine.
            services.AddSingleton<Microsoft.AspNetCore.Mvc.Razor.IRazorViewEngine, FWRazorViewEngine>();

            // Adds the razor injections
            services.AddSingleton<IFWRazorInjections, FWRazorInjections>();

            services.AddScoped<IStringLocalizer, FWStringLocalizer>();            

            // Configure application localization services
            ConfigureCultures(services);

            // Adds the memory cache service.
            services.AddMemoryCache();

            // Adds the required services for the application session state.
            services.AddSession();

            // Configures application security
            try
            {
                ApplicationSecurity(services);
                FWApp.IsSecure = true;
            }
            catch (NotImplementedException)
            {
                FWApp.IsSecure = false;
            }

            // Configures the cookie policy options.
            services.Configure<CookiePolicyOptions>(options => ConfigureCookiePolicyOptions(options));

            // Adds the custom metadata provider.
            services.AddMvc((m) =>
            {
                // Adds the custom metadata provider
                m.ModelMetadataDetailsProviders.Add(new FWModelMetadataProvider());

                // Adds the custom binder provider
                m.ModelBinderProviders.Insert(0, new FWModelBinderProvider());

                if (FWApp.IsSecure)
                {
                    // if the app has authentication, added some http headers to block the back button from loading pages after logout.
                    m.Filters.Add(new ResponseCacheAttribute()
                    {
                        NoStore = true,
                        Location = ResponseCacheLocation.None
                    });
                }                
            })
            // Adds the framework external controllers.
            .AddApplicationPart(typeof(FWStartup).Assembly)
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Configures application custom services
            ApplicationServices(services);
        }

        /// <summary>
        /// Configures the HTTP request pipeline. This method gets called by the runtime.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Allows the HttpContext to be accessed staticaly.
            FWHttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseSession();

            // Call the application custom configuration.
            ApplicationConfigure(app, env, loggerFactory);

            app.UseCookiePolicy();

            // Configure application routes.
            ConfigureRoutes(app);            
        }

        /// <summary>
        /// Creates a new configuration builder.
        /// </summary>
        /// <param name="env">The hosting enviroment.</param>
        /// <returns>The new configuration builder object.</returns>
        protected virtual ConfigurationBuilder ApplicationConfigurationBuilder(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(env.ContentRootPath)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                   .AddEnvironmentVariables();

            return builder;
        }

        /// <summary>
        /// Configures the application services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        protected virtual void ApplicationServices(IServiceCollection services)
        { }

        /// <summary>
        /// Configures the application authentication and authorization services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        protected virtual void ApplicationSecurity(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the custom configuration for the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        protected abstract void ApplicationConfigure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory);

        /// <summary>
        /// Gets the available cultures for the current application.
        /// </summary>
        protected virtual List<CultureInfo> GetApplicationCultures()
        {
            return null;
        }

        private void ConfigureCultures(IServiceCollection services)
        {
            if (GetApplicationCultures() != null)
            {
                var localizationOptions = new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture(GetApplicationCultures().First().Name, GetApplicationCultures().First().Name),
                    SupportedCultures = GetApplicationCultures(),
                    SupportedUICultures = GetApplicationCultures()
                };
                localizationOptions.RequestCultureProviders = new[]
                {
                    new RouteDataRequestCultureProvider { Options = localizationOptions }
                };

                services.AddSingleton(localizationOptions);
            }
            else
            {
                services.AddSingleton(new RequestLocalizationOptions());
            }
        }

        private void ConfigureRoutes(IApplicationBuilder app)
        {
            if (GetApplicationCultures() != null)
            {
                Routes.Enqueue(routes =>
                {
                    routes.MapRoute(
                            name: "areaCultureRoute",
                            template: "{culture}/{area:exists}/{controller}/{action}/{id?}",
                            defaults: new { controller = "Home", action = "Index" },
                            constraints: new
                            {
                                culture = new RegexRouteConstraint("^[a-zA-Z]{2}(-[a-zA-Z]{2}){0,1}$")
                            });

                    routes.MapRoute(
                        name: "cultureRoute",
                        template: "{culture}/{controller}/{action}/{id?}",
                        defaults: new { controller = "Home", action = "Index" },
                        constraints: new
                        {
                            culture = new RegexRouteConstraint("^[a-zA-Z]{2}(-[a-zA-Z]{2}){0,1}$")
                        });

                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=SetLocalization}/{id?}"
                        );
                });
            }
            else
            {
                Routes.Enqueue((routes) =>
                {
                    routes.MapRoute(
                        name: "areaRoute",
                        template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });
            }

            // Configure the default route
            app.UseMvc(routes =>
            {
                do
                {
                    Routes.Dequeue()(routes);
                } while (Routes.Count > 0);
            });
        }

        /// <summary>
        /// Configures the cookie policy options.
        /// </summary>
        /// <param name="options">The options object reference.</param>
        protected virtual void ConfigureCookiePolicyOptions(CookiePolicyOptions options)
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        }

        /// <summary>
        /// Gets the application routes.
        /// </summary>
        protected Queue<Action<IRouteBuilder>> Routes { get; private set; } = new Queue<Action<IRouteBuilder>>();

        /// <summary>
        /// Gets the application configuration object.
        /// </summary>
        protected IConfigurationRoot Configuration { get; private set; }
    }
}
