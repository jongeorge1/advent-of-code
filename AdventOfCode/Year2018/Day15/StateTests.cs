namespace AdventOfCode.Year2018.Day15;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class StateTests
{
    [TestMethod]
    public void ParseSetsYOffsetCorrectly()
    {
        var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######".Split(Environment.NewLine));

        Assert.AreEqual(7, state.YOffset);
    }

    [TestMethod]
    public void ParseSetsYMaxCorrectly()
    {
        var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######".Split(Environment.NewLine));

        Assert.AreEqual(5, state.MaxY);
    }

    [TestMethod]
    public void ParseSetsWallsCorrectly()
    {
        var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######".Split(Environment.NewLine));

        int[] wallLocations = [0, 1, 2, 3, 4, 5, 6, 7, 13, 14, 20, 21, 27, 28, 29, 30, 31, 32, 33, 34];

        foreach (int current in wallLocations)
        {
            Assert.IsNull(state.Map[current]);
        }
    }

    [TestMethod]
    public void ParseSetsSpacesCorrectly()
    {
        var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######".Split(Environment.NewLine));

        int[] spaceLocations = [8, 9, 10, 11, 12, 15, 16, 17, 18, 19, 22, 23, 24, 25, 26];

        foreach (int current in spaceLocations)
        {
            Assert.IsInstanceOfType<MapSpace>(state.Map[current]);
        }
    }

    [TestMethod]
    public void ParseSetsElvesCorrectly()
    {
        var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######".Split(Environment.NewLine));

        int[] elfLocations = [11, 15, 19, 25];

        foreach (int current in elfLocations)
        {
            Assert.IsInstanceOfType<Elf>(state.Map[current].Unit);
        }
    }

    [TestMethod]
    public void ParseSetsGoblinsCorrectly()
    {
        var state = State.Parse("#######\r\n#.G.E.#\r\n#E.G.E#\r\n#.G.E.#\r\n#######".Split(Environment.NewLine));

        int[] goblinLocations = [9, 17, 23];

        foreach (int current in goblinLocations)
        {
            Assert.IsInstanceOfType<Goblin>(state.Map[current].Unit);
        }
    }
}
