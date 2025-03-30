// -----------------------------------------------------------------------
// <copyright file="FixedNumberOfCharactersTruncator.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace MyNet.Humanizer.Truncation;

/// <summary>
/// Truncate a string to a fixed number of letters or digits.
/// </summary>
internal sealed class FixedNumberOfCharactersTruncator : ITruncator
{
    public string Truncate(string value, int length, string truncationString, TruncateFrom truncateFrom = TruncateFrom.Right)
    {
        if (value.Length == 0)
            return value;

        if (truncationString.Length > length)
            return truncateFrom == TruncateFrom.Right ? value[..length] : value.Substring(value.Length - length, length);

        var alphaNumericalCharactersProcessed = 0;

        if (value.ToCharArray().Count(char.IsLetterOrDigit) <= length)
            return value;

        if (truncateFrom == TruncateFrom.Left)
        {
            for (var i = value.Length - 1; i > 0; i--)
            {
                if (char.IsLetterOrDigit(value[i]))
                    alphaNumericalCharactersProcessed++;

                if (alphaNumericalCharactersProcessed + truncationString.Length == length)
                    return $"{truncationString}{value[i..]}";
            }
        }

        for (var i = 0; i < value.Length - truncationString.Length; i++)
        {
            if (char.IsLetterOrDigit(value[i]))
                alphaNumericalCharactersProcessed++;

            if (alphaNumericalCharactersProcessed + truncationString.Length == length)
                return $"{value[..(i + 1)]}{truncationString}";
        }

        return value;
    }
}
