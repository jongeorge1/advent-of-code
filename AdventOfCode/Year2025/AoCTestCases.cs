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
    [DataRow(3, 1, "987654321111111", "98", DisplayName = "Day 03, Part 1 - Sub example")]
    [DataRow(3, 1, "811111111111119", "89", DisplayName = "Day 03, Part 1 - Sub example")]
    [DataRow(3, 1, "234234234234278", "78", DisplayName = "Day 03, Part 1 - Sub example")]
    [DataRow(3, 1, "818181911112111", "92", DisplayName = "Day 03, Part 1 - Sub example")]
    [DataRow(3, 1, "987654321111111\r\n811111111111119\r\n234234234234278\r\n818181911112111", "357", DisplayName = "Day 03, Part 1 - Example")]
    [DataRow(3, 2, "987654321111111", "987654321111", DisplayName = "Day 03, Part 2 - Sub example")]
    [DataRow(3, 2, "811111111111119", "811111111119", DisplayName = "Day 03, Part 2 - Sub example")]
    [DataRow(3, 2, "234234234234278", "434234234278", DisplayName = "Day 03, Part 2 - Sub example")]
    [DataRow(3, 2, "818181911112111", "888911112111", DisplayName = "Day 03, Part 2 - Sub example")]
    [DataRow(3, 2, "987654321111111\r\n811111111111119\r\n234234234234278\r\n818181911112111", "3121910778619", DisplayName = "Day 03, Part 2 - Example")]
    [DataRow(4, 1, "..@@.@@@@.\r\n@@@.@.@.@@\r\n@@@@@.@.@@\r\n@.@@@@..@.\r\n@@.@@@@.@@\r\n.@@@@@@@.@\r\n.@.@.@.@@@\r\n@.@@@.@@@@\r\n.@@@@@@@@.\r\n@.@.@@@.@.", "13", DisplayName = "Day 04, Part 1 - Example")]
    [DataRow(4, 2, "..@@.@@@@.\r\n@@@.@.@.@@\r\n@@@@@.@.@@\r\n@.@@@@..@.\r\n@@.@@@@.@@\r\n.@@@@@@@.@\r\n.@.@.@.@@@\r\n@.@@@.@@@@\r\n.@@@@@@@@.\r\n@.@.@@@.@.", "43", DisplayName = "Day 04, Part 2 - Example")]
    [DataRow(5, 1, "3-5\r\n10-14\r\n16-20\r\n12-18\r\n\r\n1\r\n5\r\n8\r\n11\r\n17\r\n32", "3", DisplayName = "Day 05, Part 1 - Example")]
    [DataRow(5, 2, "3-5\r\n10-14\r\n16-20\r\n12-18\r\n\r\n1\r\n5\r\n8\r\n11\r\n17\r\n32", "14", DisplayName = "Day 05, Part 2 - Example")]
    [DataRow(6, 1, "123 328  51 64 \r\n 45 64  387 23 \r\n  6 98  215 314\r\n*   +   *   +  ", "4277556", DisplayName = "Day 06, Part 1 - Example")]
    [DataRow(6, 2, "123 328  51 64 \r\n 45 64  387 23 \r\n  6 98  215 314\r\n*   +   *   +  ", "3263827", DisplayName = "Day 06, Part 2 - Example")]
    [DataRow(7, 1, ".......S.......\r\n...............\r\n.......^.......\r\n...............\r\n......^.^......\r\n...............\r\n.....^.^.^.....\r\n...............\r\n....^.^...^....\r\n...............\r\n...^.^...^.^...\r\n...............\r\n..^...^.....^..\r\n...............\r\n.^.^.^.^.^...^.\r\n...............", "21", DisplayName = "Day 07, Part 1 - Example")]
    [DataRow(7, 2, ".......S.......\r\n...............\r\n.......^.......\r\n...............\r\n......^.^......\r\n...............\r\n.....^.^.^.....\r\n...............\r\n....^.^...^....\r\n...............\r\n...^.^...^.^...\r\n...............\r\n..^...^.....^..\r\n...............\r\n.^.^.^.^.^...^.\r\n...............", "40", DisplayName = "Day 07, Part 2 - Example")]
    [DataRow(8, 1, "162,817,812\r\n57,618,57\r\n906,360,560\r\n592,479,940\r\n352,342,300\r\n466,668,158\r\n542,29,236\r\n431,825,988\r\n739,650,466\r\n52,470,668\r\n216,146,977\r\n819,987,18\r\n117,168,530\r\n805,96,715\r\n346,949,466\r\n970,615,88\r\n941,993,340\r\n862,61,35\r\n984,92,344\r\n425,690,689", "40", DisplayName = "Day 08, Part 1 - Example")]
    [DataRow(8, 2, "162,817,812\r\n57,618,57\r\n906,360,560\r\n592,479,940\r\n352,342,300\r\n466,668,158\r\n542,29,236\r\n431,825,988\r\n739,650,466\r\n52,470,668\r\n216,146,977\r\n819,987,18\r\n117,168,530\r\n805,96,715\r\n346,949,466\r\n970,615,88\r\n941,993,340\r\n862,61,35\r\n984,92,344\r\n425,690,689", "25272", DisplayName = "Day 08, Part 2 - Example")]
    [DataRow(9, 1, "7,1\r\n11,1\r\n11,7\r\n9,7\r\n9,5\r\n2,5\r\n2,3\r\n7,3", "50", DisplayName = "Day 09, Part 1 - Example")]
    [DataRow(9, 2, "7,1\r\n11,1\r\n11,7\r\n9,7\r\n9,5\r\n2,5\r\n2,3\r\n7,3", "24", DisplayName = "Day 09, Part 2 - Example")]
    [DataRow(10, 1, "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}\r\n[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}\r\n[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}", "7", DisplayName = "Day 10, Part 1 - Example")]
    [DataRow(10, 1, "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}", "2", DisplayName = "Day 10, Part 1 - Sub-Example 1")]
    [DataRow(10, 1, "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}", "3", DisplayName = "Day 10, Part 1 - Sub-Example 2")]
    [DataRow(10, 1, "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}", "2", DisplayName = "Day 10, Part 1 - Sub-Example 3")]
    [DataRow(10, 2, "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}\r\n[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}\r\n[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}", "33", DisplayName = "Day 10, Part 2 - Example")]
    [DataRow(10, 2, "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}", "10", DisplayName = "Day 10, Part 2 - Sub-Example 1")]
    [DataRow(10, 2, "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}", "12", DisplayName = "Day 10, Part 2 - Sub-Example 2")]
    [DataRow(10, 2, "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}", "11", DisplayName = "Day 10, Part 2 - Sub-Example 3")]
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
