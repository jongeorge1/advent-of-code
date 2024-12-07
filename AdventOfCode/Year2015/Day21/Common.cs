namespace AdventOfCode.Year2015.Day21
{
    using System;
    using System.Collections.Generic;

    public static class Common
    {
        public static Combatant GetBoss(string[] input)
        {
            return new Combatant(
                int.Parse(input[0].Split(' ')[^1]),
                int.Parse(input[1].Split(' ')[^1]),
                int.Parse(input[2].Split(' ')[^1]));
        }

        public static IEnumerable<(Item Weapon, Item Armour, Item Rings)> GetKitCombos()
        {
            var weapons = new Item[]
            {
                new("Dagger", 8, 4, 0),
                new("Shortsword", 10, 5, 0),
                new("Warhammer", 25, 6, 0),
                new("Longsword", 40, 7, 0),
                new("Greataxe", 74, 8, 0),
            };

            var armour = new Item[]
            {
                new("Nothing", 0, 0, 0),
                new("Leather", 13, 0, 1),
                new("Chainmail", 31, 0, 2),
                new("Splintmail", 53, 0, 3),
                new("Bandedmail", 75, 0, 4),
                new("Platemail", 102, 0, 5),
            };

            var rings = new Item[]
            {
                new("Nothing", 0, 0, 0),
                new("Damage +1", 25, 1, 0),
                new("Damage +2", 50, 2, 0),
                new("Damage +3", 100, 3, 0),
                new("Defense +1", 20, 0, 1),
                new("Defense +2", 40, 0, 2),
                new("Defense +3", 80, 0, 3),
            };

            // We need to analyse all the possible combinations of weapons, armour and rings. This is made slightly more
            // complex by the fact that we can have up to two rings. So lets first create a rings array with all possible
            // buying options for rings.
            var ringCombos = new List<Item>(rings);

            for (int ring1Index = 0; ring1Index < rings.Length; ++ring1Index)
            {
                ringCombos.Add(rings[ring1Index]);

                for (int ring2Index = ring1Index + 1; ring2Index < rings.Length; ++ring2Index)
                {
                    ringCombos.Add(
                        new Item(
                            $"Left hand: {rings[ring1Index].Name}, Right hand: {rings[ring2Index].Name}",
                            rings[ring1Index].Cost + rings[ring2Index].Cost,
                            rings[ring1Index].Damage + rings[ring2Index].Damage,
                            rings[ring1Index].Armour + rings[ring2Index].Armour));
                }
            }

            // Now we need all possible combos of kit...
            foreach (Item weapon in weapons)
            {
                foreach (Item armourItem in armour)
                {
                    foreach (Item ring in ringCombos)
                    {
                        yield return (weapon, armourItem, ring);
                    }
                }
            }
        }

        public static bool CanKitWin(Item weapon, Item armour, Item ring, Combatant boss)
        {
            var player = new Combatant(
                100,
                weapon.Damage + armour.Damage + ring.Damage,
                weapon.Armour + armour.Armour + ring.Armour);

            while (player.HitPoints > 0 && boss.HitPoints > 0)
            {
                var newBoss = new Combatant(boss.HitPoints - Math.Max(player.Damage - boss.Armour, 1), boss.Damage, boss.Armour);
                var newPlayer = new Combatant(player.HitPoints - Math.Max(boss.Damage - player.Armour, 1), player.Damage, player.Armour);

                if (player.HitPoints == newPlayer.HitPoints && boss.HitPoints == newBoss.HitPoints)
                {
                    // Stalemate
                    return false;
                }

                player = newPlayer;
                boss = newBoss;
            }

            return boss.HitPoints <= 0;
        }
    }
}
