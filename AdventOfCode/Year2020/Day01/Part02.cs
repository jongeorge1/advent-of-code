namespace AdventOfCode.Year2020.Day01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            IEnumerable<int> numbers = input.Select(int.Parse);

            foreach (int first in numbers)
            {
                foreach (int second in numbers)
                {
                    int target = 2020 - first - second;
                    if (numbers.Contains(target))
                    {
                        return (first * second * target).ToString();
                    }
                }
            }

            return string.Empty;
        }
    }
}
