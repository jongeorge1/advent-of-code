namespace AdventOfCode.Helpers;

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ChineseRemainderTheoremTests
{
    [TestMethod]
    [DataRow("3,5,7", "1,4,6", 34)]
    public void Test(string divisorsList, string remaindersList, int expectedResult)
    {
        long[] divisors = divisorsList.Split(',').Select(long.Parse).ToArray();
        long[] remainders = remaindersList.Split(',').Select(long.Parse).ToArray();

        long result = ChineseRemainderTheorem.Solve(divisors, remainders);

        Assert.AreEqual(expectedResult, result);
    }
}
