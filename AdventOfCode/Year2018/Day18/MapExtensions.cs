﻿namespace AdventOfCode.Year2018.Day18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MapExtensions
    {
        public static void WriteToConsole(this (char[] Map, int YOffset) state)
        {
            int rows = state.Map.Length / state.YOffset;

            for (int i = 0; i < rows; i++)
            {
                string row = new(state.Map.Skip(i * state.YOffset).Take(state.YOffset).ToArray());
                Console.WriteLine(row);
            }
        }

        public static (char[] Map, int YOffset) GetNextState(this (char[] Map, int YOffset) state)
        {
            char[] result = new char[state.Map.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = state.GetNextStateForAcre(i);
            }

            return (result, state.YOffset);
        }

        public static string Memoize(this (char[] Map, int YOffset) state)
        {
            // Lazy, as I'm not bothering to include the YOffset...
            return new string(state.Map);
        }

        public static char GetNextStateForAcre(this (char[] Map, int YOffset) state, int acre)
        {
            char[] adjacentAcres = state.GetAdjacentAcres(acre).ToArray();

            switch (state.Map[acre])
            {
                case MapAcre.Open:
                    if (adjacentAcres.Count(x => x == MapAcre.Trees) >= 3)
                    {
                        return MapAcre.Trees;
                    }

                    return MapAcre.Open;

                case MapAcre.Trees:
                    if (adjacentAcres.Count(x => x == MapAcre.Lumberyard) >= 3)
                    {
                        return MapAcre.Lumberyard;
                    }

                    return MapAcre.Trees;

                case MapAcre.Lumberyard:
                    if (adjacentAcres.Count(x => x == MapAcre.Lumberyard) >= 1 && adjacentAcres.Count(x => x == MapAcre.Trees) >= 1)
                    {
                        return MapAcre.Lumberyard;
                    }

                    return MapAcre.Open;
            }

            throw new InvalidOperationException();
        }

        public static IEnumerable<char> GetAdjacentAcres(this (char[] Map, int YOffset) state, int acre)
        {
            int up = acre - state.YOffset;
            int left = acre - 1;
            int right = acre + 1;
            int down = acre + state.YOffset;

            int minX = (acre / state.YOffset) * state.YOffset;
            int maxX = minX + state.YOffset;

            bool canLookUp = up >= 0;
            bool canLookDown = down < state.Map.Length;
            bool canLookLeft = left >= minX;
            bool canLookRight = right < maxX;

            if (canLookUp)
            {
                yield return state.Map[up];

                if (canLookLeft)
                {
                    yield return state.Map[up - 1];
                }

                if (canLookRight)
                {
                    yield return state.Map[up + 1];
                }
            }

            if (canLookDown)
            {
                yield return state.Map[down];

                if (canLookLeft)
                {
                    yield return state.Map[down - 1];
                }

                if (canLookRight)
                {
                    yield return state.Map[down + 1];
                }
            }

            if (canLookLeft)
            {
                yield return state.Map[left];
            }

            if (canLookRight)
            {
                yield return state.Map[right];
            }
        }
    }
}
