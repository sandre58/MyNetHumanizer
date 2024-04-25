// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Humanizer.Truncation
{
    /// <summary>
    /// Truncate a string to a fixed length
    /// </summary>
    internal class FixedLengthTruncator : ITruncator
    {
        public string? Truncate(string value, int length, string truncationString, TruncateFrom truncateFrom = TruncateFrom.Right) => value == null
                ? null
                : value.Length == 0
                ? value
                : truncationString == null || truncationString.Length > length
                ? truncateFrom == TruncateFrom.Right
                    ? value.Substring(0, length)
                    : value.Substring(value.Length - length, length)
                : truncateFrom == TruncateFrom.Left
                ? value.Length > length
                    ? $"{truncationString}{value.Substring(value.Length - length + truncationString.Length)}"
                    : value
                : value.Length > length
                ? $"{value.Substring(0, length - truncationString.Length)}{truncationString}"
                : value;
    }
}
