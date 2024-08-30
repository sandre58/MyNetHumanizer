// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Humanizer.Transformer;
using System;
using System.Globalization;

namespace MyNet.Humanizer
{
    /// <summary>
    /// ApplyCase method to allow changing the case of a sentence easily
    /// </summary>
    public static class CasingExtensions
    {
        /// <summary>
        /// Changes the casing of the provided input
        /// </summary>
        /// <param name="input"></param>
        /// <param name="casing"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ApplyCase(this string input, LetterCasing casing, CultureInfo? culture = null) => casing switch
        {
            LetterCasing.Normal => input,
            LetterCasing.Title => input.Transform(culture, To.TitleCase),
            LetterCasing.LowerCase => input.Transform(culture, To.LowerCase),
            LetterCasing.AllCaps => input.Transform(culture, To.UpperCase),
            LetterCasing.Sentence => input.Transform(culture, To.SentenceCase),
            _ => throw new ArgumentOutOfRangeException(nameof(casing)),
        };

        public static string ToAllCaps(this string input) => ApplyCase(input, LetterCasing.AllCaps);
        public static string ToLowerCase(this string input) => ApplyCase(input, LetterCasing.LowerCase);
        public static string ToTitle(this string input) => ApplyCase(input, LetterCasing.Title);
        public static string ToSentence(this string input) => ApplyCase(input, LetterCasing.Sentence);
    }
}
