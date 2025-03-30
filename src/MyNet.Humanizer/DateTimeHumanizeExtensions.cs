// -----------------------------------------------------------------------
// <copyright file="DateTimeHumanizeExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using MyNet.Humanizer.DateTimes;
using MyNet.Utilities.Localization;
using MyNet.Utilities.Units;

namespace MyNet.Humanizer;

public static class DateTimeHumanizeExtensions
{
    static DateTimeHumanizeExtensions() => ResourceLocator.Initialize();

    /// <summary>
    /// Turns the current or provided date into a human readable sentence, overload for the nullable DateTime, returning 'never' in case null.
    /// </summary>
    /// <returns>distance of time in words.</returns>
    public static string? Humanize(this DateTime? input, DateTime? dateToCompareAgainst = null, TimeUnit unitMin = TimeUnit.Second, TimeUnit unitMax = TimeUnit.Year, bool utcDate = true, CultureInfo? culture = null)
        => input.HasValue
            ? Humanize(input.Value, dateToCompareAgainst, unitMin, unitMax, utcDate, culture)
            : LocalizationService.GetOrCurrent<IDateTimeFormatter>()?.Never();

    /// <summary>
    /// Calculates the distance of time in words between two provided dates.
    /// </summary>
    public static string Humanize(this DateTime input, DateTime? dateToCompareAgainst = null, TimeUnit unitMin = TimeUnit.Second, TimeUnit unitMax = TimeUnit.Year, bool utcDate = true, CultureInfo? culture = null)
    {
        var comparisonBase = dateToCompareAgainst ?? DateTime.UtcNow;

        if (!utcDate)
        {
            comparisonBase = comparisonBase.ToLocalTime();
        }

        return Humanize(input, comparisonBase, unitMin, unitMax, culture);
    }

    /// <summary>
    /// Calculates the distance of time in words between two provided dates.
    /// </summary>
    public static string Humanize(this DateTime input, DateTime comparisonBase, TimeUnit unitMin, TimeUnit unitMax, CultureInfo? culture = null)
    {
        var ts = new TimeSpan(Math.Abs(comparisonBase.Ticks - input.Ticks));

        return (ts.TotalSeconds < 1 && unitMin <= TimeUnit.Millisecond) || unitMax == TimeUnit.Millisecond
            ? Humanize(input, comparisonBase, TimeUnit.Millisecond, culture)
            : ts.TotalSeconds is > 59 and < 61 && unitMin <= TimeUnit.Minute
                ? Humanize(input, comparisonBase, TimeUnit.Minute, culture)
                : (ts.TotalSeconds < 90 && unitMin <= TimeUnit.Second) || unitMax == TimeUnit.Second
                    ? Humanize(input, comparisonBase, TimeUnit.Second, culture)
                    : ts.TotalMinutes is > 59 and < 61 && unitMin <= TimeUnit.Hour
                        ? Humanize(input, comparisonBase, TimeUnit.Hour, culture)
                        : (ts.TotalMinutes < 90 && unitMin <= TimeUnit.Minute) || unitMax == TimeUnit.Minute
                            ? Humanize(input, comparisonBase, TimeUnit.Minute, culture)
                            : ts.TotalHours is > 23 and < 25 && unitMin <= TimeUnit.Day
                                ? Humanize(input, comparisonBase, TimeUnit.Day, culture)
                                : (ts.TotalHours < 30 && unitMin <= TimeUnit.Hour) || unitMax == TimeUnit.Hour
                                    ? Humanize(input, comparisonBase, TimeUnit.Hour, culture)
                                    : ts.TotalSeconds is > 6 and < 8 && unitMin <= TimeUnit.Week
                                        ? Humanize(input, comparisonBase, TimeUnit.Week, culture)
                                        : (ts.TotalDays < 13 && unitMin <= TimeUnit.Day) || unitMax == TimeUnit.Day
                                            ? Humanize(input, comparisonBase, TimeUnit.Day, culture)
                                            : ts.TotalDays is > 29 and < 32 && unitMin <= TimeUnit.Month
                                                ? Humanize(input, comparisonBase, TimeUnit.Month, culture)
                                                : (ts.TotalDays < 50 && unitMin <= TimeUnit.Week) || unitMax == TimeUnit.Week
                                                    ? Humanize(input, comparisonBase, TimeUnit.Week, culture)
                                                    : ts.TotalDays is > 345 and < 380 && unitMin <= TimeUnit.Year
                                                        ? Humanize(input, comparisonBase, TimeUnit.Year, culture)
                                                        : (ts.TotalDays < 500 && unitMin <= TimeUnit.Month) || unitMax == TimeUnit.Month
                                                            ? Humanize(input, comparisonBase, TimeUnit.Month, culture)
                                                            : Humanize(input, comparisonBase, TimeUnit.Year, culture);
    }

    /// <summary>
    /// Calculates the distance of time in words between two provided dates.
    /// </summary>
    public static string Humanize(DateTime input, DateTime comparisonBase, TimeUnit timeUnit, CultureInfo? culture = null)
    {
        var tense = input > comparisonBase ? Tense.Future : Tense.Past;
        var formatter = LocalizationService.GetOrCurrent<IDateTimeFormatter>(culture);

        if (formatter is null) return string.Empty;

        var ts = new TimeSpan(Math.Abs(comparisonBase.Ticks - input.Ticks));

        var count = timeUnit switch
        {
            TimeUnit.Millisecond => (int)Math.Round(ts.TotalMilliseconds),
            TimeUnit.Second => (int)Math.Round(ts.TotalSeconds),
            TimeUnit.Minute => (int)Math.Round(ts.TotalMinutes),
            TimeUnit.Hour => (int)Math.Round(ts.TotalHours),
            TimeUnit.Day => (int)Math.Round(ts.TotalDays),
            TimeUnit.Week => (int)Math.Round(ts.TotalDays / 7),
            TimeUnit.Month => (int)Math.Round(ts.TotalDays / 29.5),
            TimeUnit.Year => (int)Math.Round(ts.TotalDays / 365),
            _ => 0
        };

        return count > 0 ? formatter.DateHumanize(tense, timeUnit, count) : formatter.Now();
    }

    public static string ToMonthAbbreviated(this DateTime date, CultureInfo? culture = null)
    {
        var result = string.Empty;

        var monthNames = (culture ?? GlobalizationService.Current.Culture).DateTimeFormat.AbbreviatedMonthNames;
        if (monthNames is { Length: > 0 })
        {
            result = monthNames[(date.Month - 1) % monthNames.Length];
        }

        return result;
    }
}
