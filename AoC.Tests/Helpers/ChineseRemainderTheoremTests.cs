namespace AoC.Tests.Helpers
{
    using System.Linq;
    using AoC.Solutions.Helpers;
    using NUnit.Framework;

    public class ChineseRemainderTheoremTests
    {
        [TestCase("3,5,7", "1,4,6", 34)]
        public void Test(string divisorsList, string remaindersList, int expectedResult)
        {
            long[] divisors = divisorsList.Split(',').Select(long.Parse).ToArray();
            long[] remainders = remaindersList.Split(',').Select(long.Parse).ToArray();

            long result = ChineseRemainderTheorem.Solve(divisors, remainders);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
