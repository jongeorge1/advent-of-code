namespace AdventOfCode.Year2024.Day11;

using System.Collections.Generic;
using System.Linq;
using AdventOfCode;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        LinkedList<long> stones = new(input[0].Split(' ').Select(long.Parse));
        var stoneBreaker = new StoneBreaker();
        return stones.Sum(x => stoneBreaker.BreakStone(x, 0, 75)).ToString();
    }
}
