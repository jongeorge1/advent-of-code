﻿namespace AdventOfCode.Year2021.Day01
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            int[] depths = input.Select(int.Parse).ToArray();
            return depths.Where((_, i) => i > 2 && depths[i] > depths[i - 3]).Count().ToString();
        }
    }
}
