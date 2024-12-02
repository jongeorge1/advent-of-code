namespace AdventOfCode.Year2021.Day18
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            SnailfishNumber[] numbers = input.Select(SnailfishNumber.Parse).ToArray();

            int largestMagnitude = 0;

            for (int a = 0; a < numbers.Length; ++a)
            {
                for (int b = 0; b < numbers.Length; ++b)
                {
                    if (a == b)
                    {
                        continue;
                    }

                    SnailfishNumber sum = numbers[a] + numbers[b];
                    largestMagnitude = Math.Max(largestMagnitude, sum.Magnitude());
                }
            }

            return largestMagnitude.ToString();
        }
    }
}
