﻿namespace AdventOfCode.Year2018.Day22
{
    using System;

    public partial class Part02
    {
        [Flags]
        public enum Tools
        {
            Neither = 0x1,

            Torch = 0x2,

            ClimbingGear = 0x4,
        }
    }
}
