namespace AdventOfCode.Year2023;

using System;
using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AoCTestCases
{
    [TestMethod]
    [DataRow(1, 1, "1abc2\r\npqr3stu8vwx\r\na1b2c3d4e5f\r\ntreb7uchet", "142", DisplayName = "Day 01 Part 1")]
    [DataRow(1, 2, "two1nine\r\neightwothree\r\nabcone2threexyz\r\nxtwone3four\r\n4nineeightseven2\r\nzoneight234\r\n7pqrstsixteen", "281", DisplayName = "Day 01 Part 2 Example")]
    [DataRow(1, 2, "two1", "21", DisplayName = "Day 01 Part 2")]
    [DataRow(1, 2, "1two", "12", DisplayName = "Day 01 Part 2")]
    [DataRow(1, 2, "nine", "99", DisplayName = "Day 01 Part 2")]
    [DataRow(2, 1, "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\r\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\r\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\r\nGame 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\r\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", "8", DisplayName = "Day 02 Part 1")]
    [DataRow(2, 2, "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\r\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\r\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\r\nGame 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\r\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", "2286", DisplayName = "Day 02 Part 2")]
    [DataRow(3, 1, "467..114..\r\n...*......\r\n..35..633.\r\n......#...\r\n617*......\r\n.....+.58.\r\n..592.....\r\n......755.\r\n...$.*....\r\n.664.598..", "4361", DisplayName = "Day 03 Part 1")]
    [DataRow(3, 2, "467..114..\r\n...*......\r\n..35..633.\r\n......#...\r\n617*......\r\n.....+.58.\r\n..592.....\r\n......755.\r\n...$.*....\r\n.664.598..", "467835", DisplayName = "Day 03 Part 2")]
    [DataRow(4, 1, "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\nCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\nCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\nCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\nCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\nCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11", "13", DisplayName = "Day 04 Part 1")]
    [DataRow(4, 2, "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\nCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\nCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\nCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\nCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\nCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11", "30", DisplayName = "Day 04 Part 2")]
    [DataRow(5, 1, "seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4", "35", DisplayName = "Day 05 Part 1")]
    [DataRow(5, 2, "seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4", "46", DisplayName = "Day 05 Part 2")]
    [DataRow(6, 1, "Time:      7  15   30\r\nDistance:  9  40  200", "288", DisplayName = "Day 06 Part 1")]
    [DataRow(6, 2, "Time:      7  15   30\r\nDistance:  9  40  200", "71503", DisplayName = "Day 06 Part 2")]
    [DataRow(7, 1, "32T3K 765\r\nT55J5 684\r\nKK677 28\r\nKTJJT 220\r\nQQQJA 483", "6440", DisplayName = "Day 07 Part 1")]
    [DataRow(7, 2, "32T3K 765\r\nT55J5 684\r\nKK677 28\r\nKTJJT 220\r\nQQQJA 483", "5905", DisplayName = "Day 07 Part 2")]
    [DataRow(8, 1, "RL\r\n\r\nAAA = (BBB, CCC)\r\nBBB = (DDD, EEE)\r\nCCC = (ZZZ, GGG)\r\nDDD = (DDD, DDD)\r\nEEE = (EEE, EEE)\r\nGGG = (GGG, GGG)\r\nZZZ = (ZZZ, ZZZ)", "2", DisplayName = "Day 08 Part 1")]
    [DataRow(8, 1, "LLR\r\n\r\nAAA = (BBB, BBB)\r\nBBB = (AAA, ZZZ)\r\nZZZ = (ZZZ, ZZZ)", "6", DisplayName = "Day 08 Part 1")]
    [DataRow(9, 1, "0 3 6 9 12 15\r\n1 3 6 10 15 21\r\n10 13 16 21 30 45", "114", DisplayName = "Day 09 Part 1")]
    [DataRow(9, 2, "0 3 6 9 12 15\r\n1 3 6 10 15 21\r\n10 13 16 21 30 45", "2", DisplayName = "Day 09 Part 2")]
    [DataRow(10, 1, "-L|F7\r\n7S-7|\r\nL|7||\r\n-L-J|\r\nL|-JF", "4", DisplayName = "Day 10 Part 1")]
    [DataRow(10, 1, "..F7.\r\n.FJ|.\r\nSJ.L7\r\n|F--J\r\nLJ...", "8", DisplayName = "Day 10 Part 1")]
    [DataRow(10, 2, "...........\r\n.S-------7.\r\n.|F-----7|.\r\n.||.....||.\r\n.||.....||.\r\n.|L-7.F-J|.\r\n.|..|.|..|.\r\n.L--J.L--J.\r\n...........", "4", DisplayName = "Day 10 Part 2")]
    [DataRow(10, 2, ".F----7F7F7F7F-7....\r\n.|F--7||||||||FJ....\r\n.||.FJ||||||||L7....\r\nFJL7L7LJLJ||LJ.L-7..\r\nL--J.L7...LJS7F-7L7.\r\n....F-J..F7FJ|L7L7L7\r\n....L7.F7||L7|.L7L7|\r\n.....|FJLJ|FJ|F7|.LJ\r\n....FJL-7.||.||||...\r\n....L---J.LJ.LJLJ...", "8", DisplayName = "Day 10 Part 2")]
    [DataRow(10, 2, "FF7FSF7F7F7F7F7F---7\r\nL|LJ||||||||||||F--J\r\nFL-7LJLJ||||||LJL-77\r\nF--JF--7||LJLJ7F7FJ-\r\nL---JF-JLJ.||-FJLJJ7\r\n|F|F-JF---7F7-L7L|7|\r\n|FFJF7L7F-JF7|JL---7\r\n7-L-JL7||F7|L7F-7F7|\r\nL.L7LFJ|||||FJL7||LJ\r\nL7JLJL-JLJLJL--JLJ.L", "10", DisplayName = "Day 10 Part 2")]
    [DataRow(11, 1, "...#......\r\n.......#..\r\n#.........\r\n..........\r\n......#...\r\n.#........\r\n.........#\r\n..........\r\n.......#..\r\n#...#.....", "374", DisplayName = "Day 11 Part 1")]
    [DataRow(12, 1, "???.### 1,1,3\r\n.??..??...?##. 1,1,3\r\n?#?#?#?#?#?#?#? 1,3,1,6\r\n????.#...#... 4,1,1\r\n????.######..#####. 1,6,5\r\n?###???????? 3,2,1", "21", DisplayName = "Day 12 Part 1")]
    [DataRow(12, 1, "???.### 1,1,3", "1", DisplayName = "Day 12 Part 1")]
    [DataRow(12, 1, ".??..??...?##. 1,1,3", "4", DisplayName = "Day 12 Part 1")]
    [DataRow(12, 1, "?#?#?#?#?#?#?#? 1,3,1,6", "1", DisplayName = "Day 12 Part 1")]
    [DataRow(12, 1, "????.#...#... 4,1,1", "1", DisplayName = "Day 12 Part 1")]
    [DataRow(12, 1, "????.######..#####. 1,6,5", "4", DisplayName = "Day 12 Part 1")]
    [DataRow(12, 1, "?###???????? 3,2,1", "10", DisplayName = "Day 12 Part 1")]
    [DataRow(12, 2, "???.### 1,1,3\r\n.??..??...?##. 1,1,3\r\n?#?#?#?#?#?#?#? 1,3,1,6\r\n????.#...#... 4,1,1\r\n????.######..#####. 1,6,5\r\n?###???????? 3,2,1", "525152", DisplayName = "Day 12 Part 2")]
    [DataRow(13, 1, "#.##..##.\r\n..#.##.#.\r\n##......#\r\n##......#\r\n..#.##.#.\r\n..##..##.\r\n#.#.##.#.\r\n\r\n#...##..#\r\n#....#..#\r\n..##..###\r\n#####.##.\r\n#####.##.\r\n..##..###\r\n#....#..#", "405", DisplayName = "Day 13 Part 1")]
    [DataRow(13, 2, "#.##..##.\r\n..#.##.#.\r\n##......#\r\n##......#\r\n..#.##.#.\r\n..##..##.\r\n#.#.##.#.\r\n\r\n#...##..#\r\n#....#..#\r\n..##..###\r\n#####.##.\r\n#####.##.\r\n..##..###\r\n#....#..#", "400", DisplayName = "Day 13 Part 2")]
    [DataRow(14, 1, "OOOO.#.O..\r\nOO..#....#\r\nOO..O##..O\r\nO..#.OO...\r\n........#.\r\n..#....#.#\r\n..O..#.O.O\r\n..O.......\r\n#....###..\r\n#....#....", "136", DisplayName = "Day 14 Part 1")]
    [DataRow(14, 2, "OOOO.#.O..\r\nOO..#....#\r\nOO..O##..O\r\nO..#.OO...\r\n........#.\r\n..#....#.#\r\n..O..#.O.O\r\n..O.......\r\n#....###..\r\n#....#....", "64", DisplayName = "Day 14 Part 2")]
    [DataRow(15, 1, "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7", "1320", DisplayName = "Day 15 Part 1")]
    [DataRow(15, 2, "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7", "145", DisplayName = "Day 15 Part 2")]
    [DataRow(17, 1, "2413432311323\r\n3215453535623\r\n3255245654254\r\n3446585845452\r\n4546657867536\r\n1438598798454\r\n4457876987766\r\n3637877979653\r\n4654967986887\r\n4564679986453\r\n1224686865563\r\n2546548887735\r\n4322674655533", "102", DisplayName = "Day 17 Part 1 Example")]
    [DataRow(17, 1, "61\r\n24", "5", DisplayName = "Day 17 Part 1 Super simple example 1")]
    [DataRow(17, 2, "2413432311323\r\n3215453535623\r\n3255245654254\r\n3446585845452\r\n4546657867536\r\n1438598798454\r\n4457876987766\r\n3637877979653\r\n4654967986887\r\n4564679986453\r\n1224686865563\r\n2546548887735\r\n4322674655533", "94", DisplayName = "Day 17 Part 2 Example 1")]
    [DataRow(17, 2, "111111111111\r\n999999999991\r\n999999999991\r\n999999999991\r\n999999999991", "71", DisplayName = "Day 17 Part 2 Example 1")]
    [DataRow(18, 1, "R 6 (#70c710)\r\nD 5 (#0dc571)\r\nL 2 (#5713f0)\r\nD 2 (#d2c081)\r\nR 2 (#59c680)\r\nD 2 (#411b91)\r\nL 5 (#8ceee2)\r\nU 2 (#caa173)\r\nL 1 (#1b58a2)\r\nU 2 (#caa171)\r\nR 2 (#7807d2)\r\nU 3 (#a77fa3)\r\nL 2 (#015232)\r\nU 2 (#7a21e3)", "62", DisplayName = "Day 18 Part 1 Example")]
    public void Tests(int day, int part, string input, string expectedResult)
    {
        ISolution solution = SolutionFactory.GetSolution(2023, day, part);
        string result = solution.Solve(input.Split(Environment.NewLine));
        Assert.AreEqual(expectedResult, result);
    }
}