﻿using System.Web;
using System.Web.Routing;

namespace CorsProxy.AspNet
{
    /// <summary>
    /// Route handler which only purpose is to act as a factory for <see cref="CorsProxyHttpHandler"/>.
    /// </summary>
    public class CorsProxyRouteHandler : IRouteHandler
    {
        /// <summary>
        /// Provides the object that processes the request.
        /// </summary>
        /// <returns>
        /// An object that processes the request.
        /// </returns>
        /// <param name="requestContext">An object that encapsulates information about the request.</param>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new CorsProxyHttpHandler();
        }
    }
}