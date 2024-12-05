namespace AdventOfCode.Year2018.Day25;

using AdventOfCode.Year2018.Day24;
using Microsoft.VisualStudio.TestTools.UnitTesting;

public class GroupTests
{
    [TestMethod]
    [DataRow("2728 units each with 5703 hit points (weak to fire) with an attack that does 18 cold damage at initiative 12", 2728, 5703, 18, "cold", 12, new[] { "fire" }, new string[0])]
    [DataRow("6601 units each with 8652 hit points (weak to fire, cold) with an attack that does 11 fire damage at initiative 19", 6601, 8652, 11, "fire", 19, new[] { "fire", "cold" }, new string[0])]
    [DataRow("7376 units each with 6574 hit points (immune to cold, slashing, fire) with an attack that does 7 bludgeoning damage at initiative 4", 7376, 6574, 7, "bludgeoning", 4, new string[0], new string[] { "cold", "slashing", "fire" })]
    [DataRow("1140 units each with 17741 hit points (weak to bludgeoning; immune to fire, slashing) with an attack that does 25 fire damage at initiative 2", 1140, 17741, 25, "fire", 2, new[] { "bludgeoning" }, new[] { "fire", "slashing" })]
    public void CreationTests(string input, int unitCount, int hitPoints, int damageCount, string damageType, int initiative, string[] weaknesses, string[] immunities)
    {
        var group = new Group(0, "test", input);

        Assert.AreEqual(unitCount, group.Units);
        Assert.AreEqual(hitPoints, group.UnitHitPoints);
        Assert.AreEqual(damageCount, group.DamageCount);
        Assert.AreEqual(damageType, group.DamageType);
        Assert.AreEqual(initiative, group.Initiative);
        CollectionAssert.AreEquivalent(weaknesses, group.Weaknesses);
        CollectionAssert.AreEquivalent(immunities, group.Immunities);
    }
}
