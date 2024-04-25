// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Humanizer.Truncation
{
    /// <summary>
    /// Gets a ITruncator
    /// </summary>
    public static class Truncator
    {
        /// <summary>
        /// Fixed length truncator
        /// </summary>
        public static ITruncator FixedLength => new FixedLengthTruncator();

        /// <summary>
        /// Fixed number of characters truncator
        /// </summary>
        public static ITruncator FixedNumberOfCharacters => new FixedNumberOfCharactersTruncator();

        /// <summary>
        /// Fixed number of words truncator
        /// </summary>
        public static ITruncator FixedNumberOfWords => new FixedNumberOfWordsTruncator();
    }
}
