// -----------------------------------------------------------------------
// <copyright file="EnglishDateTimeFormatter.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Localization;

namespace MyNet.Humanizer.DateTimes;

public class EnglishDateTimeFormatter : DateTimeFormatter
{
    public EnglishDateTimeFormatter()
        : base(Cultures.English) { }
}
