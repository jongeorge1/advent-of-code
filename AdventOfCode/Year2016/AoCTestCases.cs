namespace AdventOfCode.Year2016;

using System;
using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AoCTestCases
{
    [TestMethod]
    [DataRow(1, 1, "R2, L3", "5", DisplayName = "Day 01, Part 1 - Example 1")]
    [DataRow(1, 1, "R2, R2, R2", "2", DisplayName = "Day 01, Part 1 - Example 2")]
    [DataRow(1, 1, "R5, L5, R5, R3", "12", DisplayName = "Day 01, Part 1 - Example 3")]
    [DataRow(1, 2, "R8, R4, R4, R8", "4", DisplayName = "Day 01, Part 2 - Example 1")]
    [DataRow(2, 1, "ULL\r\nRRDDD\r\nLURDL\r\nUUUUD", "1985", DisplayName = "Day 02, Part 1 - Example")]
    [DataRow(2, 2, "ULL\r\nRRDDD\r\nLURDL\r\nUUUUD", "5DB3", DisplayName = "Day 02, Part 2 - Example")]
    [DataRow(3, 1, "5 10 25", "0", DisplayName = "Day 03, Part 1 - Example 1")]
    [DataRow(3, 1, "15 15 25", "1", DisplayName = "Day 03, Part 1 - Example 2")]
    [DataRow(5, 1, "abc", "18f47a30", DisplayName = "Day 05, Part 1 - Example")]
    [DataRow(5, 2, "abc", "05ace8e3", DisplayName = "Day 05, Part 2 - Example")]
    [DataRow(6, 1, "eedadn\r\ndrvtee\r\neandsr\r\nraavrd\r\natevrs\r\ntsrnev\r\nsdttsa\r\nrasrtv\r\nnssdts\r\nntnada\r\nsvetve\r\ntesnvt\r\nvntsnd\r\nvrdear\r\ndvrsen\r\nenarar", "easter", DisplayName = "Day 06, Part 1 - Example")]
    [DataRow(7, 1, "abba[mnop]qrst", "1", DisplayName = "Day 07, Part 1 - Example 1")]
    [DataRow(7, 1, "abcd[bddb]xyyx", "0", DisplayName = "Day 07, Part 1 - Example 2")]
    [DataRow(7, 1, "aaaa[qwer]tyui", "0", DisplayName = "Day 07, Part 1 - Example 3")]
    [DataRow(7, 1, "ioxxoj[asdfgh]zxcvbn", "1", DisplayName = "Day 07, Part 1 - Example 4")]
    [DataRow(7, 2, "aba[bab]xyz", "1", DisplayName = "Day 07, Part 2 - Example 1")]
    [DataRow(7, 2, "xyx[xyx]xyx", "0", DisplayName = "Day 07, Part 2 - Example 2")]
    [DataRow(7, 2, "aaa[kek]eke", "1", DisplayName = "Day 07, Part 2 - Example 3")]
    [DataRow(7, 2, "zazbz[bzb]cdb", "1", DisplayName = "Day 07, Part 2 - Example 4")]
    [DataRow(8, 1, "TEST\r\nrect 3x2\r\nrotate column x=1 by 1\r\nrotate row y=0 by 4\r\nrotate column x=1 by 1", "6", DisplayName = "Day 08, Part 1 - Example")]
    [DataRow(11, 1, "The first floor contains a hydrogen-compatible microchip, and a lithium-compatible microchip.\r\nThe second floor contains a hydrogen generator.\r\nThe third floor contains a lithium generator.\r\nThe fourth floor contains nothing relevant.", "11", DisplayName = "Day 11, Part 1 - Example")]
    [DataRow(13, 1, "TEST10", "11", DisplayName = "Day 13, Part 1 - Example")]
    [DataRow(14, 1, "abc", "22728", DisplayName = "Day 14, Part 1 - Example")]
    [DataRow(14, 2, "abc", "22551", DisplayName = "Day 14, Part 2 - Example")]
    [DataRow(15, 1, "Disc #1 has 5 positions; at time=0, it is at position 4.\r\nDisc #2 has 2 positions; at time=0, it is at position 1.", "5", DisplayName = "Day 15, Part 1 - Example")]
    [DataRow(16, 1, "TEST10000", "01100", DisplayName = "Day 16, Part 1 - Example")]
    [DataRow(17, 1, "hijkl", "", DisplayName = "Day 17, Part 1 - Example 1")]
    [DataRow(17, 1, "kglvqrro", "DDUDRLRRUDRD", DisplayName = "Day 17, Part 1 - Example 2")]
    [DataRow(17, 1, "ulqzkmiv", "DRURDRUDDLLDLUURRDULRLDUUDDDRR", DisplayName = "Day 17, Part 1 - Example 3")]
    [DataRow(17, 2, "ihgpwlah", "370", DisplayName = "Day 17, Part 2 - Example 1")]
    [DataRow(17, 2, "kglvqrro", "492", DisplayName = "Day 17, Part 2 - Example 2")]
    [DataRow(17, 2, "ulqzkmiv", "830", DisplayName = "Day 17, Part 2 - Example 3")]
    [DataRow(18, 1, "..^^.", "6", DisplayName = "Day 18, Part 1 - Example 1")]
    [DataRow(18, 1, ".^^.^.^^^^", "38", DisplayName = "Day 18, Part 1 - Example 2")]
    [DataRow(19, 1, "5", "3", DisplayName = "Day 19, Part 1 - Example")]
    [DataRow(19, 2, "5", "2", DisplayName = "Day 19, Part 2 - Example")]
    [DataRow(20, 1, "5-8\r\n0-2\r\n4-7", "3", DisplayName = "Day 20, Part 1 - Example")]
    [DataRow(21, 1, "TEST\r\nswap position 4 with position 0\r\nswap letter d with letter b\r\nreverse positions 0 through 4\r\nrotate left 1 step\r\nmove position 1 to position 4\r\nmove position 3 to position 0\r\nrotate based on position of letter b\r\nrotate based on position of letter d", "decab", DisplayName = "Day 21, Part 1 - Example")]
    [DataRow(21, 2, "TEST\r\nswap position 4 with position 0\r\nswap letter d with letter b\r\nreverse positions 0 through 4\r\nrotate left 1 step\r\nmove position 1 to position 4\r\nmove position 3 to position 0\r\nrotate based on position of letter b\r\nrotate based on position of letter d", "abcde", DisplayName = "Day 21, Part 2 - Example")]
    [DataRow(23, 1, "TEST\r\ncpy 2 a\r\ntgl a\r\ntgl a\r\ntgl a\r\ncpy 1 a\r\ndec a\r\ndec a", "3", DisplayName = "Day 23, Part 1 - Example")]
    [DataRow(24, 1, "###########\r\n#0.1.....2#\r\n#.#######.#\r\n#4.......3#\r\n###########", "14", DisplayName = "Day 24, Part 1 - Example")]
    public void Tests(int day, int part, string input, string expectedResult)
    {
        ISolution solution = SolutionFactory.GetSolution(2016, day, part);
        string result = solution.Solve(input.Split(Environment.NewLine));
        Assert.AreEqual(expectedResult, result);
    }
}