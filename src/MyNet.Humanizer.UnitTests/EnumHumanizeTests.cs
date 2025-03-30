// -----------------------------------------------------------------------
// <copyright file="EnumHumanizeTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Xunit;

namespace MyNet.Humanizer.UnitTests;

public class EnumHumanizeTests
{
    [Fact]
    public void OnlyStringDescriptionsApply() => Assert.Equal(EnumTestsResources.MemberWithImposterDescriptionAttribute, EnumUnderTest.MemberWithImposterDescriptionAttribute.Humanize());

    [Fact]
    public void CanHumanizeMembersWithoutDescriptionAttribute() => Assert.Equal(EnumTestsResources.MemberWithoutDescriptionAttributeSentence, EnumUnderTest.MemberWithoutDescriptionAttribute.Humanize());

    [Fact]
    public void CanApplyTitleCasingOnEnumHumanization() => Assert.Equal(
        EnumTestsResources.MemberWithoutDescriptionAttributeTitle,
        EnumUnderTest.MemberWithoutDescriptionAttribute.Humanize(LetterCasing.Title));

    [Fact]
    public void CanApplyLowerCaseCasingOnEnumHumanization() => Assert.Equal(
        EnumTestsResources.MemberWithoutDescriptionAttributeLowerCase,
        EnumUnderTest.MemberWithoutDescriptionAttribute.Humanize(LetterCasing.LowerCase));

    [Fact]
    public void AllCapitalMembersAreReturnedAsIs() => Assert.Equal(EnumUnderTest.ALLCAPITALS.ToString(), EnumUnderTest.ALLCAPITALS.Humanize());

    [Fact]
    public void HonorsDisplayAttribute() => Assert.Equal(EnumTestsResources.MemberWithDisplayAttribute, EnumUnderTest.MemberWithDisplayAttribute.Humanize());

    [Fact]
    public void HandlesDisplayAttributeWithNoDescription() => Assert.Equal(EnumTestsResources.MemberWithDisplayAttributeWithoutDescription, EnumUnderTest.MemberWithDisplayAttributeWithoutDescription.Humanize());
}
