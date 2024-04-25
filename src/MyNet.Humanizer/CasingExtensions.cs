// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Humanizer.Transformer;
using System;

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
        /// <returns></returns>
        public static string ApplyCase(this string input, LetterCasing casing) => casing switch
        {
            LetterCasing.Normal => input,
            LetterCasing.Title => input.Transform(To.TitleCase),
            LetterCasing.LowerCase => input.Transform(To.LowerCase),
            LetterCasing.AllCaps => input.Transform(To.UpperCase),
            LetterCasing.Sentence => input.Transform(To.SentenceCase),
            _ => throw new ArgumentOutOfRangeException(nameof(casing)),
        };

        public static string ToAllCaps(this string input) => ApplyCase(input, LetterCasing.AllCaps);
        public static string ToLowerCase(this string input) => ApplyCase(input, LetterCasing.LowerCase);
        public static string ToTitle(this string input) => ApplyCase(input, LetterCasing.Title);
        public static string ToSentence(this string input) => ApplyCase(input, LetterCasing.Sentence);
    }
}