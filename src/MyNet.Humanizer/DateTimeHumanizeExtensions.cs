// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using MyNet.Utilities.Extensions;
using MyNet.Humanizer.DateTimes;
using MyNet.Utilities.Units;

namespace MyNet.Humanizer
{
    public static class DateTimeHumanizeExtensions
    {
        static DateTimeHumanizeExtensions() => ResourceLocator.Initialize();

        // http://stackoverflow.com/questions/11/how-do-i-calculate-relative-time
        /// <summary>
        /// Calculates the distance of time in words between two provided dates
        /// </summary>
        public static string? Humanize(this DateTime? input, DateTime? dateToCompareAgainst = null, TimeUnit unitMin = TimeUnit.Second, TimeUnit unitMax = TimeUnit.Year, bool utcDate = true)
            => Humanize(input, CultureInfo.CurrentCulture, dateToCompareAgainst, unitMin, unitMax, utcDate);

        // http://stackoverflow.com/questions/11/how-do-i-calculate-relative-time
        /// <summary>
        /// Calculates the distance of time in words between two provided dates
        /// </summary>
        public static string? Humanize(this DateTime input, DateTime? dateToCompareAgainst = null, TimeUnit unitMin = TimeUnit.Second, TimeUnit unitMax = TimeUnit.Year, bool utcDate = true)
            => Humanize(input, CultureInfo.CurrentCulture, dateToCompareAgainst, unitMin, unitMax, utcDate);

        /// <summary>
        /// Turns the current or provided date into a human readable sentence, overload for the nullable DateTime, returning 'never' in case null
        /// </summary>
        /// <param name="input">The date to be humanized</param>
        /// <param name="utcDate">Boolean value indicating whether the date is in UTC or local</param>
        /// <param name="dateToCompareAgainst">Date to compare the input against. If null, current date is used as base</param>
        /// <param name="unitMin"></param>
        /// <param name="unitMax"></param>
        /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
        /// <returns>distance of time in words</returns>
        public static string? Humanize(this DateTime? input, CultureInfo culture, DateTime? dateToCompareAgainst = null, TimeUnit unitMin = TimeUnit.Second, TimeUnit unitMax = TimeUnit.Year, bool utcDate = true) => input.HasValue
                ? Humanize(input.Value, culture, dateToCompareAgainst, unitMin, unitMax, utcDate)
                : culture.GetProvider<IDateTimeFormatter>()?.Never();

        // http://stackoverflow.com/questions/11/how-do-i-calculate-relative-time
        /// <summary>
        /// Calculates the distance of time in words between two provided dates
        /// </summary>
        public static string? Humanize(this DateTime input, CultureInfo culture, DateTime? dateToCompareAgainst = null, TimeUnit unitMin = TimeUnit.Second, TimeUnit unitMax = TimeUnit.Year, bool utcDate = true)
        {
            var comparisonBase = dateToCompareAgainst ?? DateTime.UtcNow;

            if (!utcDate)
            {
                comparisonBase = comparisonBase.ToLocalTime();
            }

            return Humanize(input, comparisonBase, unitMin, unitMax, culture);
        }

