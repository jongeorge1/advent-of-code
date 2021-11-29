namespace AoC.Solutions.Year2015.Day05
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            return input
                .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Count(IsNice)
                .ToString();
        }

        private static bool IsNice(string input) =>
            ContainsRepeatedPair(input) &&
            ContainsRepeatedLetterWithASingleLetterBetween(input);

        private static bool ContainsRepeatedPair(string input)
        {
            for (int i = 0; i < input.Length - 2; i++)
            {
                string pair = input.Substring(i, 2);

                int position = input.IndexOf(pair);
                if (position == i || position == (i + 1) || position == (i - 1))
                {
                    position = input.IndexOf(pair, i + 2);
                }

                if (position != -1)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsRepeatedLetterWithASingleLetterBetween(string input)
        {
            for (int i = 2; i < input.Length; i++)
            {
                if (input[i - 2] == input[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
