// -----------------------------------------------------------------------
// <copyright file="DefaultOrdinalizer.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Humanizer.Ordinalizing;

public class DefaultOrdinalizer : IOrdinalizer
{
    public virtual string Convert(int number, string numberString, GrammaticalGender gender) => Convert(number, numberString);

    public virtual string Convert(int number, string numberString) => numberString;
}
