// -----------------------------------------------------------------------
// <copyright file="ToSentenceCase.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace MyNet.Humanizer.Transformer;

internal sealed class ToSentenceCase : IStringTransformer
{
    public string Transform(string input, CultureInfo culture) => input.Length >= 1 ? string.Concat(input[..1].ToUpper(culture), input[1..]) : input.ToUpper(culture);
}
