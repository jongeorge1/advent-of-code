namespace AdventOfCode.Year2021.Day02
{
    using System;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            int aim = 0;
            int forward = 0;
            int depth = 0;

            foreach (string current in input)
            {
                if (current.StartsWith("forward"))
                {
                    forward += int.Parse(current[8..]);
                    depth += aim * int.Parse(current[8..]);
                }
                else if (current.StartsWith("down"))
                {
                    aim += int.Parse(current[5..]);
                }
                else
                {
                    aim -= int.Parse(current[3..]);
                }
            }

            return (forward * depth).ToString();
        }
    }
}
