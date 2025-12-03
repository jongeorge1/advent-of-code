namespace AdventOfCode.Helpers;

/// <summary>
/// Methods implementing Pick's theorem.
/// </summary>
public static class PicksTheorem
{
    /// <summary>
    /// Calculates the number of interior points given the area and count boundary points.
    /// </summary>
    /// <param name="area">The area of the shape.</param>
    /// <param name="boundaryPoints">The number of points around the boundary.</param>
    /// <returns>The number of internal points.</returns>
    /// <remarks>The interior point count does not include the boundary points.</remarks>
    public static int CalculateInteriorPointCount(int area, int boundaryPoints)
    {
        return area - (boundaryPoints / 2) + 1;
    }

    /// <summary>
    /// Calculates the number of interior points given the area and count boundary points.
    /// </summary>
    /// <param name="area">The area of the shape.</param>
    /// <param name="boundaryPoints">The number of points around the boundary.</param>
    /// <returns>The number of internal points.</returns>
    /// <remarks>The interior point count does not include the boundary points.</remarks>
    public static long CalculateInteriorPointCount(long area, long boundaryPoints)
    {
        return area - (boundaryPoints / 2) + 1;
    }
}
