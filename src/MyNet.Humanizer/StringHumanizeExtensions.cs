// -----------------------------------------------------------------------
// <copyright file="StringHumanizeExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyNet.Humanizer;

/// <summary>
/// Contains extension methods for humanizing string values.
/// </summary>
public static class StringHumanizeExtensions
{
    private static readonly Regex PascalCaseWordPartsRegex = new(@"[\p{Lu}]?[\p{Ll}]+|[0-9]+[\p{Ll}]*|[\p{Lu}]+(?=[\p{Lu}][\p{Ll}]|[0-9]|\b)|[\p{Lo}]+",
        RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptionsUtil.Compiled);

    private static readonly Regex FreestandingSpacingCharRegex = new(@"\s[-_]|[-_]\s", RegexOptionsUtil.Compiled);

    /// <summary>
    /// Humanizes the input string; e.g. Underscored_input_String_is_turned_INTO_sentence -> 'Underscored input String is turned INTO sentence'.
    /// </summary>
    /// <param name="input">The string to be humanized.</param>
    public static string Humanize(this string input)
    {
        // if input is all capitals (e.g. an acronym) then return it without change
        if (Array.TrueForAll(input.ToCharArray(), char.IsUpper))
            return input;

        // if input contains a dash or underscore which preceeds or follows a space (or both, e.g. free-standing)
        // remove the dash/underscore and run it through FromPascalCase
        return FreestandingSpacingCharRegex.IsMatch(input)
            ? FromPascalCase(FromUnderscoreDashSeparatedWords(input))
            : input.Contains('_', StringComparison.OrdinalIgnoreCase) || input.Contains('-', StringComparison.OrdinalIgnoreCase) ? FromUnderscoreDashSeparatedWords(input) : FromPascalCase(input);
    }

    /// <summary>
    /// Humanized the input string based on the provided casing.
    /// </summary>
    /// <param name="input">The string to be humanized.</param>
    /// <param name="casing">The desired casing for the output.</param>
    public static string Humanize(this string input, LetterCasing casing) => input.Humanize().ApplyCase(casing);

    private static string FromUnderscoreDashSeparatedWords(string input) => string.Join(" ", input.Split(separator: '_', '-'));

    private static string FromPascalCase(string input)
    {
        var result = string.Join(" ", PascalCaseWordPartsRegex
            .Matches(input)
            .Select(match => Array.TrueForAll(match.Value.ToCharArray(), char.IsUpper) &&
                             (match.Value.Length > 1 || (match.Index > 0 && input[match.Index - 1] == ' ') || match.Value == "I")
                ? match.Value
                : match.Value.ToLower(CultureInfo.CurrentCulture)));

        return result.Length > 0 ? char.ToUpper(result[0], CultureInfo.CurrentCulture) +
                                   result[1..] : result;
    }
}
