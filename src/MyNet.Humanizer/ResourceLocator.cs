// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Humanizer.DateTimes;
using MyNet.Humanizer.Inflections;
using MyNet.Humanizer.Ordinalizing;
using MyNet.Humanizer.Resources;
using MyNet.Utilities.Localization;

namespace MyNet.Humanizer
{
    public static class ResourceLocator
    {
        private static bool _isInitialized;

        public static void Initialize()
        {
            if (_isInitialized) return;

            TranslationService.RegisterResources(nameof(DateHumanizeResources), DateHumanizeResources.ResourceManager);
            TranslationService.RegisterResources(nameof(EnumHumanizeResources), EnumHumanizeResources.ResourceManager);

            TranslationService.AddDefaultProvider<IInflector, DefaultInflector>();
            TranslationService.AddDefaultProvider<IOrdinalizer, DefaultOrdinalizer>();

            _ = TranslationService.Get(Cultures.English).AddProvider<IInflector, EnglishInflector>()
                                                    .AddProvider<IOrdinalizer, EnglishOrdinalizer>()
                                                    .AddProvider<IDateTimeFormatter, EnglishDateTimeFormatter>();

            _ = TranslationService.Get(Cultures.French).AddProvider<IInflector, FrenchInflector>()
                                                    .AddProvider<IOrdinalizer, FrenchOrdinalizer>()
                                                    .AddProvider<IDateTimeFormatter, FrenchDateTimeFormatter>();

            _isInitialized = true;
        }

    }
}
