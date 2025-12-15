using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Helpers;

[TestClass]
public class BitwiseHelperTests
{

    [TestMethod]
    [DataRow(new int[] { 1 }, 8, 1)]
    [DataRow(new int[] { 142, 1923 }, 32, 8259222110350)]
    [DataRow(new int[] { 1, 15, 23, 30, 16, 3, 9, 22, 29, 15, 11, 8 }, 5, 301175692241886689)]
    public void TestEncoding(int[] numbersToEncode, int bitsPerNumber, long expectedResult)
    {
        long result = BitwiseHelpers.EncodeAsInt64(numbersToEncode, bitsPerNumber);
        Assert.AreEqual(expectedResult, result);
    }

    [TestMethod]
    [DataRow(1, 1, 8, new int[] { 1 })]
    [DataRow(8259222110350, 2, 32, new int[] { 142, 1923 })]
    [DataRow(301175692241886689, 12, 5, new int[] { 1, 15, 23, 30, 16, 3, 9, 22, 29, 15, 11, 8 })]
    public void TestDecoding(long encodedValue, int componentCount, int bitsPerNumber, int[] expectedResult)
    {
        int[] result = BitwiseHelpers.DecodeFromInt64(encodedValue, componentCount, bitsPerNumber);
        CollectionAssert.AreEqual(expectedResult, result);
    }
}
