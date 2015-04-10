using System;
using System.Web.Routing;

namespace CorsProxy.AspNet
{
    /// <summary>
    /// Wrapper for our <c>EnableCorsProxy</c> extension method
    /// </summary>
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// Activates the CorsProxy.
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="url">Default route is "corsproxy/". It's the URI that the Ajax request must redirect to</param>
        public static CorsProxyRoute EnableCorsProxy(this RouteCollection routes, string url = "corsproxy/")
        {
            if (routes == null) throw new ArgumentNullException("routes");
            if (url == null) throw new ArgumentNullException("url");
            var route = new CorsProxyRoute(url);
            routes.Add(route);
            return route;
        }
    }
}
