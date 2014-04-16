using System.Linq;
using System.Net;
using System.Web;

namespace CorsProxy.AspNet
{
    /// <summary>
    /// Extension methods used to copy headers from the different requests/responses.
    /// </summary>
    public static class HeaderExtensions
    {
        private static readonly string[] IgnoredRequestHeaders = new[]
        {
            "Connection", "Content-Length", "Host", "Content-Type", "Accept", "Referer", "UrlReferer", "UserAgent",
            "User-Agent"
        };
        private static readonly string[] IgnoredResponseHeaders = new[]
        {
            "Connection", "Content-Length", "Server", "Content-Type", "X-AspNet-Version"
        };

        /// <summary>
        /// Copy headers from the ajax request to our outbound HTTP request.
        /// </summary>
        /// <param name="source">Incoming ajax request</param>
        /// <param name="destination">HTTP request used to contact the real server.</param>
        public static void CopyHeadersTo(this HttpRequest source, HttpWebRequest destination)
        {
            var cookieContainer = new CookieContainer();
            destination.CookieContainer = cookieContainer;
            foreach (string name in source.Headers)
            {
                if (IgnoredRequestHeaders.Contains(name))
                    continue;
                var value = source.Headers[name];
                if (string.IsNullOrEmpty(value))
                    continue;
                destination.Headers.Add(name, value);
            }
        }

        /// <summary>
        /// Copy headers from the response that we received from the real server to the response that we are sending for the ajax request.
        /// </summary>
        /// <param name="source">HTTP response from the proxied server</param>
        /// <param name="destination">HTTP response being sent as a reply to the Ajax request.</param>
        public static void CopyHeadersTo(this HttpWebResponse source, HttpResponse destination)
        {
            foreach (string name in source.Headers)
            {
                if (IgnoredResponseHeaders.Contains(name))
                    continue;
                var value = source.Headers[name];
                if (string.IsNullOrEmpty(value))
                    continue;
                destination.AddHeader(name, value);
            }
        }
    }
}