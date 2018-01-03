namespace System
{
    /// <summary>
    /// Common extensions of <see cref="float"/>.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Indicates whether the specified float value is between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The float value to test.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>true if the value is between <paramref name="min"/> and <paramref name="max"/>;otherwise, false.</returns>
        public static bool Between(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }
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