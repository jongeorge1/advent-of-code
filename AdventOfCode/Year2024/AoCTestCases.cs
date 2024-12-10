namespace AdventOfCode.Year2024;

using System;
using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AoCTestCases
{
    [TestMethod]
    [DataRow(1, 1, "3   4\r\n4   3\r\n2   5\r\n1   3\r\n3   9\r\n3   3", "11", DisplayName = "Day 01, Part 1 - Example")]
    [DataRow(1, 2, "3   4\r\n4   3\r\n2   5\r\n1   3\r\n3   9\r\n3   3", "31", DisplayName = "Day 01, Part 2 - Example")]
    [DataRow(2, 1, "7 6 4 2 1\r\n1 2 7 8 9\r\n9 7 6 2 1\r\n1 3 2 4 5\r\n8 6 4 4 1\r\n1 3 6 7 9", "2", DisplayName = "Day 02, Part 1 - Example")]
    [DataRow(2, 1, "7 6 4 2 1", "1", DisplayName = "Day 02, Part 1 - Example Row 1")]
    [DataRow(2, 1, "1 2 7 8 9", "0", DisplayName = "Day 02, Part 1 - Example Row 2")]
    [DataRow(2, 1, "9 7 6 2 1", "0", DisplayName = "Day 02, Part 1 - Example Row 3")]
    [DataRow(2, 1, "1 3 2 4 5", "0", DisplayName = "Day 02, Part 1 - Example Row 4")]
    [DataRow(2, 1, "8 6 4 4 1", "0", DisplayName = "Day 02, Part 1 - Example Row 5")]
    [DataRow(2, 1, "1 3 6 7 9", "1", DisplayName = "Day 02, Part 1 - Example Row 6")]
    [DataRow(2, 2, "7 6 4 2 1\r\n1 2 7 8 9\r\n9 7 6 2 1\r\n1 3 2 4 5\r\n8 6 4 4 1\r\n1 3 6 7 9", "4", DisplayName = "Day 02, Part 2 - Example")]
    [DataRow(2, 2, "7 6 4 2 1", "1", DisplayName = "Day 02, Part 2 - Example Row 1")]
    [DataRow(2, 2, "1 2 7 8 9", "0", DisplayName = "Day 02, Part 2 - Example Row 2")]
    [DataRow(2, 2, "9 7 6 2 1", "0", DisplayName = "Day 02, Part 2 - Example Row 3")]
    [DataRow(2, 2, "1 3 2 4 5", "1", DisplayName = "Day 02, Part 2 - Example Row 4")]
    [DataRow(2, 2, "8 6 4 4 1", "1", DisplayName = "Day 02, Part 2 - Example Row 5")]
    [DataRow(2, 2, "1 3 6 7 9", "1", DisplayName = "Day 02, Part 2 - Example Row 6")]
    [DataRow(3, 1, "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))mul(1000,2)", "161", DisplayName = "Day 03, Part 1 - Example")]
    [DataRow(3, 2, "xmul(2,4)&mul[3, 7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))", "48", DisplayName = "Day 03, Part 2 - Example")]
    [DataRow(4, 1, "MMMSXXMASM\r\nMSAMXMSMSA\r\nAMXSXMAAMM\r\nMSAMASMSMX\r\nXMASAMXAMM\r\nXXAMMXXAMA\r\nSMSMSASXSS\r\nSAXAMASAAA\r\nMAMMMXMMMM\r\nMXMXAXMASX", "18", DisplayName = "Day 04, Part 1 - Example")]
    [DataRow(4, 1, ".XMAS.", "1", DisplayName = "Day 04, Part 1 - Horizontal/Forward")]
    [DataRow(4, 1, ".SAMX.", "1", DisplayName = "Day 04, Part 1 - Horizontal/Backward")]
    [DataRow(4, 1, "X\r\nM\r\nA\r\nS", "1", DisplayName = "Day 04, Part 1 - Vertical/Forward")]
    [DataRow(4, 1, "X\r\nM\r\nA\r\nS\r\nA\r\nM\r\nX", "2", DisplayName = "Day 04, Part 1 - Vertical Forward & Backward")]
    [DataRow(4, 1, "S\r\nA\r\nM\r\nX", "1", DisplayName = "Day 04, Part 1 - Vertical/Backward")]
    [DataRow(4, 1, "X...\r\n.M..\r\n..A.\r\n...S", "1", DisplayName = "Day 04, Part 1 - Left to Right Diagonal Forward")]
    [DataRow(4, 1, "S...\r\n.A..\r\n..M.\r\n...X", "1", DisplayName = "Day 04, Part 1 - Left to Right Diagonal Backward")]
    [DataRow(4, 1, "...X.\r\n..M..\r\n.A...\r\nS....", "1", DisplayName = "Day 04, Part 1 - Right to Left Diagnoal Forward")]
    [DataRow(4, 1, "...S\r\n..A.\r\n.M..\r\nX...", "1", DisplayName = "Day 04, Part 1 - Right to Left Diagnoal Backward")]
    [DataRow(4, 2, "M.S\r\n.A.\r\nM.S", "1", DisplayName = "Day 04, Part 2 - Example")]
    [DataRow(4, 2, "MMMSXXMASM\r\nMSAMXMSMSA\r\nAMXSXMAAMM\r\nMSAMASMSMX\r\nXMASAMXAMM\r\nXXAMMXXAMA\r\nSMSMSASXSS\r\nSAXAMASAAA\r\nMAMMMXMMMM\r\nMXMXAXMASX", "9", DisplayName = "Day 04, Part 2 - Example")]
    [DataRow(5, 1, "47|53\r\n97|13\r\n97|61\r\n97|47\r\n75|29\r\n61|13\r\n75|53\r\n29|13\r\n97|29\r\n53|29\r\n61|53\r\n97|53\r\n61|29\r\n47|13\r\n75|47\r\n97|75\r\n47|61\r\n75|61\r\n47|29\r\n75|13\r\n53|13\r\n\r\n75,47,61,53,29\r\n97,61,53,29,13\r\n75,29,13\r\n75,97,47,61,53\r\n61,13,29\r\n97,13,75,29,47", "143", DisplayName = "Day 05, Part 1 - Example")]
    [DataRow(5, 2, "47|53\r\n97|13\r\n97|61\r\n97|47\r\n75|29\r\n61|13\r\n75|53\r\n29|13\r\n97|29\r\n53|29\r\n61|53\r\n97|53\r\n61|29\r\n47|13\r\n75|47\r\n97|75\r\n47|61\r\n75|61\r\n47|29\r\n75|13\r\n53|13\r\n\r\n75,47,61,53,29\r\n97,61,53,29,13\r\n75,29,13\r\n75,97,47,61,53\r\n61,13,29\r\n97,13,75,29,47", "123", DisplayName = "Day 05, Part 2 - Example")]
    [DataRow(6, 1, "....#.....\r\n.........#\r\n..........\r\n..#.......\r\n.......#..\r\n..........\r\n.#..^.....\r\n........#.\r\n#.........\r\n......#...", "41", DisplayName = "Day 06, Part 1 - Example")]
    [DataRow(6, 2, "....#.....\r\n.........#\r\n..........\r\n..#.......\r\n.......#..\r\n..........\r\n.#..^.....\r\n........#.\r\n#.........\r\n......#...", "6", DisplayName = "Day 06, Part 2 - Example")]
    [DataRow(7, 1, "190: 10 19\r\n3267: 81 40 27\r\n83: 17 5\r\n156: 15 6\r\n7290: 6 8 6 15\r\n161011: 16 10 13\r\n192: 17 8 14\r\n21037: 9 7 18 13\r\n292: 11 6 16 20", "3749", DisplayName = "Day 07, Part 1 - Example")]
    [DataRow(7, 2, "190: 10 19\r\n3267: 81 40 27\r\n83: 17 5\r\n156: 15 6\r\n7290: 6 8 6 15\r\n161011: 16 10 13\r\n192: 17 8 14\r\n21037: 9 7 18 13\r\n292: 11 6 16 20", "11387", DisplayName = "Day 07, Part 2 - Example")]
    [DataRow(8, 1, "............\r\n........0...\r\n.....0......\r\n.......0....\r\n....0.......\r\n......A.....\r\n............\r\n............\r\n........A...\r\n.........A..\r\n............\r\n............", "14", DisplayName = "Day 08, Part 1 - Example")]
    [DataRow(8, 1, "..........\r\n..........\r\n..........\r\n....a.....\r\n..........\r\n.....a....\r\n..........\r\n..........\r\n..........\r\n..........", "2", DisplayName = "Day 08, Part 1 - Simple Example with two antennas")]
    [DataRow(8, 1, "..........\r\n..........\r\n..........\r\n....a.....\r\n........a.\r\n.....a....\r\n..........\r\n..........\r\n..........\r\n..........", "4", DisplayName = "Day 08, Part 1 - Simple Example with three antennas")]
    [DataRow(8, 2, "............\r\n........0...\r\n.....0......\r\n.......0....\r\n....0.......\r\n......A.....\r\n............\r\n............\r\n........A...\r\n.........A..\r\n............\r\n............", "34", DisplayName = "Day 08, Part 2 - Example")]
    [DataRow(8, 2, "T.........\r\n...T......\r\n.T........\r\n..........\r\n..........\r\n..........\r\n..........\r\n..........\r\n..........\r\n..........", "9", DisplayName = "Day 08, Part 2 - Simple Example with three antennas")]
    [DataRow(9, 1, "2333133121414131402", "1928", DisplayName = "Day 09, Part 1 - Example")]
    [DataRow(9, 2, "2333133121414131402", "2858", DisplayName = "Day 09, Part 2 - Example")]
    [DataRow(10, 1, "89010123\r\n78121874\r\n87430965\r\n96549874\r\n45678903\r\n32019012\r\n01329801\r\n10456732", "36", DisplayName = "Day 10, Part 1 - Example")]
    [DataRow(10, 2, "89010123\r\n78121874\r\n87430965\r\n96549874\r\n45678903\r\n32019012\r\n01329801\r\n10456732", "81", DisplayName = "Day 10, Part 2 - Example")]
    public void Tests(int day, int part, string input, string expectedResult)
    {
        ISolution solution = SolutionFactory.GetSolution(2024, day, part);
        string result = solution.Solve(input.Split(Environment.NewLine));
        Assert.AreEqual(expectedResult, result);
    }
}
