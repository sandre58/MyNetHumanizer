// -----------------------------------------------------------------------
// <copyright file="DateTimeHumanizeExtensionsTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using MyNet.Humanizer.DateTimes;
using MyNet.Utilities.Units;
using Xunit;

namespace MyNet.Humanizer.UnitTests;

[UseCulture(Culture)]
[Collection("UseCultureSequential")]
public class DateTimeHumanizeExtensionsTests
{
    public const string Culture = "en-US";

    [Theory]
    [InlineData(1, "1 second ago")]
    [InlineData(10, "10 seconds ago")]
    [InlineData(59, "59 seconds ago")]
    [InlineData(60, "1 minute ago")]
    public void SecondsAgo(int seconds, string expected) => DateTimeHumanize.Verify(expected, Culture, seconds, TimeUnit.Second, Tense.Past);

    [Theory]
    [InlineData(1, "1 second from now")]
    [InlineData(10, "10 seconds from now")]
    [InlineData(59, "59 seconds from now")]
    [InlineData(60, "1 minute from now")]
    public void SecondsFromNow(int seconds, string expected) => DateTimeHumanize.Verify(expected, Culture, seconds, TimeUnit.Second, Tense.Future);

    [Theory]
    [InlineData(1, "1 minute ago")]
    [InlineData(10, "10 minutes ago")]
    [InlineData(44, "44 minutes ago")]
    [InlineData(45, "45 minutes ago")]
    [InlineData(59, "59 minutes ago")]
    [InlineData(60, "1 hour ago")]
    [InlineData(119, "2 hours ago")]
    [InlineData(120, "2 hours ago")]
    public void MinutesAgo(int minutes, string expected) => DateTimeHumanize.Verify(expected, Culture, minutes, TimeUnit.Minute, Tense.Past);

    [Theory]
    [InlineData(1, "1 minute from now")]
    [InlineData(10, "10 minutes from now")]
    [InlineData(44, "44 minutes from now")]
    [InlineData(45, "45 minutes from now")]
    [InlineData(119, "2 hours from now")]
    [InlineData(120, "2 hours from now")]
    public void MinutesFromNow(int minutes, string expected) => DateTimeHumanize.Verify(expected, Culture, minutes, TimeUnit.Minute, Tense.Future);

    [Theory]
    [InlineData(1, "1 hour ago")]
    [InlineData(10, "10 hours ago")]
    [InlineData(23, "23 hours ago")]
    [InlineData(24, "yesterday")]
    public void HoursAgo(int hours, string expected) => DateTimeHumanize.Verify(expected, Culture, hours, TimeUnit.Hour, Tense.Past);

    [Theory]
    [InlineData(1, "1 hour from now")]
    [InlineData(10, "10 hours from now")]
    [InlineData(23, "23 hours from now")]
    [InlineData(24, "tomorrow")]
    public void HoursFromNow(int hours, string expected) => DateTimeHumanize.Verify(expected, Culture, hours, TimeUnit.Hour, Tense.Future);

    [Theory]
    [InlineData(35, "tomorrow")]
    [InlineData(40, "2 days from now")]
    public void HoursFromNowNotTomorrow(int hours, string expected)
    {
        // Only test with injected date, as results are dependent on time of day
        var utcNow = new DateTime(2014, 6, 28, 9, 58, 22, DateTimeKind.Utc);
        var now = new DateTime(2014, 6, 28, 9, 58, 22, DateTimeKind.Local);

        DateTimeHumanize.Verify(expected, Culture, hours, TimeUnit.Hour, Tense.Future, null, now, utcNow);
    }

    [Theory]
    [InlineData(1, "yesterday")]
    [InlineData(10, "10 days ago")]
    [InlineData(27, "4 weeks ago")]
    [InlineData(32, "5 weeks ago")]
    public void DaysAgo(int days, string expected) => DateTimeHumanize.Verify(expected, Culture, days, TimeUnit.Day, Tense.Past);

    [Theory]
    [InlineData(1, "tomorrow")]
    [InlineData(10, "10 days from now")]
    [InlineData(27, "4 weeks from now")]
    [InlineData(32, "5 weeks from now")]
    public void DaysFromNow(int days, string expected) => DateTimeHumanize.Verify(expected, Culture, days, TimeUnit.Day, Tense.Future);

    [Theory]
    [InlineData(1, "1 month ago")]
    [InlineData(10, "10 months ago")]
    [InlineData(11, "11 months ago")]
    [InlineData(12, "1 year ago")]
    public void MonthsAgo(int months, string expected) => DateTimeHumanize.Verify(expected, Culture, months, TimeUnit.Month, Tense.Past);

    [Theory]
    [InlineData(1, "1 month from now")]
    [InlineData(10, "10 months from now")]
    [InlineData(11, "11 months from now")]
    [InlineData(12, "1 year from now")]
    public void MonthsFromNow(int months, string expected) => DateTimeHumanize.Verify(expected, Culture, months, TimeUnit.Month, Tense.Future);

    [Theory]
    [InlineData(1, "1 year ago")]
    [InlineData(2, "2 years ago")]
    public void YearsAgo(int years, string expected) => DateTimeHumanize.Verify(expected, Culture, years, TimeUnit.Year, Tense.Past);

    [Theory]
    [InlineData(1, "1 year from now")]
    [InlineData(2, "2 years from now")]
    public void YearsFromNow(int years, string expected) => DateTimeHumanize.Verify(expected, Culture, years, TimeUnit.Year, Tense.Future);

    [Fact]
    public void Never()
    {
        DateTime? never = null;
        Assert.Equal("never", never.Humanize());
    }

    [Fact]
    public void Nullable_ExpectSame()
    {
        DateTime? never = new DateTime(2015, 12, 7, 9, 0, 0, DateTimeKind.Utc);

        Assert.Equal(never.Value.Humanize(), never.Humanize());
    }

    [Theory]
    [InlineData(1, TimeUnit.Year, Tense.Future, "en-US", "1 year from now")]
    [InlineData(40, TimeUnit.Second, Tense.Past, "fr-FR", "il y a 40 secondes")]
    public void CanSpecifyCultureExplicitly(int unit, TimeUnit timeUnit, Tense tense, string culture, string expected) => DateTimeHumanize.Verify(expected, culture, unit, timeUnit, tense, culture: new CultureInfo(culture));
}
