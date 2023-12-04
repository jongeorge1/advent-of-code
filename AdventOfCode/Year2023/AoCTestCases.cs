namespace AdventOfCode.Year2023
{
    using System;
    using AdventOfCode;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "1abc2\r\npqr3stu8vwx\r\na1b2c3d4e5f\r\ntreb7uchet", "142")]
        [TestCase(1, 2, "two1nine\r\neightwothree\r\nabcone2threexyz\r\nxtwone3four\r\n4nineeightseven2\r\nzoneight234\r\n7pqrstsixteen", "281")]
        [TestCase(1, 2, "two1", "21")]
        [TestCase(1, 2, "1two", "12")]
        [TestCase(1, 2, "nine", "99")]
        [TestCase(2, 1, "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\r\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\r\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\r\nGame 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\r\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", "8")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(2023, day, part);
            string result = solution.Solve(input.Split(Environment.NewLine));
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}