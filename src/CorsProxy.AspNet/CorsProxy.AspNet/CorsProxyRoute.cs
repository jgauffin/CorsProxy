using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace CorsProxy.AspNet
{
    /// <summary>
    ///     Our route implementation which uses the <see cref="CorsProxyHttpHandler" />.
    /// </summary>
    public class CorsProxyRoute : Route
    {
        private readonly List<Uri> _allowedUserHostAddresses = new List<Uri>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL
        ///     pattern and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        public CorsProxyRoute(string url)
            : base(url, new CorsProxyRouteHandler())
        {
        }

        /// <summary>
        ///     Allow this host
        /// </summary>
        /// <param name="targetUri">Uri which proxying is allowed to. Typically your back-end URI.</param>
        /// <exception cref="ArgumentNullException">userHostAddress</exception>
        /// <remarks>
        ///     Used to only allow all specified hosts (calls to this method can be chained).
        /// </remarks>
        /// <example>
        ///     <para>In your <c>RouteConfig.cs</c>:</para>
        ///     <code>
        /// routes.EnableCorsProxy().Allow(new Uri("http://localhost:54160/"));
        /// </code>
        /// </example>
        public void Allow(Uri targetUri)
        {
            if (targetUri == null) throw new ArgumentNullException("targetUri");
            _allowedUserHostAddresses.Add(targetUri);
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var routeData = base.GetRouteData(httpContext);
            if (routeData == null)
                return null;

            if (_allowedUserHostAddresses.Any())
            {
                //will report error in the HTTP handler
                var urlString = httpContext.Request.Headers["X-CorsProxy-Url"];
                if (urlString == null)
                    return routeData;

                //invalid host
                var url = new Uri(urlString);
                var found = _allowedUserHostAddresses.Any(x => SameHostAndPort(x, url));
                if (!found)
                {
                    httpContext.Items.Add("CorsProxy-Forbidden", true);
                }
            }

            return routeData;
        }

        /// <summary>
        ///     Returns information about the URL that is associated with the route.
        /// </summary>
        /// <returns>
        ///     An object that contains information about the URL that is associated with the route.
        /// </returns>
        /// <param name="requestContext">An object that encapsulates information about the requested route.</param>
        /// <param name="values">An object that contains the parameters for a route.</param>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }

        private static bool SameHostAndPort(Uri url1, Uri url)
        {
            return url1.Host.Equals(url.Host)
                   && url1.Scheme.Equals(url.Scheme)
                   && url1.Port.Equals(url.Port);
        }
    }
}