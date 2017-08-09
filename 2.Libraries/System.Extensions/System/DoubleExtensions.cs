﻿namespace System
{
    /// <summary>
    /// Common extensions of <see cref="double"/>.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Indicates whether the specified double value is between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The double value to test.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>true if the value is between <paramref name="min"/> and <paramref name="max"/>;otherwise, false.</returns>
        public static bool Between(this double value, double min, double max)
        {
            return value >= min && value <= max;
        }
        /// <summary>
        /// Convert specified double value to a file size string.
        /// <para>Supported:KB MB GB TB PB EB</para>
        /// </summary>
        /// <param name="size">The file size value.</param>
        /// <returns>The file size string. eg:MB,GB...</returns>
        public static string ToFileSizeString(this double size)
        {
            if (size < 0)
            {
                throw new ArgumentException("Size must greater or equals than zero.", "size");
            }
            if (size < 1024) { return (size).ToString("F0") + " bytes"; }
            if (size < Math.Pow(1024, 2)) { return (size / 1024).ToString("F0") + "KB"; }
            if (size < Math.Pow(1024, 3)) { return (size / Math.Pow(1024, 2)).ToString("F0") + "MB"; }
            if (size < Math.Pow(1024, 4)) { return (size / Math.Pow(1024, 3)).ToString("F0") + "GB"; }
            if (size < Math.Pow(1024, 5)) { return (size / Math.Pow(1024, 4)).ToString("F0") + "TB"; }
            if (size < Math.Pow(1024, 6)) { return (size / Math.Pow(1024, 5)).ToString("F0") + "PB"; }
            return (size / Math.Pow(1024, 6)).ToString("F0") + "EB";
        }
        /// <summary>
        /// Convert specified double to an <see cref="decimal"/> value.
        /// </summary>
        /// <typeparam name="T">The type of enum.</typeparam>
        /// <param name="value">The double value.</param>
        /// <returns>The converted <see cref="decimal"/> value.</returns>
        public static decimal ToDecimal(this double value)
        {
            return Convert.ToDecimal(value);
        }
    }
}