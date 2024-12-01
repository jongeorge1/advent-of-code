namespace AdventOfCode.Year2015.Day19
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            string medicineMolecule = input[^1];
            (string In, string Out)[] replacements = input[0..^1].Select(x => x.Split(" => ")).Select(x => (x[0], x[1])).OrderByDescending(x => x.Item2.Length).ToArray();

            // We're just going to work back, repeatedly replacing things until we get to "e".
            string currentMolecule = medicineMolecule;
            int replacementCount = 0;
            int replacementIndex = 0;

            while (currentMolecule != "e")
            {
                (string In, string Out) currentReplacement = replacements[replacementIndex];
                int index = currentMolecule.IndexOf(currentReplacement.Out);
                if (index == -1)
                {
                    ++replacementIndex;

                    if (replacementIndex == replacements.Length)
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    currentMolecule = string.Concat(currentMolecule.Substring(0, index), currentReplacement.In, currentMolecule.Substring(index + currentReplacement.Out.Length));
                    replacementIndex = 0;
                    ++replacementCount;
                }
            }

            return replacementCount.ToString();
        }
    }
}