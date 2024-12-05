namespace AdventOfCode.Year2021.Day02
{
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            int forward = input.Where(x => x.StartsWith("forward")).Sum(x => int.Parse(x[8..]));
            int down = input.Where(x => x.StartsWith("down")).Sum(x => int.Parse(x[5..]));
            int up = input.Where(x => x.StartsWith("up")).Sum(x => int.Parse(x[3..]));

            return (forward * (down - up)).ToString();
        }
    }
}
