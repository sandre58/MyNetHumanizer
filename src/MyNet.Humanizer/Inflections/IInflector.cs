﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Humanizer.Inflections
{
    public interface IInflector
    {
        string? Pluralize(string word, bool inputIsKnownToBeSingular = true);

        string? Singularize(string word, bool inputIsKnownToBePlural = true, bool skipSimpleWords = false);

        void AddIrregular(string singular, string plural, bool matchEnding = true);

        void AddUncountable(string word);

        void AddPlural(string rule, string replacement);

        void AddSingular(string rule, string replacement);

        bool IsPlural(double count);

    }
}
