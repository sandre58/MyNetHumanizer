// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Text.RegularExpressions;
using MyNet.Humanizer.Inflections;
using MyNet.Utilities;
using MyNet.Utilities.Extensions;
using MyNet.Utilities.Localization;

namespace MyNet.Humanizer
{
    /// <summary>
    /// Inflector extensions
    /// </summary>
    public static partial class InflectorExtensions
    {
        public const string PluralSuffix = "Plural";
        public const string ZeroSuffix = "Zero";

        static InflectorExtensions() => ResourceLocator.Initialize();

        public static string ToPluralKey(this string key) => $"{key}{PluralSuffix}";

        public static string ToZeroKey(this string key) => $"{key}{ZeroSuffix}";

        public static string ToCountKey(this string key, double count, CultureInfo? culture = null)
        {
            var isPlural = count.IsPlural(culture);
            return count.NearlyEqual(0) ? key.ToZeroKey() : !isPlural ? key : key.ToPluralKey();
        }

        public static bool IsPlural(this int count, CultureInfo? culture = null) => LocalizationService.GetOrCurrent<IInflector>(culture)?.IsPlural(count) ?? count > 1;

        public static bool IsPlural(this double count, CultureInfo? culture = null) => LocalizationService.GetOrCurrent<IInflector>(culture)?.IsPlural(count) ?? count > 1;

        public static bool IsPlural(this long count, CultureInfo? culture = null) => LocalizationService.GetOrCurrent<IInflector>(culture)?.IsPlural(count) ?? count > 1;

        public static string TranslateWithCount(this string key, double count, bool abbreviation = false, CultureInfo? culture = null)
        {
            var countKey = key.ToCountKey(count, culture);
            return abbreviation ? countKey.TranslateAbbreviated(culture) : countKey.Translate(culture);
        }

        public static string TranslateWithCount(this string key, string filename, double count, bool abbreviation = false, CultureInfo? culture = null)
        {
            var countKey = key.ToCountKey(count, culture);
            return abbreviation ? countKey.TranslateAbbreviated(filename, culture) : countKey.Translate(filename, culture);
        }

        public static string TranslateAndFormatWithCount(this string key, double count, bool abbreviation = false, CultureInfo? culture = null)
        {
            var format = key.TranslateWithCount(count, abbreviation);

            var isPlural = count.IsPlural(culture);
            return isPlural ? count.ToString(format, culture ?? GlobalizationService.Current.Culture) : format;
        }

        /// <summary>
        /// Pluralizes the provided input considering irregular words
        /// </summary>
        /// <param name="word">Word to be pluralized</param>
        /// <param name="culture"></param>
        /// <param name="inputIsKnownToBeSingular">Normally you call Pluralize on singular words; but if you're unsure call it with false</param>
        /// <returns></returns>
        public static string Pluralize(this string word, bool inputIsKnownToBeSingular = true, CultureInfo? culture = null) => LocalizationService.GetOrCurrent<IInflector>(culture)?.Pluralize(word, inputIsKnownToBeSingular) ?? string.Empty;

        /// <summary>
        /// Singularizes the provided input considering irregular words
        /// </summary>
        /// <param name="word">Word to be singularized</param>
        /// <param name="culture"></param>
        /// <param name="inputIsKnownToBePlural">Normally you call Singularize on plural words; but if you're unsure call it with false</param>
        /// <param name="skipSimpleWords">Skip singularizing single words that have an 's' on the end</param>
        /// <returns></returns>
        public static string Singularize(this string word, bool inputIsKnownToBePlural = true, bool skipSimpleWords = false, CultureInfo? culture = null) => LocalizationService.GetOrCurrent<IInflector>(culture)?.Singularize(word, inputIsKnownToBePlural, skipSimpleWords) ?? string.Empty;

        /// <summary>
        /// Humanizes the input with Title casing
        /// </summary>
        /// <param name="input">The string to be titleized</param>
        /// <returns></returns>
        public static string Titleize(this string input) => input.Humanize(LetterCasing.Title);

        /// <summary>
        /// By default, pascalize converts strings to UpperCamelCase also removing underscores
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Pascalize(this string input) => PascalizeRegex().Replace(input, match => match.Groups[1].Value.ToUpper());

        /// <summary>
        /// Separates the input words with underscore
        /// </summary>
        /// <param name="input">The string to be underscored</param>
        /// <returns></returns>
        public static string Underscore(this string input) => UnderscoreRegex().Replace(UnderscoreRegex2().Replace(UnderscoreRegex3().Replace(input, "$1_$2"), "$1_$2"), "_").ToLower();

        /// <summary>
        /// Same as Pascalize except that the first character is lower case
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Camelize(this string input)
        {
            var word = input.Pascalize();
            return word.Length > 0 ? $"{word.Substring(0, 1).ToLower()}{word.Substring(1)}" : word;
        }

        /// <summary>
        /// Replaces underscores with dashes in the string
        /// </summary>
        /// <param name="underscoredWord"></param>
        /// <returns></returns>
        public static string Dasherize(this string underscoredWord) => underscoredWord.Replace('_', '-');

        /// <summary>
        /// Replaces underscores with hyphens in the string
        /// </summary>
        /// <param name="underscoredWord"></param>
        /// <returns></returns>
        public static string Hyphenate(this string underscoredWord) => underscoredWord.Dasherize();

        /// <summary>
        /// Separates the input words with hyphens and all the words are converted to lowercase
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Kebaberize(this string input) => input.Underscore().Dasherize();

        [GeneratedRegex("(?:^|_| +)(.)")]
        private static partial Regex PascalizeRegex();

        [GeneratedRegex("[-\\s]")]
        private static partial Regex UnderscoreRegex();

        [GeneratedRegex("([\\p{Ll}\\d])([\\p{Lu}])")]
        private static partial Regex UnderscoreRegex2();

        [GeneratedRegex("([\\p{Lu}]+)([\\p{Lu}][\\p{Ll}])")]
        private static partial Regex UnderscoreRegex3();
    }
}
