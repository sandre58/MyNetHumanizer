// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace MyNet.Humanizer.UnitTests
{
    public class DehumanizeToEnumTests
    {
        [Fact]
        public void ThrowsForEnumNoMatch() => _ = Assert.Throws<NoMatchFoundException>(() => EnumTestsResources.MemberWithDescriptionAttribute.DehumanizeTo<Dummy>());

        [Fact]
        public void DehumanizeMembersWithoutDescriptionAttribute() => Assert.Equal(EnumUnderTest.MemberWithoutDescriptionAttribute, EnumUnderTest.MemberWithoutDescriptionAttribute.ToString().DehumanizeTo<EnumUnderTest>());

        [Fact]
        public void AllCapitalMembersAreReturnedAsIs() => Assert.Equal(EnumUnderTest.ALLCAPITALS, EnumUnderTest.ALLCAPITALS.ToString().DehumanizeTo<EnumUnderTest>());

        [Fact]
        public void HonorsDisplayAttribute() => Assert.Equal(EnumUnderTest.MemberWithDisplayAttribute, EnumUnderTest.MemberWithDisplayAttribute.ToString().DehumanizeTo<EnumUnderTest>());

        private enum Dummy
        {
            First,
            Second
        }
    }



}
