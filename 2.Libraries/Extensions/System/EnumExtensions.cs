namespace System
{
    /// <summary>
    /// Common extensions of <see cref="Enum"/>.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Indicates whether the specified enum value and the specified int have the same value.
        /// </summary>
        /// <param name="enum">The enum to test.</param>
        /// <param name="value">The int value to compare to the <paramref name="enum"/>.</param>
        /// <returns>true if the value parameter is the same as the value of <paramref name="enum"/>;othervise, false.</returns>
        public static bool Equals(this Enum @enum, int value)
        {
            try
            {
                return @enum.IntValue() == value;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Indicates whether the specified enum value and the specified byte have the same value.
        /// </summary>
        /// <param name="enum">The enum to test.</param>
        /// <param name="value">The byte value to compare to the <paramref name="enum"/>.</param>
        /// <returns>true if the value parameter is the same as the value of <paramref name="enum"/>;othervise, false.</returns>
        public static bool Equals(this Enum @enum, byte value)
        {
            try
            {
                return @enum.ByteValue() == value;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Indicates whether the specified enum value and the specified byte have the same value(Ignore case during the comparison).
        /// </summary>
        /// <param name="enum">The enum to test.</param>
        /// <param name="value">The byte value to compare to the <paramref name="enum"/>.</param>
        /// <returns>true if the value parameter is the same as the value of <paramref name="enum"/>;othervise, false.</returns>
        public static bool Equals(this Enum @enum, string value)
        {
            if (value.IsNullOrBlank())
            {
                return false;
            }
            try
            {
                return @enum.ToString().ToLower() == value.TrimBlank().ToLower();
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Returns the int value of the specified <paramref name="enum"/>.
        /// </summary>
        /// <param name="enum">The enum to convert.</param>
        /// <returns>The int value of the current <paramref name="enum"/></returns>
        public static int IntValue(this Enum @enum)
        {
            return Convert.ToInt32(@enum);
        }
        /// <summary>
        /// Returns the byte value of the specified <paramref name="enum"/>.
        /// </summary>
        /// <param name="enum">The enum to convert.</param>
        /// <returns>The byte value of the current <paramref name="enum"/></returns>
        public static byte ByteValue(this Enum @enum)
        {
            return Convert.ToByte(@enum);
        }
    }
}