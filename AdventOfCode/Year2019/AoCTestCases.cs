namespace AdventOfCode.Year2019;

using System;
using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AoCTestCases
{
    [TestMethod]
    [DataRow(1, 1, "12", "2", DisplayName = "Day 01, Part 1 - Example 1")]
    [DataRow(1, 1, "14", "2", DisplayName = "Day 01, Part 1 - Example 2")]
    [DataRow(1, 1, "1969", "654", DisplayName = "Day 01, Part 1 - Example 3")]
    [DataRow(1, 1, "100756", "33583", DisplayName = "Day 01, Part 1 - Example 4")]
    [DataRow(1, 2, "14", "2", DisplayName = "Day 01, Part 2 - Example 1")]
    [DataRow(1, 2, "1969", "966", DisplayName = "Day 01, Part 2 - Example 2")]
    [DataRow(1, 2, "100756", "50346", DisplayName = "Day 01, Part 2 - Example 3")]
    [DataRow(2, 1, "1,9,10,3,2,3,11,0,99,30,40,50", "3500", DisplayName = "Day 02, Part 1 - Example 1")]
    [DataRow(2, 1, "1,0,0,0,99", "2", DisplayName = "Day 02, Part 1 - Example 2")]
    [DataRow(2, 1, "2,3,0,3,99", "2", DisplayName = "Day 02, Part 1 - Example 3")]
    [DataRow(2, 1, "2,4,4,5,99,0", "2", DisplayName = "Day 02, Part 1 - Example 4")]
    [DataRow(2, 1, "1,1,1,4,99,5,6,0,99", "30", DisplayName = "Day 02, Part 1 - Example 5")]
    [DataRow(3, 1, "R75,D30,R83,U83,L12,D49,R71,U7,L72\r\nU62,R66,U55,R34,D71,R55,D58,R83", "159", DisplayName = "Day 03, Part 1 - Example 1")]
    [DataRow(3, 1, "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\r\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", "135", DisplayName = "Day 03, Part 1 - Example 2")]
    [DataRow(3, 2, "R75,D30,R83,U83,L12,D49,R71,U7,L72\r\nU62,R66,U55,R34,D71,R55,D58,R83", "610", DisplayName = "Day 03, Part 2 - Example 1")]
    [DataRow(3, 2, "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\r\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", "410", DisplayName = "Day 03, Part 2 - Example 2")]
    [DataRow(4, 1, "111111-111111", "1", DisplayName = "Day 04, Part 1 - Example 1")]
    [DataRow(4, 1, "223450-223450", "0", DisplayName = "Day 04, Part 1 - Example 2")]
    [DataRow(4, 1, "123789-123789", "0", DisplayName = "Day 04, Part 1 - Example 3")]
    [DataRow(4, 2, "112233-112233", "1", DisplayName = "Day 04, Part 2 - Example 1")]
    [DataRow(4, 2, "123444-123444", "0", DisplayName = "Day 04, Part 2 - Example 2")]
    [DataRow(4, 2, "111122-111122", "1", DisplayName = "Day 04, Part 2 - Example 3")]
    [DataRow(6, 1, "COM)B\r\nB)C\r\nC)D\r\nD)E\r\nE)F\r\nB)G\r\nG)H\r\nD)I\r\nE)J\r\nJ)K\r\nK)L", "42", DisplayName = "Day 06, Part 1 - Example 1")]
    [DataRow(6, 2, "COM)B\r\nB)C\r\nC)D\r\nD)E\r\nE)F\r\nB)G\r\nG)H\r\nD)I\r\nE)J\r\nJ)K\r\nK)L\r\nK)YOU\r\nI)SAN", "4", DisplayName = "Day 06, Part 2 - Example 1")]
    [DataRow(7, 1, "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", "43210", DisplayName = "Day 07, Part 1 - Example 1")]
    [DataRow(7, 1, "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", "54321", DisplayName = "Day 07, Part 1 - Example 2")]
    [DataRow(7, 1, "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", "65210", DisplayName = "Day 07, Part 1 - Example 3")]
    [DataRow(7, 2, "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5", "139629729", DisplayName = "Day 07, Part 2 - Example 1")]
    [DataRow(7, 2, "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10", "18216", DisplayName = "Day 07, Part 2 - Example 2")]
    [DataRow(8, 1, "123456789012", "1", DisplayName = "Day 08, Part 1 - Example")]
    [DataRow(8, 2, "0222112222120000", " X\r\nX ", DisplayName = "Day 08, Part 2 - Example")]
    [DataRow(9, 1, "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99", "99", DisplayName = "Day 09, Part 1 - Example 1")]
    [DataRow(9, 1, "1102,34915192,34915192,7,4,7,99,0", "1219070632396864", DisplayName = "Day 09, Part 1 - Example 2")]
    [DataRow(9, 1, "104,1125899906842624,99", "1125899906842624", DisplayName = "Day 09, Part 1 - Example 3")]
    [DataRow(10, 1, ".#..#\r\n.....\r\n#####\r\n....#\r\n...##", "8", DisplayName = "Day 10, Part 1 - Example 1")]
    [DataRow(10, 1, "......#.#.\r\n#..#.#....\r\n..#######.\r\n.#.#.###..\r\n.#..#.....\r\n..#....#.#\r\n#..#....#.\r\n.##.#..###\r\n##...#..#.\r\n.#....####", "33", DisplayName = "Day 10, Part 1 - Example 2")]
    [DataRow(10, 1, "#.#...#.#.\r\n.###....#.\r\n.#....#...\r\n##.#.#.#.#\r\n....#.#.#.\r\n.##..###.#\r\n..#...##..\r\n..##....##\r\n......#...\r\n.####.###.", "35", DisplayName = "Day 10, Part 1 - Example 3")]
    [DataRow(10, 1, ".#..#..###\r\n####.###.#\r\n....###.#.\r\n..###.##.#\r\n##.##.#.#.\r\n....###..#\r\n..#.#..#.#\r\n#..#.#.###\r\n.##...##.#\r\n.....#.#..", "41", DisplayName = "Day 10, Part 1 - Example 4")]
    [DataRow(10, 1, ".#..##.###...#######\r\n##.############..##.\r\n.#.######.########.#\r\n.###.#######.####.#.\r\n#####.##.#.##.###.##\r\n..#####..#.#########\r\n####################\r\n#.####....###.#.#.##\r\n##.#################\r\n#####.##.###..####..\r\n..######..##.#######\r\n####.##.####...##..#\r\n.#####..#.######.###\r\n##...#.##########...\r\n#.##########.#######\r\n.####.#.###.###.#.##\r\n....##.##.###..#####\r\n.#.#.###########.###\r\n#.#.#.#####.####.###\r\n###.##.####.##.#..##", "210", DisplayName = "Day 10, Part 1 - Example 5")]
    [DataRow(10, 2, ".#..##.###...#######\r\n##.############..##.\r\n.#.######.########.#\r\n.###.#######.####.#.\r\n#####.##.#.##.###.##\r\n..#####..#.#########\r\n####################\r\n#.####....###.#.#.##\r\n##.#################\r\n#####.##.###..####..\r\n..######..##.#######\r\n####.##.####...##..#\r\n.#####..#.######.###\r\n##...#.##########...\r\n#.##########.#######\r\n.####.#.###.###.#.##\r\n....##.##.###..#####\r\n.#.#.###########.###\r\n#.#.#.#####.####.###\r\n###.##.####.##.#..##", "802", DisplayName = "Day 10, Part 2 - Example 1")]
    [DataRow(12, 1, "<x=-1, y=0, z=2>\r\n<x=2, y=-10, z=-7>\r\n<x=4, y=-8, z=8>\r\n<x=3, y=5, z=-1>", "183", DisplayName = "Day 12, Part 1 - Example")]
    [DataRow(12, 2, "<x=-1, y=0, z=2>\r\n<x=2, y=-10, z=-7>\r\n<x=4, y=-8, z=8>\r\n<x=3, y=5, z=-1>", "2772", DisplayName = "Day 12, Part 2 - Example 1")]
    [DataRow(12, 2, "<x=-8, y=-10, z=0>\r\n<x=5, y=5, z=10>\r\n<x=2, y=-7, z=3>\r\n< x=9, y=-8, z=-3>", "4686774924", DisplayName = "Day 12, Part 2 - Example 2")]
    public void Tests(int day, int part, string input, string expectedResult)
    {
        ISolution solution = SolutionFactory.GetSolution(2019, day, part);
        string result = solution.Solve(input.Split(Environment.NewLine));
        Assert.AreEqual(expectedResult, result);
    }
}