// -----------------------------------------------------------------------
// <copyright file="IDateTimeFormatter.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Units;

namespace MyNet.Humanizer.DateTimes;

/// <summary>
/// Implement this interface if your language has complex rules around dealing with numbers.
/// For example in Romanian "5 days" is "5 zile", while "24 days" is "24 de zile" and
/// in Arabic 2 days is يومين not 2 يوم.
/// </summary>
public interface IDateTimeFormatter
{
    /// <summary>
    /// Now.
    /// </summary>
    /// <returns>Returns Now.</returns>
    string Now();

    /// <summary>
    /// Never.
    /// </summary>
    /// <returns>Returns Never.</returns>
    string Never();

    /// <summary>
    /// 0 seconds.
    /// </summary>
    /// <returns>Returns 0 seconds as the string representation of Zero TimeSpan.</returns>
    string Zero();

    /// <summary>
    /// Returns the string representation of the provided DateTime.
    /// </summary>
    string DateHumanize(Tense timeUnitTense, TimeUnit timeUnit, int count);

    /// <summary>
    /// Returns the string representation of the provided TimeSpan.
    /// </summary>
    string TimeSpanHumanize(TimeUnit timeUnit, int count);
}
