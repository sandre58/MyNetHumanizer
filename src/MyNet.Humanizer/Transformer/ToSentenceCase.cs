// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;

namespace MyNet.Humanizer.Transformer
{
    internal class ToSentenceCase : IStringTransformer
    {
        public string Transform(string input, CultureInfo culture) => input.Length >= 1 ? string.Concat(input.Substring(0, 1).ToUpper(culture), input.Substring(1)) : input.ToUpper(culture);
    }
}
