namespace AdventOfCode.Year2022.Day23
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class Map
    {
        private const char Elf = '#';

        private readonly TryProposeDelegate[] proposalFunctions;

        private HashSet<(int X, int Y)> elfLocations;

        private int currentProposalFunctionStartIndex = 0;

        public Map(string[] input)
        {
            this.proposalFunctions =
            [
                this.TryProposeMovingNorth,
                this.TryProposeMovingSouth,
                this.TryProposeMovingWest,
                this.TryProposeMovingEast,
            ];

            this.elfLocations = input
                .SelectMany((row, y) => row.Select((col, x) => (x, y, col)))
                .Where(space => space.col == Elf)
                .Select(space => (space.x, space.y))
                .ToHashSet();
        }

        private delegate bool TryProposeDelegate((int X, int Y) current, [NotNullWhen(true)] out (int X, int Y)? proposed);

        public bool ExecuteRound()
        {
            bool atLeastOneElfMoved = false;

            List<((int X, int Y) CurrentLocation, (int X, int Y) ProposedLocation)> proposals = this.elfLocations
                .Select(elf =>
                {
                    if (this.ElfWillProposeMoving(elf))
                    {
                        return (elf, this.ProposeNewLocation(elf));
                    }

                    return (elf, elf);
                })
                .ToList();

            // Count up all of the proposed locations
            var groupedProposedLocations = proposals.GroupBy(x => x.ProposedLocation).ToList();

            // Move
            this.elfLocations = proposals.Select(proposal =>
            {
                if (groupedProposedLocations.Single(group => group.Key == proposal.ProposedLocation).Count() == 1)
                {
                    // The elf at this location is the only one that suggested the move.
                    if (proposal.CurrentLocation != proposal.ProposedLocation)
                    {
                        atLeastOneElfMoved = true;
                    }

                    return proposal.ProposedLocation;
                }

                // Multiple elves suggested this location
                return proposal.CurrentLocation;
            }).ToHashSet();

            ++this.currentProposalFunctionStartIndex;

            return atLeastOneElfMoved;
        }

        public bool ElfWillProposeMoving((int X, int Y) currentLocation)
        {
            if (this.elfLocations.Contains((currentLocation.X - 1, currentLocation.Y - 1))
                || this.elfLocations.Contains((currentLocation.X, currentLocation.Y - 1))
                || this.elfLocations.Contains((currentLocation.X + 1, currentLocation.Y - 1))
                || this.elfLocations.Contains((currentLocation.X - 1, currentLocation.Y))
                || this.elfLocations.Contains((currentLocation.X + 1, currentLocation.Y))
                || this.elfLocations.Contains((currentLocation.X - 1, currentLocation.Y + 1))
                || this.elfLocations.Contains((currentLocation.X, currentLocation.Y + 1))
                || this.elfLocations.Contains((currentLocation.X + 1, currentLocation.Y + 1)))
            {
                return true;
            }

            return false;
        }

        public (int X, int Y) ProposeNewLocation((int X, int Y) currentLocation)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (this.proposalFunctions[(i + this.currentProposalFunctionStartIndex) % 4](currentLocation, out (int, int)? proposedLocation))
                {
                    return proposedLocation.Value;
                }
            }

            return currentLocation;
        }

        public bool TryProposeMovingNorth((int X, int Y) currentLocation, [NotNullWhen(true)] out (int X, int Y)? newLocation)
        {
            if (this.elfLocations.Contains((currentLocation.X - 1, currentLocation.Y - 1))
                || this.elfLocations.Contains((currentLocation.X, currentLocation.Y - 1))
                || this.elfLocations.Contains((currentLocation.X + 1, currentLocation.Y - 1)))
            {
                newLocation = null;
                return false;
            }

            newLocation = (currentLocation.X, currentLocation.Y - 1);
            return true;
        }

        public bool TryProposeMovingEast((int X, int Y) currentLocation, [NotNullWhen(true)] out (int X, int Y)? newLocation)
        {
            if (this.elfLocations.Contains((currentLocation.X + 1, currentLocation.Y - 1))
                || this.elfLocations.Contains((currentLocation.X + 1, currentLocation.Y))
                || this.elfLocations.Contains((currentLocation.X + 1, currentLocation.Y + 1)))
            {
                newLocation = null;
                return false;
            }

            newLocation = (currentLocation.X + 1, currentLocation.Y);
            return true;
        }

        public bool TryProposeMovingSouth((int X, int Y) currentLocation, [NotNullWhen(true)] out (int X, int Y)? newLocation)
        {
            if (this.elfLocations.Contains((currentLocation.X - 1, currentLocation.Y + 1))
                || this.elfLocations.Contains((currentLocation.X, currentLocation.Y + 1))
                || this.elfLocations.Contains((currentLocation.X + 1, currentLocation.Y + 1)))
            {
                newLocation = null;
                return false;
            }

            newLocation = (currentLocation.X, currentLocation.Y + 1);
            return true;
        }

        public bool TryProposeMovingWest((int X, int Y) currentLocation, [NotNullWhen(true)] out (int X, int Y)? newLocation)
        {
            if (this.elfLocations.Contains((currentLocation.X - 1, currentLocation.Y - 1))
                || this.elfLocations.Contains((currentLocation.X - 1, currentLocation.Y))
                || this.elfLocations.Contains((currentLocation.X - 1, currentLocation.Y + 1)))
            {
                newLocation = null;
                return false;
            }

            newLocation = (currentLocation.X - 1, currentLocation.Y);
            return true;
        }

        public int CountEmptyTilesInOccupiedArea()
        {
            int minX = this.elfLocations.Min(location => location.X);
            int maxX = this.elfLocations.Max(location => location.X);
            int minY = this.elfLocations.Min(location => location.Y);
            int maxY = this.elfLocations.Max(location => location.Y);

            int spacesInTargetArea = (maxX - minX + 1) * (maxY - minY + 1);

            return spacesInTargetArea - this.elfLocations.Count;
        }
    }
}