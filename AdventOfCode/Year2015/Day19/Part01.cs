namespace AdventOfCode.Year2015.Day19
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            string calibrationMolecule = input[^1];
            (string In, string Out)[] replacements = input[0..^2].Select(x => x.Split(" => ")).Select(x => (x[0], x[1])).ToArray();

            var potentialMolecules = new List<string>();
            foreach ((string In, string Out) current in replacements)
            {
                int index = calibrationMolecule.IndexOf(current.In);
                while (index != -1)
                {
                    string newMolecule = string.Concat(calibrationMolecule.Substring(0, index), current.Out, calibrationMolecule.Substring(index + current.In.Length));
                    potentialMolecules.Add(newMolecule);
                    index = calibrationMolecule.IndexOf(current.In, index + 1);
                }
            }

            return potentialMolecules.Distinct().Count().ToString();
        }
    }
}