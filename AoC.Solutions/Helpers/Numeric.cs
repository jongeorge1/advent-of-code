namespace AoC.Solutions.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Numeric
    {
        public static IEnumerable<int> Factorise(int target)
        {
            for (int i = 1; i <= (int)Math.Ceiling(target / 2d); i++)
            {
                if (target % i == 0)
                {
                    yield return i;
                }
            }
        }

        public static long LeastCommonMultiple(IEnumerable<long> numbers)
        {
            return numbers.Aggregate(LeastCommonMultiple);
        }

        public static long LeastCommonMultiple(long a, long b)
        {
            return Math.Abs(a * b) / GreatestCommonDivisor(a, b);
        }

        public static long GreatestCommonDivisor(long a, long b)
        {
            return b == 0 ? a : GreatestCommonDivisor(b, a % b);
        }
    }
}
