// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.RegularExpressions;

namespace MyNet.Humanizer
{
    internal static class RegexOptionsUtil
    {
        static RegexOptionsUtil() => Compiled = Enum.TryParse("Compiled", out RegexOptions compiled) ? compiled : RegexOptions.None;

        public static RegexOptions Compiled { get; }
    }
}
