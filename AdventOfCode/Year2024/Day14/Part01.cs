namespace AdventOfCode.Year2024.Day14;

using System.Linq;
using AdventOfCode;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        int width = input[0] == "TEST" ? 11 : 101;
        int height = input[0] == "TEST" ? 7 : 103;

        if (input[0] == "TEST")
        {
            input = input[1..];
        }

        Robot[] robots = input.Select(x => new Robot(x)).ToArray();

        (int X, int Y)[] finalPositions = robots.Select(x => x.GetPosition(100, width, height)).ToArray();

        int halfWidth = width / 2;
        int halfHeight = height / 2;

        int upperLeftCount = finalPositions.Count(x => x.X < halfWidth && x.Y < halfHeight);
        int upperRightCount = finalPositions.Count(x => x.X > halfWidth && x.Y < halfHeight);
        int lowerLeftCount = finalPositions.Count(x => x.X < halfWidth && x.Y > halfHeight);
        int lowerRightCount = finalPositions.Count(x => x.X > halfWidth && x.Y > halfHeight);

        return (upperLeftCount * upperRightCount * lowerLeftCount * lowerRightCount).ToString();
    }
}
