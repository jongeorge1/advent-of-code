﻿namespace AdventOfCode.Year2018.Day12
{
    using System.Linq;

    public static class StateExtensions
    {
        public static State Apply(this State state, Rule[] rules)
        {
            (Rule Rule, int MatchLocation)[] matches = rules.SelectMany(rule => state.FindMatches(rule).Select(matchLocation => (rule, matchLocation))).ToArray();

            // Now apply all the rules to build a new state
            return new State(matches.Where(x => x.Rule.Result).Select(x => x.MatchLocation).ToList());
        }

        /// <summary>
        /// Finds pot numbers that match this rule.
        /// </summary>
        public static int[] FindMatches(this State state, Rule rule)
        {
            // We need to look at the range of pots that currently have plants,
            // and two either side, to see if the rule matches any of them
            int firstPotToCheck = state.PotsContainingPlants.Min() - 2;
            int lastPotToCheck = state.PotsContainingPlants.Max() + 2;

            return Enumerable.Range(firstPotToCheck, lastPotToCheck - firstPotToCheck + 1).Where(x => state.MatchesAt(rule, x)).ToArray();
        }

        public static bool MatchesAt(this State state, Rule rule, int potLocation)
        {
            for (int i = 0; i < 5; i++)
            {
                if (state.PotsContainingPlants.Contains(potLocation - 2 + i) != rule.Pattern[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
