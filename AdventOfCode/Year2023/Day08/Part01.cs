namespace AdventOfCode.Year2023.Day08
{
    using System.Collections.Generic;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            string directions = input[0];

            Dictionary<string, Map> maps = [];

            foreach (string step in input[2..])
            {
                var map = new Map(step);
                maps.Add(map.Location, map);
            }

            int position = 0;
            int steps = 0;
            string location = "AAA";

            do
            {
                position %= directions.Length;
                location = maps[location].Directions[directions[position]];
                ++position;
                ++steps;
            }
            while (location != "ZZZ");

            return steps.ToString();
        }
    }
}
