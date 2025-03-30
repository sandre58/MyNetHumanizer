// -----------------------------------------------------------------------
// <copyright file="CasingExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using MyNet.Humanizer.Transformer;

namespace MyNet.Humanizer;

/// <summary>
/// ApplyCase method to allow changing the case of a sentence easily.
/// </summary>
public static class CasingExtensions
{
    /// <summary>
    /// Changes the casing of the provided input.
    /// </summary>
    public static string ApplyCase(this string input, LetterCasing casing, CultureInfo? culture = null) => casing switch
    {
        LetterCasing.Normal => input,
        LetterCasing.Title => input.Transform(culture, To.TitleCase),
        LetterCasing.LowerCase => input.Transform(culture, To.LowerCase),
        LetterCasing.AllCaps => input.Transform(culture, To.UpperCase),
        LetterCasing.Sentence => input.Transform(culture, To.SentenceCase),
        _ => throw new ArgumentOutOfRangeException(nameof(casing))
    };

    public static string ToAllCaps(this string input) => ApplyCase(input, LetterCasing.AllCaps);

    public static string ToLowerCase(this string input) => ApplyCase(input, LetterCasing.LowerCase);

    public static string ToTitle(this string input) => ApplyCase(input, LetterCasing.Title);

    public static string ToSentence(this string input) => ApplyCase(input, LetterCasing.Sentence);
}
