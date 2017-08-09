namespace System
{
    /// <summary>
    /// Common extensions of <see cref="byte"/>.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Indicates whether the specified byte value is between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The byte value to test.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>true if the value is between <paramref name="min"/> and <paramref name="max"/>;otherwise, false.</returns>
        public static bool Between(this byte value, byte min, byte max)
        {
            return value >= min && value <= max;
        }
        /// <summary>
        /// Convert specified byte to an <see cref="Enum"/> value.
        /// </summary>
        /// <typeparam name="T">The type of enum.</typeparam>
        /// <param name="value">The byte value.</param>
        /// <returns>The converted <see cref="Enum"/> value.</returns>
        public static T ToEnum<T>(this byte value)
        {
            return (T)(object)value;
        }
    }
}