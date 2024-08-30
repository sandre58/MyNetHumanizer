// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using MyNet.Utilities;
using MyNet.Utilities.Extensions;

namespace MyNet.Humanizer
{
    /// <summary>
    /// Contains extension methods for humanizing Enums
    /// </summary>
    public static class EnumerationHumanizeExtensions
    {
        static EnumerationHumanizeExtensions() => ResourceLocator.Initialize();

        public static string? Humanize(this IEnumeration value, bool abbreviation = false, CultureInfo? culture = null)
        {
            var result = abbreviation ? value.ResourceKey.TranslateAbbreviated(culture) : value.ResourceKey.Translate(culture);

            return result == value.ResourceKey ? value.ToString()?.Humanize() : result;
        }

        /// <summary>
        /// Turns an enum member into a human readable string with the provided casing; e.g. AnonymousUser with Title casing -> Anonymous User. It also honors DescriptionAttribute data annotation
        /// </summary>
        /// <param name="input">The enum member to be humanized</param>
        /// <param name="casing">The casing to use for humanizing the enum member</param>
        /// <returns></returns>
        public static string? Humanize(this IEnumeration input, LetterCasing casing, CultureInfo? culture = null)
        {
            var humanizedEnum = input.Humanize(culture: culture);

            return humanizedEnum?.ApplyCase(casing);
        }
    }
}
