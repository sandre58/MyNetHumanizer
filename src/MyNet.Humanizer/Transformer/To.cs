// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Linq;
using MyNet.Utilities.Localization;

namespace MyNet.Humanizer.Transformer
{
    /// <summary>
    /// A portal to string transformation using IStringTransformer
    /// </summary>
    public static class To
    {
        /// <summary>
        /// Transforms a string using the provided transformers. Transformations are applied in the provided order.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="transformers"></param>
        /// <returns></returns>
        public static string Transform(this string input, CultureInfo? culture = null, params IStringTransformer[] transformers) => transformers.Aggregate(input, (current, stringTransformer) => stringTransformer.Transform(current, culture ?? GlobalizationService.Current.Culture));

        /// <summary>
        /// Changes string to title case
        /// </summary>
        /// <example>
        /// "INvalid caSEs arE corrected" -> "Invalid Cases Are Corrected"
        /// </example>
        public static IStringTransformer TitleCase => new ToTitleCase();

        /// <summary>
        /// Changes the string to lower case
        /// </summary>
        /// <example>
        /// "Sentence casing" -> "sentence casing"
        /// </example>
        public static IStringTransformer LowerCase => new ToLowerCase();

        /// <summary>
        /// Changes the string to upper case
        /// </summary>
        /// <example>
        /// "lower case statement" -> "LOWER CASE STATEMENT"
        /// </example>
        public static IStringTransformer UpperCase => new ToUpperCase();

        /// <summary>
        /// Changes the string to sentence case
        /// </summary>
        /// <example>
        /// "lower case statement" -> "Lower case statement"
        /// </example>
        public static IStringTransformer SentenceCase => new ToSentenceCase();
    }
}
