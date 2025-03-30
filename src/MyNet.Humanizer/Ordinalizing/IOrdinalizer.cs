// -----------------------------------------------------------------------
// <copyright file="IOrdinalizer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Humanizer.Ordinalizing;

/// <summary>
/// The interface used to localise the Ordinalize method.
/// </summary>
public interface IOrdinalizer
{
    /// <summary>
    /// Ordinalizes the number.
    /// </summary>
    string Convert(int number, string numberString);

    /// <summary>
    /// Ordinalizes the number using the provided grammatical gender.
    /// </summary>
    string Convert(int number, string numberString, GrammaticalGender gender);
}
