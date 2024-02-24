namespace AdventOfCode.Year2023
{
    using System;
    using AdventOfCode;
    using NUnit.Framework;

    public class AoCTestCases
    {
        [TestCase(1, 1, "1abc2\r\npqr3stu8vwx\r\na1b2c3d4e5f\r\ntreb7uchet", "142")]
        [TestCase(1, 2, "two1nine\r\neightwothree\r\nabcone2threexyz\r\nxtwone3four\r\n4nineeightseven2\r\nzoneight234\r\n7pqrstsixteen", "281")]
        [TestCase(1, 2, "two1", "21")]
        [TestCase(1, 2, "1two", "12")]
        [TestCase(1, 2, "nine", "99")]
        [TestCase(2, 1, "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\r\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\r\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\r\nGame 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red\r\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", "8")]
        [TestCase(2, 2, "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green\r\nGame 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue\r\nGame 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red\r\nGame 4: 1 green, 3 red, 6 blue; 3 green,1 6 red; 3 green, 15 blue, 14 red\r\nGame 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", "2286")]
        [TestCase(3, 1, "467..114..\r\n...*......\r\n..35..633.\r\n......#...\r\n617*......\r\n.....+.58.\r\n..592.....\r\n......755.\r\n...$.*....\r\n.664.598..", "4361")]
        [TestCase(3, 2, "467..114..\r\n...*......\r\n..35..633.\r\n......#...\r\n617*......\r\n.....+.58.\r\n..592.....\r\n......755.\r\n...$.*....\r\n.664.598..", "467835")]
        [TestCase(4, 1, "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\nCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\nCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\nCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\nCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\nCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11", "13")]
        [TestCase(4, 2, "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53\r\nCard 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19\r\nCard 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1\r\nCard 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83\r\nCard 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36\r\nCard 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11", "30")]
        [TestCase(5, 1, "seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4", "35")]
        [TestCase(5, 2, "seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4", "46")]
        [TestCase(6, 1, "Time:      7  15   30\r\nDistance:  9  40  200", "288")]
        [TestCase(6, 2, "Time:      7  15   30\r\nDistance:  9  40  200", "71503")]
        [TestCase(7, 1, "32T3K 765\r\nT55J5 684\r\nKK677 28\r\nKTJJT 220\r\nQQQJA 483", "6440")]
        [TestCase(7, 2, "32T3K 765\r\nT55J5 684\r\nKK677 28\r\nKTJJT 220\r\nQQQJA 483", "5905")]
        [TestCase(8, 1, "RL\r\n\r\nAAA = (BBB, CCC)\r\nBBB = (DDD, EEE)\r\nCCC = (ZZZ, GGG)\r\nDDD = (DDD, DDD)\r\nEEE = (EEE, EEE)\r\nGGG = (GGG, GGG)\r\nZZZ = (ZZZ, ZZZ)", "2")]
        [TestCase(8, 1, "LLR\r\n\r\nAAA = (BBB, BBB)\r\nBBB = (AAA, ZZZ)\r\nZZZ = (ZZZ, ZZZ)", "6")]
        [TestCase(8, 2, "LR\r\n\r\n11A = (11B, XXX)\r\n11B = (XXX, 11Z)\r\n11Z = (11B, XXX)\r\n22A = (22B, XXX)\r\n22B = (22C, 22C)\r\n22C = (22Z, 22Z)\r\n22Z = (22B, 22B)\r\nXXX = (XXX, XXX)", "6")]
        [TestCase(9, 1, "0 3 6 9 12 15\r\n1 3 6 10 15 21\r\n10 13 16 21 30 45", "114")]
        [TestCase(9, 2, "0 3 6 9 12 15\r\n1 3 6 10 15 21\r\n10 13 16 21 30 45", "2")]
        [TestCase(10, 1, "-L|F7\r\n7S-7|\r\nL|7||\r\n-L-J|\r\nL|-JF", "4")]
        [TestCase(10, 1, "..F7.\r\n.FJ|.\r\nSJ.L7\r\n|F--J\r\nLJ...", "8")]
        [TestCase(10, 2, "...........\r\n.S-------7.\r\n.|F-----7|.\r\n.||.....||.\r\n.||.....||.\r\n.|L-7.F-J|.\r\n.|..|.|..|.\r\n.L--J.L--J.\r\n...........", "4")]
        [TestCase(10, 2, ".F----7F7F7F7F-7....\r\n.|F--7||||||||FJ....\r\n.||.FJ||||||||L7....\r\nFJL7L7LJLJ||LJ.L-7..\r\nL--J.L7...LJS7F-7L7.\r\n....F-J..F7FJ|L7L7L7\r\n....L7.F7||L7|.L7L7|\r\n.....|FJLJ|FJ|F7|.LJ\r\n....FJL-7.||.||||...\r\n....L---J.LJ.LJLJ...", "8")]
        [TestCase(10, 2, "FF7FSF7F7F7F7F7F---7\r\nL|LJ||||||||||||F--J\r\nFL-7LJLJ||||||LJL-77\r\nF--JF--7||LJLJ7F7FJ-\r\nL---JF-JLJ.||-FJLJJ7\r\n|F|F-JF---7F7-L7L|7|\r\n|FFJF7L7F-JF7|JL---7\r\n7-L-JL7||F7|L7F-7F7|\r\nL.L7LFJ|||||FJL7||LJ\r\nL7JLJL-JLJLJL--JLJ.L", "10")]
        [TestCase(11, 1, "...#......\r\n.......#..\r\n#.........\r\n..........\r\n......#...\r\n.#........\r\n.........#\r\n..........\r\n.......#..\r\n#...#.....", "374")]
        [TestCase(12, 1, "???.### 1,1,3\r\n.??..??...?##. 1,1,3\r\n?#?#?#?#?#?#?#? 1,3,1,6\r\n????.#...#... 4,1,1\r\n????.######..#####. 1,6,5\r\n?###???????? 3,2,1", "21")]
        [TestCase(12, 1, "???.### 1,1,3", "1")]
        [TestCase(12, 1, ".??..??...?##. 1,1,3", "4")]
        [TestCase(12, 1, "?#?#?#?#?#?#?#? 1,3,1,6", "1")]
        [TestCase(12, 1, "????.#...#... 4,1,1", "1")]
        [TestCase(12, 1, "????.######..#####. 1,6,5", "4")]
        [TestCase(12, 1, "?###???????? 3,2,1", "10")]
        [TestCase(12, 2, "???.### 1,1,3\r\n.??..??...?##. 1,1,3\r\n?#?#?#?#?#?#?#? 1,3,1,6\r\n????.#...#... 4,1,1\r\n????.######..#####. 1,6,5\r\n?###???????? 3,2,1", "525152")]
        [TestCase(13, 1, "#.##..##.\r\n..#.##.#.\r\n##......#\r\n##......#\r\n..#.##.#.\r\n..##..##.\r\n#.#.##.#.\r\n\r\n#...##..#\r\n#....#..#\r\n..##..###\r\n#####.##.\r\n#####.##.\r\n..##..###\r\n#....#..#", "405")]
        [TestCase(13, 2, "#.##..##.\r\n..#.##.#.\r\n##......#\r\n##......#\r\n..#.##.#.\r\n..##..##.\r\n#.#.##.#.\r\n\r\n#...##..#\r\n#....#..#\r\n..##..###\r\n#####.##.\r\n#####.##.\r\n..##..###\r\n#....#..#", "400")]
        [TestCase(14, 1, "OOOO.#.O..\r\nOO..#....#\r\nOO..O##..O\r\nO..#.OO...\r\n........#.\r\n..#....#.#\r\n..O..#.O.O\r\n..O.......\r\n#....###..\r\n#....#....", "136")]
        [TestCase(14, 2, "OOOO.#.O..\r\nOO..#....#\r\nOO..O##..O\r\nO..#.OO...\r\n........#.\r\n..#....#.#\r\n..O..#.O.O\r\n..O.......\r\n#....###..\r\n#....#....", "64")]
        public void Tests(int day, int part, string input, string expectedResult)
        {
            ISolution solution = SolutionFactory.GetSolution(2023, day, part);
            string result = solution.Solve(input.Split(Environment.NewLine));
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
