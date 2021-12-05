namespace AoC.Solutions.Year2021.Day02
{
    using System;
    using System.Linq;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] directions = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            int forward = directions.Where(x => x.StartsWith("forward")).Sum(x => int.Parse(x[8..]));
            int down = directions.Where(x => x.StartsWith("down")).Sum(x => int.Parse(x[5..]));
            int up = directions.Where(x => x.StartsWith("up")).Sum(x => int.Parse(x[3..]));

            return (forward * (down - up)).ToString();
        }
    }
}
