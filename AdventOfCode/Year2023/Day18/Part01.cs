namespace AdventOfCode.Year2023.Day18;

using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part01 : ISolution
{
    public string Solve(string[] input)
    {
        IEnumerable<Instruction> instructions = input.Select(x => new Instruction(x));

        // Today's challenge requires two algorithms.
        // Firstly, we use the Shoelace formula to determine the area enclosed by the hole we're digging.
        // Then, we use Pick's Theorem to determine the number of interior points
        // In order to execute the shoelace formula, we need to determine the vertices of the polygon formed by the hole we're digging.
        // We also need to keep track of the boundary length at the same time.
        (int X, int Y) currentLocation = (0, 0);
        List<(int X, int Y)> vertices = [currentLocation];
        int boundaryLength = 0;

        foreach (Instruction instruction in instructions)
        {
            currentLocation = instruction.Direction.GetNextLocation(currentLocation, instruction.Distance);
            vertices.Add(currentLocation);
            boundaryLength += instruction.Distance;
        }

        int area = ShoelaceFormula.ShoelaceArea([.. vertices]);
        int interiorPointCount = PicksTheorem.CalculateInteriorPointCount(area, boundaryLength);

        int totalPoints = interiorPointCount + boundaryLength;

        return totalPoints.ToString();
    }

    public readonly struct Instruction
    {
        public Instruction(string input)
        {
            string[] components = input.Split(' ');
            this.Direction = Direction2D.UpDownLeftRightToDirectionMap[components[0][0]];
            this.Distance = int.Parse(components[1]);
            this.Colour = components[2];
        }

        public Direction2D Direction { get; }

        public int Distance { get; }

        public string Colour { get; }
    }
}
