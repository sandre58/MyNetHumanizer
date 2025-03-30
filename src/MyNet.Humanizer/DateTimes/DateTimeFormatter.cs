// -----------------------------------------------------------------------
// <copyright file="DateTimeFormatter.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using MyNet.Utilities;
using MyNet.Utilities.Units;

namespace MyNet.Humanizer.DateTimes;

/// <summary>
/// Default implementation of IFormatter interface.
/// </summary>
/// <remarks>
/// Constructor.
/// </remarks>
public class DateTimeFormatter(CultureInfo culture) : IDateTimeFormatter
{
    /// <summary>
    /// Resource key for Now.
    /// </summary>
    public const string NowFormat = "DateTimeNow";

    /// <summary>
    /// Resource key for Zero.
    /// </summary>
    public const string ZeroFormat = "DateTimeZero";

    /// <summary>
    /// Resource key for Never.
    /// </summary>
    public const string NeverFormat = "DateTimeNever";

    /// <summary>
    /// Resource key for Tomorrow.
    /// </summary>
    public const string TomorrowFormat = "DateTimeTomorrow";

    /// <summary>
    /// Resource key for Yesterday.
    /// </summary>
    public const string YesterdayFormat = "DateTimeYesterday";

    /// <summary>
    /// Examples: DateTimePastMinute, DateTimeFutureHour.
    /// </summary>
    private const string DateTimeFormat = "DateTime{0}{1}";

    /// <summary>
    /// Examples: TimeSpanMinute, TimeSpanHour.
    /// </summary>
    private const string TimeSpanFormat = "TimeSpan{0}";

    /// <summary>
    /// Now.
    /// </summary>
    /// <returns>Returns Now.</returns>
    public virtual string Now() => NowFormat.Translate(culture);

    /// <summary>
    /// Never.
    /// </summary>
    /// <returns>Returns Never.</returns>
    public virtual string Never() => NeverFormat.Translate(culture);

    /// <summary>
    /// 0 seconds.
    /// </summary>
    /// <returns>Returns 0 seconds as the string representation of Zero TimeSpan.</returns>
    public virtual string Zero() => ZeroFormat.Translate(culture);

    /// <summary>
    /// Returns the string representation of the provided DateTime.
    /// </summary>
    public virtual string DateHumanize(Tense timeUnitTense, TimeUnit timeUnit, int count) => count.ToString(GetDateTimeResourceKey(timeUnitTense, timeUnit, count).Translate(culture), culture);

    /// <summary>
    /// Returns the string representation of the provided TimeSpan.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Is thrown when timeUnit is larger than TimeUnit.Week.</exception>
    public virtual string TimeSpanHumanize(TimeUnit timeUnit, int count) => count.ToString(GetTimeSpanResourceKey(timeUnit, count).Translate(culture), culture);

    /// <summary>
    /// Override this method if your locale has complex rules around multiple units; e.g. Arabic, Russian.
    /// </summary>
    protected virtual string GetDateTimeResourceKey(Tense timeUnitTense, TimeUnit unit, int count) => count == 1 && unit == TimeUnit.Day
        ? timeUnitTense == Tense.Future ? TomorrowFormat : YesterdayFormat
        : DateTimeFormat.FormatWith(timeUnitTense.ToString(), unit.ToString()).ToCountKey(count);

    /// <summary>
    /// Override this method if your locale has complex rules around multiple units; e.g. Arabic, Russian.
    /// </summary>
    protected virtual string GetTimeSpanResourceKey(TimeUnit unit, int count) => TimeSpanFormat.FormatWith(unit.ToString()).ToCountKey(count);
}
