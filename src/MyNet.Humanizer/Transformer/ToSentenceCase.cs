// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;

namespace MyNet.Humanizer.Transformer
{
    internal class ToSentenceCase : IStringTransformer
    {
        public string Transform(string input) => input.Length >= 1 ? string.Concat(input.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture), input.Substring(1)) : input.ToUpper(CultureInfo.CurrentCulture);
    }
}
