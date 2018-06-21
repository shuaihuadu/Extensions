using Extensions.Resources;

namespace System
{
    /// <summary>
    /// The <see cref="bool"/> extensions.
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// To the yes or no string.
        /// </summary>
        /// <param name="value">if set to <c>true</c> return <paramref name="yesString"/> ;otherwise <paramref name="noString"/> .</param>
        /// <param name="yesString">The yes string.</param>
        /// <param name="noString">The no string.</param>
        /// <returns><paramref name="yesString"/> or <paramref name="noString"/></returns>
        public static string ToYesOrNoString(this bool value, string yesString = "", string noString = "")
        {
            if (yesString.IsNullOrBlank())
            {
                yesString = Resource.Yes;
            }
            if (noString.IsNullOrBlank())
            {
                noString = Resource.No;
            }
            return value ? yesString : noString;
        }
    }
}
