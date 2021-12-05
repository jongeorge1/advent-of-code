namespace AoC.Solutions.Year2016.Day06
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return new string(Enumerable.Range(0, rows[0].Length).Select(x => rows.Select(row => row[x]).GroupBy(x => x).OrderBy(x => x.Count()).First().Key).ToArray());
        }
    }
}
