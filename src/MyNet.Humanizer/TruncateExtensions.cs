﻿// -----------------------------------------------------------------------
// <copyright file="TruncateExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Humanizer.Truncation;

namespace MyNet.Humanizer;

/// <summary>
/// Truncation location for humanizer.
/// </summary>
public enum TruncateFrom
{
    /// <summary>
    /// Truncate letters from the left (start) of the string.
    /// </summary>
    Left,

    /// <summary>
    /// Truncate letters from the right (end) of the string.
    /// </summary>
    Right
}

/// <summary>
/// Allow strings to be truncated.
/// </summary>
public static class TruncateExtensions
{
    /// <summary>
    /// Truncate the string.
    /// </summary>
    /// <param name="input">The string to be truncated.</param>
    /// <param name="length">The length to truncate to.</param>
    /// <returns>The truncated string.</returns>
    public static string? Truncate(this string input, int length) => input.Truncate(length, "…", Truncator.FixedLength);

    /// <summary>
    /// Truncate the string.
    /// </summary>
    /// <param name="input">The string to be truncated.</param>
    /// <param name="length">The length to truncate to.</param>
    /// <param name="truncator">The truncate to use.</param>
    /// <param name="from">The enum value used to determine from where to truncate the string.</param>
    /// <returns>The truncated string.</returns>
    public static string? Truncate(this string input, int length, ITruncator truncator, TruncateFrom from = TruncateFrom.Right) => input.Truncate(length, "…", truncator, from);

    /// <summary>
    /// Truncate the string.
    /// </summary>
    /// <param name="input">The string to be truncated.</param>
    /// <param name="length">The length to truncate to.</param>
    /// <param name="truncationString">The string used to truncate with.</param>
    /// <param name="from">The enum value used to determine from where to truncate the string.</param>
    /// <returns>The truncated string.</returns>
    public static string? Truncate(this string input, int length, string truncationString, TruncateFrom from = TruncateFrom.Right) => input.Truncate(length, truncationString, Truncator.FixedLength, from);

    /// <summary>
    /// Truncate the string.
    /// </summary>
    /// <param name="input">The string to be truncated.</param>
    /// <param name="length">The length to truncate to.</param>
    /// <param name="truncationString">The string used to truncate with.</param>
    /// <param name="truncator">The truncator to use.</param>
    /// <param name="from">The enum value used to determine from where to truncate the string.</param>
    /// <returns>The truncated string.</returns>
    public static string? Truncate(this string input, int length, string truncationString, ITruncator truncator, TruncateFrom from = TruncateFrom.Right) => truncator.Truncate(input, length, truncationString, from);
}
