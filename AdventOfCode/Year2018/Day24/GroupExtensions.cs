﻿namespace AdventOfCode.Year2018.Day24
{
    using System.Collections.Generic;
    using System.Linq;

    public static class GroupExtensions
    {
        public static bool ContainsUnits(this List<Group> army)
        {
            return army.TotalUnits() != 0;
        }

        public static int TotalUnits(this List<Group> army)
        {
            return army.Sum(x => x.Units);
        }

        public static void Boost(this List<Group> army, int boost)
        {
            foreach (Group? current in army)
            {
                current.DamageCount += boost;
            }
        }

        public static List<(Group Attackers, Group Defenders)> SelectTargets(this List<Group> attackers, List<Group> defenders)
        {
            var allocations = new List<(Group, Group)>();
            var allocatedDefenders = new List<Group>();

            foreach (Group attacker in attackers.Where(a => a.EffectivePower > 0).OrderByDescending(x => x.EffectivePower).ThenByDescending(x => x.Initiative))
            {
                Group? selectedDefender = defenders.Where(d => d.EffectivePower > 0 && !allocatedDefenders.Contains(d))
                    .Select(defender => (defender, attackDamage: defender.CalculateDamageFrom(attacker)))
                    .Where(d => d.attackDamage != 0)
                    .OrderByDescending(d => d.attackDamage)
                    .ThenByDescending(d => d.defender.EffectivePower)
                    .ThenByDescending(d => d.defender.Initiative)
                    .Select(d => d.defender)
                    .FirstOrDefault();

                if (selectedDefender != null)
                {
                    allocatedDefenders.Add(selectedDefender);
                    allocations.Add((attacker, selectedDefender));
                }
            }

            return allocations;
        }
    }
}
