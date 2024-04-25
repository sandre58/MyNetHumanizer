// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;

namespace MyNet.Humanizer
{
    public class NoMatchFoundException : Exception
    {
        public NoMatchFoundException()
        {
        }

        public NoMatchFoundException(string message)
            : base(message)
        {
        }

        public NoMatchFoundException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
