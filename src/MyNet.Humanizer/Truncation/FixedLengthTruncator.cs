// -----------------------------------------------------------------------
// <copyright file="FixedLengthTruncator.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Humanizer.Truncation;

/// <summary>
/// Truncate a string to a fixed length.
/// </summary>
internal sealed class FixedLengthTruncator : ITruncator
{
    public string Truncate(string value, int length, string truncationString, TruncateFrom truncateFrom = TruncateFrom.Right) => value.Length == 0
            ? value
            : truncationString.Length > length
                ? truncateFrom == TruncateFrom.Right
                    ? value[..length]
                    : value.Substring(value.Length - length, length)
                : truncateFrom == TruncateFrom.Left
                    ? value.Length > length
                        ? $"{truncationString}{value[(value.Length - length + truncationString.Length)..]}"
                        : value
                    : value.Length > length
                        ? $"{value[..(length - truncationString.Length)]}{truncationString}"
                        : value;
}
