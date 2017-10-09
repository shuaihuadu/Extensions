namespace System.Web.Mvc
{
    /// <summary>
    /// The <see cref="HttpBrowserCapabilitiesBase"/> extensions.
    /// </summary>
    public static class HttpBrowserCapabilitiesBaseExtensions
    {
        /// <summary>
        /// Determines whether the request browser is MicroMessage browser.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <returns>
        ///   <c>true</c> if is MicroMessage browser; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMicroMessageBrowser(this HttpBrowserCapabilitiesBase browser)
        {
            return GetUserAgent(browser).ToLower().Contains("micromessage");
        }

        /// <summary>
        /// Determines whether the request browser is QQ browser.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <returns>
        ///   <c>true</c> if is QQ browser; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsQQBrowser(this HttpBrowserCapabilitiesBase browser)
        {
            return GetUserAgent(browser).ToLower().Contains("qqbrowser");
        }
        /// <summary>
        /// Gets the user agent.
        /// </summary>
        /// <param name="browser">The request browser.</param>
        /// <returns>The request user agent string.</returns>
        static string GetUserAgent(HttpBrowserCapabilitiesBase browser)
        {
            var userAgent = browser[""];
            if (string.IsNullOrEmpty(userAgent))
            {
                userAgent = HttpContext.Current.Request.UserAgent;
            }
            if (string.IsNullOrEmpty(userAgent))
            {
                return string.Empty;
            }
            return userAgent;
        }
    }
}
