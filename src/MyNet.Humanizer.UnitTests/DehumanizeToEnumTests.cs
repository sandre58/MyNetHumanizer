// -----------------------------------------------------------------------
// <copyright file="DehumanizeToEnumTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Xunit;

namespace MyNet.Humanizer.UnitTests;

public class DehumanizeToEnumTests
{
    private enum Dummy
    {
        // ReSharper disable once UnusedMember.Local
        First,

        // ReSharper disable once UnusedMember.Local
        Second
    }

    [Fact]
    public void ThrowsForEnumNoMatch() => _ = Assert.Throws<NoMatchFoundException>(() => EnumTestsResources.MemberWithDescriptionAttribute.DehumanizeTo<Dummy>(onNoMatch: OnNoMatch.ThrowsException));

    [Fact]
    public void DehumanizeMembersWithoutDescriptionAttribute() => Assert.Equal(EnumUnderTest.MemberWithoutDescriptionAttribute, EnumUnderTest.MemberWithoutDescriptionAttribute.ToString().DehumanizeTo<EnumUnderTest>());

    [Fact]
    public void AllCapitalMembersAreReturnedAsIs() => Assert.Equal(EnumUnderTest.ALLCAPITALS, EnumUnderTest.ALLCAPITALS.ToString().DehumanizeTo<EnumUnderTest>());

    [Fact]
    public void HonorsDisplayAttribute() => Assert.Equal(EnumUnderTest.MemberWithDisplayAttribute, EnumTestsResources.MemberWithDisplayAttribute.DehumanizeTo<EnumUnderTest>());
}
