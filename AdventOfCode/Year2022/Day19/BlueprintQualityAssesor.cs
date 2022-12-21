namespace AdventOfCode.Year2022.Day19
{
    using System;
    using System.Diagnostics;
    using AdventOfCode.Year2016.Day17;

    public static class BlueprintQualityAssesor
    {
        public static int CalculateQualityLevel(ref Blueprint blueprint)
        {
            int geodeCount = DetermineLargestNumberOfGeodesThatCanBeOpened(ref blueprint);
            return geodeCount * blueprint.Number;
        }

        private static int DetermineLargestNumberOfGeodesThatCanBeOpened(ref Blueprint blueprint)
        {
            var startingState = new MineralCollectionState(24, 1, 0, 0, 0, 0, 0, 0, 0);
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

                if (timeToNextRobot < state.TimeRemaining - 1)
                {
                    nextState = state.AfterTime(timeToNextRobot).AddGeodeRobot(blueprint.GeodeRobotOreCost, blueprint.GeodeRobotObsidianCost);

                    bestResultSoFar = Math.Max(
                        bestResultSoFar,
                        DetermineLargestNumberOfGeodesThatCanBeOpenedFromState(ref nextState, ref blueprint, bestResultSoFar));
                }
            }

            // Second, obsidan. We may not be in a position where we can just wait for one of these, as we may not have a
            // clay robot.
            if (state.ClayRobotCount > 0)
            {
                int timeToRequiredOre = (int)Math.Ceiling(Math.Max(blueprint.ObsidianRobotOreCost - state.AvailableOre, 0) / (decimal)state.OreRobotCount) + 1;
                int timeToRequiredClay = (int)Math.Ceiling(Math.Max(blueprint.ObsidianRobotClayCost - state.AvailableClay, 0) / (decimal)state.ClayRobotCount) + 1;
                timeToNextRobot = Math.Max(timeToRequiredOre, timeToRequiredClay);

                if (timeToNextRobot < state.TimeRemaining - 1)
                {
                    nextState = state.AfterTime(timeToNextRobot).AddObsidianRobot(blueprint.ObsidianRobotOreCost, blueprint.ObsidianRobotClayCost);

                    bestResultSoFar = Math.Max(
                        bestResultSoFar,
                        DetermineLargestNumberOfGeodesThatCanBeOpenedFromState(ref nextState, ref blueprint, bestResultSoFar));
                }
            }

            // Next, clay. We will always be in a position where we can build a new clay robot at some point.
            timeToNextRobot = (int)Math.Ceiling(Math.Max(blueprint.ClayRobotOreCost - state.AvailableOre, 0) / (decimal)state.OreRobotCount) + 1;
            if (timeToNextRobot < state.TimeRemaining - 1)
            {
                nextState = state.AfterTime(timeToNextRobot).AddClayRobot(blueprint.ClayRobotOreCost);

                bestResultSoFar = Math.Max(
                    bestResultSoFar,
                    DetermineLargestNumberOfGeodesThatCanBeOpenedFromState(ref nextState, ref blueprint, bestResultSoFar));
            }

            // Finally, ore. We will always be in a position where we can build a new ore robot at some point.
            // To build an ore robot, we need to wait for (the cost of an ore robot) - (number of ore we already have) * minutes to gather the materials,
            // plus 1 minute to actually build the thing.
            timeToNextRobot = (int)Math.Ceiling(Math.Max(blueprint.OreRobotOreCost - state.AvailableOre, 0) / (decimal)state.OreRobotCount) + 1;
            // It's only of any use if there's a minute left after that
            if (timeToNextRobot < state.TimeRemaining - 1) 
            {
                nextState = state.AfterTime(timeToNextRobot).AddOreRobot(blueprint.OreRobotOreCost);

                // This will definitely be the best result so far.
                bestResultSoFar = Math.Max(
                    bestResultSoFar,
                    DetermineLargestNumberOfGeodesThatCanBeOpenedFromState(ref nextState, ref blueprint, bestResultSoFar));
            }

            // What if we just stopped now and did nothing?
            nextState = state.AfterTime(state.TimeRemaining);

            return nextState.CrackedGeodes > bestResultSoFar
                ? nextState.CrackedGeodes
                : bestResultSoFar;
        }
    }

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
            if (time < 1)
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
}
