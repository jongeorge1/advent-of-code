namespace AdventOfCode.Year2018.Day15;

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class UnitExtensionTests
{
    [TestMethod]
    public void FindInRangeSpacesTests()
    {
        var state = State.Parse("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######".Split(Environment.NewLine));

        // Get the elf in the top left corner
        Unit targetUnit = state.Map[8].Unit!;

        MapSpace[] inRangeSpaces = targetUnit.FindSpacesInRangeOfAnEnemy(state);
        int[] inRangeLocations = inRangeSpaces.Select(x => x.Location).ToArray();
        CollectionAssert.AreEquivalent(new[] { 10, 12, 16, 19, 22, 24 }, inRangeLocations);
    }

    [TestMethod]
    [DataRow("#######\r\n#E..G.#\r\n#...#.#\r\n#.G.#G#\r\n#######", 8, new[] { 8, 9, 10 })]
    [DataRow("#######\r\n#.E...#\r\n#...?.#\r\n#..?G?#\r\n#######", 9, new[] { 9, 10, 11, 18 })]
    public void GetPathToClosestTests(string input, int startLocation, int[] expectedPath)
    {
        var state = State.Parse(input.Split(Environment.NewLine));

        // Get the elf in the top left corner
        Unit targetUnit = state.Map[startLocation].Unit!;
        MapSpace[] inRangeSpaces = targetUnit.FindSpacesInRangeOfAnEnemy(state);
        MapSpace[] path = targetUnit.GetPathToClosest(inRangeSpaces, state);
        var pathLocations = path.Select(x => x.Location).ToArray();

        CollectionAssert.AreEquivalent(expectedPath, pathLocations);
    }
}