        // http://stackoverflow.com/questions/11/how-do-i-calculate-relative-time
        /// <summary>
        /// Calculates the distance of time in words between two provided dates
        /// </summary>
        public static string? Humanize(this DateTime input, DateTime comparisonBase, TimeUnit unitMin, TimeUnit unitMax, CultureInfo culture)
        {
            var ts = new TimeSpan(Math.Abs(comparisonBase.Ticks - input.Ticks));

            return ts.TotalSeconds < 1 && unitMin <= TimeUnit.Millisecond || unitMax == TimeUnit.Millisecond
                ? Humanize(input, comparisonBase, TimeUnit.Millisecond, culture)
                : ts.TotalSeconds > 59 && ts.TotalSeconds < 61 && unitMin <= TimeUnit.Minute
                ? Humanize(input, comparisonBase, TimeUnit.Minute, culture)
                : ts.TotalSeconds < 90 && unitMin <= TimeUnit.Second || unitMax == TimeUnit.Second
                ? Humanize(input, comparisonBase, TimeUnit.Second, culture)
                : ts.TotalMinutes > 59 && ts.TotalMinutes < 61 && unitMin <= TimeUnit.Hour
                ? Humanize(input, comparisonBase, TimeUnit.Hour, culture)
                : ts.TotalMinutes < 90 && unitMin <= TimeUnit.Minute || unitMax == TimeUnit.Minute
                ? Humanize(input, comparisonBase, TimeUnit.Minute, culture)
                : ts.TotalHours > 23 && ts.TotalHours < 25 && unitMin <= TimeUnit.Day
                ? Humanize(input, comparisonBase, TimeUnit.Day, culture)
                : ts.TotalHours < 30 && unitMin <= TimeUnit.Hour || unitMax == TimeUnit.Hour
                ? Humanize(input, comparisonBase, TimeUnit.Hour, culture)
                : ts.TotalSeconds > 6 && ts.TotalSeconds < 8 && unitMin <= TimeUnit.Week
                ? Humanize(input, comparisonBase, TimeUnit.Week, culture)
                : ts.TotalDays < 13 && unitMin <= TimeUnit.Day || unitMax == TimeUnit.Day
                ? Humanize(input, comparisonBase, TimeUnit.Day, culture)
                : ts.TotalDays > 29 && ts.TotalDays < 32 && unitMin <= TimeUnit.Month
                ? Humanize(input, comparisonBase, TimeUnit.Month, culture)
                : ts.TotalDays < 50 && unitMin <= TimeUnit.Week || unitMax == TimeUnit.Week
                ? Humanize(input, comparisonBase, TimeUnit.Week, culture)
                : ts.TotalDays > 345 && ts.TotalDays < 380 && unitMin <= TimeUnit.Year
                ? Humanize(input, comparisonBase, TimeUnit.Year, culture)
                : ts.TotalDays < 500 && unitMin <= TimeUnit.Month || unitMax == TimeUnit.Month
                ? Humanize(input, comparisonBase, TimeUnit.Month, culture)
                : Humanize(input, comparisonBase, TimeUnit.Year, culture);
        }

        // http://stackoverflow.com/questions/11/how-do-i-calculate-relative-time
        /// <summary>
        /// Calculates the distance of time in words between two provided dates
        /// </summary>
        public static string? Humanize(DateTime input, DateTime comparisonBase, TimeUnit timeUnit) => Humanize(input, comparisonBase, timeUnit, CultureInfo.CurrentCulture);

        // http://stackoverflow.com/questions/11/how-do-i-calculate-relative-time
        /// <summary>
        /// Calculates the distance of time in words between two provided dates
        /// </summary>
        public static string? Humanize(DateTime input, DateTime comparisonBase, TimeUnit timeUnit, CultureInfo culture)
        {
            var tense = input > comparisonBase ? Tense.Future : Tense.Past;
            var formatter = culture.GetProvider<IDateTimeFormatter>();
            var ts = new TimeSpan(Math.Abs(comparisonBase.Ticks - input.Ticks));

            var count = 0;
            switch (timeUnit)
            {
                case TimeUnit.Millisecond:
                    count = (int)Math.Round(ts.TotalMilliseconds);
                    break;

                case TimeUnit.Second:
                    count = (int)Math.Round(ts.TotalSeconds);
                    break;

                case TimeUnit.Minute:
                    count = (int)Math.Round(ts.TotalMinutes);
                    break;

                case TimeUnit.Hour:
                    count = (int)Math.Round(ts.TotalHours);
                    break;

                case TimeUnit.Day:
                    count = (int)Math.Round(ts.TotalDays);
                    break;

                case TimeUnit.Week:
                    count = (int)Math.Round(ts.TotalDays / 7);
                    break;

                case TimeUnit.Month:
                    count = (int)Math.Round(ts.TotalDays / 29.5);
                    break;

                case TimeUnit.Year:
                    count = (int)Math.Round(ts.TotalDays / 365);
                    break;
            }

            return count > 0 ? formatter?.DateHumanize(tense, timeUnit, count) : formatter?.Now();
        }

        public static string? ToMonthAbbreviated(this DateTime date) => ToMonthAbbreviated(date, CultureInfo.CurrentCulture);

        public static string? ToMonthAbbreviated(this DateTime date, CultureInfo culture)
        {
            var result = string.Empty;

            var monthNames = culture.DateTimeFormat.AbbreviatedMonthNames;
            if (monthNames != null && monthNames.Length > 0)
            {
                result = monthNames[(date.Month - 1) % monthNames.Length];
            }

            return result;
        }
    }
}
