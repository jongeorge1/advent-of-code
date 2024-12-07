namespace AdventOfCode.Year2016.Day07
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Part02 : ISolution
    {
        private static readonly Regex NotRegex = new(@"\[(\w+)\]", RegexOptions.Compiled);

        public string Solve(string[] input)
        {
            int supportsSslCount = 0;

            for (int i = 0; i < input.Length; i++)
            {
                // Extract the sections in square brackets
                string row = input[i];

                MatchCollection matches = NotRegex.Matches(row);

                string[] sections = NotRegex.Replace(row, "~").Split("~");

                string[] abas = sections.SelectMany(GetAbas).ToArray();

                if (abas.Length == 0)
                {
                    continue;
                }

                string[] babs = matches.Select(x => x.Value).SelectMany(GetAbas).ToArray();

                if (abas.Any(aba => babs.Any(bab => AbaAndBabCorrespond(aba, bab))))
                {
                    ++supportsSslCount;
                }
            }

            return supportsSslCount.ToString();
        }

        private static IEnumerable<string> GetAbas(string input)
        {
            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == input[i + 2] && input[i] != input[i + 1])
                {
                    yield return input[i..(i + 3)];
                }
            }
        }

        private static bool AbaAndBabCorrespond(string aba, string bab)
        {
            return aba[0] == bab[1] && aba[1] == bab[0];
        }
    }
}
