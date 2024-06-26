﻿// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Humanizer.Ordinalizing
{
    /// <summary>
    /// The interface used to localise the Ordinalize method
    /// </summary>
    public interface IOrdinalizer
    {
        /// <summary>
        /// Ordinalizes the number 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="numberString"></param>
        /// <returns></returns>
        string Convert(int number, string numberString);

        /// <summary>
        /// Ordinalizes the number using the provided grammatical gender
        /// </summary>
        /// <param name="number"></param>
        /// <param name="numberString"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        string Convert(int number, string numberString, GrammaticalGender gender);
    }
}
