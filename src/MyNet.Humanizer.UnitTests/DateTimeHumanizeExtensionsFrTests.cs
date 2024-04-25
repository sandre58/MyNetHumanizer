// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Units;
using MyNet.Humanizer.DateTimes;
using Xunit;

namespace MyNet.Humanizer.UnitTests
{
    [UseCulture("fr-FR")]
    public class DateTimeHumanizeExtensionsFrTests
    {

        [Theory]
        [InlineData(1, "il y a 1 seconde")]
        [InlineData(2, "il y a 2 secondes")]
        [InlineData(10, "il y a 10 secondes")]
        public void SecondsAgo(int seconds, string expected) => DateTimeHumanize.Verify(expected, seconds, TimeUnit.Second, Tense.Past);

        [Theory]
        [InlineData(1, "dans 1 seconde")]
        [InlineData(2, "dans 2 secondes")]
        [InlineData(10, "dans 10 secondes")]
        public void SecondsFromNow(int seconds, string expected) => DateTimeHumanize.Verify(expected, seconds, TimeUnit.Second, Tense.Future);

        [Theory]
        [InlineData(1, "il y a 1 minute")]
        [InlineData(2, "il y a 2 minutes")]
        [InlineData(10, "il y a 10 minutes")]
        [InlineData(60, "il y a 1 heure")]
        public void MinutesAgo(int minutes, string expected) => DateTimeHumanize.Verify(expected, minutes, TimeUnit.Minute, Tense.Past);

        [Theory]
        [InlineData(1, "dans 1 minute")]
        [InlineData(2, "dans 2 minutes")]
        [InlineData(10, "dans 10 minutes")]
        public void MinutesFromNow(int minutes, string expected) => DateTimeHumanize.Verify(expected, minutes, TimeUnit.Minute, Tense.Future);

        [Theory]
        [InlineData(1, "il y a 1 heure")]
        [InlineData(2, "il y a 2 heures")]
        [InlineData(10, "il y a 10 heures")]
        public void HoursAgo(int hours, string expected) => DateTimeHumanize.Verify(expected, hours, TimeUnit.Hour, Tense.Past);

        [Theory]
        [InlineData(1, "dans 1 heure")]
        [InlineData(2, "dans 2 heures")]
        [InlineData(10, "dans 10 heures")]
        public void HoursFromNow(int hours, string expected) => DateTimeHumanize.Verify(expected, hours, TimeUnit.Hour, Tense.Future);

        [Theory]
        [InlineData(1, "hier")]
        [InlineData(2, "il y a 2 jours")]
        [InlineData(10, "il y a 10 jours")]
        public void DaysAgo(int days, string expected) => DateTimeHumanize.Verify(expected, days, TimeUnit.Day, Tense.Past);

        [Theory]
        [InlineData(1, "demain")]
        [InlineData(2, "dans 2 jours")]
        [InlineData(10, "dans 10 jours")]
        public void DaysFromNow(int days, string expected) => DateTimeHumanize.Verify(expected, days, TimeUnit.Day, Tense.Future);

        [Theory]
        [InlineData(1, "il y a 1 mois")]
        [InlineData(2, "il y a 2 mois")]
        [InlineData(10, "il y a 10 mois")]
        public void MonthsAgo(int months, string expected) => DateTimeHumanize.Verify(expected, months, TimeUnit.Month, Tense.Past);

        [Theory]
        [InlineData(1, "dans 1 mois")]
        [InlineData(2, "dans 2 mois")]
        [InlineData(10, "dans 10 mois")]
        public void MonthsFromNow(int months, string expected) => DateTimeHumanize.Verify(expected, months, TimeUnit.Month, Tense.Future);

        [Theory]
        [InlineData(1, "il y a 1 an")]
        [InlineData(2, "il y a 2 ans")]
        public void YearsAgo(int years, string expected) => DateTimeHumanize.Verify(expected, years, TimeUnit.Year, Tense.Past);

        [Theory]
        [InlineData(1, "dans 1 an")]
        [InlineData(2, "dans 2 ans")]
        public void YearsFromNow(int years, string expected) => DateTimeHumanize.Verify(expected, years, TimeUnit.Year, Tense.Future);
    }
}
