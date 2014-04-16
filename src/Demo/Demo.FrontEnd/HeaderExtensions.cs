using System.Linq;
using System.Net;
using System.Web;

namespace Demo.FrontEnd
{
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