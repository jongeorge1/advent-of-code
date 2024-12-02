#nullable disable
namespace AdventOfCode.Year2018.Day15
{
    using System.Collections.Generic;
    using System.Linq;

    public static class StateExtensions
    {
        public static bool Round(this State initialState)
        {
            // Every unit gets a turn, taken in reading order.
            // (we get reading order just by iterating the original
            // state
            State currentState = initialState;

            // Get the units. Note that it is important to materialise this collection immediately,
            // otherwise we run the risk of double moving units as we iterate through the map.
            Unit[] units = initialState.Map.Where(x => x?.Unit != null).OrderBy(x => x.Location).Select(x => x!.Unit).ToArray();

            foreach (Unit current in units)
            {
                if (currentState?.IsCombatEnded() == true)
                {
                    return false;
                }

                // Make sure it hasn't been killed already
                if (current?.IsKilled() == false)
                {
                    current.TakeTurn(currentState!);
                }
            }

            return true;
        }

        public static IEnumerable<MapSpace> GetAdjacentSpaces(this State state, MapSpace space)
        {
            // Note: because we know the caverns in question are walled all around, we
            // don't need to worry about bounds checking. If this wasn't the case we'd
            // need to check to see if the space we're looking at was on the edge of the
            // map before checking the adjacent spaces.
            int[] adjacentLocations =
            [
                space.Location - state.YOffset,
                space.Location - 1,
                space.Location + 1,
                space.Location + state.YOffset,
            ];

#pragma warning disable SA1009 // Closing parenthesis should be spaced correctly
            return adjacentLocations.Select(x => state.Map[x]).Where(x => x != null)!;
#pragma warning restore SA1009 // Closing parenthesis should be spaced correctly
        }

        public static IEnumerable<MapSpace> GetEmptyAdjacentSpaces(this State state, MapSpace space)
        {
            return state.GetAdjacentSpaces(space).Where(MapSpaceExtensions.IsEmpty);
        }

        public static bool IsCombatEnded(this State state)
        {
            return !state.Map.Any(x => x?.Unit is Elf) || !state.Map.Any(x => x?.Unit is Goblin);
        }

        public static int TotalRemainingHitPoints(this State state)
        {
            return state.Map.Sum(x => x?.Unit?.HitPoints ?? 0);
        }

        public static int CountUnits<T>(this State state)
            where T : Unit
        {
            return state.Map.Count(x => x?.Unit?.GetType() == typeof(T));
        }

        public static T[] GetUnits<T>(this State state)
            where T : Unit
        {
            return state.Map.Where(x => x?.Unit?.GetType() == typeof(T)).Select(x => (T)x.Unit).ToArray();
        }
    }
}
