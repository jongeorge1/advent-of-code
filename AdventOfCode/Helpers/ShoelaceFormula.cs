namespace AdventOfCode.Helpers;

using System;

public static class ShoelaceFormula
{
    /// <summary>
    /// Given a set of vertices of a polygon, uses the Shoelace formula to calculate the area.
    /// </summary>
    /// <param name="vertices">The list of vertices.</param>
    /// <returns>The area of the polygon</returns>
    /// <remarks>Assumes the polygon is closed - i.e. it does not include the last/first combination of vertices in the calculation.</remarks>
    public static int ShoelaceArea((int X, int Y)[] vertices)
    {
        int area = 0;
        int n = vertices.Length;
        for (int i = 0; i < (n - 1); i++)
        {
            (int X, int Y) current = vertices[i];
            (int X, int Y) next = vertices[i + 1];
            area += current.X * next.Y;
            area -= current.Y * next.X;
        }

        return Math.Abs(area) / 2;
    }

    /// <summary>
    /// Given a set of vertices of a polygon, uses the Shoelace formula to calculate the area.
    /// </summary>
    /// <param name="vertices">The list of vertices.</param>
    /// <returns>The area of the polygon</returns>
    /// <remarks>Assumes the polygon is closed - i.e. it does not include the last/first combination of vertices in the calculation.</remarks>
    public static long ShoelaceArea((long X, long Y)[] vertices)
    {
        long area = 0;
        long n = vertices.Length;
        for (long i = 0; i < (n - 1); i++)
        {
            (long X, long Y) current = vertices[i];
            (long X, long Y) next = vertices[i + 1];
            area += current.X * next.Y;
            area -= current.Y * next.X;
        }

        return Math.Abs(area) / 2;
    }
}
