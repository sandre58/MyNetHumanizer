// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Humanizer.Ordinalizing
{
    public class DefaultOrdinalizer : IOrdinalizer
    {
        public virtual string Convert(int number, string numberString, GrammaticalGender gender) => Convert(number, numberString);

        public virtual string Convert(int number, string numberString) => numberString;
    }
}
