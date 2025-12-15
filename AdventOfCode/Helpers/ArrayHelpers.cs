namespace AdventOfCode.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class ArrayHelpers
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list) => list.GetPermutations(list.Count());

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }

            return GetPermutations(list, length - 1)
                .SelectMany(
                    t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static int Product(this IEnumerable<int> list)
        {
            return list.Aggregate(1, (a, b) => a * b);
        }

        public static long Product(this IEnumerable<long> list)
        {
            return list.Aggregate(1, (long a, long b) => a * b);
        }

        public static bool IsIdenticalTo<T>(this T[] list, T[] comparison)
        {
            if (list.Length != comparison.Length)
            {
                return false;
            }

            for (int i = 0; i < list.Length; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(list[i], comparison[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
