namespace AdventOfCode.Year2019.Day01
{
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            return input
                .Select(x => int.Parse(x))
                .Select(this.CalculateFuelRequirement)
                .Sum()
                .ToString();
        }

        public int CalculateFuelRequirement(int mass)
        {
            return (mass / 3) - 2;
        }
    }
}
