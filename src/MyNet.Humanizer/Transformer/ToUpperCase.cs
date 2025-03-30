// -----------------------------------------------------------------------
// <copyright file="ToUpperCase.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace MyNet.Humanizer.Transformer;

internal sealed class ToUpperCase : IStringTransformer
{
    public string Transform(string input, CultureInfo culture) => input.ToUpper(CultureInfo.CurrentCulture);
}
