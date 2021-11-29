namespace AoC.Solutions.Year2018.Day15
{
    public abstract class Unit
    {
        public MapSpace? CurrentLocation { get; set; }

        public int AttackStrength { get; set; } = 3;

        public int HitPoints { get; set; } = 200;
    }
}
