namespace AdventOfCode.Year2023.Day18;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        IEnumerable<Instruction> instructions = input.Select(x => new Instruction(x));

        // Today's challenge requires two algorithms.
        // Firstly, we use the Shoelace formula to determine the area enclosed by the hole we're digging.
        // Then, we use Pick's Theorem to determine the number of interior points
        // In order to execute the shoelace formula, we need to determine the vertices of the polygon formed by the hole we're digging.
        // We also need to keep track of the boundary length at the same time.
        (long X, long Y) currentLocation = (0, 0);
        List<(long X, long Y)> vertices = [currentLocation];
        long boundaryLength = 0;

        foreach (Instruction instruction in instructions)
        {
            currentLocation = instruction.Direction.GetNextLocation(currentLocation, instruction.Distance);
            vertices.Add(currentLocation);
            boundaryLength += instruction.Distance;
        }

        long area = ShoelaceFormula.ShoelaceArea([.. vertices]);
        long interiorPointCount = PicksTheorem.CalculateInteriorPointCount(area, boundaryLength);

        long totalPoints = interiorPointCount + boundaryLength;

        return totalPoints.ToString();
    }

    public readonly struct Instruction
    {
        public Instruction(string input)
        {
            string[] components = input.Split(' ');
            string colour = components[2];

            this.Distance = Convert.ToInt64(colour[2..7], 16);
            this.Direction = colour[7] switch
            {
                '0' => Direction2D.East,
                '1' => Direction2D.South,
                '2' => Direction2D.West,
                '3' => Direction2D.North,
                _ => throw new NotImplementedException(),
            };
        }

        public Direction2D Direction { get; }

        public long Distance { get; }
    }
}
