namespace AdventOfCode.Year2025.Day05;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Year2015.Day15;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        (List<Rule> rules, List<long> ingredients) = ParseInput(input);

        int freshIngredientCount = ingredients.Count(ingredient => rules.Any(rule => rule.IsMatch(ingredient)));

        return freshIngredientCount.ToString();
    }

    private static (List<Rule> Rules, List<long> Ingredients) ParseInput(string[] input)
    {
        List<Rule> rules = [];
        List<long> ingredients = [];
        bool parsingRules = true;

        foreach (string line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                parsingRules = false;
                continue;
            }

            if (parsingRules)
            {
                rules.Add(new(line));
            }
            else
            {
                ingredients.Add(long.Parse(line));
            }
        }

        return (rules, ingredients);
    }

    private record Rule
    {
        public Rule(string definition)
        {
            this.Definition = definition;

            int separatorIndex = definition.IndexOf("-");
            Debug.Assert(separatorIndex != -1);

            this.Min = long.Parse(definition[..separatorIndex]);
            this.Max = long.Parse(definition[(separatorIndex + 1)..]);
        }

        public string Definition { get; }

        public long Min { get; }

        public long Max { get; }

        public bool IsMatch(long value) => value >= this.Min && value <= this.Max;
    }
}
