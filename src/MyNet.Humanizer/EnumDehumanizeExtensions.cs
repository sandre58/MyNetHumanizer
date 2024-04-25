// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;

namespace MyNet.Humanizer
{
    /// <summary>
    /// Contains extension methods for dehumanizing Enum string values.
    /// </summary>
    public static class EnumDehumanizeExtensions
    {
        public static TTargetEnum? DehumanizeToNullable<TTargetEnum>(this string input)
            where TTargetEnum : Enum => DehumanizeToPrivate(input, typeof(TTargetEnum), OnNoMatch.ReturnsDefault) is TTargetEnum result ? result : default;

        /// <summary>
        /// Dehumanizes a string into the Enum it was originally Humanized from!
        /// </summary>
        /// <typeparam name="TTargetEnum">The target enum</typeparam>
        /// <param name="input">The string to be converted</param>
        /// <param name="onNoMatch"></param>
        /// <exception cref="ArgumentException">If TTargetEnum is not an enum</exception>
        /// <exception cref="NoMatchFoundException">Couldn't find any enum member that matches the string</exception>
        /// <returns></returns>
        public static TTargetEnum DehumanizeTo<TTargetEnum>(this string input, OnNoMatch onNoMatch = OnNoMatch.ThrowsException)
            where TTargetEnum : struct, Enum => DehumanizeToPrivate(input, typeof(TTargetEnum), onNoMatch) is TTargetEnum result ? result : default;

        /// <summary>
        /// Dehumanizes a string into the Enum it was originally Humanized from!
        /// </summary>
        /// <param name="input">The string to be converted</param>
        /// <param name="targetEnum">The target enum</param>
        /// <param name="onNoMatch">What to do when input is not matched to the enum.</param>
        /// <returns></returns>
        /// <exception cref="NoMatchFoundException">Couldn't find any enum member that matches the string</exception>
        /// <exception cref="ArgumentException">If targetEnum is not an enum</exception>
        public static Enum? DehumanizeTo(this string input, Type targetEnum, OnNoMatch onNoMatch = OnNoMatch.ThrowsException) => DehumanizeToPrivate(input, targetEnum, onNoMatch);

        private static Enum? DehumanizeToPrivate(string input, Type targetEnum, OnNoMatch onNoMatch)
        {
            var nullableType = Nullable.GetUnderlyingType(targetEnum);

            if (nullableType is not null && string.IsNullOrEmpty(input)) return null;

            var type = nullableType ?? targetEnum;

            var match = Enum.GetValues(type)
                            .OfType<Enum>()
                            .FirstOrDefault(value =>
                                string.Equals(value.ToString(), input, StringComparison.OrdinalIgnoreCase)
                                || string.Equals(value.Humanize(), input, StringComparison.OrdinalIgnoreCase));

            return match == null && onNoMatch == OnNoMatch.ThrowsException
                ? throw new NoMatchFoundException("Couldn't find any enum member that matches the string " + input)
                : match;
        }
    }
}
