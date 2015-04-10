using System;
using System.Web.Mvc;
using System.Web.Routing;
using CorsProxy.AspNet;

namespace Demo.FrontEnd.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.EnableCorsProxy()
                .Allow(new Uri("http://localhost:54160/"));//optional, all hosts by default

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}