namespace AdventOfCode.Year2017;

using System;
using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

public class AoCTestCases
{
    [TestMethod]
    [DataRow(1, 1, "1122", "3")]
    [DataRow(1, 1, "1111", "4")]
    [DataRow(1, 1, "1234", "0")]
    [DataRow(1, 1, "91212129", "9")]
    [DataRow(1, 2, "1212", "6")]
    [DataRow(1, 2, "1221", "0")]
    [DataRow(1, 2, "123425", "4")]
    [DataRow(1, 2, "123123", "12")]
    [DataRow(1, 2, "12131415", "4")]
    [DataRow(2, 1, "5\t1\t9\t5\r\n7\t5\t3\r\n2\t4\t6\t8", "18")]
    [DataRow(2, 2, "5\t9\t2\t8\r\n9\t4\t7\t3\r\n3\t8\t6\t5", "9")]
    public void Tests(int day, int part, string input, string expectedResult)
    {
        ISolution solution = SolutionFactory.GetSolution(2017, day, part);
        string result = solution.Solve(input.Split(Environment.NewLine));
        Assert.AreEqual(expectedResult, result);
    }
}