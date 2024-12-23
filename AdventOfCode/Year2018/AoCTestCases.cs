namespace AdventOfCode.Year2018;

using System;
using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AoCTestCases
{
    [TestMethod]
    [DataRow(1, 1, "+1\r\n-2\r\n+3\r\n+1", "3")]
    [DataRow(1, 1, "+1\r\n+1\r\n+1", "3")]
    [DataRow(1, 1, "+1\r\n+1\r\n-2", "0")]
    [DataRow(1, 1, "-1\r\n-2\r\n-3", "-6")]
    [DataRow(1, 2, "+1\r\n-1", "0")]
    [DataRow(1, 2, "+3\r\n+3\r\n+4\r\n-2\r\n-4", "10")]
    [DataRow(1, 2, "-6\r\n+3\r\n+8\r\n+5\r\n-6", "5")]
    [DataRow(1, 2, "+7\r\n+7\r\n-2\r\n-7\r\n-4", "14")]
    [DataRow(2, 1, "abcdef\r\nbababc\r\nabbcde\r\nabcccd\r\naabcdd\r\nabcdee\r\nababab", "12")]
    [DataRow(2, 2, "abcde\r\nfghij\r\nklmno\r\npqrst\r\nfguij\r\naxcye\r\nwvxyz", "fgij")]
    [DataRow(3, 1, "#1 @ 1,3: 4x4\r\n#2 @ 3,1: 4x4\r\n#3 @ 5,5: 2x2", "4")]
    [DataRow(3, 2, "#1 @ 1,3: 4x4\r\n#2 @ 3,1: 4x4\r\n#3 @ 5,5: 2x2", "3")]
    [DataRow(4, 1, "[1518-11-01 00:00] Guard #10 begins shift\r\n[1518-11-01 00:05] falls asleep\r\n[1518-11-01 00:25] wakes up\r\n[1518-11-01 00:30] falls asleep\r\n[1518-11-01 00:55] wakes up\r\n[1518-11-01 23:58] Guard #99 begins shift\r\n[1518-11-02 00:40] falls asleep\r\n[1518-11-02 00:50] wakes up\r\n[1518-11-03 00:05] Guard #10 begins shift\r\n[1518-11-03 00:24] falls asleep\r\n[1518-11-03 00:29] wakes up\r\n[1518-11-04 00:02] Guard #99 begins shift\r\n[1518-11-04 00:36] falls asleep\r\n[1518-11-04 00:46] wakes up\r\n[1518-11-05 00:03] Guard #99 begins shift\r\n[1518-11-05 00:45] falls asleep\r\n[1518-11-05 00:55] wakes up", "240")]
    [DataRow(4, 2, "[1518-11-01 00:00] Guard #10 begins shift\r\n[1518-11-01 00:05] falls asleep\r\n[1518-11-01 00:25] wakes up\r\n[1518-11-01 00:30] falls asleep\r\n[1518-11-01 00:55] wakes up\r\n[1518-11-01 23:58] Guard #99 begins shift\r\n[1518-11-02 00:40] falls asleep\r\n[1518-11-02 00:50] wakes up\r\n[1518-11-03 00:05] Guard #10 begins shift\r\n[1518-11-03 00:24] falls asleep\r\n[1518-11-03 00:29] wakes up\r\n[1518-11-04 00:02] Guard #99 begins shift\r\n[1518-11-04 00:36] falls asleep\r\n[1518-11-04 00:46] wakes up\r\n[1518-11-05 00:03] Guard #99 begins shift\r\n[1518-11-05 00:45] falls asleep\r\n[1518-11-05 00:55] wakes up", "4455")]
    [DataRow(5, 1, "aA", "0")]
    [DataRow(5, 1, "abBA", "0")]
    [DataRow(5, 1, "abAB", "4")]
    [DataRow(5, 1, "aabAAB", "6")]
    [DataRow(5, 1, "dabAcCaCBAcCcaDA", "10")]
    [DataRow(5, 2, "dabAcCaCBAcCcaDA", "4")]
    [DataRow(6, 1, "1, 1\r\n1, 6\r\n8, 3\r\n3, 4\r\n5, 5\r\n8, 9", "17")]
    [DataRow(7, 1, "Step C must be finished before step A can begin.\r\nStep C must be finished before step F can begin.\r\nStep A must be finished before step B can begin.\r\nStep A must be finished before step D can begin.\r\nStep B must be finished before step E can begin.\r\nStep D must be finished before step E can begin.\r\nStep F must be finished before step E can begin.", "CABDFE")]
    [DataRow(8, 1, "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2", "138")]
    [DataRow(8, 2, "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2", "66")]
    [DataRow(11, 1, "18", "33,45")]
    [DataRow(11, 1, "42", "21,61")]
    ////[DataRow(11, 2, "18", "90,269,16")]
    ////[DataRow(11, 2, "42", "232,251,12")]
    [DataRow(12, 1, "initial state: #..#.#..##......###...###\r\n\r\n...## => #\r\n..#.. => #\r\n.#... => #\r\n.#.#. => #\r\n.#.## => #\r\n.##.. => #\r\n.#### => #\r\n#.#.# => #\r\n#.### => #\r\n##.#. => #\r\n##.## => #\r\n###.. => #\r\n###.# => #\r\n####. => #", "325")]
    [DataRow(13, 1, "/->-\\        \r\n|   |  /----\\\r\n| /-+--+-\\  |\r\n| | |  | v  |\r\n\\-+-/  \\-+--/\r\n  \\------/   ", "7,3")]
    [DataRow(13, 2, "/>-<\\  \r\n|   |  \r\n| /<+-\\\r\n| | | v\r\n\\>+</ |\r\n  |   ^\r\n  \\<->/", "6,4")]
    [DataRow(14, 1, "9", "5158916779")]
    [DataRow(14, 1, "5", "0124515891")]
    [DataRow(14, 1, "18", "9251071085")]
    [DataRow(14, 1, "2018", "5941429882")]
    [DataRow(14, 2, "51589", "9")]
    [DataRow(14, 2, "01245", "5")]
    [DataRow(14, 2, "92510", "18")]
    [DataRow(14, 2, "59414", "2018")]
    [DataRow(15, 1, "#######\r\n#G..#E#\r\n#E#E.E#\r\n#G.##.#\r\n#...#E#\r\n#...E.#\r\n#######", "36334")]
    [DataRow(15, 1, "#######\r\n#E..EG#\r\n#.#G.E#\r\n#E.##E#\r\n#G..#.#\r\n#..E#.#\r\n#######", "39514")]
    [DataRow(15, 1, "#######\r\n#E.G#.#\r\n#.#G..#\r\n#G.#.G#\r\n#G..#.#\r\n#...E.#\r\n#######", "27755")]
    [DataRow(15, 1, "#######\r\n#.E...#\r\n#.#..G#\r\n#.###.#\r\n#E#G#G#\r\n#...#G#\r\n#######", "28944")]
    [DataRow(15, 1, "#########\r\n#G......#\r\n#.E.#...#\r\n#..##..G#\r\n#...##..#\r\n#...#...#\r\n#.G...G.#\r\n#.....G.#\r\n#########", "18740")]
    [DataRow(16, 1, "Before: [3, 2, 1, 1]\r\n9 2 1 2\r\nAfter:  [3, 2, 2, 1]", "1")]
    [DataRow(17, 1, "x=495, y=2..7\r\ny=7, x=495..501\r\nx=501, y=3..7\r\nx=498, y=2..4\r\nx=506, y=1..2\r\nx=498, y=10..13\r\nx=504, y=10..13\r\ny=13, x=498..504", "57")]
    [DataRow(17, 2, "x=495, y=2..7\r\ny=7, x=495..501\r\nx=501, y=3..7\r\nx=498, y=2..4\r\nx=506, y=1..2\r\nx=498, y=10..13\r\nx=504, y=10..13\r\ny=13, x=498..504", "29")]
    [DataRow(18, 1, ".#.#...|#.\r\n.....#|##|\r\n.|..|...#.\r\n..|#.....#\r\n#.#|||#|#|\r\n...#.||...\r\n.|....|...\r\n||...#|.#|\r\n|.||||..|.\r\n...#.|..|.", "1147")]
    [DataRow(19, 1, "#ip 0\r\nseti 5 0 1\r\nseti 6 0 2\r\naddi 0 1 0\r\naddr 1 2 3\r\nsetr 1 0 0\r\nseti 8 0 4\r\nseti 9 0 5", "6")]
    [DataRow(20, 1, "^WNE$", "3")]
    [DataRow(20, 1, "^ENWWW(NEEE|SSE(EE|N))$", "10")]
    [DataRow(20, 1, "^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$", "18")]
    [DataRow(20, 1, "^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$", "23")]
    [DataRow(20, 1, "^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$", "31")]
    [DataRow(22, 1, "depth: 510\r\ntarget: 10,10", "114")]
    [DataRow(22, 2, "depth: 510\r\ntarget: 10,10", "45")]
    [DataRow(23, 1, "pos=<0,0,0>, r=4\r\npos=<1,0,0>, r=1\r\npos=<4,0,0>, r=3\r\npos=<0,2,0>, r=1\r\npos=<0,5,0>, r=3\r\npos=<0,0,3>, r=1\r\npos=<1,1,1>, r=1\r\npos=<1,1,2>, r=1\r\npos=<1,3,1>, r=1", "7")]
    [DataRow(23, 2, "pos=<10,12,12>, r=2\r\npos=<12,14,12>, r=2\r\npos=<16,12,12>, r=4\r\npos=<14,14,14>, r=6\r\npos=<50,50,50>, r=200\r\npos=<10,10,10>, r=5", "36")]
    [DataRow(25, 1, "-1,2,2,0\r\n0,0,2,-2\r\n0,0,0,-2\r\n-1,2,0,0\r\n-2,-2,-2,2\r\n3,0,2,-1\r\n-1,3,2,2\r\n-1,0,-1,0\r\n0,2,1,-2\r\n3,0,0,0", "4")]
    [DataRow(25, 1, "1,-1,0,1\r\n2,0,-1,0\r\n3,2,-1,0\r\n0,0,3,1\r\n0,0,-1,-1\r\n2,3,-2,0\r\n-2,2,0,0\r\n2,-2,0,-1\r\n1,-1,0,-1\r\n3,2,0,2", "3")]
    [DataRow(25, 1, "1,-1,-1,-2\r\n-2,-2,0,1\r\n0,2,1,3\r\n-2,3,-2,1\r\n0,2,3,-2\r\n-1,-1,1,-2\r\n0,-2,-1,0\r\n-2,2,3,-1\r\n1,2,2,0\r\n-1,-2,0,-2", "8")]
    [DataRow(24, 1, "Immune System:\r\n17 units each with 5390 hit points (weak to radiation, bludgeoning) with an attack that does 4507 fire damage at initiative 2\r\n989 units each with 1274 hit points (immune to fire; weak to bludgeoning, slashing) with an attack that does 25 slashing damage at initiative 3\r\n\r\nInfection:\r\n801 units each with 4706 hit points (weak to radiation) with an attack that does 116 bludgeoning damage at initiative 1\r\n4485 units each with 2961 hit points (immune to radiation; weak to fire, cold) with an attack that does 12 slashing damage at initiative 4", "5216")]
    [DataRow(24, 2, "Immune System:\r\n17 units each with 5390 hit points (weak to radiation, bludgeoning) with an attack that does 4507 fire damage at initiative 2\r\n989 units each with 1274 hit points (immune to fire; weak to bludgeoning, slashing) with an attack that does 25 slashing damage at initiative 3\r\n\r\nInfection:\r\n801 units each with 4706 hit points (weak to radiation) with an attack that does 116 bludgeoning damage at initiative 1\r\n4485 units each with 2961 hit points (immune to radiation; weak to fire, cold) with an attack that does 12 slashing damage at initiative 4", "51")]
    public void Tests(int day, int part, string input, string expectedResult)
    {
        ISolution solution = SolutionFactory.GetSolution(2018, day, part);
        string result = solution.Solve(input.Split(Environment.NewLine));
        Assert.AreEqual(result, expectedResult);
    }

    [TestMethod]
    public void Day06Part02()
    {
        var solution = new Year2018.Day06.Part02();
        string result = solution.Solve("1, 1\r\n1, 6\r\n8, 3\r\n3, 4\r\n5, 5\r\n8, 9".Split(Environment.NewLine), 32);
        Assert.AreEqual(result, "16");
    }

    [TestMethod]
    public void Day07Part02()
    {
        var solution = new AdventOfCode.Year2018.Day07.Part02();
        string result = solution.Solve("Step C must be finished before step A can begin.\r\nStep C must be finished before step F can begin.\r\nStep A must be finished before step B can begin.\r\nStep A must be finished before step D can begin.\r\nStep B must be finished before step E can begin.\r\nStep D must be finished before step E can begin.\r\nStep F must be finished before step E can begin.".Split(Environment.NewLine), 2, 0);
        Assert.AreEqual(result, "15");
    }

    [TestMethod]
    [DataRow(9, 25, 32)]
    [DataRow(10, 1618, 8317)]
    [DataRow(13, 7999, 146373)]
    [DataRow(17, 1104, 2764)]
    [DataRow(21, 6111, 54718)]
    [DataRow(30, 5807, 37305)]
    public void Day09Part01(int players, int lastMarble, long expectedHighScore)
    {
        var solution = new AdventOfCode.Year2018.Day09.Part01();
        long result = solution.Solve(players, lastMarble);
        Assert.AreEqual(result, expectedHighScore);
    }
}