// -----------------------------------------------------------------------
// <copyright file="Truncator.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Humanizer.Truncation;

/// <summary>
/// Gets a ITruncator.
/// </summary>
public static class Truncator
{
    /// <summary>
    /// Gets fixed length truncator.
    /// </summary>
    public static ITruncator FixedLength => new FixedLengthTruncator();

    /// <summary>
    /// Gets fixed number of characters truncator.
    /// </summary>
    public static ITruncator FixedNumberOfCharacters => new FixedNumberOfCharactersTruncator();

    /// <summary>
    /// Gets fixed number of words truncator.
    /// </summary>
    public static ITruncator FixedNumberOfWords => new FixedNumberOfWordsTruncator();
}
