namespace AdventOfCode.Year2024.Day21;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BotTests
{
    [TestMethod]
    public void TestLeftOnlyMovement()
    {
        DoorBot sut = new();
        string result = sut.GetMovementsForButton('0');
        Assert.AreEqual("<", result);
    }

    [TestMethod]
    public void TestUpOnlyMovement()
    {
        DoorBot sut = new();
        string result = sut.GetMovementsForButton('6');
        Assert.AreEqual("^^", result);
    }

    [TestMethod]
    public void TestLocationToLocationMovement()
    {
        DoorBot sut = new();
        string result = sut.GetMovementsForButton('7');
        Assert.AreEqual("<<^^^", result);

        result = sut.GetMovementsForButton('6');
        Assert.AreEqual(">>v", result);
    }

    [TestMethod]
    public void TestBasicSequence()
    {
        DoorBot sut = new();
        ReadOnlySpan<char> result = sut.GetMovementsForSequence("029A");
        Assert.AreEqual("<A^A>^^AvvvA", result.ToString());
    }

    [TestMethod]
    public void TestSecondarySequence()
    {
        DirectionBot sut = new();
        ReadOnlySpan<char> result = sut.GetMovementsForSequence("<A^A>^^AvvvA");
        Assert.AreEqual("<<vA>>^A<A>AvA<^AA>A<vAAA>^A", result.ToString());
    }
}
