using Autofac.Integration.Mvc;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DISessionLegacy.Modules;

namespace DISessionLegacy.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            //Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            //Register our Rep dependencies
            builder.RegisterModule(new RepositoryModule());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}