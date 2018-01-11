namespace System
{
    /// <summary>
    /// Common extensions of <see cref="decimal"/>.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// Convert specified decimal value to a file size string.
        /// <para>Supported:KB MB GB TB PB EB</para>
        /// </summary>
        /// <param name="size">The file size value.</param>
        /// <returns>The file size string. eg:MB,GB...</returns>
        public static string ToFileSizeString(this decimal size)
        {
            if (size < 0)
            {
                throw new ArgumentException("Size must greater or equals than zero.", nameof(size));
            }
            if (size < 1024) { return (size).ToString("F0") + " bytes"; }
            if (size < Convert.ToDecimal(Math.Pow(1024, 2))) { return (size / 1024).ToString("F0") + "KB"; }
            if (size < Convert.ToDecimal(Math.Pow(1024, 3))) { return (size / Convert.ToDecimal(Math.Pow(1024, 2))).ToString("F0") + "MB"; }
            if (size < Convert.ToDecimal(Math.Pow(1024, 4))) { return (size / Convert.ToDecimal(Math.Pow(1024, 3))).ToString("F0") + "GB"; }
            if (size < Convert.ToDecimal(Math.Pow(1024, 5))) { return (size / Convert.ToDecimal(Math.Pow(1024, 4))).ToString("F0") + "TB"; }
            if (size < Convert.ToDecimal(Math.Pow(1024, 6))) { return (size / Convert.ToDecimal(Math.Pow(1024, 5))).ToString("F0") + "PB"; }
            return (size / Convert.ToDecimal(Math.Pow(1024, 6))).ToString("F0") + "EB";
        }
    }
}