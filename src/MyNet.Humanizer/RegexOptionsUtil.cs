// -----------------------------------------------------------------------
// <copyright file="RegexOptionsUtil.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace MyNet.Humanizer;

internal static class RegexOptionsUtil
{
    static RegexOptionsUtil() => Compiled = Enum.TryParse("Compiled", out RegexOptions compiled) ? compiled : RegexOptions.None;

    public static RegexOptions Compiled { get; }
}
