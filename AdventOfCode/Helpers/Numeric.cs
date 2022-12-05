namespace AdventOfCode.Helpers
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

        /// <summary>
        /// Finds the highest '1' bit in an unsigned integer. This is effectively the
        /// highest power of 2 that's less than or equal to the parameter value.
        /// </summary>
        /// <param name="i">The number to find the highest one bit for.</param>
        /// <returns>The value of the highest one bit.</returns>
        public static uint HighestOneBit(uint i)
        {
            i |= i >> 1;
            i |= i >> 2;
            i |= i >> 4;
            i |= i >> 8;
            i |= i >> 16;
            return i - (i >> 1);
        }
    }
}
