// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using MyNet.Utilities;
using MyNet.Utilities.Extensions;
using MyNet.Utilities.Units;

namespace MyNet.Humanizer.DateTimes
{
    /// <summary>
    /// Default implementation of IFormatter interface.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    public class DateTimeFormatter(CultureInfo culture) : IDateTimeFormatter
    {
        /// <summary>
        /// Examples: DateTimePastMinute, DateTimeFutureHour
        /// </summary>
        private const string DateTimeFormat = "DateTime{0}{1}";

        /// <summary>
        /// Examples: TimeSpanMinute, TimeSpanHour.
        /// </summary>
        private const string TimeSpanFormat = "TimeSpan{0}";

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

        private readonly CultureInfo _culture = culture;

        /// <summary>
        /// Now
        /// </summary>
        /// <returns>Returns Now</returns>
        public virtual string? Now() => _culture.Translate(NowFormat);

        /// <summary>
        /// Never
        /// </summary>
        /// <returns>Returns Never</returns>
        public virtual string? Never() => _culture.Translate(NeverFormat);

        /// <summary>
        /// 0 seconds
        /// </summary>
        /// <returns>Returns 0 seconds as the string representation of Zero TimeSpan</returns>
        public virtual string? Zero() => _culture.Translate(ZeroFormat);

        /// <summary>
        /// Returns the string representation of the provided DateTime
        /// </summary>
        /// <param name="timeUnit"></param>
        /// <param name="count"></param>
        /// <param name="timeUnitTense"></param>
        /// <returns></returns>
        public virtual string? DateHumanize(Tense timeUnitTense, TimeUnit timeUnit, int count) => count.ToString(_culture.Translate(GetDateTimeResourceKey(timeUnitTense, timeUnit, count)), _culture);

        /// <summary>
        /// Returns the string representation of the provided TimeSpan
        /// </summary>
        /// <param name="timeUnit">A time unit to represent.</param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when timeUnit is larger than TimeUnit.Week</exception>
        public virtual string? TimeSpanHumanize(TimeUnit timeUnit, int count) => count.ToString(_culture.Translate(GetTimeSpanResourceKey(timeUnit, count)), _culture);

        /// <summary>
        /// Override this method if your locale has complex rules around multiple units; e.g. Arabic, Russian
        /// </summary>
        /// <param name="timeUnitTense"></param>
        /// <param name="unit"></param>
        /// <param name="count">The number of the units being used in formatting</param>
        /// <returns></returns>
        protected virtual string? GetDateTimeResourceKey(Tense timeUnitTense, TimeUnit unit, int count) => count == 1 && unit == TimeUnit.Day
                ? timeUnitTense == Tense.Future ? TomorrowFormat : YesterdayFormat
                : DateTimeFormat.FormatWith(timeUnitTense.ToString(), unit.ToString()).WithCountSuffix(count);

        /// <summary>
        /// Override this method if your locale has complex rules around multiple units; e.g. Arabic, Russian
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="count">The number of the units being used in formatting</param>
        /// <returns></returns>
        protected virtual string? GetTimeSpanResourceKey(TimeUnit unit, int count) => TimeSpanFormat.FormatWith(unit.ToString()).WithCountSuffix(count);
    }
}
