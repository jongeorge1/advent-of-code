namespace AoC.Solutions.Year2020.Day06
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AoC.Solutions;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            return input
                .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Sum(CountAnswers)
                .ToString();
        }

        private static int CountAnswers(string input)
        {
            IEnumerable<char>? answers = input.ToCharArray().Distinct();
            return answers.Count(x => x != '\r' && x != '\n');
        }
    }
}
