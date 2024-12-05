namespace AdventOfCode.Year2018.Day15;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class StateExtensionTests
{
    [TestMethod]
    [DataRow("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######", 8, new[] { 9, 15 }, DisplayName = "Walls above/left")]
    [DataRow("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######", 11, new[] { 10, 12 }, DisplayName = "Walls above/below")]
    [DataRow("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######", 26, new[] { 19 }, DisplayName = "Walls above/left/right")]
    [DataRow("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######", 16, new[] { 9, 15, 17 }, DisplayName = "Unit below")]
    [DataRow("#######\r\n#E..G.#\r\n#...#E#\r\n#.G.#G#\r\n#######", 26, new int[0], DisplayName = "Unit below")]
    public void GetAdjacentSpacesTests(string stateInput, int location, int[] expectedResults)
    {
        var state = State.Parse(stateInput.Split(Environment.NewLine));
        MapSpace startLocation = state.Map[location];
        MapSpace[] result = state.GetEmptyAdjacentSpaces(startLocation).ToArray();

        Assert.AreEqual(expectedResults.Length, result.Length);
        IEnumerable<int> resultLocations = result.Select(x => x.Location);

        CollectionAssert.AreEquivalent(expectedResults, resultLocations.ToArray());
    }

    [TestMethod]
    [DataRow("#########\r\n#G..G..G#\r\n#.......#\r\n#.......#\r\n#G..E..G#\r\n#.......#\r\n#.......#\r\n#G..G..G#\r\n#########", 1, "#########\r\n#.G...G.#\r\n#...G...#\r\n#...E..G#\r\n#.G.....#\r\n#.......#\r\n#G..G..G#\r\n#.......#\r\n#########")]
    [DataRow("#########\r\n#G..G..G#\r\n#.......#\r\n#.......#\r\n#G..E..G#\r\n#.......#\r\n#.......#\r\n#G..G..G#\r\n#########", 2, "#########\r\n#..G.G..#\r\n#...G...#\r\n#.G.E.G.#\r\n#.......#\r\n#G..G..G#\r\n#.......#\r\n#.......#\r\n#########")]
    [DataRow("#########\r\n#G..G..G#\r\n#.......#\r\n#.......#\r\n#G..E..G#\r\n#.......#\r\n#.......#\r\n#G..G..G#\r\n#########", 3, "#########\r\n#.......#\r\n#..GGG..#\r\n#..GEG..#\r\n#G..G...#\r\n#......G#\r\n#.......#\r\n#.......#\r\n#########")]
    public void RoundTests(string initialState, int numberOfRounds, string expectedState)
    {
        var state = State.Parse(initialState.Split(Environment.NewLine));

        for (int i = 0; i < numberOfRounds; i++)
        {
            state.Round();
        }

        var expected = State.Parse(expectedState.Split(Environment.NewLine));

        Assert.AreEqual(expected.ToString(), state.ToString());
    }
}
