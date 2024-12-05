namespace AdventOfCode.Year2022.Day22;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CubeMapTests
{
    ////  0123456789012345
    //// 0        ...#     0
    //// 1        .#..     1
    //// 2        #...     2
    //// 3        ....     3
    //// 4...#.......#     4
    //// 5........#..A     5
    //// 6..#....#....     6
    //// 7.D........#.     7
    //// 8        ...#..B. 8
    //// 9        .....#.. 9
    ////10        .#......10
    ////11        ..C...#.11
    ////  0123456789012345

    private static readonly string[] TestMapData = "        ...#\r\n        .#..\r\n        #...\r\n        ....\r\n...#.......#\r\n........#...\r\n..#....#....\r\n..........#.\r\n        ...#....\r\n        .....#..\r\n        .#......\r\n        ......#.".Split(Environment.NewLine);

    [TestMethod]
    public void BuildMap()
    {
        var map = new CubeMap(TestMapData);

        // We know how this should translate into a cube.
        Assert.AreEqual(map.CubeFaces[5], map.CubeFaces[0].ConnectedFaces[0]);
        Assert.AreEqual(map.CubeFaces[3], map.CubeFaces[0].ConnectedFaces[1]);
        Assert.AreEqual(map.CubeFaces[2], map.CubeFaces[0].ConnectedFaces[2]);
        Assert.AreEqual(map.CubeFaces[1], map.CubeFaces[0].ConnectedFaces[3]);

        Assert.AreEqual(map.CubeFaces[2], map.CubeFaces[1].ConnectedFaces[0]);
        Assert.AreEqual(map.CubeFaces[4], map.CubeFaces[1].ConnectedFaces[1]);
        Assert.AreEqual(map.CubeFaces[5], map.CubeFaces[1].ConnectedFaces[2]);
        Assert.AreEqual(map.CubeFaces[0], map.CubeFaces[1].ConnectedFaces[3]);

        Assert.AreEqual(map.CubeFaces[3], map.CubeFaces[2].ConnectedFaces[0]);
        Assert.AreEqual(map.CubeFaces[4], map.CubeFaces[2].ConnectedFaces[1]);
        Assert.AreEqual(map.CubeFaces[1], map.CubeFaces[2].ConnectedFaces[2]);
        Assert.AreEqual(map.CubeFaces[0], map.CubeFaces[2].ConnectedFaces[3]);

        Assert.AreEqual(map.CubeFaces[5], map.CubeFaces[3].ConnectedFaces[0]);
        Assert.AreEqual(map.CubeFaces[4], map.CubeFaces[3].ConnectedFaces[1]);
        Assert.AreEqual(map.CubeFaces[2], map.CubeFaces[3].ConnectedFaces[2]);
        Assert.AreEqual(map.CubeFaces[0], map.CubeFaces[3].ConnectedFaces[3]);

        Assert.AreEqual(map.CubeFaces[5], map.CubeFaces[4].ConnectedFaces[0]);
        Assert.AreEqual(map.CubeFaces[1], map.CubeFaces[4].ConnectedFaces[1]);
        Assert.AreEqual(map.CubeFaces[2], map.CubeFaces[4].ConnectedFaces[2]);
        Assert.AreEqual(map.CubeFaces[3], map.CubeFaces[4].ConnectedFaces[3]);

        Assert.AreEqual(map.CubeFaces[0], map.CubeFaces[5].ConnectedFaces[0]);
        Assert.AreEqual(map.CubeFaces[1], map.CubeFaces[5].ConnectedFaces[1]);
        Assert.AreEqual(map.CubeFaces[4], map.CubeFaces[5].ConnectedFaces[2]);
        Assert.AreEqual(map.CubeFaces[3], map.CubeFaces[5].ConnectedFaces[3]);
    }

    [TestMethod]
    [DataRow(3, 11, 5, 0, 5, 14, 8, 1)]
    [DataRow(4, 10, 11, 1, 1, 1, 7, 3)]
    [DataRow(2, 6, 4, 3, 0, 8, 2, 0)]
    public void DirectionChanges(int originalCubeFaceIndex, int originalX, int originalY, int originalDirection, int expectedCubeFaceIndex, int expectedX, int expectedY, int expectedDirection)
    {
        var map = new CubeMap(TestMapData);

        CubeFaceDescriptor originalCubeFace = map.CubeFaces[originalCubeFaceIndex];
        CubeFaceDescriptor expectedCubeFace = map.CubeFaces[expectedCubeFaceIndex];
        (int originalX, int originalY) originalLocation = (originalX, originalY);
        (int expectedX, int expectedY) expectedLocation = (expectedX, expectedY);

        ((int X, int Y) actualLocation, CubeFaceDescriptor actualCubeFace, int actualDirection) = originalCubeFace.GetNextLocationFrom(originalLocation, originalDirection);

        Assert.AreEqual(expectedLocation, actualLocation);
        Assert.AreEqual(expectedCubeFace, actualCubeFace);
        Assert.AreEqual(expectedDirection, actualDirection);
    }
}
