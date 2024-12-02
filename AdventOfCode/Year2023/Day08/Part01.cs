namespace AdventOfCode.Year2023.Day08
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            string directions = input[0];

            Dictionary<string, (string Left, string Right)> maps = new();

            foreach (string step in input[2..])
            {
                maps.Add(
                    step[0..3],
                    (step[7..10], step[12..15]));
            }

            int position = 0;
            int steps = 0;
            string location = "AAA";

            do
            {
                position %= directions.Length;

                (string Left, string Right) map = maps[location];

                if (directions[position] == 'L')
                {
                    location = map.Left;
                }
                else
                {
                    location = map.Right;
                }

                ++position;
                ++steps;
            }
            while (location != "ZZZ");

            return steps.ToString();
        }
    }
}
