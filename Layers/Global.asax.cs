using Layers.Models;
using Layers.Util;
using LayersDAL.EF;
using LayersDLL.Infrastructure;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using Layers.App_Start;
using Ninject.Web.WebApi.Filter;

namespace Layers
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(new StoreDbInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            NinjectModule orderModule = new OrderModule();
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            
            var kernel = new StandardKernel(orderModule, serviceModule);
            kernel.Bind<DefaultFilterProviders>().ToSelf().WithConstructorArgument(GlobalConfiguration.Configuration.Services.GetFilterProviders());
            var ninjectResolver = new Util.NinjectDependencyResolver(kernel);
            //DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            DependencyResolver.SetResolver(ninjectResolver);
            GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver;

            ModelValidatorProviders.Providers.Clear();
        }
    }
}
