namespace AdventOfCode.Year2015.Day21
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            Combatant boss = Common.GetBoss(input);

            IEnumerable<(Item Weapon, Item Armour, Item rings)> winningKitCombos = Common.GetKitCombos().Where(x => !Common.CanKitWin(x.Weapon, x.Armour, x.rings, boss));

            return winningKitCombos.Max(x => x.Weapon.Cost + x.Armour.Cost + x.rings.Cost).ToString();
        }
    }
}