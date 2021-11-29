namespace AoC.Solutions.Year2019.Day12
{
    using System.Linq;
    using AoC.Solutions.Helpers;

    public static class MoonExtensions
    {
        public static int TotalEnergy(this Moon[] moons)
        {
            return moons.Sum(x => x.PotentialEnergy() * x.KineticEnergy());
        }

        public static int PotentialEnergy(this Moon moon)
        {
            return Distance.Manhattan(moon.Position);
        }

        public static int KineticEnergy(this Moon moon)
        {
            return Distance.Manhattan(moon.Velocity);
        }
    }
}
