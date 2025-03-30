// -----------------------------------------------------------------------
// <copyright file="NumberHumanizeExtensionsTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using MyNet.Utilities.Units;
using Xunit;

namespace MyNet.Humanizer.UnitTests;

[UseCulture("en-US")]
[Collection("UseCultureSequential")]
public class NumberHumanizeExtensionsTests
{
    [Theory]
    [InlineData(244587587, FileSizeUnit.Byte, true, "244,587,587.00 b")]
    [InlineData(244587587.76, FileSizeUnit.Byte, true, "244,587,587.76 b")]
    [InlineData(244587587, FileSizeUnit.Byte, false, "244,587,587.00 bytes")]
    [InlineData(1, FileSizeUnit.Byte, false, "1.00 byte")]
    [InlineData(0, FileSizeUnit.Byte, false, "0.00 bytes")]
    [InlineData(25452, FileSizeUnit.Megabyte, true, "25,452.00 mb")]
    [InlineData(25452.76, FileSizeUnit.Megabyte, true, "25,452.76 mb")]
    [InlineData(25452, FileSizeUnit.Megabyte, false, "25,452.00 megabytes")]
    [InlineData(1, FileSizeUnit.Megabyte, false, "1.00 megabyte")]
    [InlineData(0, FileSizeUnit.Megabyte, false, "0.00 megabytes")]
    public void ToFileSize(double value, FileSizeUnit unit, bool abbreviation, string expected)
        => Assert.Equal(expected, value.Humanize(unit, abbreviation), StringComparer.Ordinal);

    [UseCulture("fr-FR")]
    [Theory]
    [InlineData(244587587, FileSizeUnit.Byte, true, "244 587 587,00 o")]
    [InlineData(244587587.76, FileSizeUnit.Byte, true, "244 587 587,76 o")]
    [InlineData(244587587, FileSizeUnit.Byte, false, "244 587 587,00 octets")]
    [InlineData(1, FileSizeUnit.Byte, false, "1,00 octet")]
    [InlineData(0, FileSizeUnit.Byte, false, "0,00 octet")]
    [InlineData(25452, FileSizeUnit.Megabyte, true, "25 452,00 mo")]
    [InlineData(25452.76, FileSizeUnit.Megabyte, true, "25 452,76 mo")]
    [InlineData(25452, FileSizeUnit.Megabyte, false, "25 452,00 megaoctets")]
    [InlineData(1, FileSizeUnit.Megabyte, false, "1,00 megaoctet")]
    [InlineData(0, FileSizeUnit.Megabyte, false, "0,00 megaoctet")]
    public void ToFileSizeFr(double value, FileSizeUnit unit, bool abbreviation, string expected)
    {
        if (!CultureInfo.CurrentCulture.NumberFormat.IsReadOnly)
            CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator = " ";
        Assert.Equal(expected, value.Humanize(unit, abbreviation), StringComparer.Ordinal);
    }

    [Theory]
    [InlineData(0, FileSizeUnit.Byte, FileSizeUnit.Byte, FileSizeUnit.Gigabyte, "0.00 b")]
    [InlineData(0.34, FileSizeUnit.Byte, FileSizeUnit.Byte, FileSizeUnit.Gigabyte, "0.34 b")]
    [InlineData(1, FileSizeUnit.Byte, FileSizeUnit.Byte, FileSizeUnit.Gigabyte, "1.00 b")]
    [InlineData(212454, FileSizeUnit.Byte, FileSizeUnit.Byte, FileSizeUnit.Gigabyte, "207.47 kb")]
    [InlineData(21245451, FileSizeUnit.Byte, FileSizeUnit.Byte, FileSizeUnit.Gigabyte, "20.26 mb")]
    [InlineData(2445875877, FileSizeUnit.Byte, FileSizeUnit.Byte, FileSizeUnit.Gigabyte, "2.28 gb")]
    [InlineData(244587587, FileSizeUnit.Byte, FileSizeUnit.Byte, FileSizeUnit.Kilobyte, "238,855.07 kb")]
    [InlineData(244587587, FileSizeUnit.Byte, FileSizeUnit.Gigabyte, FileSizeUnit.Terabyte, "0.23 gb")]
    [InlineData(212454, FileSizeUnit.Kilobyte, FileSizeUnit.Byte, FileSizeUnit.Byte, "217,552,896.00 b")]
    [InlineData(212454, FileSizeUnit.Kilobyte, FileSizeUnit.Byte, FileSizeUnit.Terabyte, "207.47 mb")]
    [InlineData(21245451, FileSizeUnit.Kilobyte, FileSizeUnit.Byte, FileSizeUnit.Terabyte, "20.26 gb")]
    [InlineData(2445875877, FileSizeUnit.Kilobyte, FileSizeUnit.Byte, FileSizeUnit.Terabyte, "2.28 tb")]
    [InlineData(244587587, FileSizeUnit.Kilobyte, FileSizeUnit.Byte, FileSizeUnit.Kilobyte, "244,587,587.00 kb")]
    [InlineData(244587587, FileSizeUnit.Kilobyte, FileSizeUnit.Gigabyte, FileSizeUnit.Terabyte, "233.26 gb")]
    public void ToPreferredFileSize(double value, FileSizeUnit unit, FileSizeUnit minUnit, FileSizeUnit maxUnit, string expected)
        => Assert.Equal(expected, value.Humanize(unit, minUnit, maxUnit), StringComparer.Ordinal);
}
