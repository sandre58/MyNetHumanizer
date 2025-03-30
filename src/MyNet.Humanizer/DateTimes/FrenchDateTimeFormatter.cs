// -----------------------------------------------------------------------
// <copyright file="FrenchDateTimeFormatter.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Localization;

namespace MyNet.Humanizer.DateTimes;

public class FrenchDateTimeFormatter : DateTimeFormatter
{
    public FrenchDateTimeFormatter()
        : base(Cultures.French) { }
}
