// -----------------------------------------------------------------------
// <copyright file="EnumHumanizeExtensions.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Resources;
using MyNet.Utilities;
using MyNet.Utilities.Localization;

namespace MyNet.Humanizer;

/// <summary>
/// Contains extension methods for humanizing Enums.
/// </summary>
public static class EnumHumanizeExtensions
{
    static EnumHumanizeExtensions() => ResourceLocator.Initialize();

    /// <summary>
    /// Turns an enum member into a human readable string; e.g. AnonymousUser -> Anonymous user. It also honors DescriptionAttribute data annotation.
    /// </summary>
    public static string? Humanize(this Enum value, bool abbreviation = false, CultureInfo? culture = null)
    {
        var displayAttr = value.GetAttribute<DisplayAttribute>();
        if (displayAttr != null)
        {
            if (displayAttr.ResourceType != null)
                return new ResourceManager(displayAttr.ResourceType).GetString(displayAttr.Name ?? displayAttr.Description ?? string.Empty, culture ?? GlobalizationService.Current.Culture);

            if (!string.IsNullOrEmpty(displayAttr.Description))
                return displayAttr.Description;

            if (!string.IsNullOrEmpty(displayAttr.Name))
                return displayAttr.Name;
        }

        var descAttr = value.GetAttribute<DescriptionAttribute>();
        if (descAttr != null)
            return descAttr.Description;

        var resourceName = value.GetType().Name + value;
        var result = abbreviation ? resourceName.TranslateAbbreviated(culture) : resourceName.Translate(culture);

        return result == resourceName ? value.ToString().Humanize() : result;
    }

    /// <summary>
    /// Turns an enum member into a human readable string with the provided casing; e.g. AnonymousUser with Title casing -> Anonymous User. It also honors DescriptionAttribute data annotation.
    /// </summary>
    public static string? Humanize(this Enum input, LetterCasing casing, CultureInfo? culture = null)
    {
        var humanizedEnum = input.Humanize(culture: culture);

        return humanizedEnum?.ApplyCase(casing);
    }

    public static string ToDescription(this Enum value) => value.GetType().GetField(value.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is DescriptionAttribute descriptionAttribute ? descriptionAttribute.Description : value.ToString();
}
