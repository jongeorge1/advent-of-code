namespace AdventOfCode.Year2025;

using System;
using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AoCTestCases
{
    [TestMethod]
    [DataRow(1, 1, "", "", DisplayName = "Day 01, Part 1 - Example")]
    [DataRow(1, 2, "", "", DisplayName = "Day 01, Part 2 - Example")]
    [DataRow(2, 1, "", "", DisplayName = "Day 02, Part 1 - Example")]
    [DataRow(2, 2, "", "", DisplayName = "Day 02, Part 2 - Example")]
    [DataRow(3, 1, "", "", DisplayName = "Day 03, Part 1 - Example")]
    [DataRow(3, 2, "", "", DisplayName = "Day 03, Part 2 - Example")]
    [DataRow(4, 1, "", "", DisplayName = "Day 04, Part 1 - Example")]
    [DataRow(4, 2, "", "", DisplayName = "Day 04, Part 2 - Example")]
    [DataRow(5, 1, "", "", DisplayName = "Day 05, Part 1 - Example")]
    [DataRow(5, 2, "", "", DisplayName = "Day 05, Part 2 - Example")]
    [DataRow(6, 1, "", "", DisplayName = "Day 06, Part 1 - Example")]
    [DataRow(6, 2, "", "", DisplayName = "Day 06, Part 2 - Example")]
    [DataRow(7, 1, "", "", DisplayName = "Day 07, Part 1 - Example")]
    [DataRow(7, 2, "", "", DisplayName = "Day 07, Part 2 - Example")]
    [DataRow(8, 1, "", "", DisplayName = "Day 08, Part 1 - Example")]
    [DataRow(8, 2, "", "", DisplayName = "Day 08, Part 2 - Example")]
    [DataRow(9, 1, "", "", DisplayName = "Day 09, Part 1 - Example")]
    [DataRow(9, 2, "", "", DisplayName = "Day 09, Part 2 - Example")]
    [DataRow(10, 1, "", "", DisplayName = "Day 10, Part 1 - Example")]
    [DataRow(10, 2, "", "", DisplayName = "Day 10, Part 2 - Example")]
    [DataRow(11, 1, "", "", DisplayName = "Day 11, Part 1 - Example")]
    [DataRow(11, 2, "", "", DisplayName = "Day 11, Part 2 - Example")]
    [DataRow(12, 1, "", "", DisplayName = "Day 12, Part 1 - Example")]
    [DataRow(12, 2, "", "", DisplayName = "Day 12, Part 2 - Example")]
    public void Tests(int day, int part, string input, string expectedResult)
    {
        ISolution solution = SolutionFactory.GetSolution(2025, day, part);
        string result = solution.Solve(input.Split(Environment.NewLine));
        Assert.AreEqual(expectedResult, result);
    }
}
