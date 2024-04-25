// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Localization;

namespace MyNet.Humanizer.DateTimes
{
    public class EnglishDateTimeFormatter : DateTimeFormatter
    {
        public EnglishDateTimeFormatter() : base(Cultures.English) { }
    }
}
