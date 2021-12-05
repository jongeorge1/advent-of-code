namespace AoC.Solutions.Year2021.Day02
{
    using System;
    using System.Linq;
    using AoC.Solutions;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] directions = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            int aim = 0;
            int forward = 0;
            int depth = 0;

            foreach (string current in directions)
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
