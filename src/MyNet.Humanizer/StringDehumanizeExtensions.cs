// -----------------------------------------------------------------------
// <copyright file="StringDehumanizeExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace MyNet.Humanizer;

/// <summary>
/// Contains extension methods for dehumanizing strings.
/// </summary>
public static class StringDehumanizeExtensions
{
    /// <summary>
    /// Dehumanizes a string; e.g. 'some string', 'Some String', 'Some string' -> 'SomeString'.
    /// </summary>
    /// <param name="input">The string to be dehumanized.</param>
    public static string Dehumanize(this string input)
    {
        var titlizedWords = input.Split(' ').Select(word => word.Humanize(LetterCasing.Title));
        return string.Join(string.Empty, titlizedWords).Replace(" ", string.Empty, System.StringComparison.OrdinalIgnoreCase);
    }
}
