namespace AdventOfCode.Year2020.Day01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            IEnumerable<int> numbers = input.Select(int.Parse);

            foreach (int number in numbers)
            {
                int target = 2020 - number;
                if (numbers.Contains(target))
                {
                    return (number * target).ToString();
                }
            }

            return string.Empty;
        }
    }
}
