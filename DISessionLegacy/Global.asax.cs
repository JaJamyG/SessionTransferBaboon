using Autofac;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Web;
using DISession.Service;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Web.Compilation;
using System;
using System.Linq;
using System.Net.Mime;
using System.Web;
using Autofac.Integration.Mvc;
using DISessionLegacy.App_Start;

namespace DISessionLegacy
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SystemWebAdapterConfiguration.AddSystemWebAdapters(this)
                .AddProxySupport(options => options.UseForwardedHeaders = true)
                .AddJsonSessionSerializer(options =>
                {
                    options.RegisterKey<string>("Baboon");
                })
                .AddRemoteAppServer(options => options.ApiKey = "6b5b73c9-454e-4916-918e-34e16e27e72f")
                .AddSessionServer();
        }
    }
}
