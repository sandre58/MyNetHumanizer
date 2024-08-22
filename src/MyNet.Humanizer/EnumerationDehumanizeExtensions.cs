// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Linq;
using MyNet.Utilities;

namespace MyNet.Humanizer
{
    /// <summary>
    /// Contains extension methods for dehumanizing Enum string values.
    /// </summary>
    public static class EnumerationDehumanizeExtensions
    {
        /// <summary>
        /// Dehumanizes a string into the Enum it was originally Humanized from!
        /// </summary>
        /// <typeparam name="TTargetEnum">The target enum</typeparam>
        /// <param name="input">The string to be converted</param>
        /// <param name="onNoMatch"></param>
        /// <exception cref="ArgumentException">If TTargetEnum is not an enum</exception>
        /// <exception cref="NoMatchFoundException">Couldn't find any enum member that matches the string</exception>
        /// <returns></returns>
        public static TTargetEnum DehumanizeTo<TTargetEnum>(this string input, OnNoMatch onNoMatch = OnNoMatch.ReturnsDefault, CultureInfo? culture = null)
            where TTargetEnum : Enumeration<TTargetEnum>
            => (TTargetEnum)DehumanizeToPrivate(input, typeof(TTargetEnum), onNoMatch, culture)!;

        public static IEnumeration? DehumanizeTo(this string input, Type targetEnum, OnNoMatch onNoMatch = OnNoMatch.ReturnsDefault, CultureInfo? culture = null) => (IEnumeration?)DehumanizeToPrivate(input, targetEnum, onNoMatch, culture);

        private static object? DehumanizeToPrivate(string input, Type targetEnum, OnNoMatch onNoMatch, CultureInfo? culture = null)
        {
            var match = Enumeration.GetAll(targetEnum)
                            .OfType<IEnumeration>()
                            .FirstOrDefault(value =>
                                string.Equals(value.ToString(), input, StringComparison.OrdinalIgnoreCase)
                                || string.Equals(value.Humanize(culture: culture), input, StringComparison.OrdinalIgnoreCase));

            return match is null && onNoMatch == OnNoMatch.ThrowsException
                ? throw new NoMatchFoundException("Couldn't find any enum member that matches the string " + input)
                : match;
        }
    }
}
