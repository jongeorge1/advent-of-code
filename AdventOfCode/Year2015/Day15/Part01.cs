namespace AdventOfCode.Year2015.Day15
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Ingredient[] ingredients = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Ingredient(x)).ToArray();

            return BuildQuantityCombinations().Max(combination => ScoreCombination(combination, ingredients)).ToString();
        }

        private static long ScoreCombination(int[] combination, Ingredient[] ingredients)
        {
            long combinedCapacity = 0;
            long combinedDurability = 0;
            long combinedFlavor = 0;
            long combinedTexture = 0;

            for (int i = 0; i < combination.Length; i++)
            {
                combinedCapacity += combination[i] * ingredients[i].Capacity;
                combinedDurability += combination[i] * ingredients[i].Durability;
                combinedFlavor += combination[i] * ingredients[i].Flavor;
                combinedTexture += combination[i] * ingredients[i].Texture;
            }

            return Math.Max(0, combinedCapacity) *
                Math.Max(0, combinedDurability) *
                Math.Max(0, combinedFlavor) *
                Math.Max(0, combinedTexture);
        }

        private static IEnumerable<int[]> BuildQuantityCombinations()
        {
            // Cheat: we know there are 4 ingredients...
            for (int i0 = 0; i0 <= 100; i0++)
            {
                for (int i1 = 0; i1 <= 100; i1++)
                {
                    for (int i2 = 0; i2 <= 100; i2++)
                    {
                        for (int i3 = 0; i3 <= 100; i3++)
                        {
                            if (i0 + i1 + i2 + i3 == 100)
                            {
                                yield return new[] { i0, i1, i2, i3 };
                            }
                        }
                    }
                }
            }
        }
    }
}
