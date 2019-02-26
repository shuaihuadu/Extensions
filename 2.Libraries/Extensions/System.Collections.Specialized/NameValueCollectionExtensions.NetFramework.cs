using System.Linq;
using System.Web.Routing;
namespace System.Collections.Specialized
{
    /// <summary>
    /// Common extensions of <see cref="NameValueCollection"/>
    /// </summary>
    public static partial class NameValueCollectionExtensions
    {
        /// <summary>
        /// Convert the <see cref="NameValueCollection"/> to <see cref="RouteValueDictionary"/>.
        /// </summary>
        /// <param name="collection">The <see cref="NameValueCollection"/></param>
        /// <returns>The <see cref="RouteValueDictionary"/> converted.</returns>
        public static RouteValueDictionary ToRouteValues(this NameValueCollection collection)
        {
            if (collection.IsNull() || collection.HasKeys() == false)
            {
                return new RouteValueDictionary();
            }
            var routeValues = new RouteValueDictionary();
            foreach (string key in collection.AllKeys)
            {
                routeValues.Add(key, collection[key]);
            }
            return routeValues;
        }
    }
}
