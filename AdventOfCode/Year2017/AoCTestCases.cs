namespace AdventOfCode.Year2017
{
    using AdventOfCode;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "1122", "3")]
        [TestCase(1, 1, "1111", "4")]
        [TestCase(1, 1, "1234", "0")]
        [TestCase(1, 1, "91212129", "9")]
        [TestCase(1, 2, "1212", "6")]
        [TestCase(1, 2, "1221", "0")]
        [TestCase(1, 2, "123425", "4")]
        [TestCase(1, 2, "123123", "12")]
        [TestCase(1, 2, "12131415", "4")]
        [TestCase(2, 1, "5\t1\t9\t5\r\n7\t5\t3\r\n2\t4\t6\t8", "18")]
        [TestCase(2, 2, "5\t9\t2\t8\r\n9\t4\t7\t3\r\n3\t8\t6\t5", "9")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(2017, day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}