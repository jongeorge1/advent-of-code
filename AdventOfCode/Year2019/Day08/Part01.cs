﻿namespace AdventOfCode.Year2019.Day08
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] data)
        {
            bool testMode = data.Length < 100;
            int width = testMode ? 3 : 25;
            int height = testMode ? 2 : 6;

            int layerSize = width * height;

            char[][] layers = data[0]
                .ToCharArray()
                .Select((x, i) => (i, x))
                .GroupBy(p => p.i / layerSize)
                .Select(g => g.Select(l => l.x).ToArray())
                .ToArray();

            IEnumerable<(int Layer, int CountOfZeros)> countsOfZeros = layers.Select((l, i) => (i, l.Count(p => p == '0')));
            int layerWithFewestZeros = countsOfZeros.OrderBy(x => x.CountOfZeros).First().Layer;
            int countOfOnes = layers[layerWithFewestZeros].Count(x => x == '1');
            int countOfTwos = layers[layerWithFewestZeros].Count(x => x == '2');

            return (countOfOnes * countOfTwos).ToString();
        }
    }
}
