﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ExpressiveAnnotations.MvcUnobtrusive.Providers;
using GAPS.TSC.Consillium.App_Start;

namespace GAPS.TSC.Consillium
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewModelMappings.Init();

            ModelValidatorProviders.Providers.Remove(
      ModelValidatorProviders.Providers
          .FirstOrDefault(x => x is DataAnnotationsModelValidatorProvider));
            ModelValidatorProviders.Providers.Add(
                new ExpressiveAnnotationsModelValidatorProvider());

        }
    }
}
