namespace AdventOfCode.Year2017.Day03
{
    using System;

    public static class Spiral
    {
        public static (int X, int Y) GetGridPosition(int n)
        {
            int k = (int)Math.Ceiling((Math.Sqrt(n) - 1) / 2);
            int t = (2 * k) + 1;
            int m = (int)Math.Pow(t, 2);

            t = t - 1;

            if (n >= m - t)
            {
                return (k - (m - n), -k);
            }
            else
            {
                m = m - t;
            }

            if (n >= m - t)
            {
                return (-k, -k + (m - n));
            }
            else
            {
                m = m - t;
            }

            if (n >= m - t)
            {
                return (-k + (m - n), k);
            }
            else
            {
                return (k, k - (m - n - t));
            }
        }
    }
}
