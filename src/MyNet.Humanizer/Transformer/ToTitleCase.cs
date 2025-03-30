// -----------------------------------------------------------------------
// <copyright file="ToTitleCase.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyNet.Humanizer.Transformer;

internal sealed partial class ToTitleCase : IStringTransformer
{
    public string Transform(string input, CultureInfo culture)
    {
        var result = input;

        var matches = TitleRegex().Matches(input);
        foreach (var match in matches)
        {
            if (match is Match word && !AllCapitals(word.Value))
                result = ReplaceWithTitleCase(word, result);
        }

        return result;
    }

    private static bool AllCapitals(string input) => Array.TrueForAll(input.ToCharArray(), char.IsUpper);

    private static string ReplaceWithTitleCase(Match word, string source)
    {
        var wordToConvert = word.Value;
        var replacement = char.ToUpper(wordToConvert[0], CultureInfo.CurrentCulture) + wordToConvert.Remove(0, 1).ToLower(CultureInfo.CurrentCulture);
        return $"{source[..word.Index]}{replacement}{source[(word.Index + word.Length)..]}";
    }

    [GeneratedRegex("(\\w|[^\\u0000-\\u007F])+'?\\w*")]
    private static partial Regex TitleRegex();
}
