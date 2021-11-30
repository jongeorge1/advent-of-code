namespace AoC.Solutions.Year2015.Day21
{
    using System.Diagnostics;

    [DebuggerDisplay("Hit Points: {HitPoints}, Damage: {Damage}, Armour: {Armour}")]
    public class Combatant
    {
        public Combatant(int hitPoints, int damage, int armour)
        {
            this.HitPoints = hitPoints;
            this.Damage = damage;
            this.Armour = armour;
        }

        public int HitPoints { get; }

        public int Damage { get; }

        public int Armour { get; }
    }
}
