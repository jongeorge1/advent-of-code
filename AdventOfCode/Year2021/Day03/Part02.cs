namespace AdventOfCode.Year2021.Day03
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] numbers = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            return (OxygenGeneratorReading(numbers) * Co2ScrubberRating(numbers)).ToString();
        }

        private static int OxygenGeneratorReading(string[] numbers)
        {
            int position = 0;

            while (position < numbers[0].Length && numbers.Length > 1)
            {
                char mostCommonAtPosition = GetMostCommonDigitInPosition(numbers, position);
                numbers = numbers.Where(x => x[position] == mostCommonAtPosition).ToArray();
                ++position;
            }

            return Convert.ToInt32(numbers[0], 2);
        }

        private static int Co2ScrubberRating(string[] numbers)
        {
            int position = 0;

            while (position < numbers[0].Length && numbers.Length > 1)
            {
                char mostCommonAtPosition = GetMostCommonDigitInPosition(numbers, position);
                numbers = numbers.Where(x => x[position] != mostCommonAtPosition).ToArray();
                ++position;
            }

            return Convert.ToInt32(numbers[0], 2);
        }

        private static char GetMostCommonDigitInPosition(string[] numbers, int position)
        {
            int ones = numbers.Count(x => x[position] == '1');
            int zeros = numbers.Count(x => x[position] == '0');

            return ones >= zeros ? '1' : '0';
        }
    }
}
