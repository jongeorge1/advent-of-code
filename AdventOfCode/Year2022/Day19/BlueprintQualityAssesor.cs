namespace AdventOfCode.Year2022.Day19;

using System;

public static class BlueprintQualityAssesor
{
    public static int CalculateQualityLevel(ref Blueprint blueprint, byte availableTime)
    {
        int geodeCount = DetermineLargestNumberOfGeodesThatCanBeOpened(ref blueprint, availableTime);
        int quality = geodeCount * blueprint.Number;

        ////Console.WriteLine($"Blueprint {blueprint.Number} can obtain {geodeCount} geodes and has quality {quality}");

        return quality;
    }

    public static int DetermineLargestNumberOfGeodesThatCanBeOpened(ref Blueprint blueprint, byte availableTime)
    {
        var startingState = new MineralCollectionState(availableTime, 1, 0, 0, 0, 0, 0, 0, 0);
        return DetermineLargestNumberOfGeodesThatCanBeOpenedFromState(ref startingState, ref blueprint);
    }

    private static int DetermineLargestNumberOfGeodesThatCanBeOpenedFromState(ref MineralCollectionState state, ref Blueprint blueprint, int bestResultSoFar = 0)
    {
        // All of our choices come down to what robot to build next.
        // So for each robot type, look at how long it will take to have sufficient resource to build another (the answer may be never)
        // Then produce a new state based on that and go again.
        // We need to start with geode producing robots and work back, since the earlier we get geode robots, the more geodes we can crack.
        MineralCollectionState nextState;
        int timeToNextRobot = 0;

        if (bestResultSoFar > state.BestPossibleOutcome())
        {
            return -1;
        }

        // First, geode. We may not be in a position where we can just wait, as we may not have an obsidian robot.
        if (state.ObsidianRobotCount > 0)
        {
            int timeToRequiredOre = (int)Math.Ceiling(Math.Max(blueprint.GeodeRobotOreCost - state.AvailableOre, 0) / (decimal)state.OreRobotCount) + 1;
            int timeToRequiredObsidian = (int)Math.Ceiling(Math.Max(blueprint.GeodeRobotObsidianCost - state.AvailableObsidian, 0) / (decimal)state.ObsidianRobotCount) + 1;
            timeToNextRobot = Math.Max(timeToRequiredOre, timeToRequiredObsidian);

            if (timeToNextRobot < state.TimeRemaining)
            {
                nextState = state.AfterTime(timeToNextRobot).AddGeodeRobot(blueprint.GeodeRobotOreCost, blueprint.GeodeRobotObsidianCost);

                bestResultSoFar = Math.Max(
                    bestResultSoFar,
                    DetermineLargestNumberOfGeodesThatCanBeOpenedFromState(ref nextState, ref blueprint, bestResultSoFar));
            }
            else
            {
                // We're in a position where don't have enough time to build any more geode robots, so what we have is as good as it
                // gets. In this case, just look at how many geodes we'll have by the end.
                nextState = state.AfterTime(state.TimeRemaining);
                bestResultSoFar = Math.Max(nextState.CrackedGeodes, bestResultSoFar);
            }
        }

        // Second, obsidan. We may not be in a position where we can just wait for one of these, as we may not have a
        // clay robot. Also, we may have the maximum useful number of obsidian robots already
        if (state.ClayRobotCount > 0 && state.ObsidianRobotCount < blueprint.MaximumRequiredObsidianRobots)
        {
            int timeToRequiredOre = (int)Math.Ceiling(Math.Max(blueprint.ObsidianRobotOreCost - state.AvailableOre, 0) / (decimal)state.OreRobotCount) + 1;
            int timeToRequiredClay = (int)Math.Ceiling(Math.Max(blueprint.ObsidianRobotClayCost - state.AvailableClay, 0) / (decimal)state.ClayRobotCount) + 1;
            timeToNextRobot = Math.Max(timeToRequiredOre, timeToRequiredClay);

            if (timeToNextRobot < state.TimeRemaining)
            {
                nextState = state.AfterTime(timeToNextRobot).AddObsidianRobot(blueprint.ObsidianRobotOreCost, blueprint.ObsidianRobotClayCost);

                bestResultSoFar = Math.Max(
                    bestResultSoFar,
                    DetermineLargestNumberOfGeodesThatCanBeOpenedFromState(ref nextState, ref blueprint, bestResultSoFar));
            }
        }

        // Next, clay. We will always be in a position where we can build a new clay robot at some point. However, we might not need to.
        if (state.ClayRobotCount < blueprint.MaximumRequiredClayRobots)
        {
            timeToNextRobot = (int)Math.Ceiling(Math.Max(blueprint.ClayRobotOreCost - state.AvailableOre, 0) / (decimal)state.OreRobotCount) + 1;
            if (timeToNextRobot < state.TimeRemaining)
            {
                nextState = state.AfterTime(timeToNextRobot).AddClayRobot(blueprint.ClayRobotOreCost);

                bestResultSoFar = Math.Max(
                    bestResultSoFar,
                    DetermineLargestNumberOfGeodesThatCanBeOpenedFromState(ref nextState, ref blueprint, bestResultSoFar));
            }
        }

        // Finally, ore. We will always be in a position where we can build a new ore robot at some point, but we won't always need to.
        if (state.OreRobotCount < blueprint.MaximumRequiredOreRobots)
        {
            // To build an ore robot, we need to wait for (the cost of an ore robot) - (number of ore we already have) * minutes to gather the materials,
            // plus 1 minute to actually build the thing.
            timeToNextRobot = (int)Math.Ceiling(Math.Max(blueprint.OreRobotOreCost - state.AvailableOre, 0) / (decimal)state.OreRobotCount) + 1;

            // It's only of any use if there's a minute left after that
            if (timeToNextRobot < state.TimeRemaining)
            {
                nextState = state.AfterTime(timeToNextRobot).AddOreRobot(blueprint.OreRobotOreCost);

                // This will definitely be the best result so far.
                bestResultSoFar = Math.Max(
                    bestResultSoFar,
                    DetermineLargestNumberOfGeodesThatCanBeOpenedFromState(ref nextState, ref blueprint, bestResultSoFar));
            }
        }

        return bestResultSoFar;
    }
}
