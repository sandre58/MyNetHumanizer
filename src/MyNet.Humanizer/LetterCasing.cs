// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Humanizer
{
    /// <summary>
    /// Options for specifying the desired letter casing for the output string 
    /// </summary>
    public enum LetterCasing
    {
        /// <summary>
        /// SoMeStrIng -> SoMeStrIng
        /// </summary>
        Normal,
        /// <summary>
        /// SomeString -> Some String
        /// </summary>
        Title,
        /// <summary>
        /// SomeString -> SOME STRING
        /// </summary>
        AllCaps,
        /// <summary>
        /// SomeString -> some string
        /// </summary>
        LowerCase,
        /// <summary>
        /// SomeString -> Some string
        /// </summary>
        Sentence,
    }
}
