using System.Web.SessionState;

namespace System.Web
{
    public static class HttpSessionStateExtension
    {
        public static T SessionValue<T>(this HttpSessionState session, string key) where T : class
        {
            if (null != session[key])
            {
                return session[key] as T;
            }
            else
            {
                return null;
            }
        }
    }
}
