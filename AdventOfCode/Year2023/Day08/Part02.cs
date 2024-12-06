namespace AdventOfCode.Year2023.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AdventOfCode;
    using static AdventOfCode.Helpers.Numeric;

    public class Part02 : ISolution
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

            string[] startLocations = maps.Keys.Where(x => x.EndsWith('A')).ToArray();
            long[] periods = new long[startLocations.Length];

            // A bit of investigation and it looks like each of the paths will reach and end square in a different number of steps, but after
            // executing all of directions a certain number of times. So once we hit it the first time, we essentially have the period for
            // reaching that location.
            // Note that this isn't true of the test case, which is why there's no test for this part.
            for (int index = 0; index < startLocations.Length; ++index)
            {
                int position = 0;
                int steps = 0;
                string location = startLocations[index];

                do
                {
                    position %= directions.Length;
                    location = maps[location].Directions[directions[position]];
                    ++position;
                    ++steps;
                }
                while (!location.EndsWith('Z'));

                Debug.Assert(position == directions.Length);

                periods[index] = steps;
            }

            long result = LeastCommonMultiple(periods);

            return result.ToString();
        }
    }
}
