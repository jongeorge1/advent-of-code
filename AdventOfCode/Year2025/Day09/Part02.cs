namespace AdventOfCode.Year2025.Day09;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

public class Part02 : ISolution
{
    public string Solve(string[] input)
    {
        (long X, long Y)[] coordinates = ParseInput(input);

        CoordinateCompressor.CompressedCoordinatesResult compressedCoordinates = CoordinateCompressor.CompressCoordinates(coordinates);
        (int X, int Y)[] outlineMap = BuildOutlineMap(compressedCoordinates.CompressedCoordinates);

        (int X, int Y)[] outsidePoints = FindAllOutsidePoints(outlineMap, compressedCoordinates.XMapping.Count, compressedCoordinates.YMapping.Count);

        // Now build the set of possible valid rectangles in the compressed coordinate space
        List<((int X, int Y) TopLeft, (int X, int Y) BottomRight)> validRectangles = FindValidRectangles(compressedCoordinates, outsidePoints);

        long largestArea = 0;
        foreach (((int X, int Y) TopLeft, (int X, int Y) BottomRight) rectangle in validRectangles)
        {
            long area = GetUncompressedSize(rectangle, compressedCoordinates);

            if (area > largestArea)
            {
                largestArea = area;
            }
        }

        return largestArea.ToString();
    }

    private static long GetUncompressedSize(((int X, int Y) TopLeft, (int X, int Y) BottomRight) rectangle, CoordinateCompressor.CompressedCoordinatesResult compressedCoordinates)
    {
        long width = compressedCoordinates.XMapping.First(kvp => kvp.Value == rectangle.BottomRight.X).Key -
                     compressedCoordinates.XMapping.First(kvp => kvp.Value == rectangle.TopLeft.X).Key + 1;
        long height = compressedCoordinates.YMapping.First(kvp => kvp.Value == rectangle.BottomRight.Y).Key -
                      compressedCoordinates.YMapping.First(kvp => kvp.Value == rectangle.TopLeft.Y).Key + 1;

        return width * height;
    }

    private static List<((int X, int Y) TopLeft, (int X, int Y) BottomRight)> FindValidRectangles(CoordinateCompressor.CompressedCoordinatesResult compressedCoordinates, (int X, int Y)[] outsidePoints)
    {
        List<((int X, int Y) TopLeft, (int X, int Y) BottomRight)> validRectangles = [];

        for (int firstIndex = 0; firstIndex < compressedCoordinates.CompressedCoordinates.Length; firstIndex++)
        {
            for (int secondIndex = firstIndex + 1; secondIndex < compressedCoordinates.CompressedCoordinates.Length; secondIndex++)
            {
                if (secondIndex <= firstIndex)
                {
                    continue;
                }

                (int X, int Y) first = compressedCoordinates.CompressedCoordinates[firstIndex];
                (int X, int Y) second = compressedCoordinates.CompressedCoordinates[secondIndex];

                (int X, int Y) topLeft = (Math.Min(first.X, second.X), Math.Min(first.Y, second.Y));
                (int X, int Y) bottomRight = (Math.Max(first.X, second.X), Math.Max(first.Y, second.Y));

                // Now see if there are any outside points within this rectangle.
                bool hasOutsidePointInside = false;
                foreach ((int X, int Y) outsidePoint in outsidePoints)
                {
                    if (outsidePoint.X >= topLeft.X && outsidePoint.X <= bottomRight.X &&
                        outsidePoint.Y >= topLeft.Y && outsidePoint.Y <= bottomRight.Y)
                    {
                        hasOutsidePointInside = true;
                        break;
                    }
                }

                if (!hasOutsidePointInside)
                {
                    validRectangles.Add((topLeft, bottomRight));
                }
            }
        }

        return validRectangles;
    }

    private static (long X, long Y)[] ParseInput(string[] input)
    {
        return [.. input.Select(row =>
        {
            long[] parts = [.. row.Split(',').Select(long.Parse)];
            return (parts[0], parts[1]);
        })];
    }

    private static (int X, int Y)[] BuildOutlineMap((int X, int Y)[] coordinates)
    {
        // Now build the outline map
        List<(int X, int Y)> outline = [.. coordinates];
        (int X, int Y)[] pointsToJoin = [.. coordinates, coordinates[0]];

        for (int i = 1; i < pointsToJoin.Length; i++)
        {
            (int X, int Y) first = pointsToJoin [i - 1];
            (int X, int Y) second = pointsToJoin [i];

            if (first.X == second.X)
            {
                int startY = Math.Min(first.Y, second.Y);
                int endY = Math.Max(first.Y, second.Y);
                for (int y = startY; y <= endY; y++)
                {
                    outline.Add((first.X, y));
                }
            }
            else if (first.Y == second.Y)
            {
                int startX = Math.Min(first.X, second.X);
                int endX = Math.Max(first.X, second.X);
                for (int x = startX; x <= endX; x++)
                {
                    outline.Add((x, first.Y));
                }
            }
            else
            {
                Debug.Fail("Unexpected diagonal line");
            }
        }

        return [..outline];
    }

    private static (int X, int Y)[] FindAllOutsidePoints((int X, int Y)[] outlineMap, int maxX, int maxY)
    {
        HashSet<(int X, int Y)> outsidePoints = new();
        Queue<(int X, int Y)> toExplore = new();

        // Start from the edges
        for (int x = 0; x < maxX; x++)
        {
            toExplore.Enqueue((x, 0));
            toExplore.Enqueue((x, maxY - 1));
        }

        for (int y = 0; y < maxY; y++)
        {
            toExplore.Enqueue((0, y));
            toExplore.Enqueue((maxX - 1, y));
        }

        HashSet<(int X, int Y)> outlineSet = [.. outlineMap];

        while (toExplore.Count > 0)
        {
            (int X, int Y) point = toExplore.Dequeue();
            if (outsidePoints.Contains(point) || outlineSet.Contains(point))
            {
                continue;
            }

            outsidePoints.Add(point);

            // Explore neighbors
            (int X, int Y)[] neighbors = [
                (point.X + 1, point.Y),
                (point.X - 1, point.Y),
                (point.X, point.Y + 1),
                (point.X, point.Y - 1)
            ];
            foreach ((int X, int Y) neighbor in neighbors)
            {
                if (neighbor.X >= 0 && neighbor.X < maxX && neighbor.Y >= 0 && neighbor.Y < maxY)
                {
                    toExplore.Enqueue(neighbor);
                }
            }
        }

        return [.. outsidePoints];
    }
}
