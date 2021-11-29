﻿namespace AoC.Solutions.Year2018.Day20
{
    using System;

    [Flags]
    public enum Doors
    {
        None = 0x0,

        North = 0x01,

        West = 0x02,

        South = 0x04,

        East = 0x08,
    }
}
