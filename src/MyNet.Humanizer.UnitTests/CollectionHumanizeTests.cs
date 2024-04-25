// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Xunit;

namespace MyNet.Humanizer.UnitTests
{
    public class SomeClass
    {
        public string? SomeString { get; set; }
        public int SomeInt { get; set; }
        public override string ToString() => "ToString";
    }

    [UseCulture("en")]
    public class CollectionHumanizeTests
    {
        [Fact]
        public void HumanizeReturnsOnlyNameWhenCollectionContainsOneItem()
        {
            var collection = new List<string> { "A String" };

            Assert.Equal("A String", collection.Humanize(", ", "and"));
        }

        [Fact]
        public void HumanizeUsesSeparatorWhenMoreThanOneItemIsInCollection()
        {
            var collection = new List<string>
            {
                "A String",
                "Another String",
            };

            Assert.Equal("A String or Another String", collection.Humanize(", ", "or"));
        }

        [Fact]
        public void HumanizeDefaultsSeparatorToAnd()
        {
            var collection = new List<string>
            {
                "A String",
                "Another String",
            };

            Assert.Equal("A String and Another String", collection.Humanize(", ", "and"));
        }

        [Fact]
        public void HumanizeUsesOxfordComma()
        {
            var collection = new List<string>
            {
                "A String",
                "Another String",
                "A Third String",
            };

            Assert.Equal("A String, Another String or A Third String", collection.Humanize(", ", "or"));
        }

        private readonly List<SomeClass> _testCollection =
        [
            new SomeClass { SomeInt = 1, SomeString = "One" },
            new SomeClass { SomeInt = 2, SomeString = "Two" },
            new SomeClass { SomeInt = 3, SomeString = "Three" }
        ];

        [Fact]
        public void HumanizeDefaultsToToString() => Assert.Equal("ToString, ToString or ToString", _testCollection.Humanize(", ", "or"));

        [Fact]
        public void HumanizeUsesStringDisplayFormatter()
        {
            var humanized = _testCollection.Humanize(sc => string.Format("SomeObject #{0} - {1}", sc.SomeInt, sc.SomeString), ", ", "and");
            Assert.Equal("SomeObject #1 - One, SomeObject #2 - Two and SomeObject #3 - Three", humanized);
        }

        [Fact]
        public void HumanizeUsesObjectDisplayFormatter()
        {
            var humanized = _testCollection.Humanize(sc => sc.SomeInt, ", ", "and");
            Assert.Equal("1, 2 and 3", humanized);
        }

        [Fact]
        public void HumanizeUsesStringDisplayFormatterWhenSeparatorIsProvided()
        {
            var humanized = _testCollection.Humanize(sc => string.Format("SomeObject #{0} - {1}", sc.SomeInt, sc.SomeString), ", ", "or");
            Assert.Equal("SomeObject #1 - One, SomeObject #2 - Two or SomeObject #3 - Three", humanized);
        }

        [Fact]
        public void HumanizeUsesObjectDisplayFormatterWhenSeparatorIsProvided()
        {
            var humanized = _testCollection.Humanize(sc => sc.SomeInt, ", ", "or");
            Assert.Equal("1, 2 or 3", humanized);
        }

        [Fact]
        public void HumanizeHandlesNullItemsWithoutAnException() => Assert.Null(Record.Exception(() => new object?[] { null, null }.Humanize(", ", "and")));

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "It's for Unit Tests")]
        public void HumanizeHandlesNullStringDisplayFormatterReturnsWithoutAnException() => Assert.Null(Record.Exception(() => new[] { "A", "B", "C" }.Humanize(_ => null, ", ", "and")));

        [Fact]
        public void HumanizeRunsStringDisplayFormatterOnNulls() => Assert.Equal("1, (null) and 3", new int?[] { 1, null, 3 }.Humanize(_ => _?.ToString() ?? "(null)", ", ", "and"));

        [Fact]
        public void HumanizeRunsObjectDisplayFormatterOnNulls() => Assert.Equal("1, 2 and 3", new int?[] { 1, null, 3 }.Humanize(_ => _ ?? 2, ", ", "and"));

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "It's for Unit Tests")]
        public void HumanizeRemovesEmptyItemsByDefault() => Assert.Equal("A and C", new[] { "A", " ", "C" }.Humanize(DummyFormatter, ", ", "and"));

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "It's for Unit Tests")]
        public void HumanizeTrimsItemsByDefault() => Assert.Equal("A, B and C", new[] { "A", "  B  ", "C" }.Humanize(DummyFormatter, ", ", "and"));

        private static readonly Func<string, string> DummyFormatter = input => input;
    }
}
