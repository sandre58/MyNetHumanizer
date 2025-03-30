// -----------------------------------------------------------------------
// <copyright file="CollectionHumanizeExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MyNet.Utilities;

namespace MyNet.Humanizer;

/// <summary>
/// Humanizes an IEnumerable into a human readable list.
/// </summary>
public static class CollectionHumanizeExtensions
{
    /// <summary>
    /// Formats the collection for display, calling ToString() on each object
    /// and using the provided separator.
    /// </summary>
    public static string Humanize<T>(this IEnumerable<T> collection, string separator, string? lastSeparator = null) => Humanize(collection, o => o?.ToString(), separator, lastSeparator);

    /// <summary>
    /// Formats the collection for display, calling <paramref name="displayFormatter"/> on each element
    /// and using the provided separator.
    /// </summary>
    public static string Humanize<T>(this IEnumerable<T> collection, Func<T, string?> displayFormatter, string separator, string? lastSeparator = null)
        => HumanizeDisplayStrings(collection.Select(displayFormatter), separator, lastSeparator);

    /// <summary>
    /// Formats the collection for display, calling <paramref name="displayFormatter"/> on each element
    /// and using the provided separator.
    /// </summary>
    public static string Humanize<T>(this IEnumerable<T> collection, Func<T, object> displayFormatter, string separator, string? lastSeparator = null)
        => HumanizeDisplayStrings(collection.Select(displayFormatter).Select(o => o.ToString()), separator, lastSeparator);

    private static string HumanizeDisplayStrings(IEnumerable<string?> strings, string separator, string? lastSeparator = null)
    {
        var itemsArray = strings
            .Select(item => item.OrEmpty().Trim())
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .ToArray();

        if (lastSeparator is null)
        {
            return string.Join(separator, itemsArray);
        }

        var count = itemsArray.Length;

        switch (count)
        {
            case 0:
                return string.Empty;
            case 1:
                return itemsArray[0];
        }

        var itemsBeforeLast = itemsArray.Take(count - 1);
        var lastItem = itemsArray.Skip(count - 1).First();

        return string.Format(CultureInfo.CurrentCulture, "{0} {1} {2}", string.Join(separator, itemsBeforeLast), lastSeparator, lastItem);
    }
}
