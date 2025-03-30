﻿// -----------------------------------------------------------------------
// <copyright file="MetricNumeralExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MyNet.Humanizer;

/// <summary>
/// Contains extension methods for changing a number to Metric representation (ToMetric)
/// and from Metric representation back to the number (FromMetric).
/// </summary>
public static class MetricNumeralExtensions
{
    private const int Limit = 27;
    private static readonly double BigLimit = Math.Pow(10, Limit);
    private static readonly double SmallLimit = Math.Pow(10, -Limit);

    /// <summary>
    /// Symbols is a list of every symbols for the Metric system.
    /// </summary>
    private static readonly List<char>[] Symbols =
    [
        ['k', 'M', 'G', 'T', 'P', 'E', 'Z', 'Y'],
        ['m', 'μ', 'n', 'p', 'f', 'a', 'z', 'y']
    ];

    /// <summary>
    /// Names link a Metric symbol (as key) to its name (as value).
    /// </summary>
    /// <remarks>
    /// We dont support :
    /// {'h', "hecto"},
    /// {'da', "deca" }, // !string
    /// {'d', "deci" },
    /// {'c', "centi"}.
    /// </remarks>
    private static readonly Dictionary<char, string> Names = new()
    {
        {
            'Y', "yotta"
        },
        {
            'Z', "zetta"
        },
        {
            'E', "exa"
        },
        {
            'P', "peta"
        },
        {
            'T', "tera"
        },
        {
            'G', "giga"
        },
        {
            'M', "mega"
        },
        {
            'k', "kilo"
        },
        {
            'm', "milli"
        },
        {
            'μ', "micro"
        },
        {
            'n', "nano"
        },
        {
            'p', "pico"
        },
        {
            'f', "femto"
        },
        {
            'a', "atto"
        },
        {
            'z', "zepto"
        },
        {
            'y', "yocto"
        }
    };

    /// <summary>
    /// Converts a Metric representation into a number.
    /// </summary>
    /// <remarks>
    /// We don't support input in the format {number}{name} nor {number} {name}.
    /// We only provide a solution for {number}{symbol} and {number} {symbol}.
    /// </remarks>
    /// <param name="input">Metric representation to convert to a number.</param>
    /// <example>
    /// <code>
    /// "1k".FromMetric() => 1000d
    /// "123".FromMetric() => 123d
    /// "100m".FromMetric() => 1E-1
    /// </code>
    /// </example>
    /// <returns>A number after a conversion from a Metric representation.</returns>
    public static double FromMetric(this string? input)
    {
        input = CleanRepresentation(input);
        return BuildNumber(input, input[^1]);
    }

    /// <summary>
    /// Converts a number into a valid and Human-readable Metric representation.
    /// </summary>
    /// <remarks>
    /// Inspired by a snippet from Thom Smith.
    /// See <a href="http://stackoverflow.com/questions/12181024/formatting-a-number-with-a-metric-prefix">this link</a> for more.
    /// </remarks>
    /// <param name="input">Number to convert to a Metric representation.</param>
    /// <param name="hasSpace">True will split the number and the symbol with a whitespace.</param>
    /// <param name="useSymbol">True will use symbol instead of name.</param>
    /// <param name="decimals">If not null it is the numbers of decimals to round the number to.</param>
    /// <example>
    /// <code>
    /// 1000.ToMetric() => "1k"
    /// 123.ToMetric() => "123"
    /// 1E-1.ToMetric() => "100m"
    /// </code>
    /// </example>
    /// <returns>A valid Metric representation.</returns>
    public static string ToMetric(this int input, bool hasSpace = false, bool useSymbol = true, int? decimals = null) => ((double)input).ToMetric(hasSpace, useSymbol, decimals);

    /// <summary>
    /// Converts a number into a valid and Human-readable Metric representation.
    /// </summary>
    /// <remarks>
    /// Inspired by a snippet from Thom Smith.
    /// See <a href="http://stackoverflow.com/questions/12181024/formatting-a-number-with-a-metric-prefix">this link</a> for more.
    /// </remarks>
    /// <param name="input">Number to convert to a Metric representation.</param>
    /// <param name="hasSpace">True will split the number and the symbol with a whitespace.</param>
    /// <param name="useSymbol">True will use symbol instead of name.</param>
    /// <param name="decimals">If not null it is the numbers of decimals to round the number to.</param>
    /// <example>
    /// <code>
    /// 1000d.ToMetric() => "1k"
    /// 123d.ToMetric() => "123"
    /// 1E-1.ToMetric() => "100m"
    /// </code>
    /// </example>
    /// <returns>A valid Metric representation.</returns>
    public static string ToMetric(this double input, bool hasSpace = false, bool useSymbol = true, int? decimals = null) => input.Equals(0)
        ? input.ToString(CultureInfo.CurrentCulture)
        : input.IsOutOfRange()
            ? throw new ArgumentOutOfRangeException(nameof(input))
            : BuildRepresentation(input, hasSpace, useSymbol, decimals);

