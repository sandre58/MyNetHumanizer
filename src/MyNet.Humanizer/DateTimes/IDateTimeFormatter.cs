// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Units;

namespace MyNet.Humanizer.DateTimes
{
    /// <summary>
    /// Implement this interface if your language has complex rules around dealing with numbers. 
    /// For example in Romanian "5 days" is "5 zile", while "24 days" is "24 de zile" and 
    /// in Arabic 2 days is يومين not 2 يوم
    /// </summary>
    public interface IDateTimeFormatter
    {
        /// <summary>
        /// Now
        /// </summary>
        /// <returns>Returns Now</returns>
        string Now();

        /// <summary>
        /// Never
        /// </summary>
        /// <returns>Returns Never</returns>
        string Never();

        /// <summary>
        /// 0 seconds
        /// </summary>
        /// <returns>Returns 0 seconds as the string representation of Zero TimeSpan</returns>
        string Zero();


        /// <summary>
        /// Returns the string representation of the provided DateTime
        /// </summary>
        /// <param name="timeUnit"></param>
        /// <param name="timeUnitTense"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        string DateHumanize(Tense timeUnitTense, TimeUnit timeUnit, int count);


        /// <summary>
        /// Returns the string representation of the provided TimeSpan
        /// </summary>
        /// <param name="timeUnit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        string TimeSpanHumanize(TimeUnit timeUnit, int count);
    }
}
