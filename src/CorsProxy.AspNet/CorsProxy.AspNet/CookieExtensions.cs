using System.Net;
using System.Web;

namespace CorsProxy.AspNet
{
    /// <summary>
    /// Extensions used to copy cookies
    /// </summary>
    public static class CookieExtensions
    {
        /// <summary>
        /// Copy cookies from the incoming request to the outbound request
        /// </summary>
        /// <param name="source">Incoming ajax request</param>
        /// <param name="destination">Request used to call the intended destination</param>
        public static void CopyCookiesTo(this HttpRequest source, HttpWebRequest destination)
        {
            var cookieContainer = new CookieContainer();
            destination.CookieContainer = cookieContainer;
            foreach (HttpCookie cookie in source.Cookies)
            {
                cookieContainer.Add(new Cookie(cookie.Name, cookie.Value));
            }
        }

        /// <summary>
        /// Copy cookies from the inbound response to the outbound response
        /// </summary>
        /// <param name="source">Reply from the intended destination</param>
        /// <param name="destination">response being sent back to the ajax request</param>
        public static void CopyCookiesTo(this HttpWebResponse source, HttpResponse destination)
        {
            foreach (HttpCookie cookie in source.Cookies)
            {
                destination.SetCookie(new HttpCookie(cookie.Name, cookie.Value));
            }
        }
    }
}