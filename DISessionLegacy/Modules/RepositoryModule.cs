using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using DISession.Service;
using DISessionLegacy.Services;
using Microsoft.AspNetCore.Http;

namespace DISessionLegacy.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SessionCompatibilization>().As<ISessionCompatibilization>();
            builder.RegisterType<GiveMeBaboon>().As<IGiveMeBaboon>();

            base.Load(builder);
        }
    }
}