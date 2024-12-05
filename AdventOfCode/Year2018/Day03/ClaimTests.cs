namespace AdventOfCode.Year2018.Day03;

using Microsoft.VisualStudio.TestTools.UnitTesting;

public class ClaimTests
{
    [TestMethod]
    [DataRow("#1 @ 1,3: 4x4", 1, 1, 3, 4, 4, 4, 6)]
    public void CreationFromClaimString(string input, int expectedNumber, int expectedX, int expectedY, int expectedWidth, int expectedHeight, int expectedMaxX, int expectedMaxY)
    {
        var claim = Claim.FromString(input);
        Assert.AreEqual(expectedNumber, claim.Number);
        Assert.AreEqual(expectedX, claim.Position.X);
        Assert.AreEqual(expectedY, claim.Position.Y);
        Assert.AreEqual(expectedWidth, claim.Size.Width);
        Assert.AreEqual(expectedHeight, claim.Size.Height);
    }
}
