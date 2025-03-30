// -----------------------------------------------------------------------
// <copyright file="CollectionHumanizeTests.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace MyNet.Humanizer.UnitTests;

[UseCulture("en")]
[Collection("UseCultureSequential")]
public class CollectionHumanizeTests
{
    private static readonly Func<string, string> DummyFormatter = input => input;

    private readonly List<SomeClass> _testCollection =
    [
        new() { SomeInt = 1, SomeString = "One" },
        new() { SomeInt = 2, SomeString = "Two" },
        new() { SomeInt = 3, SomeString = "Three" }
    ];

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
            "Another String"
        };

        Assert.Equal("A String or Another String", collection.Humanize(", ", "or"));
    }

    [Fact]
    public void HumanizeDefaultsSeparatorToAnd()
    {
        var collection = new List<string>
        {
            "A String",
            "Another String"
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
            "A Third String"
        };

        Assert.Equal("A String, Another String or A Third String", collection.Humanize(", ", "or"));
    }

    [Fact]
    public void HumanizeDefaultsToToString() => Assert.Equal("ToString, ToString or ToString", _testCollection.Humanize(", ", "or"));

    [Fact]
    public void HumanizeUsesStringDisplayFormatter()
    {
        var humanized = _testCollection.Humanize(sc => string.Format(CultureInfo.CurrentCulture, "SomeObject #{0} - {1}", sc.SomeInt, sc.SomeString), ", ", "and");
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
        var humanized = _testCollection.Humanize(sc => string.Format(CultureInfo.CurrentCulture, "SomeObject #{0} - {1}", sc.SomeInt, sc.SomeString), ", ", "or");
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
    public void HumanizeHandlesNullStringDisplayFormatterReturnsWithoutAnException() => Assert.Null(Record.Exception(() => new[] { "A", "B", "C" }.Humanize(_ => null, ", ", "and")));

    [Fact]
    public void HumanizeRunsStringDisplayFormatterOnNulls() => Assert.Equal("1, (null) and 3", new int?[] { 1, null, 3 }.Humanize(x => x?.ToString(CultureInfo.CurrentCulture) ?? "(null)", ", ", "and"));

    [Fact]
    public void HumanizeRunsObjectDisplayFormatterOnNulls() => Assert.Equal("1, 2 and 3", new int?[] { 1, null, 3 }.Humanize(x => x ?? 2, ", ", "and"));

    [Fact]
    public void HumanizeRemovesEmptyItemsByDefault() => Assert.Equal("A and C", new[] { "A", " ", "C" }.Humanize(DummyFormatter, ", ", "and"));

    [Fact]
    public void HumanizeTrimsItemsByDefault() => Assert.Equal("A, B and C", new[] { "A", "  B  ", "C" }.Humanize(DummyFormatter, ", ", "and"));
}

internal sealed class SomeClass
{
    public string? SomeString { get; set; }

    public int SomeInt { get; set; }

    public override string ToString() => "ToString";
}
