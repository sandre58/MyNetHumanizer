// -----------------------------------------------------------------------
// <copyright file="ResourceLocator.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Humanizer.DateTimes;
using MyNet.Humanizer.Inflections;
using MyNet.Humanizer.Ordinalizing;
using MyNet.Humanizer.Resources;
using MyNet.Utilities.Localization;

namespace MyNet.Humanizer;

public static class ResourceLocator
{
    private static bool _isInitialized;

    public static void Initialize()
    {
        if (_isInitialized) return;

        TranslationService.RegisterResources(nameof(DateHumanizeResources), DateHumanizeResources.ResourceManager);
        TranslationService.RegisterResources(nameof(EnumHumanizeResources), EnumHumanizeResources.ResourceManager);

        LocalizationService.Register<IInflector, DefaultInflector>();
        LocalizationService.Register<IOrdinalizer, DefaultOrdinalizer>();

        LocalizationService.Register<IInflector, EnglishInflector>(Cultures.English);
        LocalizationService.Register<IOrdinalizer, EnglishOrdinalizer>(Cultures.English);
        LocalizationService.Register<IDateTimeFormatter, EnglishDateTimeFormatter>(Cultures.English);

        LocalizationService.Register<IInflector, FrenchInflector>(Cultures.French);
        LocalizationService.Register<IOrdinalizer, FrenchOrdinalizer>(Cultures.French);
        LocalizationService.Register<IDateTimeFormatter, FrenchDateTimeFormatter>(Cultures.French);

        _isInitialized = true;
    }
}
