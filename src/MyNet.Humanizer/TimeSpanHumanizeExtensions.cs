// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MyNet.Humanizer.DateTimes;
using MyNet.Utilities;
using MyNet.Utilities.Localization;
using MyNet.Utilities.Units;

namespace MyNet.Humanizer
{
    /// <summary>
    /// Humanizes TimeSpan into human readable form
    /// </summary>
    public static class TimeSpanHumanizeExtensions
    {
        static TimeSpanHumanizeExtensions() => ResourceLocator.Initialize();

        /// <summary>
        /// Turns a TimeSpan into a human readable form. E.g. 1 day.
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <param name="precision">The maximum number of time units to return. Defaulted is 1 which means the largest unit is returned</param>
        /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
        /// <param name="maxUnit">The maximum unit of time to output. The default value is <see cref="TimeUnit.Week"/>. The time units <see cref="TimeUnit.Month"/> and <see cref="TimeUnit.Year"/> will give approximations for time spans bigger 30 days by calculating with 365.2425 days a year and 30.4369 days a month.</param>
        /// <param name="minUnit">The minimum unit of time to output.</param>
        /// <param name="collectionSeparator">The separator to use when combining humanized time parts. If null, the default collection formatter for the current culture is used.</param>
        /// <param name="lastSeparator"></param>
        /// <returns></returns>
        public static string? Humanize(this TimeSpan timeSpan, int precision = 1, TimeUnit maxUnit = TimeUnit.Year, TimeUnit minUnit = TimeUnit.Millisecond, string collectionSeparator = ", ", string? lastSeparator = null, CultureInfo? culture = null) => timeSpan.Humanize(precision, false, maxUnit, minUnit, collectionSeparator, lastSeparator, culture);

        /// <summary>
        /// Turns a TimeSpan into a human readable form. E.g. 1 day.
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <param name="precision">The maximum number of time units to return.</param>
        /// <param name="countEmptyUnits">Controls whether empty time units should be counted towards maximum number of time units. Leading empty time units never count.</param>
        /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
        /// <param name="maxUnit">The maximum unit of time to output. The default value is <see cref="TimeUnit.Week"/>. The time units <see cref="TimeUnit.Month"/> and <see cref="TimeUnit.Year"/> will give approximations for time spans bigger than 30 days by calculating with 365.2425 days a year and 30.4369 days a month.</param>
        /// <param name="minUnit">The minimum unit of time to output.</param>
        /// <param name="collectionSeparator">The separator to use when combining humanized time parts. If null, the default collection formatter for the current culture is used.</param>
        /// <param name="lastSeparator"></param>
        /// <returns></returns>
        public static string? Humanize(this TimeSpan timeSpan, int precision, bool countEmptyUnits, TimeUnit maxUnit = TimeUnit.Year, TimeUnit minUnit = TimeUnit.Millisecond, string collectionSeparator = ", ", string? lastSeparator = null, CultureInfo? culture = null)
        {
            IEnumerable<string> timeParts = CreateTheTimePartsWithUpperAndLowerLimits(timeSpan, maxUnit, minUnit, culture);
            timeParts = SetPrecisionOfTimeSpan(timeParts, precision, countEmptyUnits);

            return ConcatenateTimeSpanParts(timeParts, collectionSeparator, lastSeparator);
        }

        private static List<string> CreateTheTimePartsWithUpperAndLowerLimits(TimeSpan timespan, TimeUnit maxUnit, TimeUnit minUnit, CultureInfo? culture = null)
        {
            var cultureFormatter = LocalizationService.GetOrCurrent<IDateTimeFormatter>(culture);
            var firstValueFound = false;
            var timeUnitsEnumTypes = GetEnumTypesForTimeUnit();
            var timeParts = new List<string>();

            foreach (var timeUnitType in timeUnitsEnumTypes)
            {
                var timepart = GetTimeUnitPart(timeUnitType, timespan, maxUnit, minUnit, cultureFormatter);

                if (timepart != null || firstValueFound)
                {
                    firstValueFound = true;
                    timeParts.Add(timepart ?? string.Empty);
                }
            }
            if (IsContainingOnlyNullValue(timeParts))
            {
                var noTimeValueCultureFormatted = minUnit <= TimeUnit.Second ? cultureFormatter?.Zero() : cultureFormatter?.TimeSpanHumanize(minUnit, 0);
                timeParts = CreateTimePartsWithNoTimeValue(noTimeValueCultureFormatted);
            }
            return timeParts;
        }

        private static IEnumerable<TimeUnit> GetEnumTypesForTimeUnit()
        {
            var enumTypeEnumerator = (IEnumerable<TimeUnit>)Enum.GetValues(typeof(TimeUnit));
            return enumTypeEnumerator.Reverse();
        }

        private static string? GetTimeUnitPart(TimeUnit timeUnitToGet, TimeSpan timespan, TimeUnit maximumTimeUnit, TimeUnit minimumTimeUnit, IDateTimeFormatter? cultureFormatter)
        {
            if (timeUnitToGet <= maximumTimeUnit && timeUnitToGet >= minimumTimeUnit)
            {
                var isTimeUnitToGetTheMaximumTimeUnit = timeUnitToGet == maximumTimeUnit;
                var numberOfTimeUnits = GetTimeUnitNumericalValue(timeUnitToGet, timespan, isTimeUnitToGetTheMaximumTimeUnit);
                return BuildFormatTimePart(cultureFormatter, timeUnitToGet, numberOfTimeUnits);
            }
            return null;
        }

