// -----------------------------------------------------------------------
// <copyright file="ToQuantityExtensionsTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using MyNet.Utilities;
using Xunit;

namespace MyNet.Humanizer.UnitTests;

[UseCulture("en-US")]
[Collection("UseCultureSequential")]
public class ToQuantityExtensionsTests
{
    [Theory]
    [InlineData("case", 0, "0 cases")]
    [InlineData("case", 1, "1 case")]
    [InlineData("case", 5, "5 cases")]
    [InlineData("man", 0, "0 men")]
    [InlineData("man", 1, "1 man")]
    [InlineData("man", 2, "2 men")]
    [InlineData("men", 2, "2 men")]
    [InlineData("process", 2, "2 processes")]
    [InlineData("process", 1, "1 process")]
    [InlineData("processes", 2, "2 processes")]
    [InlineData("processes", 1, "1 process")]
    [InlineData("slice", 1, "1 slice")]
    [InlineData("slice", 2, "2 slices")]
    [InlineData("slices", 1, "1 slice")]
    [InlineData("slices", 2, "2 slices")]
    public void ToQuantity(string word, int quantity, string expected) => Assert.Equal(expected, word.ToQuantity(quantity));

    [Theory]
    [InlineData("case", 0, "cases")]
    [InlineData("case", 1, "case")]
    [InlineData("case", 5, "cases")]
    [InlineData("man", 0, "men")]
    [InlineData("man", 1, "man")]
    [InlineData("man", 2, "men")]
    [InlineData("men", 2, "men")]
    [InlineData("process", 2, "processes")]
    [InlineData("process", 1, "process")]
    [InlineData("processes", 2, "processes")]
    [InlineData("processes", 1, "process")]
    public void ToQuantityWithNoQuantity(string word, int quantity, string expected) => Assert.Equal(expected, word.ToQuantity(quantity, ShowQuantityAs.None));

    [Theory]
    [InlineData("case", 0, "0 cases")]
    [InlineData("case", 1, "1 case")]
    [InlineData("case", 5, "5 cases")]
    [InlineData("man", 0, "0 men")]
    [InlineData("man", 1, "1 man")]
    [InlineData("man", 2, "2 men")]
    [InlineData("men", 2, "2 men")]
    [InlineData("process", 2, "2 processes")]
    [InlineData("process", 1, "1 process")]
    [InlineData("processes", 2, "2 processes")]
    [InlineData("processes", 1, "1 process")]
    public void ToQuantityNumeric(string word, int quantity, string expected) => Assert.Equal(expected, word.ToQuantity(quantity));

    [Theory]
    [InlineData("hour", 1, "1 hour")]
    [InlineData("hour", 0.5, "0.5 hours")]
    [InlineData("hour", 22.4, "22.4 hours")]
    public void ToDoubleQuantityNumeric(string word, double quantity, string expected) => Assert.Equal(expected, word.ToQuantity(quantity, formatProvider: CultureInfo.CurrentCulture));

    [Theory]
    [InlineData("case", 0, null, "0 cases")]
    [InlineData("case", 1, null, "1 case")]
    [InlineData("case", 2, null, "2 cases")]
    [InlineData("case", 1, "N0", "1 case")]
    [InlineData("case", 2, "N0", "2 cases")]
    [InlineData("case", 123456, "N0", "123,456 cases")]
    [InlineData("case", 123456, "N2", "123,456.00 cases")]
    [InlineData("dollar", 0, "C0", "$0 dollars")]
    [InlineData("dollar", 1, "C0", "$1 dollar")]
    [InlineData("dollar", 2, "C0", "$2 dollars")]
    [InlineData("dollar", 2, "C2", "$2.00 dollars")]
    public void ToQuantityWordsWithCurrentCultureFormatting(string word, int quantity, string? format, string expected)
    {
        Assert.Equal(expected, word.ToQuantity(quantity, format.OrEmpty(), CultureInfo.CurrentCulture));
        Assert.Equal(expected, word.ToQuantity((long)quantity, format.OrEmpty(), CultureInfo.CurrentCulture));
    }

    [Theory]
    [InlineData("case", 0, "N0", "it-IT", "0 case")]
    [InlineData("case", 1, "N0", "it-IT", "1 case")]
    [InlineData("case", 2, "N0", "it-IT", "2 cases")]
    [InlineData("case", 1234567, "N0", "it-IT", "1.234.567 cases")]
    [InlineData("case", 1234567, "N2", "it-IT", "1.234.567,00 cases")]
    [InlineData("euro", 0, "C0", "es-ES", "0 € euro")]
    [InlineData("euro", 1, "C0", "es-ES", "1 € euro")]
    [InlineData("euro", 2, "C0", "es-ES", "2 € euros")]
    [InlineData("euro", 2, "C2", "es-ES", "2,00 € euros")]
    public void ToQuantityWordsWithCustomCultureFormatting(string word, int quantity, string format, string cultureCode, string expected)
    {
        var culture = new CultureInfo(cultureCode);

        Assert.Equal(expected, word.ToQuantity(quantity, format, culture), GetStringComparer(culture));
        Assert.Equal(expected, word.ToQuantity((long)quantity, format, culture), GetStringComparer(culture));
    }

    internal static StringComparer GetStringComparer(CultureInfo culture) =>
        StringComparer.Create(culture, false);
}
