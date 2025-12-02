namespace AdventOfCode.Year2025;

using System;
using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AoCTestCases
{
    [TestMethod]
    [DataRow(1, 1, "L68\r\nL30\r\nR48\r\nL5\r\nR60\r\nL55\r\nL1\r\nL99\r\nR14\r\nL82", "3", DisplayName = "Day 01, Part 1 - Example")]
    [DataRow(1, 2, "L68\r\nL30\r\nR48\r\nL5\r\nR60\r\nL55\r\nL1\r\nL99\r\nR14\r\nL82", "6", DisplayName = "Day 01, Part 2 - Example")]
    [DataRow(2, 1, "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124", "1227775554", DisplayName = "Day 02, Part 1 - Example")]
    [DataRow(2, 1, "11-22", "33", DisplayName = "Day 02, Part 1 - Sub-Example")]
    [DataRow(2, 1, "95-115", "99", DisplayName = "Day 02, Part 1 - Sub-Example")]
    [DataRow(2, 1, "998-1012", "1010", DisplayName = "Day 02, Part 1 - Sub-Example")]
    [DataRow(2, 1, "1188511880-1188511890", "1188511885", DisplayName = "Day 02, Part 1 - Sub-Example")]
    [DataRow(2, 1, "222220-222224", "222222", DisplayName = "Day 02, Part 1 - Sub-Example")]
    [DataRow(2, 1, "1698522-1698528", "0", DisplayName = "Day 02, Part 1 - Sub-Example")]
    [DataRow(2, 1, "446443-446449", "446446", DisplayName = "Day 02, Part 1 - Sub-Example")]
    [DataRow(2, 1, "38593856-38593862", "38593859", DisplayName = "Day 02, Part 1 - Sub-Example")]
    [DataRow(2, 2, "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124", "4174379265", DisplayName = "Day 02, Part 2 - Example")]
    [DataRow(2, 2, "11-22", "33", DisplayName = "Day 02, Part 2 - Sub-Example")]
    [DataRow(2, 2, "95-115", "210", DisplayName = "Day 02, Part 2 - Sub-Example")]
    [DataRow(2, 2, "998-1012", "2009", DisplayName = "Day 02, Part 2 - Sub-Example")]
    [DataRow(2, 2, "1188511880-1188511890", "1188511885", DisplayName = "Day 02, Part 2 - Sub-Example")]
    [DataRow(2, 2, "222220-222224", "222222", DisplayName = "Day 02, Part 2 - Sub-Example")]
    [DataRow(2, 2, "1698522-1698528", "0", DisplayName = "Day 02, Part 2 - Sub-Example")]
    [DataRow(2, 2, "446443-446449", "446446", DisplayName = "Day 02, Part 2 - Sub-Example")]
    [DataRow(2, 2, "38593856-38593862", "38593859", DisplayName = "Day 02, Part 2 - Sub-Example")]
    [DataRow(2, 2, "565653-565659", "565656", DisplayName = "Day 02, Part 2 - Sub-Example")]
    [DataRow(2, 2, "824824821-824824827", "824824824", DisplayName = "Day 02, Part 2 - Sub-Example")]
    [DataRow(2, 2, "2121212118-2121212124", "2121212121", DisplayName = "Day 02, Part 2 - Sub-Example")]
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
