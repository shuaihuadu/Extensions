namespace System
{
    /// <summary>
    /// Common extensions of <see cref="int"/>.
    /// </summary>
    public static class Int32Extensions
    {
        /// <summary>
        /// Convert specified int to an <see cref="Enum"/> value.
        /// </summary>
        /// <typeparam name="T">The type of enum.</typeparam>
        /// <param name="value">The int value.</param>
        /// <returns>The converted <see cref="Enum"/> value.</returns>
        public static T ToEnum<T>(this int value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
        /// <summary>
        /// Convert specified int value to a file size string.
        /// <para>Supported:KB MB GB TB PB EB</para>
        /// </summary>
        /// <param name="size">The file size value.</param>
        /// <returns>The file size string. eg:MB,GB...</returns>
        public static string ToFileSizeString(this int size)
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
    }
}