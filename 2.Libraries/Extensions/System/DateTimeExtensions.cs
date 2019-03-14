using Extensions.Resources;
using System.Configuration;
using System.Globalization;

namespace System
{
    internal class DateTimeExtensionConstants
    {
        /// <summary>
        /// The datetime format partten.
        /// </summary>
        public const string DEFAULT_DATETIME_FORMAT_PARTTEN = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// The datet format partten
        /// </summary>
        public const string DEFAULT_DATE_FORMAT_PARTTEN = "yyyy-MM-dd";
        /// <summary>
        /// The date month format partten
        /// </summary>
        public const string DEFAULT_DATE_MONTH_FORMAT_PARTTEN = "yyyy-MM";
        /// <summary>
        /// The database null datetime
        /// </summary>
        public const string DB_NULL_DATETIME_STRING = "1900-01-01 00:00:00.000";
        /// <summary>
        /// The database null datetime
        /// </summary>
        public static readonly DateTime DB_NULL_DATETIME = new DateTime(1900, 1, 1, 0, 0, 0, 0);
    }
    /// <summary>
    /// Common extensions of <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///  Returns a string representing of a <see cref="DateTime"/> relative value.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>The string value of relative time.</returns>
        public static string ToRelativeTime(this DateTime dateTime)
        {
            TimeSpan diff = DateTime.Now - dateTime;
            string suffix = string.Empty;
            int numeral = 0;

            if (diff.TotalDays >= 365)
            {
                numeral = (int)Math.Floor(diff.TotalDays / 365);
                suffix = numeral == 1 ? Resource.YearAgo : Resource.YearsAgo;
            }
            else if (diff.TotalDays >= 31)
            {
                numeral = (int)Math.Floor(diff.TotalDays / 31);
                suffix = numeral == 1 ? Resource.MonthAgo : Resource.MonthsAgo;
            }
            else if (diff.TotalDays >= 7)
            {
                numeral = (int)Math.Floor(diff.TotalDays / 7);
                suffix = numeral == 1 ? Resource.WeekAgo : Resource.WeeksAgo;
            }
            else if (diff.TotalDays >= 1)
            {
                numeral = (int)Math.Floor(diff.TotalDays);
                suffix = numeral == 1 ? Resource.DayAgo : Resource.DaysAgo;
            }
            else if (diff.TotalHours >= 1)
            {
                numeral = (int)Math.Floor(diff.TotalHours);
                suffix = numeral == 1 ? Resource.HourAgo : Resource.HoursAgo;
            }
            else if (diff.TotalMinutes >= 1)
            {
                numeral = (int)Math.Floor(diff.TotalMinutes);
                suffix = numeral == 1 ? Resource.MinuteAgo : Resource.MinutesAgo;
            }
            else if (diff.TotalSeconds >= 1)
            {
                numeral = (int)Math.Floor(diff.TotalSeconds);
                suffix = numeral == 1 ? Resource.SecondAgo : Resource.SecondsAgo;
            }
            else
            {
                suffix = Resource.JustNow;
            }

            string output = numeral == 0 ? suffix : string.Format(CultureInfo.InvariantCulture, "{0} {1}", numeral, suffix);
            return output;
        }
        /// <summary>
        /// Returns the <see cref="DateTime"/> with max time value of the specified date.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>The datetime with max time of the <paramref name="dateTime"/></returns>
        public static DateTime ToDateWithMaxTime(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddMilliseconds(-1);
        }
        /// <summary>
        /// Returns the <see cref="DateTime"/> with min time value of the specified date.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>The datetime with min time of the <paramref name="dateTime"/></returns>
        public static DateTime ToDateWithMinTime(this DateTime dateTime)
        {
            return dateTime.Date;
        }
        /// <summary>
        /// Returns the min value of sql server datetime.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>1753/01/01 00:00:00 000</returns>
        public static DateTime SqlServerMinValue(this DateTime dateTime)
        {
            return new DateTime(1753, 1, 1, 0, 0, 0, 0);
        }
        /// <summary>
        /// Returns the min value of sql server datetime.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>1753/01/01 00:00:00 000</returns>
        public static DateTime SqlServerMinValue(this DateTime? dateTime)
        {
            return new DateTime(1753, 1, 1, 0, 0, 0, 0);
        }
        /// <summary>
        /// Returns the max value of sql server datetime.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>9999/12/31 23:59:59 997</returns>
        public static DateTime SqlServerMaxValue(this DateTime dateTime)
        {
            return new DateTime(9999, 12, 31, 23, 59, 59, 997);
        }
        /// <summary>
        /// Returns the max value of sql server datetime.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>9999/12/31 23:59:59 997</returns>
        public static DateTime SqlServerMaxValue(this DateTime? dateTime)
        {
            return new DateTime(9999, 12, 31, 23, 59, 59, 997);
        }
        /// <summary>
        /// Returns the safe value of sql server datetime.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>A safe value of sql server dateTime</returns>
        public static DateTime ToSqlServerSafeDateTime(this DateTime dateTime)
        {
            DateTime sqlServerMaxDateTime = new DateTime(9999, 12, 31, 23, 59, 59, 997);
            DateTime sqlServerMinDateTime = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            if (dateTime == null)
            {
                return sqlServerMinDateTime;
            }
            if (dateTime > sqlServerMaxDateTime)
            {
                dateTime = sqlServerMaxDateTime;
            }
            if (dateTime < sqlServerMinDateTime)
            {
                dateTime = sqlServerMinDateTime;
            }
            return dateTime;
        }
        /// <summary>
        /// Returns the safe value of sql server datetime.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>A safe value of sql server dateTime</returns>
        public static DateTime ToSqlServerSafeDateTime(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
            {
                return dateTime.SqlServerMinValue();
            }
            else
            {
                return dateTime.Value.ToSqlServerSafeDateTime();
            }
        }
        /// <summary>
        /// Returns the last day of the month of the specified datetime.
        /// </summary>
        /// <param name="dateTime">A DateTime</param>
        /// <returns>The last day of the month of the <paramref name="dateTime"/></returns>
        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            dateTime = new DateTime(dateTime.Year, dateTime.Month, 1);
            return dateTime.AddMonths(1).AddDays(-1);
        }
        /// <summary>
        /// Returns the ages of the specified date time until now.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>The age.</returns>
        public static int Age(this DateTime dateTime)
        {
            if (dateTime > DateTime.Now)
            {
                return 0;
            }
            var age = DateTime.Now.Year - dateTime.Year;
            if (DateTime.Now < dateTime.AddYears(age))
            {
                age--;
            }
            return age;
        }
        /// <summary>
        /// Indicates whether the specified date time is a working day.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>true if the value is Monday,Tuesday,Wednesday,Thursday or Friday;otherwise, false.</returns>
        public static bool IsWorkingDay(this DateTime dateTime)
        {
            return dateTime.DayOfWeek != DayOfWeek.Saturday && dateTime.DayOfWeek != DayOfWeek.Sunday;
        }
        /// <summary>
        /// Indicates whether the specified date time is weekend.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/></param>
        /// <returns>true if the value is Saturday or Sunday;otherwise, false.</returns>
        public static bool IsWeekend(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }
        /// <summary>
        /// Determines whether [is null or empty].
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified date time]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue || dateTime <= DateTimeExtensionConstants.DB_NULL_DATETIME;
        }
        /// <summary>
        /// To the application month string.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="partten">The partten.</param>
        /// <returns></returns>
        public static string ToApplicationMonthString(this DateTime dateTime, string partten = DateTimeExtensionConstants.DEFAULT_DATE_MONTH_FORMAT_PARTTEN)
        {
            if (dateTime.IsNullOrEmpty())
            {
                return string.Empty;
            }
            return dateTime.ToString(partten);
        }
        /// <summary>
        /// To the application month string.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="emptyContent">The empty content.</param>
        /// <returns></returns>
        public static string ToApplicationMonthString(this DateTime? dateTime, string emptyContent = "", string partten = DateTimeExtensionConstants.DEFAULT_DATE_FORMAT_PARTTEN)
        {
            if (!dateTime.HasValue)
            {
                return string.Empty;
            }
            return dateTime.Value.ToApplicationMonthString(partten);
        }
        /// <summary>
        /// To the application date partten string.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="partten">The partten.</param>
        /// <returns></returns>
        public static string ToApplicationDateParttenString(this DateTime dateTime, string partten = DateTimeExtensionConstants.DEFAULT_DATE_FORMAT_PARTTEN)
        {
            if (dateTime.IsNullOrEmpty())
            {
                return string.Empty;
            }
            return dateTime.ToString(partten);
        }
        /// <summary>
        /// To the application date partten string.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="emptyContent">The empty content.</param>
        /// <param name="partten">The partten.</param>
        /// <returns></returns>
        public static string ToApplicationDateParttenString(this DateTime? dateTime, string emptyContent = "", string partten = DateTimeExtensionConstants.DEFAULT_DATE_FORMAT_PARTTEN)
        {
            if (!dateTime.HasValue)
            {
                return emptyContent;
            }
            return dateTime.Value.ToApplicationDateParttenString(partten);
        }
        /// <summary>
        /// To the application date time string.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="partten">The partten.</param>
        /// <returns></returns>
        public static string ToApplicationDateTimeString(this DateTime dateTime, string partten = DateTimeExtensionConstants.DEFAULT_DATETIME_FORMAT_PARTTEN)
        {
            if (dateTime.IsNullOrEmpty())
            {
                return string.Empty;
            }
            return dateTime.ToString(partten);
        }
        /// <summary>
        /// To the application date time string.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="emptyContent">The empty content.</param>
        /// <param name="partten">The partten.</param>
        /// <returns></returns>
        public static string ToApplicationDateTimeString(this DateTime? dateTime, string emptyContent = "", string partten = DateTimeExtensionConstants.DEFAULT_DATETIME_FORMAT_PARTTEN)
        {
            if (!dateTime.HasValue)
            {
                return emptyContent;
            }
            return dateTime.Value.ToApplicationDateTimeString(partten);
        }
        /// <summary>
        /// To the date with minimum time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime? ToDateWithMinTime(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToDateWithMinTime() : dateTime;
        }
        /// <summary>
        /// To the date with maximum time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime? ToDateWithMaxTime(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToDateWithMaxTime() : dateTime;
        }
        /// <summary>
        /// To the date with maximum time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime? ToSystemDateWithMaxTime(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToSystemDateWithMaxTime() : dateTime;
        }
        /// <summary>
        /// To the date with maximum time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime ToSystemDateWithMaxTime(this DateTime dateTime)
        {
            return dateTime.AddDays(1).AddSeconds(-1);
        }
    }
}