// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Text.RegularExpressions;
using MyNet.Utilities.Extensions;
using MyNet.Humanizer.Inflections;
using MyNet.Utilities.Localization;
using MyNet.Utilities;

namespace MyNet.Humanizer
{
    /// <summary>
    /// Inflector extensions
    /// </summary>
    public static partial class InflectorExtensions
    {
        static InflectorExtensions() => ResourceLocator.Initialize();

        private static string GetPluralKey(this string key) => key + "Plural";
        private static string GetZeroKey(this string key) => key + "Zero";

        public static string? Translate(this string key, double count, bool abbreviation = false) => key?.WithCountSuffix(count)?.Translate(abbreviation);

        public static string? Translate(this string key, string filename, double count) => TranslationService.Current.Translate(key.WithCountSuffix(count), filename);

        public static string? TranslateWithCountAndOptionalFormat(this string key, double count, bool abbreviation = false)
        {
            var format = key?.WithCountSuffix(count)?.Translate(abbreviation);

            var isPlural = TranslationService.Current.GetProvider<IInflector>()?.IsPlural(count);
            return isPlural.IsTrue() ? count.ToString(format, CultureInfo.CurrentCulture) : format;
        }

        public static string? WithCountSuffix(this string key, double count)
        {
            var isPlural = TranslationService.Current.GetProvider<IInflector>()?.IsPlural(count);
            return count.NearlyEqual(0) ? key.GetZeroKey() : isPlural != null && !isPlural.Value ? key : key.GetPluralKey();
        }

        /// <summary>
        /// Pluralizes the provided input considering irregular words
        /// </summary>
        /// <param name="word">Word to be pluralized</param>
        /// <param name="inputIsKnownToBeSingular">Normally you call Pluralize on singular words; but if you're unsure call it with false</param>
        /// <returns></returns>
        public static string? Pluralize(this string word, bool inputIsKnownToBeSingular = true) => word.Pluralize(CultureInfo.CurrentCulture, inputIsKnownToBeSingular);

        /// <summary>
        /// Pluralizes the provided input considering irregular words
        /// </summary>
        /// <param name="word">Word to be pluralized</param>
        /// <param name="culture"></param>
        /// <param name="inputIsKnownToBeSingular">Normally you call Pluralize on singular words; but if you're unsure call it with false</param>
        /// <returns></returns>
        public static string? Pluralize(this string word, CultureInfo culture, bool inputIsKnownToBeSingular = true) => culture.GetProvider<IInflector>()?.Pluralize(word, inputIsKnownToBeSingular);

        /// <summary>
        /// Singularizes the provided input considering irregular words
        /// </summary>
        /// <param name="word">Word to be singularized</param>
        /// <param name="inputIsKnownToBePlural">Normally you call Singularize on plural words; but if you're unsure call it with false</param>
        /// <param name="skipSimpleWords">Skip singularizing single words that have an 's' on the end</param>
        /// <returns></returns>
        public static string? Singularize(this string word, bool inputIsKnownToBePlural = true, bool skipSimpleWords = false) => word.Singularize(CultureInfo.CurrentCulture, inputIsKnownToBePlural, skipSimpleWords);

        /// <summary>
        /// Singularizes the provided input considering irregular words
        /// </summary>
        /// <param name="word">Word to be singularized</param>
        /// <param name="culture"></param>
        /// <param name="inputIsKnownToBePlural">Normally you call Singularize on plural words; but if you're unsure call it with false</param>
        /// <param name="skipSimpleWords">Skip singularizing single words that have an 's' on the end</param>
        /// <returns></returns>
        public static string? Singularize(this string word, CultureInfo culture, bool inputIsKnownToBePlural = true, bool skipSimpleWords = false) => culture.GetProvider<IInflector>()?.Singularize(word, inputIsKnownToBePlural, skipSimpleWords);

        /// <summary>
        /// Humanizes the input with Title casing
        /// </summary>
        /// <param name="input">The string to be titleized</param>
        /// <returns></returns>
        public static string? Titleize(this string input) => input.Humanize(LetterCasing.Title);

        /// <summary>
        /// By default, pascalize converts strings to UpperCamelCase also removing underscores
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string? Pascalize(this string input) => PascalizeRegex().Replace(input, match => match.Groups[1].Value.ToUpper());

        /// <summary>
        /// Separates the input words with underscore
        /// </summary>
        /// <param name="input">The string to be underscored</param>
        /// <returns></returns>
        public static string? Underscore(this string input) => UnderscoreRegex().Replace(UnderscoreRegex2().Replace(UnderscoreRegex3().Replace(input, "$1_$2"), "$1_$2"), "_").ToLower();

        /// <summary>
        /// Same as Pascalize except that the first character is lower case
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string? Camelize(this string input)
        {
            var word = input.Pascalize();
            return word?.Length > 0 ? $"{word.Substring(0, 1).ToLower()}{word.Substring(1)}" : word;
        }

        /// <summary>
        /// Replaces underscores with dashes in the string
        /// </summary>
        /// <param name="underscoredWord"></param>
        /// <returns></returns>
        public static string? Dasherize(this string underscoredWord) => underscoredWord.Replace('_', '-');

        /// <summary>
        /// Replaces underscores with hyphens in the string
        /// </summary>
        /// <param name="underscoredWord"></param>
        /// <returns></returns>
        public static string? Hyphenate(this string underscoredWord) => underscoredWord.Dasherize();

        /// <summary>
        /// Separates the input words with hyphens and all the words are converted to lowercase
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string? Kebaberize(this string input) => input.Underscore()?.Dasherize();

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
