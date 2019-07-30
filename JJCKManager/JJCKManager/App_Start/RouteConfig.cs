using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JJCKManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Account",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "SystemAccount", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "IotTempData",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "IotTempData", action = "Index", id = UrlParameter.Optional }
                );

        }
    }
}
