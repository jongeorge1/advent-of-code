﻿namespace AdventOfCode.Year2020.Day16
{
    using System;
    using System.Linq;

    public class Rule
    {
        public Rule(string input)
        {
            string[] components = input.Split(new[] { ": ", "-", " or " }, StringSplitOptions.RemoveEmptyEntries);

            this.Name = components[0];
            this.ValidRanges = new[]
            {
                (int.Parse(components[1]), int.Parse(components[2])),
                (int.Parse(components[3]), int.Parse(components[4])),
            };
        }

        public string Name { get; }

        public (int Min, int Max)[] ValidRanges { get; }

        public bool Validate(int number)
        {
            return this.ValidRanges.Any(r => number >= r.Min && number <= r.Max);
        }
    }
}
