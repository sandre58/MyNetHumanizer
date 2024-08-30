// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;

namespace MyNet.Humanizer
{
    /// <summary>
    /// Contains extension methods for dehumanizing Enum string values.
    /// </summary>
    public static class EnumDehumanizeExtensions
    {
        /// <summary>
        /// Dehumanizes a string into the Enum it was originally Humanized from!
        /// </summary>
        /// <typeparam name="TTargetEnum">The target enum</typeparam>
        /// <param name="input">The string to be converted</param>
        /// <param name="onNoMatch"></param>
        /// <param name="culture"></param>
        /// <exception cref="ArgumentException">If TTargetEnum is not an enum</exception>
        /// <exception cref="NoMatchFoundException">Couldn't find any enum member that matches the string</exception>
        /// <returns></returns>
        public static TTargetEnum DehumanizeTo<TTargetEnum>(this string input, OnNoMatch onNoMatch = OnNoMatch.ReturnsDefault, CultureInfo? culture = null)
            where TTargetEnum : struct, Enum => DehumanizeToPrivate(input, typeof(TTargetEnum), onNoMatch, culture) is TTargetEnum result ? result : default;

        /// <summary>
        /// Dehumanizes a string into the Enum it was originally Humanized from!
        /// </summary>
        /// <param name="input">The string to be converted</param>
        /// <param name="targetEnum">The target enum</param>
        /// <param name="onNoMatch">What to do when input is not matched to the enum.</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NoMatchFoundException">Couldn't find any enum member that matches the string</exception>
        /// <exception cref="ArgumentException">If targetEnum is not an enum</exception>
        public static Enum? DehumanizeTo(this string input, Type targetEnum, OnNoMatch onNoMatch = OnNoMatch.ReturnsDefault, CultureInfo? culture = null) => DehumanizeToPrivate(input, targetEnum, onNoMatch, culture);

        private static Enum? DehumanizeToPrivate(string input, Type targetEnum, OnNoMatch onNoMatch, CultureInfo? culture = null)
        {
            var nullableType = Nullable.GetUnderlyingType(targetEnum);

            if (nullableType is not null && string.IsNullOrEmpty(input)) return null;

            var type = nullableType ?? targetEnum;

            var match = Enum.GetValues(type)
                            .OfType<Enum>()
                            .FirstOrDefault(value =>
                                string.Equals(value.ToString(), input, StringComparison.OrdinalIgnoreCase)
                                || string.Equals(value.Humanize(culture: culture), input, StringComparison.OrdinalIgnoreCase));

            return match == null && onNoMatch == OnNoMatch.ThrowsException
                ? throw new NoMatchFoundException("Couldn't find any enum member that matches the string " + input)
                : match;
        }
    }
}
