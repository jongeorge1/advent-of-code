namespace AdventOfCode.Year2018.Day06;

using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PointExtensionTests
{
    private static readonly Point[] Points =
        [
            new Point { X = 1, Y = 1 },
            new Point { X = 1, Y = 6 },
            new Point { X = 8, Y = 3 },
            new Point { X = 3, Y = 4 },
            new Point { X = 5, Y = 5 },
            new Point { X = 8, Y = 9 },
        ];

    [TestMethod]
    public void FindClosestWithSingleMatch()
    {
        var testPoint = new Point { X = 2, Y = 2 };

        Point[] closest = testPoint.FindClosest(Points);

        Assert.AreEqual(1, closest.Length);
    }

    [TestMethod]
    public void FindClosestWithExactMatch()
    {
        var testPoint = new Point { X = 1, Y = 1 };

        Point[] closest = testPoint.FindClosest(Points);

        Assert.AreEqual(1, closest.Length);
    }

    [TestMethod]
    public void FindClosestWithMultipleMatches()
    {
        var testPoint = new Point { X = 1, Y = 4 };

        Point[] closest = testPoint.FindClosest(Points);

        Assert.AreEqual(2, closest.Length);
    }
}
