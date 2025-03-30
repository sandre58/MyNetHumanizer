// -----------------------------------------------------------------------
// <copyright file="FrenchOrdinalizer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Humanizer.Ordinalizing;

public class FrenchOrdinalizer : DefaultOrdinalizer
{
    public override string Convert(int number, string numberString) => Convert(number, numberString, GrammaticalGender.Masculine);

    public override string Convert(int number, string numberString, GrammaticalGender gender) => number == 1 ? gender == GrammaticalGender.Feminine ? numberString + "ère" : numberString + "er" : numberString + "ème";
}
