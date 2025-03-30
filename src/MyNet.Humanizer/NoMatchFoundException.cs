// -----------------------------------------------------------------------
// <copyright file="NoMatchFoundException.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace MyNet.Humanizer;

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
