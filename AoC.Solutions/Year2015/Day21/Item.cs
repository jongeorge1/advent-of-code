namespace AoC.Solutions.Year2015.Day21
{
    using System.Diagnostics;

    [DebuggerDisplay("{Name}: Cost - {Cost}, Damage - {Damage}, Armour - {Armour}")]
    public class Item
    {
        public Item(string name, int cost, int damage, int armour)
        {
            this.Name = name;
            this.Cost = cost;
            this.Damage = damage;
            this.Armour = armour;
        }

        public string Name { get; }

        public int Cost { get; }

        public int Damage { get; }

        public int Armour { get; }
    }
}
