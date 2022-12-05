namespace AdventOfCode.Year2022
{
    using AdventOfCode;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "1000\r\n2000\r\n3000\r\n\r\n4000\r\n\r\n5000\r\n6000\r\n\r\n7000\r\n8000\r\n9000\r\n\r\n10000", "24000")]
        [TestCase(1, 2, "1000\r\n2000\r\n3000\r\n\r\n4000\r\n\r\n5000\r\n6000\r\n\r\n7000\r\n8000\r\n9000\r\n\r\n10000", "45000")]
        [TestCase(2, 1, "A Y\r\nB X\r\nC Z", "15")]
        [TestCase(2, 2, "A Y\r\nB X\r\nC Z", "12")]
        [TestCase(3, 1, "vJrwpWtwJgWrhcsFMMfFFhFp\r\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\r\nPmmdzqPrVvPwwTWBwg\r\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\r\nttgJtRGJQctTZtZT\r\nCrZsJsPPZsGzwwsLwLmpwMDw", "157")]
        [TestCase(3, 2, "vJrwpWtwJgWrhcsFMMfFFhFp\r\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\r\nPmmdzqPrVvPwwTWBwg\r\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\r\nttgJtRGJQctTZtZT\r\nCrZsJsPPZsGzwwsLwLmpwMDw", "70")]
        [TestCase(4, 1, "2-4,6-8\r\n2-3,4-5\r\n5-7,7-9\r\n2-8,3-7\r\n6-6,4-6\r\n2-6,4-8", "2")]
        [TestCase(4, 2, "2-4,6-8\r\n2-3,4-5\r\n5-7,7-9\r\n2-8,3-7\r\n6-6,4-6\r\n2-6,4-8", "4")]
        [TestCase(5, 1, "    [D]    \r\n[N] [C]    \r\n[Z] [M] [P]\r\n 1   2   3 \r\n\r\nmove 1 from 2 to 1\r\nmove 3 from 1 to 3\r\nmove 2 from 2 to 1\r\nmove 1 from 1 to 2", "CMZ")]
        [TestCase(5, 2, "    [D]    \r\n[N] [C]    \r\n[Z] [M] [P]\r\n 1   2   3 \r\n\r\nmove 1 from 2 to 1\r\nmove 3 from 1 to 3\r\nmove 2 from 2 to 1\r\nmove 1 from 1 to 2", "MCD")]
        [TestCase(6, 1, "", "")]
        [TestCase(6, 2, "", "")]
        [TestCase(7, 1, "", "")]
        [TestCase(7, 2, "", "")]
        [TestCase(8, 1, "", "")]
        [TestCase(8, 2, "", "")]
        [TestCase(9, 1, "", "")]
        [TestCase(9, 2, "", "")]
        [TestCase(10, 1, "", "")]
        [TestCase(10, 2, "", "")]
        [TestCase(11, 1, "", "")]
        [TestCase(11, 2, "", "")]
        [TestCase(12, 1, "", "")]
        [TestCase(12, 2, "", "")]
        [TestCase(13, 1, "", "")]
        [TestCase(13, 2, "", "")]
        [TestCase(14, 1, "", "")]
        [TestCase(14, 2, "", "")]
        [TestCase(15, 1, "", "")]
        [TestCase(15, 2, "", "")]
        [TestCase(16, 1, "", "")]
        [TestCase(16, 2, "", "")]
        [TestCase(17, 1, "", "")]
        [TestCase(17, 2, "", "")]
        [TestCase(18, 1, "", "")]
        [TestCase(18, 2, "", "")]
        [TestCase(19, 1, "", "")]
        [TestCase(19, 2, "", "")]
        [TestCase(20, 1, "", "")]
        [TestCase(20, 2, "", "")]
        [TestCase(21, 1, "", "")]
        [TestCase(21, 2, "", "")]
        [TestCase(22, 1, "", "")]
        [TestCase(22, 2, "", "")]
        [TestCase(23, 1, "", "")]
        [TestCase(23, 2, "", "")]
        [TestCase(24, 1, "", "")]
        [TestCase(24, 2, "", "")]
        [TestCase(25, 1, "", "")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(2022, day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}