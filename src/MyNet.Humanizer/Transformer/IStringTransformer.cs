// -----------------------------------------------------------------------
// <copyright file="IStringTransformer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace MyNet.Humanizer.Transformer;

/// <summary>
/// Can tranform a string.
/// </summary>
public interface IStringTransformer
{
    /// <summary>
    /// Transform the input.
    /// </summary>
    string Transform(string input, CultureInfo culture);
}