    /// <summary>
    /// Clean or handle any wrong input.
    /// </summary>
    /// <param name="input">Metric representation to clean.</param>
    /// <returns>A cleaned representation.</returns>
    private static string CleanRepresentation(string? input)
    {
        ArgumentNullException.ThrowIfNull(input);

        input = input.Trim();
        input = ReplaceNameBySymbol(input);
        return input.Length == 0 || input.IsInvalidMetricNumeral()
            ? throw new ArgumentException(@"Empty or invalid Metric string.", nameof(input))
            : input.Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Build a number from a metric representation or from a number.
    /// </summary>
    /// <param name="input">A Metric representation to parse to a number.</param>
    /// <param name="last">The last character of input.</param>
    /// <returns>A number build from a Metric representation.</returns>
    private static double BuildNumber(string input, char last) => char.IsLetter(last)
        ? BuildMetricNumber(input, last)
        : double.Parse(input, CultureInfo.CurrentCulture);

    /// <summary>
    /// Build a number from a metric representation.
    /// </summary>
    /// <param name="input">A Metric representation to parse to a number.</param>
    /// <param name="last">The last character of input.</param>
    /// <returns>A number build from a Metric representation.</returns>
    private static double BuildMetricNumber(string input, char last)
    {
        var number = double.Parse(input.Remove(input.Length - 1), CultureInfo.CurrentCulture);
        var exponent = Math.Pow(10, Symbols[0].Contains(last) ? getExponent(Symbols[0]) : -getExponent(Symbols[1]));
        return number * exponent;

        double getExponent(List<char> symbols) => (symbols.IndexOf(last) + 1) * 3;
    }

    /// <summary>
    /// Replace every symbol's name by its symbol representation.
    /// </summary>
    /// <param name="input">Metric representation with a name or a symbol.</param>
    /// <returns>A metric representation with a symbol.</returns>
    private static string ReplaceNameBySymbol(string input) => Names.Aggregate(input, (current, name) =>
        current.Replace(name.Value, name.Key.ToString(CultureInfo.CurrentCulture), StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Build a Metric representation of the number.
    /// </summary>
    /// <param name="input">Number to convert to a Metric representation.</param>
    /// <param name="hasSpace">True will split the number and the symbol with a whitespace.</param>
    /// <param name="useSymbol">True will use symbol instead of name.</param>
    /// <param name="decimals">If not null it is the numbers of decimals to round the number to.</param>
    /// <returns>A number in a Metric representation.</returns>
    private static string BuildRepresentation(double input, bool hasSpace, bool useSymbol, int? decimals)
    {
        var exponent = (int)Math.Floor(Math.Log10(Math.Abs(input)) / 3);

        if (!exponent.Equals(0)) return BuildMetricRepresentation(input, exponent, hasSpace, useSymbol, decimals);
        var representation = decimals.HasValue ? Math.Round(input, decimals.Value).ToString(CultureInfo.CurrentCulture) : input.ToString(CultureInfo.CurrentCulture);
        if (hasSpace)
            representation += " ";
        return representation;
    }

    /// <summary>
    /// Build a Metric representation of the number.
    /// </summary>
    /// <param name="input">Number to convert to a Metric representation.</param>
    /// <param name="exponent">Exponent of the number in a scientific notation.</param>
    /// <param name="hasSpace">True will split the number and the symbol with a whitespace.</param>
    /// <param name="useSymbol">True will use symbol instead of name.</param>
    /// <param name="decimals">If not null it is the numbers of decimals to round the number to.</param>
    /// <returns>A number in a Metric representation.</returns>
    private static string BuildMetricRepresentation(double input, int exponent, bool hasSpace, bool useSymbol, int? decimals)
    {
        var number = input * Math.Pow(1000, -exponent);
        if (decimals.HasValue)
            number = Math.Round(number, decimals.Value);

        var symbol = Math.Sign(exponent) == 1
            ? Symbols[0][exponent - 1]
            : Symbols[1][-exponent - 1];
        return number
               + (hasSpace ? " " : string.Empty)
               + GetUnit(symbol, useSymbol);
    }

    /// <summary>
    /// Get the unit from a symbol of from the symbol's name.
    /// </summary>
    /// <param name="symbol">The symbol linked to the unit.</param>
    /// <param name="useSymbol">True will use symbol instead of name.</param>
    /// <returns>A symbol or a symbol's name.</returns>
    private static string GetUnit(char symbol, bool useSymbol) => useSymbol ? symbol.ToString() : Names[symbol];

    /// <summary>
    /// Check if a Metric representation is out of the valid range.
    /// </summary>
    /// <param name="input">A Metric representation who might be out of the valid range.</param>
    /// <returns>True if input is out of the valid range.</returns>
    private static bool IsOutOfRange(this double input)
    {
        return (Math.Sign(input) == 1 && outside(SmallLimit, BigLimit))
               || (Math.Sign(input) == -1 && outside(-BigLimit, -SmallLimit));

        bool outside(double min, double max) => !(max > input && input > min);
    }

    /// <summary>
    /// Check if a string is not a valid Metric representation.
    /// A valid representation is in the format "{0}{1}" or "{0} {1}"
    /// where {0} is a number and {1} is an allowed symbol.
    /// </summary>
    /// <param name="input">A string who might contain a invalid Metric representation.</param>
    /// <returns>True if input is not a valid Metric representation.</returns>
    private static bool IsInvalidMetricNumeral(this string input)
    {
        var index = input.Length - 1;
        var last = input[index];
        var isSymbol = Symbols[0].Contains(last) || Symbols[1].Contains(last);
        return !double.TryParse(isSymbol ? input.Remove(index) : input, out _);
    }
}
