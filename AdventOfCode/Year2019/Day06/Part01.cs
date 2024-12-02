namespace AdventOfCode.Year2019.Day06
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            var directOrbits = input
                .Select(x => x.Split(new string[] { ")" }, StringSplitOptions.RemoveEmptyEntries))
                .GroupBy(x => x[0])
                .ToDictionary(x => x.Key, x => x.Select(i => i[1]).ToArray());

            return directOrbits.Keys.Sum(k => this.CountAllOrbits(k, directOrbits)).ToString();
        }

        private int CountAllOrbits(string origin, Dictionary<string, string[]> directOrbits)
        {
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
            if (directOrbits.TryGetValue(origin, out string[]? directOrbitsOfOrigin))
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
            {
                return directOrbitsOfOrigin.Length + directOrbitsOfOrigin.Sum(x => this.CountAllOrbits(x, directOrbits));
            }

            return 0;
        }
    }
}
