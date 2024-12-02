namespace AdventOfCode.Year2024
{
    using System;
    using System.Runtime.CompilerServices;
    using AdventOfCode;
    using AdventOfCode.Year2016.Day11;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "3   4\r\n4   3\r\n2   5\r\n1   3\r\n3   9\r\n3   3", "11")]
        [TestCase(1, 2, "3   4\r\n4   3\r\n2   5\r\n1   3\r\n3   9\r\n3   3", "31")]
        [TestCase(2, 1, "7 6 4 2 1\r\n1 2 7 8 9\r\n9 7 6 2 1\r\n1 3 2 4 5\r\n8 6 4 4 1\r\n1 3 6 7 9", "2")]
        [TestCase(2, 1, "7 6 4 2 1", "1")]
        [TestCase(2, 1, "1 2 7 8 9", "0")]
        [TestCase(2, 1, "9 7 6 2 1", "0")]
        [TestCase(2, 1, "1 3 2 4 5", "0")]
        [TestCase(2, 1, "8 6 4 4 1", "0")]
        [TestCase(2, 1, "1 3 6 7 9", "1")]
        [TestCase(2, 2, "7 6 4 2 1\r\n1 2 7 8 9\r\n9 7 6 2 1\r\n1 3 2 4 5\r\n8 6 4 4 1\r\n1 3 6 7 9", "4")]
        [TestCase(2, 2, "7 6 4 2 1", "1")]
        [TestCase(2, 2, "1 2 7 8 9", "0")]
        [TestCase(2, 2, "9 7 6 2 1", "0")]
        [TestCase(2, 2, "1 3 2 4 5", "1")]
        [TestCase(2, 2, "8 6 4 4 1", "1")]
        [TestCase(2, 2, "1 3 6 7 9", "1")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(2024, day, part);
            string result = solution.Solve(input.Split(Environment.NewLine));
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
