namespace AoC.Solutions.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Array
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
    }
}
