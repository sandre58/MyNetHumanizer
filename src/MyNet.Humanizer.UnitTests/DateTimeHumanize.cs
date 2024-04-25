// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using MyNet.Utilities.Units;
using MyNet.Humanizer.DateTimes;
using Xunit;

namespace MyNet.Humanizer.UnitTests
{
    public static class DateTimeHumanize
    {
        private static readonly object LockObject = new();

        private static void VerifyWithCurrentDate(string expectedString, TimeSpan deltaFromNow, CultureInfo? culture)
        {
            var utcNow = DateTime.UtcNow;
            var localNow = DateTime.Now;

            // feels like the only way to avoid breaking tests because CPU ticks over is to inject the base date
            VerifyWithDate(expectedString, deltaFromNow, culture, localNow, utcNow);
        }

        private static void VerifyWithDateInjection(string expectedString, TimeSpan deltaFromNow, CultureInfo? culture)
        {
            var utcNow = new DateTime(2013, 6, 20, 9, 58, 22, DateTimeKind.Utc);
            var now = new DateTime(2013, 6, 20, 11, 58, 22, DateTimeKind.Local);

            VerifyWithDate(expectedString, deltaFromNow, culture, now, utcNow);
        }

        private static void VerifyWithDate(string expectedString, TimeSpan deltaFromBase, CultureInfo? culture, DateTime? baseDate, DateTime? baseDateUtc)
        {
            if (culture == null)
            {
                Assert.Equal(expectedString, baseDateUtc?.Add(deltaFromBase).Humanize(utcDate: true, dateToCompareAgainst: baseDateUtc));
                Assert.Equal(expectedString, baseDate?.Add(deltaFromBase).Humanize(baseDate, utcDate: false));
            }
            else
            {
                Assert.Equal(expectedString, baseDateUtc?.Add(deltaFromBase).Humanize(utcDate: true, dateToCompareAgainst: baseDateUtc, culture: culture));
                Assert.Equal(expectedString, baseDate?.Add(deltaFromBase).Humanize(culture, baseDate, utcDate: false));
            }
        }

        public static void Verify(string expectedString, int unit, TimeUnit timeUnit, Tense tense, CultureInfo? culture = null, DateTime? baseDate = null, DateTime? baseDateUtc = null)
        {
            // We lock this as these tests can be multi-threaded and we're setting a static
            lock (LockObject)
            {
                var deltaFromNow = new TimeSpan();
                unit = Math.Abs(unit);

                if (tense == Tense.Past)
                    unit = -unit;

                switch (timeUnit)
                {
                    case TimeUnit.Millisecond:
                        deltaFromNow = TimeSpan.FromMilliseconds(unit);
                        break;
                    case TimeUnit.Second:
                        deltaFromNow = TimeSpan.FromSeconds(unit);
                        break;
                    case TimeUnit.Minute:
                        deltaFromNow = TimeSpan.FromMinutes(unit);
                        break;
                    case TimeUnit.Hour:
                        deltaFromNow = TimeSpan.FromHours(unit);
                        break;
                    case TimeUnit.Day:
                        deltaFromNow = TimeSpan.FromDays(unit);
                        break;
                    case TimeUnit.Month:
                        deltaFromNow = TimeSpan.FromDays(unit * 30);
                        break;
                    case TimeUnit.Year:
                        deltaFromNow = TimeSpan.FromDays(unit * 365);
                        break;
                }

                if (baseDate == null)
                {
                    VerifyWithCurrentDate(expectedString, deltaFromNow, culture);
                    VerifyWithDateInjection(expectedString, deltaFromNow, culture);
                }
                else
                    VerifyWithDate(expectedString, deltaFromNow, culture, baseDate, baseDateUtc);
            }
        }
    }
}
