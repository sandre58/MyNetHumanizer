// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using MyNet.Utilities.Extensions;
using MyNet.Humanizer.Ordinalizing;
using MyNet.Utilities.Localization;

namespace MyNet.Humanizer
{
    /// <summary>
    /// Ordinalize extensions
    /// </summary>
    public static class OrdinalizeExtensions
    {
        static OrdinalizeExtensions() => ResourceLocator.Initialize();

        /// <summary>
        /// Turns a number into an ordinal string used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
        /// </summary>
        /// <param name="numberString">The number, in string, to be ordinalized</param>
        /// <returns></returns>
        public static string? Ordinalize(this string numberString) => Ordinalize(numberString, CultureInfo.CurrentCulture);

        /// <summary>
        /// Turns a number into an ordinal string used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
        /// </summary>
        /// <param name="numberString">The number, in string, to be ordinalized</param>
        /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
        /// <returns></returns>
        public static string? Ordinalize(this string numberString, CultureInfo culture) => culture.GetProvider<IOrdinalizer>()?.Convert(int.Parse(numberString, culture), numberString);

        /// <summary>
        /// Turns a number into an ordinal string used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
        /// Gender for Brazilian Portuguese locale
        /// "1".Ordinalize(GrammaticalGender.Masculine) -> "1º"
        /// "1".Ordinalize(GrammaticalGender.Feminine) -> "1ª"
        /// </summary>
        /// <param name="numberString">The number, in string, to be ordinalized</param>
        /// <param name="gender">The grammatical gender to use for output words</param>
        /// <returns></returns>
        public static string? Ordinalize(this string numberString, GrammaticalGender gender) => Ordinalize(numberString, gender, CultureInfo.CurrentCulture);

        /// <summary>
        /// Turns a number into an ordinal string used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
        /// Gender for Brazilian Portuguese locale
        /// "1".Ordinalize(GrammaticalGender.Masculine) -> "1º"
        /// "1".Ordinalize(GrammaticalGender.Feminine) -> "1ª"
        /// </summary>
        /// <param name="numberString">The number, in string, to be ordinalized</param>
        /// <param name="gender">The grammatical gender to use for output words</param>
        /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
        /// <returns></returns>
        public static string? Ordinalize(this string numberString, GrammaticalGender gender, CultureInfo culture) => culture.GetProvider<IOrdinalizer>()?.Convert(int.Parse(numberString, culture), numberString, gender);

        /// <summary>
        /// Turns a number into an ordinal number used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
        /// </summary>
        /// <param name="number">The number to be ordinalized</param>
        /// <returns></returns>
        public static string? Ordinalize(this int number) => Ordinalize(number, CultureInfo.CurrentCulture);

        /// <summary>
        /// Turns a number into an ordinal number used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
        /// </summary>
        /// <param name="number">The number to be ordinalized</param>
        /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
        /// <returns></returns>
        public static string? Ordinalize(this int number, CultureInfo culture) => culture.GetProvider<IOrdinalizer>()?.Convert(number, number.ToString(culture));

        /// <summary>
        /// Turns a number into an ordinal number used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
        /// Gender for Brazilian Portuguese locale
        /// 1.Ordinalize(GrammaticalGender.Masculine) -> "1º"
        /// 1.Ordinalize(GrammaticalGender.Feminine) -> "1ª"
        /// </summary>
        /// <param name="number">The number to be ordinalized</param>
        /// <param name="gender">The grammatical gender to use for output words</param>
        /// <returns></returns>
        public static string? Ordinalize(this int number, GrammaticalGender gender) => TranslationService.Current.GetProvider<IOrdinalizer>()?.Convert(number, number.ToString(CultureInfo.InvariantCulture), gender);

        /// <summary>
        /// Turns a number into an ordinal number used to denote the position in an ordered sequence such as 1st, 2nd, 3rd, 4th.
        /// Gender for Brazilian Portuguese locale
        /// 1.Ordinalize(GrammaticalGender.Masculine) -> "1º"
        /// 1.Ordinalize(GrammaticalGender.Feminine) -> "1ª"
        /// </summary>
        /// <param name="number">The number to be ordinalized</param>
        /// <param name="gender">The grammatical gender to use for output words</param>
        /// <param name="culture">Culture to use. If null, current thread's UI culture is used.</param>
        /// <returns></returns>
        public static string? Ordinalize(this int number, GrammaticalGender gender, CultureInfo culture) => culture.GetProvider<IOrdinalizer>()?.Convert(number, number.ToString(culture), gender);
    }
}
