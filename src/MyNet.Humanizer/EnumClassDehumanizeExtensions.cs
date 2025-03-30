// -----------------------------------------------------------------------
// <copyright file="EnumClassDehumanizeExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Linq;
using MyNet.Utilities;

namespace MyNet.Humanizer;

/// <summary>
/// Contains extension methods for dehumanizing Enum string values.
/// </summary>
public static class EnumClassDehumanizeExtensions
{
    /// <summary>
    /// Dehumanizes a string into the Enum it was originally Humanized from!.
    /// </summary>
    /// <exception cref="ArgumentException">If TTargetEnum is not an enum.</exception>
    /// <exception cref="NoMatchFoundException">Couldn't find any enum member that matches the string.</exception>
    public static TTargetEnum DehumanizeTo<TTargetEnum>(this string input, OnNoMatch onNoMatch = OnNoMatch.ReturnsDefault, CultureInfo? culture = null)
        where TTargetEnum : EnumClass<TTargetEnum>
        => (TTargetEnum)DehumanizeToPrivate(input, typeof(TTargetEnum), onNoMatch, culture)!;

    public static IEnumeration? DehumanizeTo(this string input, Type targetEnum, OnNoMatch onNoMatch = OnNoMatch.ReturnsDefault, CultureInfo? culture = null) => (IEnumeration?)DehumanizeToPrivate(input, targetEnum, onNoMatch, culture);

    private static object? DehumanizeToPrivate(string input, Type targetEnum, OnNoMatch onNoMatch, CultureInfo? culture = null)
    {
        var match = EnumClass.GetAll(targetEnum)
            .OfType<IEnumeration>()
            .FirstOrDefault(value =>
                string.Equals(value.ToString(), input, StringComparison.OrdinalIgnoreCase)
                || string.Equals(value.Humanize(culture: culture), input, StringComparison.OrdinalIgnoreCase));

        return match is null && onNoMatch == OnNoMatch.ThrowsException
            ? throw new NoMatchFoundException("Couldn't find any enum member that matches the string " + input)
            : match;
    }
}
