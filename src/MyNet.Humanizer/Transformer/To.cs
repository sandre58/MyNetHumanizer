// -----------------------------------------------------------------------
// <copyright file="To.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using System.Linq;
using MyNet.Utilities.Localization;

namespace MyNet.Humanizer.Transformer;

/// <summary>
/// A portal to string transformation using IStringTransformer.
/// </summary>
public static class To
{
    /// <summary>
    /// Gets changes string to title case.
    /// </summary>
    /// <example>
    /// "INvalid caSEs arE corrected" -> "Invalid Cases Are Corrected".
    /// </example>
    public static IStringTransformer TitleCase => new ToTitleCase();

    /// <summary>
    /// Gets changes the string to lower case.
    /// </summary>
    /// <example>
    /// "Sentence casing" -> "sentence casing".
    /// </example>
    public static IStringTransformer LowerCase => new ToLowerCase();

    /// <summary>
    /// Gets changes the string to upper case.
    /// </summary>
    /// <example>
    /// "lower case statement" -> "LOWER CASE STATEMENT".
    /// </example>
    public static IStringTransformer UpperCase => new ToUpperCase();

    /// <summary>
    /// Gets changes the string to sentence case.
    /// </summary>
    /// <example>
    /// "lower case statement" -> "Lower case statement".
    /// </example>
    public static IStringTransformer SentenceCase => new ToSentenceCase();

    /// <summary>
    /// Transforms a string using the provided transformers. Transformations are applied in the provided order.
    /// </summary>
    public static string Transform(this string input, CultureInfo? culture = null, params IStringTransformer[] transformers) => transformers.Aggregate(input, (current, stringTransformer) => stringTransformer.Transform(current, culture ?? GlobalizationService.Current.Culture));
}
