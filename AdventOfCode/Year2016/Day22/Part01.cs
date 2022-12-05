namespace AdventOfCode.Year2016.Day22
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Node[] nodes = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Skip(2).Select(x => new Node(x)).ToArray();

            int viablePairs = nodes.Where(a => a.Used != 0).Select(a => nodes.Count(b => a != b && a.Used <= b.Avail)).Sum();

            return viablePairs.ToString();
        }
    }
}