﻿namespace AdventOfCode.Year2018.Day05
{
    using System.Linq;

    public static class PolymerReactor
    {
        public static string React(string polymer)
        {
            var current = polymer.ToCharArray().ToList();

            bool changed;

            do
            {
                changed = false;

                int i = 1;

                while (i < current.Count)
                {
                    char first = current[i - 1];
                    char second = current[i];

                    if (first != second && char.ToUpper(first) == char.ToUpper(second))
                    {
                        current.RemoveRange(i - 1, 2);
                        changed = true;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            while (changed);

            return new string(current.ToArray());
        }
    }
}
