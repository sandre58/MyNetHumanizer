﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using MyNet.Utilities.Extensions;
using MyNet.Utilities;

namespace MyNet.Humanizer
{
    public static class NumberHumanizeExtensions
    {
        public const string DefaultUnitDoubleFormat = "{0:N2} {1}";
        public const string DefaultUnitIntFormat = "{0:N0} {1}";

        static NumberHumanizeExtensions() => ResourceLocator.Initialize();

        public static string? Humanize<T, TUnit>(this T value, TUnit unit, TUnit? minUnit = default, TUnit? maxUnit = default, bool abbreviation = true, string? format = null, CultureInfo? culture = null)
            where T : struct, IComparable<T>
            where TUnit : Enum
            => Humanize(value, unit.GetType(), unit, minUnit, maxUnit, abbreviation, format, culture);

        public static string? Humanize<T>(this T value, Type type, Enum unit, Enum? minUnit = null, Enum? maxUnit = null, bool abbreviation = true, string? format = null, CultureInfo? culture = null)
            where T : struct, IComparable<T>
        {
            var (newValue, newUnit) = value.Simplify(type, unit, minUnit, maxUnit);

            return Humanize(newValue, type, newUnit, abbreviation, format, culture);
        }

        public static string? Humanize<T, TUnit>(this T value, TUnit unit, bool abbreviation = true, string? format = null, CultureInfo? culture = null)
            where T : struct, IComparable<T>
            where TUnit : Enum
            => Humanize(value, unit.GetType(), unit, abbreviation, format, culture);

        public static string? Humanize<T>(this T value, Type type, Enum unit, bool abbreviation = true, string? format = null, CultureInfo? culture = null)
            where T : struct, IComparable<T>
        {
            if (double.TryParse(value.ToString(), out var dbl))
            {
                var input = type.Name + unit;
                var isInt = value is int;
                var transformedInput = abbreviation ? input.TranslateAbbreviated(culture) : input.Translate(culture);
                if (!abbreviation)
                {
                    var isPlural = dbl.IsPlural(culture);
                    transformedInput = !isPlural
                        ? transformedInput?.Singularize(inputIsKnownToBePlural: false)
                        : transformedInput?.Pluralize(inputIsKnownToBeSingular: false);
                }

                return (format ?? (isInt ? DefaultUnitIntFormat : DefaultUnitDoubleFormat)).FormatWith(dbl, transformedInput ?? string.Empty);
            }

            return string.Empty;
        }
    }
}
