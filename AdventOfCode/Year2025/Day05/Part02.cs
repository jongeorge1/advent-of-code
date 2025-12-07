namespace AdventOfCode.Year2025.Day05;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AdventOfCode;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        List<Rule> rules = ParseInput(input);

        int mergedCount = -1;

        do
        {
            int currentRuleCount = rules.Count;
            rules = MergeRules(rules);

            mergedCount = currentRuleCount - rules.Count;
        }
        while (mergedCount > 0);

        long possibleFreshIngredientCount = 0;
        foreach (Rule rule in rules)
        {
            possibleFreshIngredientCount += rule.Range;
            Debug.Assert(possibleFreshIngredientCount > 0);
        }

        return possibleFreshIngredientCount.ToString();
    }

    private static List<Rule> MergeRules(List<Rule> rules)
    {
        List<Rule> allMergedRules = [];

        // The chosen approach won't work if there are duplicates
        rules = [..rules.Distinct()];

        while (rules.Count > 0)
        {
            // Find ranges that can be merged with the first item in the list.
            Rule currentRange = rules[0];
            List<Rule> currentMergedRules = [currentRange];

            foreach (Rule rule in rules[1..])
            {
                if (Rule.TryMergeRanges(currentRange, rule, out Rule? mergedRule))
                {
                    currentRange = mergedRule;
                    currentMergedRules.Add(rule);
                }
            }

            // Remove all the merged ranges from the list
            foreach (Rule mergedRule in currentMergedRules)
            {
                rules.Remove(mergedRule);
            }

            allMergedRules.Add(currentRange);
        }

        return allMergedRules;
    }

    private static List<Rule> ParseInput(string[] input)
    {
        List<Rule> rules = [];

        foreach (string line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                return rules;
            }

            rules.Add(new(line));
        }

        Debug.Fail("Something has gone wrong");
        return rules;
    }

    private record Rule
    {
        public Rule(string definition)
        {
            int separatorIndex = definition.IndexOf("-");
            Debug.Assert(separatorIndex != -1);

            this.Min = long.Parse(definition[..separatorIndex]);
            this.Max = long.Parse(definition[(separatorIndex + 1)..]);
        }

        public Rule(long min, long max)
        {
            this.Min = min;
            this.Max = max;
        }

        public long Min { get; }

        public long Max { get; }

        public long Range { get => this.Max - this.Min + 1; }

        public static bool TryMergeRanges(Rule range1, Rule range2, [NotNullWhen(true)] out Rule? mergedRule)
        {
            // Check if ranges overlap or are adjacent
            if (range1.Min <= range2.Max + 1 && range2.Min <= range1.Max + 1)
            {
                mergedRule = new(
                    Math.Min(range1.Min, range2.Min),
                    Math.Max(range1.Max, range2.Max));

                ////Console.WriteLine("Merging ranges: {0}-{1} and {2}-{3} -> {4}-{5}", range1.Min, range1.Max, range2.Min, range2.Max, mergedRule.Min, mergedRule.Max);

                return true;
            }

            // Ranges don't overlap
            mergedRule = null;
            return false;
        }
    }
}
