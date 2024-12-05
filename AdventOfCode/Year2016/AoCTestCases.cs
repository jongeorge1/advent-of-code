namespace AdventOfCode.Year2016;

using System;
using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AoCTestCases
{
    [TestMethod]
    [DataRow(1, 1, "R2, L3", "5")]
    [DataRow(1, 1, "R2, R2, R2", "2")]
    [DataRow(1, 1, "R5, L5, R5, R3", "12")]
    [DataRow(1, 2, "R8, R4, R4, R8", "4")]
    [DataRow(2, 1, "ULL\r\nRRDDD\r\nLURDL\r\nUUUUD", "1985")]
    [DataRow(2, 2, "ULL\r\nRRDDD\r\nLURDL\r\nUUUUD", "5DB3")]
    [DataRow(3, 1, "5 10 25", "0")]
    [DataRow(3, 1, "15 15 25", "1")]
    [DataRow(5, 1, "abc", "18f47a30")]
    [DataRow(5, 2, "abc", "05ace8e3")]
    [DataRow(6, 1, "eedadn\r\ndrvtee\r\neandsr\r\nraavrd\r\natevrs\r\ntsrnev\r\nsdttsa\r\nrasrtv\r\nnssdts\r\nntnada\r\nsvetve\r\ntesnvt\r\nvntsnd\r\nvrdear\r\ndvrsen\r\nenarar", "easter")]
    [DataRow(7, 1, "abba[mnop]qrst", "1")]
    [DataRow(7, 1, "abcd[bddb]xyyx", "0")]
    [DataRow(7, 1, "aaaa[qwer]tyui", "0")]
    [DataRow(7, 1, "ioxxoj[asdfgh]zxcvbn", "1")]
    [DataRow(7, 2, "aba[bab]xyz", "1")]
    [DataRow(7, 2, "xyx[xyx]xyx", "0")]
    [DataRow(7, 2, "aaa[kek]eke", "1")]
    [DataRow(7, 2, "zazbz[bzb]cdb", "1")]
    [DataRow(8, 1, "TEST\r\nrect 3x2\r\nrotate column x=1 by 1\r\nrotate row y=0 by 4\r\nrotate column x=1 by 1", "6")]
    [DataRow(11, 1, "The first floor contains a hydrogen-compatible microchip, and a lithium-compatible microchip.\r\nThe second floor contains a hydrogen generator.\r\nThe third floor contains a lithium generator.\r\nThe fourth floor contains nothing relevant.", "11")]
    [DataRow(13, 1, "TEST10", "11")]
    [DataRow(14, 1, "abc", "22728")]
    [DataRow(14, 2, "abc", "22551")]
    [DataRow(15, 1, "Disc #1 has 5 positions; at time=0, it is at position 4.\r\nDisc #2 has 2 positions; at time=0, it is at position 1.", "5")]
    [DataRow(16, 1, "TEST10000", "01100")]
    [DataRow(17, 1, "hijkl", "")]
    [DataRow(17, 1, "kglvqrro", "DDUDRLRRUDRD")]
    [DataRow(17, 1, "ulqzkmiv", "DRURDRUDDLLDLUURRDULRLDUUDDDRR")]
    [DataRow(17, 2, "ihgpwlah", "370")]
    [DataRow(17, 2, "kglvqrro", "492")]
    [DataRow(17, 2, "ulqzkmiv", "830")]
    [DataRow(18, 1, "..^^.", "6")]
    [DataRow(18, 1, ".^^.^.^^^^", "38")]
    [DataRow(19, 1, "5", "3")]
    [DataRow(19, 2, "5", "2")]
    [DataRow(20, 1, "5-8\r\n0-2\r\n4-7", "3")]
    [DataRow(21, 1, "TESTswap position 4 with position 0\r\nswap letter d with letter b\r\nreverse positions 0 through 4\r\nrotate left 1 step\r\nmove position 1 to position 4\r\nmove position 3 to position 0\r\nrotate based on position of letter b\r\nrotate based on position of letter d", "decab")]
    [DataRow(21, 2, "TESTswap position 4 with position 0\r\nswap letter d with letter b\r\nreverse positions 0 through 4\r\nrotate left 1 step\r\nmove position 1 to position 4\r\nmove position 3 to position 0\r\nrotate based on position of letter b\r\nrotate based on position of letter d", "abcde")]
    [DataRow(23, 1, "TESTcpy 2 a\r\ntgl a\r\ntgl a\r\ntgl a\r\ncpy 1 a\r\ndec a\r\ndec a", "3")]
    [DataRow(24, 1, "###########\r\n#0.1.....2#\r\n#.#######.#\r\n#4.......3#\r\n###########", "14")]
    public void Tests(int day, int part, string input, string expectedResult)
    {
        ISolution solution = SolutionFactory.GetSolution(2016, day, part);
        string result = solution.Solve(input.Split(Environment.NewLine));
        Assert.AreEqual(expectedResult, result);
    }
}