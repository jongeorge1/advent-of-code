namespace AoC.Solutions.Helpers
{
    using System.Linq;

    public static class ChineseRemainderTheorem
    {
        public static long Solve(long[] divisors, long[] remainders)
        {
            long prod = divisors.Aggregate(1L, (i, j) => i * j);
            long result = 0;
            for (long i = 0; i < divisors.Length; i++)
            {
                long p = prod / divisors[i];
                result += remainders[i] * ModularMultiplicativeInverse(p, divisors[i]) * p;
            }

            return result % prod;
        }

        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (int x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }

            return 1;
        }
    }
}
