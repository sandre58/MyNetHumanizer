// -----------------------------------------------------------------------
// <copyright file="FrenchInflector.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Humanizer.Inflections;

public class FrenchInflector : InflectorBase
{
    public FrenchInflector()
    {
        AddPlural("$", "s");
        AddPlural("s$", "s");
        AddPlural("x$", "x");
        AddPlural("z$", "z");
        AddPlural("(eau|au|eu)$", "$1x");
        AddPlural("al$", "aux");

        AddSingular("s$", string.Empty);
        AddSingular("aux$", "al");
        AddSingular("(eau|eu)x$", "$1");

        AddIrregular("boyau", "boyaux");
        AddIrregular("joyau", "joyaux");
        AddIrregular("tuyau", "tuyaux");
        AddIrregular("noyau", "noyaux");
        AddIrregular("cabillau", "cabillaux");
        AddIrregular("cabillau", "cabillaux");
        AddIrregular("chaux", "chaux");
        AddIrregular("faux", "faux");
        AddIrregular("oeil", "yeux");
        AddIrregular("pneu", "pneus");
        AddIrregular("bleu", "bleus");
        AddIrregular("�meu", "�meus");
        AddIrregular("carnaval", "carnavals");
        AddIrregular("caracal", "caracals");
        AddIrregular("chacal", "chacals");
        AddIrregular("choral", "chorals");
        AddIrregular("corral", "corrals");
        AddIrregular("�tal", "�tals");
        AddIrregular("festival", "festivals");
        AddIrregular("r�cital ", "r�citals");
        AddIrregular("val", "vals");
        AddIrregular("aspirail", "aspiraux");
        AddIrregular("corail", "coraux");
        AddIrregular("�mail", "�maux");
        AddIrregular("fermail", "fermaux");
        AddIrregular("soupirail", "soupiraux");
        AddIrregular("travail", "travaux");
        AddIrregular("vantail", "vantaux");
        AddIrregular("vitrail", "vitraux");
        AddIrregular("bail", "baux");
        AddIrregular("bijou", "bijoux");
        AddIrregular("caillou", "cailloux");
        AddIrregular("chou", "choux");
        AddIrregular("genou", "genoux");
        AddIrregular("hibou", "hiboux");
        AddIrregular("joujou", "joujoux");
        AddIrregular("pou", "poux");
    }
}
