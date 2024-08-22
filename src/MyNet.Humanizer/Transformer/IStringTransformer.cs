// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Globalization;

namespace MyNet.Humanizer.Transformer
{
    /// <summary>
    /// Can tranform a string
    /// </summary>
    public interface IStringTransformer
    {
        /// <summary>
        /// Transform the input
        /// </summary>
        /// <param name="input">String to be transformed</param>
        /// <returns></returns>
        string Transform(string input, CultureInfo culture);
    }
}
