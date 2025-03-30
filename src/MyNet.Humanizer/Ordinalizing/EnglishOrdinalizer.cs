// -----------------------------------------------------------------------
// <copyright file="EnglishOrdinalizer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Humanizer.Ordinalizing;

public class EnglishOrdinalizer : DefaultOrdinalizer
{
    public override string Convert(int number, string numberString)
    {
        var nMod100 = number % 100;

        return nMod100 is >= 11 and <= 13
            ? numberString + "th"
            : (number % 10) switch
            {
                1 => numberString + "st",
                2 => numberString + "nd",
                3 => numberString + "rd",
                _ => numberString + "th"
            };
    }
}
