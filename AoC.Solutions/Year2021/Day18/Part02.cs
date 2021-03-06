namespace AoC.Solutions.Year2021.Day18
{
    using System;
    using System.Linq;
    using AoC.Solutions;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            SnailfishNumber[] numbers = input.Split(Environment.NewLine).Select(SnailfishNumber.Parse).ToArray();

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
