namespace System
{
    /// <summary>
    /// Common extensions of <see cref="float"/>.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Convert specified float to an <see cref="decimal"/> value.
        /// </summary>
        /// <param name="value">The float value.</param>
        /// <returns>The converted <see cref="decimal"/> value.</returns>
        public static decimal ToDecimal(this float value)
        {
            return Convert.ToDecimal(value);
        }
    }
}