using System.Web.Routing;

namespace CorsProxy.AspNet
{
    /// <summary>
    /// Our route implementation which uses the <see cref="CorsProxyHttpHandler"/>.
    /// </summary>
    public class CorsProxyRoute : Route
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Web.Routing.Route"/> class, by using the specified URL pattern and handler class. 
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        public CorsProxyRoute(string url)
            : base(url, new CorsProxyRouteHandler())
        {
        }
    }
}