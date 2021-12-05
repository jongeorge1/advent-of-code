namespace AoC.Tests.Year2021
{
    using AoC.Solutions;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "199\r\n200\r\n208\r\n210\r\n200\r\n207\r\n240\r\n269\r\n260\r\n263", "7")]
        [TestCase(1, 2, "199\r\n200\r\n208\r\n210\r\n200\r\n207\r\n240\r\n269\r\n260\r\n263", "5")]
        [TestCase(2, 1, "forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2", "150")]
        [TestCase(2, 2, "forward 5\r\ndown 5\r\nforward 8\r\nup 3\r\ndown 8\r\nforward 2", "900")]
        [TestCase(3, 1, "00100\r\n11110\r\n10110\r\n10111\r\n10101\r\n01111\r\n00111\r\n11100\r\n10000\r\n11001\r\n00010\r\n01010", "198")]
        [TestCase(3, 2, "00100\r\n11110\r\n10110\r\n10111\r\n10101\r\n01111\r\n00111\r\n11100\r\n10000\r\n11001\r\n00010\r\n01010", "230")]
        [TestCase(4, 1, "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1\r\n\r\n22 13 17 11  0\r\n 8  2 23  4 24\r\n21  9 14 16  7\r\n 6 10  3 18  5\r\n 1 12 20 15 19\r\n\r\n 3 15  0  2 22\r\n 9 18 13 17  5\r\n19  8  7 25 23\r\n20 11 10 24  4\r\n14 21 16 12  6\r\n\r\n14 21 17 24  4\r\n10 16 15  9 19\r\n18  8 23 26 20\r\n22 11 13  6  5\r\n 2  0 12  3  7", "4512")]
        [TestCase(4, 2, "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1\r\n\r\n22 13 17 11  0\r\n 8  2 23  4 24\r\n21  9 14 16  7\r\n 6 10  3 18  5\r\n 1 12 20 15 19\r\n\r\n 3 15  0  2 22\r\n 9 18 13 17  5\r\n19  8  7 25 23\r\n20 11 10 24  4\r\n14 21 16 12  6\r\n\r\n14 21 17 24  4\r\n10 16 15  9 19\r\n18  8 23 26 20\r\n22 11 13  6  5\r\n 2  0 12  3  7", "1924")]
        [TestCase(5, 1, "0,9 -> 5,9\r\n8,0 -> 0,8\r\n9,4 -> 3,4\r\n2,2 -> 2,1\r\n7,0 -> 7,4\r\n6,4 -> 2,0\r\n0,9 -> 2,9\r\n3,4 -> 1,4\r\n0,0 -> 8,8\r\n5,5 -> 8,2", "5")]
        [TestCase(5, 2, "0,9 -> 5,9\r\n8,0 -> 0,8\r\n9,4 -> 3,4\r\n2,2 -> 2,1\r\n7,0 -> 7,4\r\n6,4 -> 2,0\r\n0,9 -> 2,9\r\n3,4 -> 1,4\r\n0,0 -> 8,8\r\n5,5 -> 8,2", "12")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(2021, day, part);
            string result = solution.Solve(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}