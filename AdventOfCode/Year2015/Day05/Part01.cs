namespace AdventOfCode.Year2015.Day05
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        private static readonly string[] DisallowedStrings = new[] { "ab", "cd", "pq", "xy" };

        public string Solve(string input)
        {
            return input
                .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Count(IsNice)
                .ToString();
        }

        private static bool IsNice(string input) =>
            HasAtLeastThreeVowels(input) &&
            HasALetterThatAppearsTwiceInARow(input) &&
            DoesNotContainADisallowedString(input);

        private static bool HasAtLeastThreeVowels(string input)
        {
            int vowelCount = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char curr = input[i];
                if (curr == 'a' || curr == 'e' || curr == 'i' || curr == 'o' || curr == 'u')
                {
                    if (vowelCount == 2)
                    {
                        return true;
                    }

                    vowelCount++;
                }
            }

            return false;
        }

        private static bool HasALetterThatAppearsTwiceInARow(string input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == input[i - 1])
                {
                    return true;
                }
            }

            return false;
        }

        private static bool DoesNotContainADisallowedString(string input)
        {
            for (int i = 0; i < DisallowedStrings.Length; i++)
            {
                if (input.IndexOf(DisallowedStrings[i]) != -1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
