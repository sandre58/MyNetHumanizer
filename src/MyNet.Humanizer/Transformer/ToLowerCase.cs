// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;

namespace MyNet.Humanizer.Transformer
{
    internal class ToLowerCase : IStringTransformer
    {
        public string Transform(string input, CultureInfo culture) => culture.TextInfo.ToLower(input);
    }
}
