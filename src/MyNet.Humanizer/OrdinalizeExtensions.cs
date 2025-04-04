﻿// -----------------------------------------------------------------------
// <copyright file="OrdinalizeExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using MyNet.Humanizer.Ordinalizing;
using MyNet.Utilities.Localization;

namespace MyNet.Humanizer;

/// <summary>
/// Ordinalize extensions.
/// </summary>
public static class OrdinalizeExtensions
{
    static OrdinalizeExtensions() => ResourceLocator.Initialize();

    /// <summary>
    /// Turns a number into an ordinal string used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
    /// </summary>
    /// <param name="numberString">The number, in string, to be ordinalized.</param>
    /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
    public static string Ordinalize(this string numberString, CultureInfo? culture = null) => LocalizationService.GetOrCurrent<IOrdinalizer>(culture)?.Convert(int.Parse(numberString, culture), numberString) ?? string.Empty;

    /// <summary>
    /// Turns a number into an ordinal string used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
    /// Gender for Brazilian Portuguese locale
    /// "1".Ordinalize(GrammaticalGender.Masculine) -> "1º"
    /// "1".Ordinalize(GrammaticalGender.Feminine) -> "1ª".
    /// </summary>
    /// <param name="numberString">The number, in string, to be ordinalized.</param>
    /// <param name="gender">The grammatical gender to use for output words.</param>
    /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
    public static string Ordinalize(this string numberString, GrammaticalGender gender, CultureInfo? culture = null) => LocalizationService.GetOrCurrent<IOrdinalizer>(culture)?.Convert(int.Parse(numberString, culture), numberString, gender) ?? string.Empty;

    /// <summary>
    /// Turns a number into an ordinal number used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
    /// </summary>
    /// <param name="number">The number to be ordinalized.</param>
    /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
    public static string Ordinalize(this int number, CultureInfo? culture = null) => LocalizationService.GetOrCurrent<IOrdinalizer>(culture)?.Convert(number, number.ToString(culture)) ?? string.Empty;

    /// <summary>
    /// Turns a number into an ordinal number used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
    /// Gender for Brazilian Portuguese locale
    /// 1.Ordinalize(GrammaticalGender.Masculine) -> "1º"
    /// 1.Ordinalize(GrammaticalGender.Feminine) -> "1ª".
    /// </summary>
    /// <param name="number">The number to be ordinalized.</param>
    /// <param name="gender">The grammatical gender to use for output words.</param>
    /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
    public static string Ordinalize(this int number, GrammaticalGender gender, CultureInfo? culture = null) => LocalizationService.GetOrCurrent<IOrdinalizer>(culture)?.Convert(number, number.ToString(culture), gender) ?? string.Empty;
}
