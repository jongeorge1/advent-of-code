namespace AdventOfCode.Year2022.Day19;

using System;

public readonly record struct MineralCollectionState(
    byte TimeRemaining,
    short OreRobotCount,
    short ClayRobotCount,
    short ObsidianRobotCount,
    short GeodeRobotCount,
    short AvailableOre,
    short AvailableClay,
    short AvailableObsidian,
    short CrackedGeodes)
{
    public MineralCollectionState AfterTime(int time)
    {
        if (time < 1 || time > this.TimeRemaining)
        {
            throw new Exception("Invalid time");
        }

        return new MineralCollectionState(
            (byte)(this.TimeRemaining - time),
            this.OreRobotCount,
            this.ClayRobotCount,
            this.ObsidianRobotCount,
            this.GeodeRobotCount,
            (short)(this.AvailableOre + (this.OreRobotCount * time)),
            (short)(this.AvailableClay + (this.ClayRobotCount * time)),
            (short)(this.AvailableObsidian + (this.ObsidianRobotCount * time)),
            (short)(this.CrackedGeodes + (this.GeodeRobotCount * time)));
    }

    public MineralCollectionState AddOreRobot(short cost)
    {
        if (cost > this.AvailableOre)
        {
            throw new Exception("Not enough ore for an ore robot");
        }

        return new MineralCollectionState(
            this.TimeRemaining,
            (short)(this.OreRobotCount + 1),
            this.ClayRobotCount,
            this.ObsidianRobotCount,
            this.GeodeRobotCount,
            (short)(this.AvailableOre - cost),
            this.AvailableClay,
            this.AvailableObsidian,
            this.CrackedGeodes);
    }

    public MineralCollectionState AddClayRobot(short cost)
    {
        if (cost > this.AvailableOre)
        {
            throw new Exception("Not enough ore for a clay robot");
        }

        return new MineralCollectionState(
            this.TimeRemaining,
            this.OreRobotCount,
            (short)(this.ClayRobotCount + 1),
            this.ObsidianRobotCount,
            this.GeodeRobotCount,
            (short)(this.AvailableOre - cost),
            this.AvailableClay,
            this.AvailableObsidian,
            this.CrackedGeodes);
    }

    public MineralCollectionState AddObsidianRobot(short oreCost, short clayCost)
    {
        if (oreCost > this.AvailableOre)
        {
            throw new Exception("Not enough ore for an obsidian robot");
        }

        if (clayCost > this.AvailableClay)
        {
            throw new Exception("Not enough clay for an obsidian robot");
        }

        return new MineralCollectionState(
            this.TimeRemaining,
            this.OreRobotCount,
            this.ClayRobotCount,
            (short)(this.ObsidianRobotCount + 1),
            this.GeodeRobotCount,
            (short)(this.AvailableOre - oreCost),
            (short)(this.AvailableClay - clayCost),
            this.AvailableObsidian,
            this.CrackedGeodes);
    }

    public MineralCollectionState AddGeodeRobot(short oreCost, short obsidianCost)
    {
        if (oreCost > this.AvailableOre)
        {
            throw new Exception("Not enough ore for an geode robot");
        }

        if (obsidianCost > this.AvailableObsidian)
        {
            throw new Exception("Not enough obsidian for a geode robot");
        }

        return new MineralCollectionState(
            this.TimeRemaining,
            this.OreRobotCount,
            this.ClayRobotCount,
            this.ObsidianRobotCount,
            (short)(this.GeodeRobotCount + 1),
            (short)(this.AvailableOre - oreCost),
            this.AvailableClay,
            (short)(this.AvailableObsidian - obsidianCost),
            this.CrackedGeodes);
    }

    public int BestPossibleOutcome()
    {
        // For the best possible outcome, let's assume we could produce a new geode robot every minute from now
        // until the end of the time.
        int result = this.CrackedGeodes;
        int geodeRobotCount = this.GeodeRobotCount;

        // There is a formula we could use for this, but...
        for (int i = this.TimeRemaining; i > 0; --i)
        {
            result += geodeRobotCount++;
        }

        return result;
    }
}