        private static int GetTimeUnitNumericalValue(TimeUnit timeUnitToGet, TimeSpan timespan, bool isTimeUnitToGetTheMaximumTimeUnit) => timeUnitToGet switch
        {
            TimeUnit.Millisecond => GetNormalCaseTimeAsInteger(timespan.Milliseconds, timespan.TotalMilliseconds, isTimeUnitToGetTheMaximumTimeUnit),
            TimeUnit.Second => GetNormalCaseTimeAsInteger(timespan.Seconds, timespan.TotalSeconds, isTimeUnitToGetTheMaximumTimeUnit),
            TimeUnit.Minute => GetNormalCaseTimeAsInteger(timespan.Minutes, timespan.TotalMinutes, isTimeUnitToGetTheMaximumTimeUnit),
            TimeUnit.Hour => GetNormalCaseTimeAsInteger(timespan.Hours, timespan.TotalHours, isTimeUnitToGetTheMaximumTimeUnit),
            TimeUnit.Day => GetSpecialCaseDaysAsInteger(timespan, isTimeUnitToGetTheMaximumTimeUnit),
            TimeUnit.Week => GetSpecialCaseWeeksAsInteger(timespan, isTimeUnitToGetTheMaximumTimeUnit),
            TimeUnit.Month => GetSpecialCaseMonthAsInteger(timespan, isTimeUnitToGetTheMaximumTimeUnit),
            TimeUnit.Year => GetSpecialCaseYearAsInteger(timespan),
            _ => 0,
        };

        private static int GetSpecialCaseMonthAsInteger(TimeSpan timespan, bool isTimeUnitToGetTheMaximumTimeUnit)
        {
            if (isTimeUnitToGetTheMaximumTimeUnit)
                return (int)(timespan.Days / TimeSpanExtensions.DaysInAMonth);
            else
            {
                var remainingDays = timespan.Days % TimeSpanExtensions.DaysInAYear;
                return (int)(remainingDays / TimeSpanExtensions.DaysInAMonth);
            }
        }

        private static int GetSpecialCaseYearAsInteger(TimeSpan timespan) => (int)(timespan.Days / TimeSpanExtensions.DaysInAYear);

        private static int GetSpecialCaseWeeksAsInteger(TimeSpan timespan, bool isTimeUnitToGetTheMaximumTimeUnit) => isTimeUnitToGetTheMaximumTimeUnit || timespan.Days < TimeSpanExtensions.DaysInAMonth ? timespan.Days / TimeSpanExtensions.DaysInAWeek : 0;

        private static int GetSpecialCaseDaysAsInteger(TimeSpan timespan, bool isTimeUnitToGetTheMaximumTimeUnit)
        {
            if (isTimeUnitToGetTheMaximumTimeUnit)
                return timespan.Days;
            if (timespan.Days < TimeSpanExtensions.DaysInAMonth)
            {
                var remainingDays = timespan.Days % TimeSpanExtensions.DaysInAWeek;
                return remainingDays;
            }
            return (int)(timespan.Days % TimeSpanExtensions.DaysInAMonth);
        }

        private static int GetNormalCaseTimeAsInteger(int timeNumberOfUnits, double totalTimeNumberOfUnits, bool isTimeUnitToGetTheMaximumTimeUnit)
        {
            if (isTimeUnitToGetTheMaximumTimeUnit)
                try
                {
                    return (int)totalTimeNumberOfUnits;
                }
                catch
                {
                    //To be implemented so that TimeSpanHumanize method accepts double type as unit
                    return 0;
                }
            return timeNumberOfUnits;
        }

        private static string? BuildFormatTimePart(IDateTimeFormatter? cultureFormatter, TimeUnit timeUnitType, int amountOfTimeUnits) =>
            // Always use positive units to account for negative timespans
            amountOfTimeUnits != 0
                ? cultureFormatter?.TimeSpanHumanize(timeUnitType, Math.Abs(amountOfTimeUnits))
                : null;

        private static List<string> CreateTimePartsWithNoTimeValue(string? noTimeValue) => [noTimeValue ?? string.Empty];

        private static bool IsContainingOnlyNullValue(IEnumerable<string> timeParts) => !timeParts.Any(x => x != null);

        private static IEnumerable<string> SetPrecisionOfTimeSpan(IEnumerable<string> timeParts, int precision, bool countEmptyUnits)
        {
            if (!countEmptyUnits)
                timeParts = timeParts.Where(x => !string.IsNullOrEmpty(x));

            timeParts = timeParts.Take(precision);
            if (countEmptyUnits)
                timeParts = timeParts.Where(x => !string.IsNullOrEmpty(x));

            return timeParts;
        }

        private static string? ConcatenateTimeSpanParts(IEnumerable<string> timeSpanParts, string collectionSeparator, string? lastSeparator = null) => timeSpanParts.Humanize(collectionSeparator, lastSeparator);
    }
}
