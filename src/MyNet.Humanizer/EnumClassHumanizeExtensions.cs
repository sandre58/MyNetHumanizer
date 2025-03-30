// -----------------------------------------------------------------------
// <copyright file="EnumClassHumanizeExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using MyNet.Utilities;

namespace MyNet.Humanizer;

/// <summary>
/// Contains extension methods for humanizing Enums.
/// </summary>
public static class EnumClassHumanizeExtensions
{
    static EnumClassHumanizeExtensions() => ResourceLocator.Initialize();

    public static string? Humanize(this IEnumeration value, bool abbreviation = false, CultureInfo? culture = null)
    {
        var result = abbreviation ? value.ResourceKey.TranslateAbbreviated(culture) : value.ResourceKey.Translate(culture);

        return result == value.ResourceKey ? value.ToString()?.Humanize() : result;
    }

    /// <summary>
    /// Turns an enum member into a human readable string with the provided casing; e.g. AnonymousUser with Title casing -> Anonymous User. It also honors DescriptionAttribute data annotation.
    /// </summary>
    /// <param name="input">The enum member to be humanized.</param>
    /// <param name="casing">The casing to use for humanizing the enum member.</param>
    /// <param name="culture">Current culture.</param>
    public static string? Humanize(this IEnumeration input, LetterCasing casing, CultureInfo? culture = null)
    {
        var humanizedEnum = input.Humanize(culture: culture);

        return humanizedEnum?.ApplyCase(casing);
    }
}
