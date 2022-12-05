namespace AdventOfCode.Year2021.Day06
{
    using System.Collections.Generic;

    public static class Day06Common
    {
        private static readonly Dictionary<int, long> CountCache = new Dictionary<int, long>();

        public static long CalculateDescendantCountAfterDays(int days, int firstOffset)
        {
            if (CountCache.TryGetValue(days - firstOffset, out long count))
            {
                return count;
            }

            long totalLanternFish = 0;

            for (int i = days - firstOffset; i > 0; i -= 7)
            {
                // A new descendant will spawn...
                ++totalLanternFish;

                // ...and will have its own descendants over time.
                totalLanternFish += CalculateDescendantCountAfterDays(i, 9);
            }

            CountCache[days - firstOffset] = totalLanternFish;

            return totalLanternFish;
        }
    }
}
