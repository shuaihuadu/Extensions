using System.Web.SessionState;

namespace System.Web
{
    /// <summary>
    /// The <see cref="HttpSessionState"/> extensions.
    /// </summary>
    public static class HttpSessionStateExtension
    {
        /// <summary>
        /// Get the vaule from <see cref="HttpSessionState"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session">The session.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static T SessionValue<T>(this HttpSessionState session, string key) where T : class
        {
            if (null != session[key])
            {
                return session[key] as T;
            }
            else
            {
                return default(T);
            }
        }
    }
}
