// -----------------------------------------------------------------------
// <copyright file="ToLowerCase.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace MyNet.Humanizer.Transformer;

internal sealed class ToLowerCase : IStringTransformer
{
    public string Transform(string input, CultureInfo culture) => culture.TextInfo.ToLower(input);
}
