namespace AdventOfCode.Year2021.Day03
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            return (GammaRate(input) * EpsilonRate(input)).ToString();
        }

        private static int GammaRate(string[] numbers)
        {
            string result = string.Empty;

            for (int i = 0; i < numbers[0].Length; i++)
            {
                result += GetMostCommonDigitInPosition(numbers, i);
            }

            return Convert.ToInt32(result, 2);
        }

        private static int EpsilonRate(string[] numbers)
        {
            string result = string.Empty;

            for (int i = 0; i < numbers[0].Length; i++)
            {
                result += GetMostCommonDigitInPosition(numbers, i) == '1' ? '0' : '1';
            }

            return Convert.ToInt32(result, 2);
        }

        private static char GetMostCommonDigitInPosition(string[] numbers, int position)
        {
            int ones = numbers.Count(x => x[position] == '1');
            int zeros = numbers.Count(x => x[position] == '0');

            return ones >= zeros ? '1' : '0';
        }
    }
}
