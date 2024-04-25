// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel.DataAnnotations;

namespace MyNet.Humanizer.UnitTests
{
    public enum EnumUnderTest
    {
        MemberWithDescriptionAttributeSubclass,
        [CustomDescription(EnumTestsResources.MemberWithCustomDescriptionAttribute)]
        MemberWithCustomDescriptionAttribute,
        [ImposterDescription(42)]
        MemberWithImposterDescriptionAttribute,
        [CustomProperty(EnumTestsResources.MemberWithCustomPropertyAttribute)]
        MemberWithCustomPropertyAttribute,
        MemberWithoutDescriptionAttribute,
        ALLCAPITALS,
        [Display(Description = EnumTestsResources.MemberWithDisplayAttribute)]
        MemberWithDisplayAttribute,
        [Display(Description = "MemberWithLocalizedDisplayAttribute", ResourceType = typeof(EnumTestsResources))]
        MemberWithLocalizedDisplayAttribute,
        [Display(Name = EnumTestsResources.MemberWithDisplayAttributeWithoutDescription)]
        MemberWithDisplayAttributeWithoutDescription
    }

    public abstract class EnumTestsResources
    {
        protected EnumTestsResources()
        {

        }

        public const string MemberWithDescriptionAttribute = "Some Description";
        public const string MemberWithDescriptionAttributeSubclass = "Description in Description subclass";
        public const string MemberWithCustomDescriptionAttribute = "Description in custom Description attribute";
        public const string MemberWithImposterDescriptionAttribute = "Member with imposter description attribute";
        public const string MemberWithCustomPropertyAttribute = "Description in custom property attribute";
        public const string MemberWithoutDescriptionAttributeSentence = "Member without description attribute";
        public const string MemberWithoutDescriptionAttributeTitle = "Member Without Description Attribute";
        public const string MemberWithoutDescriptionAttributeLowerCase = "member without description attribute";
        public const string MemberWithDisplayAttribute = "Description from Display attribute";
        public const string MemberWithDisplayAttributeWithoutDescription = "Displayattribute without description";
        public static string MemberWithLocalizedDisplayAttribute => "Localized description from Display attribute";
    }

    [AttributeUsage(AttributeTargets.All)]
    public sealed class ImposterDescriptionAttribute(int description) : Attribute
    {
        public int Description { get; set; } = description;
    }

    [AttributeUsage(AttributeTargets.All)]
    public sealed class CustomDescriptionAttribute(string description) : Attribute
    {
        public string Description { get; set; } = description;
    }

    [AttributeUsage(AttributeTargets.All)]
    public sealed class CustomPropertyAttribute(string info) : Attribute
    {
        public string Info { get; set; } = info;
    }
}
