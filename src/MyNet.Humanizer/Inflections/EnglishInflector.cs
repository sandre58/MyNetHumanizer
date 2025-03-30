// -----------------------------------------------------------------------
// <copyright file="EnglishInflector.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using MyNet.Utilities;

namespace MyNet.Humanizer.Inflections;

public class EnglishInflector : InflectorBase
{
    public EnglishInflector()
    {
        AddPlural("$", "s");
        AddPlural("s$", "s");
        AddPlural("(ax|test)is$", "$1es");
        AddPlural("(octop|vir|alumn|fung|cact|foc|hippopotam|radi|stimul|syllab|nucle)us$", "$1i");
        AddPlural("(alias|bias|iris|status|campus|apparatus|virus|walrus|trellis)$", "$1es");
        AddPlural("(buffal|tomat|volcan|ech|embarg|her|mosquit|potat|torped|vet)o$", "$1oes");
        AddPlural("([dti])um$", "$1a");
        AddPlural("sis$", "ses");
        AddPlural("(?:([^f])fe|([lr])f)$", "$1$2ves");
        AddPlural("(hive)$", "$1s");
        AddPlural("([^aeiouy]|qu)y$", "$1ies");
        AddPlural("(x|ch|ss|sh)$", "$1es");
        AddPlural("(matr|vert|ind|d)ix|ex$", "$1ices");
        AddPlural("(^[m|l])ouse$", "$1ice");
        AddPlural("^(ox)$", "$1en");
        AddPlural("(quiz)$", "$1zes");
        AddPlural("(buz|blit|walt)z$", "$1zes");
        AddPlural("(hoo|lea|loa|thie)f$", "$1ves");
        AddPlural("(alumn|alg|larv|vertebr)a$", "$1ae");
        AddPlural("(criteri|phenomen)on$", "$1a");

        AddSingular("s$", string.Empty);
        AddSingular("(n)ews$", "$1ews");
        AddSingular("([dti])a$", "$1um");
        AddSingular("(analy|ba|diagno|parenthe|progno|synop|the|ellip|empha|neuro|oa|paraly)ses$", "$1sis");
        AddSingular("([^f])ves$", "$1fe");
        AddSingular("(hive)s$", "$1");
        AddSingular("(tive)s$", "$1");
        AddSingular("([lr]|hoo|lea|loa|thie)ves$", "$1f");
        AddSingular("(^zomb)?([^aeiouy]|qu)ies$", "$2y");
        AddSingular("(s)eries$", "$1eries");
        AddSingular("(m)ovies$", "$1ovie");
        AddSingular("(x|ch|ss|sh)es$", "$1");
        AddSingular("(^[m|l])ice$", "$1ouse");
        AddSingular("(o)es$", "$1");
        AddSingular("(shoe)s$", "$1");
        AddSingular("(cris|ax|test)es$", "$1is");
        AddSingular("(octop|vir|alumn|fung|cact|foc|hippopotam|radi|stimul|syllab|nucle)i$", "$1us");
        AddSingular("(alias|bias|iris|status|campus|apparatus|virus|walrus|trellis)es$", "$1");
        AddSingular("^(ox)en", "$1");
        AddSingular("(matr|d)ices$", "$1ix");
        AddSingular("(vert|ind)ices$", "$1ex");
        AddSingular("(quiz)zes$", "$1");
        AddSingular("(buz|blit|walt)zes$", "$1z");
        AddSingular("(alumn|alg|larv|vertebr)ae$", "$1a");
        AddSingular("(criteri|phenomen)a$", "$1on");
        AddSingular("([b|r|c]ook|room|smooth)ies$", "$1ie");

        AddIrregular("person", "people");
        AddIrregular("man", "men");
        AddIrregular("human", "humans");
        AddIrregular("child", "children");
        AddIrregular("sex", "sexes");
        AddIrregular("move", "moves");
        AddIrregular("goose", "geese");
        AddIrregular("wave", "waves");
        AddIrregular("die", "dice");
        AddIrregular("foot", "feet");
        AddIrregular("tooth", "teeth");
        AddIrregular("curriculum", "curricula");
        AddIrregular("database", "databases");
        AddIrregular("zombie", "zombies");
        AddIrregular("personnel", "personnel");
        AddIrregular("cache", "caches");

        AddIrregular("is", "are", matchEnding: false);
        AddIrregular("that", "those", matchEnding: false);
        AddIrregular("this", "these", matchEnding: false);
        AddIrregular("bus", "buses", matchEnding: false);
        AddIrregular("staff", "staff", matchEnding: false);

        AddUncountable("equipment");
        AddUncountable("information");
        AddUncountable("corn");
        AddUncountable("milk");
        AddUncountable("rice");
        AddUncountable("money");
        AddUncountable("species");
        AddUncountable("series");
        AddUncountable("fish");
        AddUncountable("sheep");
        AddUncountable("deer");
        AddUncountable("aircraft");
        AddUncountable("oz");
        AddUncountable("tsp");
        AddUncountable("tbsp");
        AddUncountable("ml");
        AddUncountable("l");
        AddUncountable("water");
        AddUncountable("waters");
        AddUncountable("semen");
        AddUncountable("sperm");
        AddUncountable("bison");
        AddUncountable("grass");
        AddUncountable("hair");
        AddUncountable("mud");
        AddUncountable("elk");
        AddUncountable("luggage");
        AddUncountable("moose");
        AddUncountable("offspring");
        AddUncountable("salmon");
        AddUncountable("shrimp");
        AddUncountable("someone");
        AddUncountable("swine");
        AddUncountable("trout");
        AddUncountable("tuna");
        AddUncountable("corps");
        AddUncountable("scissors");
        AddUncountable("means");
        AddUncountable("mail");
        AddUncountable("data");
    }

    public override bool IsPlural(double count) => !Math.Abs(count).NearlyEqual(1);
}
