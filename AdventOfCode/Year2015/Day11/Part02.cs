namespace AdventOfCode.Year2015.Day11
{
    public class Part02 : ISolution
    {
        private static readonly char[] BannedLetters = new[] { 'i', 'o', 'l' };

        public string Solve(string[] input)
        {
            string data = "cqjxxyzz";

            while (true)
            {
                data = IncrementPassword(data);
                if (IsPasswordValid(data))
                {
                    return data;
                }
            }
        }

        private static string IncrementPassword(string input)
        {
            char[] chars = input.ToCharArray();
            for (int i = chars.Length - 1; i >= 0; i--)
            {
                char curr = chars[i];
                if (curr == 'z')
                {
                    chars[i] = 'a';
                }
                else
                {
                    chars[i] = ++curr;
                    break;
                }
            }

            return string.Join(string.Empty, chars);
        }

        private static bool IsPasswordValid(string input) =>
            ContainsIncreasingStraight(input)
            && DoesNotContainBannedLetters(input)
            && ContainsTwoNonOverlappingPairs(input);

        private static bool ContainsIncreasingStraight(string input)
        {
            for (int i = 1; i < (input.Length - 1); i++)
            {
                char curr = input[i];
                if ((input[i - 1] == (curr - 1)) && (input[i + 1] == (curr + 1)))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsTwoNonOverlappingPairs(string input)
        {
            int firstPairIndex = -1;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i - 1] == input[i])
                {
                    if (firstPairIndex == -1)
                    {
                        firstPairIndex = i - 1;
                        i++;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool DoesNotContainBannedLetters(string input)
        {
            for (int i = 0; i < BannedLetters.Length; i++)
            {
                if (input.IndexOf(BannedLetters[i]) != -1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
